using Application.Core;
using Application.Interfaces;
using Application.Payments.Core;
using AutoMapper;
using Domain;
using FluentValidation;
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
    public class CreateGift
    {
        public class Command : IRequest<Result<GiftPaymentDto>>
        {
            public GiftPaymentCmdDto GiftPayment { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<GiftPaymentDto>>
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

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.GiftPayment).SetValidator(new PaymentCmdValidator());
                }
            }

            public async Task<Result<GiftPaymentDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                // Check
                if (req.GiftPayment.Py_ID == null) req.GiftPayment.Py_ID = new Guid("1e614759-be5c-4620-98d5-1dd079a120a6"); // In-App Purchase

                if (req.GiftPayment.Sb_ID == null)
                {
                    var subscription = await _ctx.Subscriptions.FirstOrDefaultAsync(s => s.Sb_Active && s.Sb_Days == req.GiftPayment.Pm_Days);
                    req.GiftPayment.Sb_ID = (subscription != null) ? subscription.Sb_ID : new Guid("3be961a9-bef4-4f95-b6d4-08db91462ed2"); // Default : Monthly
                }

                GiftPayment giftPayment = _mpr.Map<GiftPayment>(req.GiftPayment);
                giftPayment.Pm_Creator = user?.Id; // Remove later because read from token
                giftPayment.PM_CouponCode = new string(Guid.NewGuid().ToString().Take(8).ToArray());
                giftPayment.Pm_Active = true;

                _ctx.GiftPayments.Add(giftPayment);

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (result)
                {
                    // TO DO : Integrate Email

                    giftPayment = await _ctx.GiftPayments
                        .Include(i => i.PaymentMethod)
                        //.Include(i => i.Subscriber)
                        .Include(i => i.Subscription)
                        .Include(i => i.Creator)
                        .Where(s => s.Pm_ID == giftPayment.Pm_ID).FirstOrDefaultAsync(cancellationToken: cancellationToken);
                    return Result<GiftPaymentDto>.Success(_mpr.Map<GiftPaymentDto>(giftPayment));
                }
                else
                {
                    return Result<GiftPaymentDto>.Failure("Failed to gift Payment");
                }
            }
        }
    }
}
