using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.Groups.Core
{
    public class GroupValidator : AbstractValidator<Group>
    {
        public GroupValidator()
        {
            RuleFor(s => s.Gr_Title).NotEmpty().Length(3,250);
            RuleFor(s => s.Gr_Title_Ar).NotEmpty().Length(3, 250);
        }
    }
}