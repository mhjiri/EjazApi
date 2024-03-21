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
using System.Security.Policy;

namespace Application.Subscriptions
{
    public class Create
    {
        public class Command : IRequest<Result<SubscriptionQryDto>>
        {
            public SubscriptionCmdDto Subscription { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<SubscriptionQryDto>>
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

            public async Task<Result<SubscriptionQryDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                Subscription subscription = _mpr.Map<Subscription>(req.Subscription);
                subscription.Sb_Creator = user.Id;

                _ctx.Subscriptions.Add(subscription);

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<SubscriptionQryDto>.Failure("Failed to create Subscription");
                subscription = await _ctx.Subscriptions.Where(s => s.Sb_ID == subscription.Sb_ID).FirstOrDefaultAsync(cancellationToken: cancellationToken);
                return Result<SubscriptionQryDto>.Success(_mpr.Map<SubscriptionQryDto>(subscription)); 
            }
        }
    }
}