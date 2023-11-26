using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.ThematicAreas.Core
{
    public class ThematicAreaCmdValidator : AbstractValidator<ThematicAreaCmdDto>
    {
        public ThematicAreaCmdValidator()
        {
            RuleFor(s => s.Th_Title).NotEmpty().Length(3,250);
            RuleFor(s => s.Th_Title_Ar).NotEmpty().Length(3, 250);
        }
    }
}