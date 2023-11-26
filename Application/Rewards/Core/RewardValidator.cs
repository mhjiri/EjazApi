using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.Rewards.Core
{
    public class RewardValidator : AbstractValidator<Reward>
    {
        public RewardValidator()
        {
            RuleFor(s => s.Rw_Name).NotEmpty().Length(3, 250);
            RuleFor(s => s.Rw_Name_Ar).NotEmpty().Length(3, 250);
            RuleFor(s => s.Rw_Title).NotEmpty().Length(3,250);
            RuleFor(s => s.Rw_Title_Ar).NotEmpty().Length(3, 250);
            RuleFor(s => s.Rw_Coins).NotNull().NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(s => s.Rw_Duration).NotNull().NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(s => s.Creator).NotEmpty();
        }
    }
}