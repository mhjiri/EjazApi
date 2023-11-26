using System.Diagnostics;   
using Application.Core;
using Application.Subscriptions.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Subscriptions
{
    public class Update
    {
        public class Command : IRequest<Result<SubscriptionDto>>
        {
            public SubscriptionCmdDto Subscription { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<SubscriptionDto>>
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
                    RuleFor(x => x.Subscription).SetValidator(new SubscriptionCmdValidator());
                }
            }

            public async Task<Result<SubscriptionDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var subscription = await _ctx.Subscriptions.Include(i => i.Creator).Where(s => s.Sb_ID == req.Subscription.Sb_ID).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (subscription == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                DateTime dateTime = DateTime.UtcNow;
                req.Subscription.Sb_Creator = subscription.Sb_Creator;

                _mpr.Map(req.Subscription, subscription);

                subscription.Sb_Modifier = user.Id;
                subscription.Sb_ModifyOn = dateTime;
                

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<SubscriptionDto>.Failure("Failed to update Subscription");
                return Result<SubscriptionDto>.Success(_mpr.Map<SubscriptionDto>(subscription));
            }
        }
    }
}