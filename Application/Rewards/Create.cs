using System.Diagnostics;
using Application.Core;
using Application.Rewards.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Reflection;

namespace Application.Rewards
{
    public class Create
    {
        public class Command : IRequest<Result<RewardDto>>
        {
            public RewardCmdDto Reward { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<RewardDto>>
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
                    RuleFor(x => x.Reward).SetValidator(new RewardCmdValidator());
                }
            }

            public async Task<Result<RewardDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                Reward reward = _mpr.Map<Reward>(req.Reward);
                reward.Rw_Creator = user.Id;

                

                //foreach (ItemDto item in req.Reward.Groups)
                //{
                //    var rewardGroup = new RewardGroup
                //    {
                //        Gr_ID = item.It_ID,
                //        RwGr_Ordinal = item.It_Ordinal,
                //        RwGr_Active = true,
                //        RwGr_Creator = user.Id
                //    };
                //    reward.Groups.Add(rewardGroup);

                //    var group = await _ctx.Groups.FindAsync(new object[] { item.It_ID }, cancellationToken: cancellationToken);
                //    if (group == null) continue;

                //    int i = 0;
                //    foreach (var customer in group.Customers.Where(s => s.Customer.Us_Active == true))
                //    {
                //        var rewardCustomer = reward.Customers.Where(s => s.Cs_ID == customer.Cs_ID).FirstOrDefault();
                //        if (rewardCustomer == null)
                //        {
                //            var customerReward = new CustomerReward
                //            {
                //                Cs_ID = customer.Cs_ID,
                //                CsRw_Ordinal = i,
                //                CsRw_Active = true,
                //                CsRw_Creator = user.Id,
                //                CsRw_Coins = reward.Rw_Coins,
                //                CsRw_CoinsLeft = reward.Rw_Coins,
                //                CsRw_Duration = reward.Rw_Duration,
                //                CsRw_ExpiredOn = DateTime.UtcNow.AddMonths(reward.Rw_Duration)
                //            };
                //            reward.Customers.Add(customerReward);
                //            i++;
                //        } else
                //        {
                //            rewardCustomer.CsRw_Active = true;
                //            rewardCustomer.CsRw_Modifier = user.Id;
                //            rewardCustomer.CsRw_ModifyOn = DateTime.UtcNow;
                //            rewardCustomer.CsRw_Coins = reward.Rw_Coins;
                //            rewardCustomer.CsRw_CoinsLeft = reward.Rw_Coins;
                //            rewardCustomer.CsRw_Duration = reward.Rw_Duration;
                //            rewardCustomer.CsRw_ExpiredOn = DateTime.UtcNow.AddMonths(reward.Rw_Duration);
                //        }
                //    }
                //}

                _ctx.Rewards.Add(reward);

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<RewardDto>.Failure("Failed to create Reward");
                return Result<RewardDto>.Success(_mpr.Map<RewardDto>(reward)); 
            }
        }
    }
}