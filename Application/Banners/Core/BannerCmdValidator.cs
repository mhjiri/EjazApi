using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.Banners.Core
{
    public class BannerCmdValidator : AbstractValidator<BannerCmdDto>
    {
        public BannerCmdValidator()
        {
            RuleFor(s => s.Bn_Title).NotEmpty().Length(3, 250);
            RuleFor(s => s.Bn_Title_Ar).NotEmpty().Length(3, 250);
            RuleFor(s => s.Bl_ID).NotEmpty();
            RuleFor(s => s.Gr_ID).NotEmpty();
            RuleFor(s => s.Md_ID).NotEmpty();
        }
    }
}