using Application.Core;
using Application.Interfaces;
using Application.Payments.Core;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payments
{
    public class ActivateGift
    {
        public class Command : IRequest<Result<PaymentDto>>
        {
            public string CouponCode { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<PaymentDto>>
        {
            private readonly DataContext _ctx;
            private readonly IMapper _mpr;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext ctx, IMapper mpr, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _ctx = ctx;
                _mpr = mpr;
            }

            public async Task<Result<PaymentDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var subscriber = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                if (subscriber == null) return null;

                var giftedPayment = await _ctx.GiftPayments.FirstOrDefaultAsync(g =>
                    g.PM_Recipient.Equals(subscriber.Email) && g.PM_CouponCode.Equals(req.CouponCode), cancellationToken: cancellationToken);

                if (giftedPayment == null) return null;

                if (!giftedPayment.PM_CouponCode.Equals(req.CouponCode, StringComparison.Ordinal))
                    return Result<PaymentDto>.Failure("Coupon Code is case-sensitive.");
                else
                {
                    giftedPayment.PM_Used = true;
                    giftedPayment.Pm_Active = false;
                }

                Payment payment = new Payment();
                payment.Pm_Days = giftedPayment.Pm_Days;
                payment.Pm_DisplayPrice = giftedPayment.Pm_DisplayPrice;
                payment.Pm_Price = giftedPayment.Pm_Price;
                payment.Pm_RefernceID = giftedPayment?.Pm_RefernceID;
                payment.Pm_Ordinal = giftedPayment.Pm_Ordinal;
                payment.Pm_Creator = giftedPayment.Pm_Creator;
                payment.Pm_Subscriber = subscriber?.Id;
                payment.Pm_ID = giftedPayment.Pm_ID;
                payment.Py_ID = giftedPayment.Py_ID;
                payment.Sb_ID = giftedPayment.Sb_ID;

                if (!String.IsNullOrEmpty(subscriber.Id))
                {
                    subscriber.Us_SubscriptionExpiryDate = DateTime.Now.AddDays(payment.Pm_Days);
                    subscriber.Us_SubscriptionStartDate = DateTime.Now;
                    subscriber.Us_SubscriptionDays = payment.Pm_Days;
                }
                else
                {
                    subscriber.Us_SubscriptionExpiryDate = DateTime.Now.AddDays(payment.Pm_Days);
                }

                _ctx.Payments.Add(payment);

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<PaymentDto>.Failure("Failed to activate Payment.");
                payment = await _ctx.Payments
                    .Include(i => i.PaymentMethod)
                    .Include(i => i.Subscriber)
                    .Include(i => i.Subscription)
                    .Include(i => i.Creator)
                    .Where(s => s.Pm_ID == payment.Pm_ID).FirstOrDefaultAsync(cancellationToken: cancellationToken);
                return Result<PaymentDto>.Success(_mpr.Map<PaymentDto>(payment));
            }
        }
    }
}
