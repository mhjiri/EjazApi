using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.Subscriptions.Core
{
    public class SubscriptionCmdValidator : AbstractValidator<SubscriptionCmdDto>
    {
        public SubscriptionCmdValidator()
        {
            RuleFor(s => s.Sb_Name).NotEmpty().Length(3, 250);
            RuleFor(s => s.Sb_Name_Ar).NotEmpty().Length(3, 250);
            RuleFor(s => s.Sb_Title).NotEmpty().Length(3, 250);
            RuleFor(s => s.Sb_Title_Ar).NotEmpty().Length(3, 250);
            RuleFor(s => s.Sb_Days).NotEmpty().GreaterThan(0);
            RuleFor(s => s.Sb_Price).NotEmpty();
        }
    }
}