using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.BannerLocations.Core
{
    public class BannerLocationValidator : AbstractValidator<BannerLocationDto>
    {
        public BannerLocationValidator()
        {
            RuleFor(s => s.Bl_Title).NotEmpty().Length(3,250);
            RuleFor(s => s.Bl_Title_Ar).NotEmpty().Length(3, 250);
            RuleFor(s => s.Bl_Ratio).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}