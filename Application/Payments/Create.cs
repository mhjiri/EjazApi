using System.Diagnostics;
using Application.Core;
using Application.Payments.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Security.Policy;

namespace Application.Payments
{
    public class Create
    {
        public class Command : IRequest<Result<PaymentDto>>
        {
            public PaymentCmdDto Payment { get; set; }
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

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.Payment).SetValidator(new PaymentCmdValidator());
                }
            }

            public async Task<Result<PaymentDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                if (req.Payment.Py_ID == null) req.Payment.Py_ID = new Guid("3b86485c-183c-4899-789e-08db9159aa3a");


                if (req.Payment.Sb_ID == null)
                {
                    var subscription = await _ctx.Subscriptions.FirstOrDefaultAsync(s => s.Sb_Active && s.Sb_Days == req.Payment.Pm_Days);
                    req.Payment.Sb_ID = (subscription != null) ? subscription.Sb_ID : new Guid("3be961a9-bef4-4f95-b6d4-08db91462ed2");
                }

                Payment payment = _mpr.Map<Payment>(req.Payment);
                payment.Pm_Creator = user.Id;

                if (!String.IsNullOrEmpty(req.Payment.pm_Subscriber))
                {
                    var subscriber = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == req.Payment.pm_Subscriber, cancellationToken: cancellationToken);
                    if (subscriber == null) return null;
                    else
                    {
                        payment.Pm_Subscriber = subscriber.Id;
                        subscriber.Us_SubscriptionExpiryDate = DateTime.Now.AddDays(payment.Pm_Days);
                        subscriber.Us_SubscriptionStartDate = DateTime.Now;
                        subscriber.Us_SubscriptionDays = payment.Pm_Days;
                    }
                }
                else
                {
                    payment.Pm_Subscriber = user.Id;
                    user.Us_SubscriptionExpiryDate = DateTime.Now.AddDays(payment.Pm_Days);
                }


                _ctx.Payments.Add(payment);

                

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<PaymentDto>.Failure("Failed to create Payment");
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