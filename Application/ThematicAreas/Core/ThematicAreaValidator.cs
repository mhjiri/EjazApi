using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.ThematicAreas.Core
{
    public class ThematicAreaValidator : AbstractValidator<ThematicArea>
    {
        public ThematicAreaValidator()
        {
            RuleFor(s => s.Th_Title).NotEmpty().Length(3,250);
            RuleFor(s => s.Th_Title_Ar).NotEmpty().Length(3, 250);
        }
    }
}