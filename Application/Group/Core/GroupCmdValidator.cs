using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.Groups.Core
{
    public class GroupCmdValidator : AbstractValidator<GroupCmdDto>
    {
        public GroupCmdValidator()
        {
            RuleFor(s => s.Gr_Title).NotEmpty().Length(3, 250);
            RuleFor(s => s.Gr_Title_Ar).NotEmpty().Length(3, 250);
            RuleFor(s => s.Gr_AgeFrom).LessThanOrEqualTo(s => s.Gr_AgeTill);
            RuleFor(s => s.Gr_AgeFrom).GreaterThanOrEqualTo(s => s.Gr_AgeFrom);
        }
    }
}