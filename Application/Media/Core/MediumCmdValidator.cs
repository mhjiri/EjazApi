using System.Diagnostics;
using Application.Core;
using Domain;
using FluentValidation;

namespace Application.Media.Core
{
    public class MediumCmdValidator : AbstractValidator<MediumCmdDto>
    {
        public MediumCmdValidator()
        {
            RuleFor(s => s.Md_Title).NotEmpty().Length(3, 250);
            RuleFor(s => s.Md_Title_Ar).NotEmpty().Length(3, 250);
            //RuleFor(s => s.Md_Creator).NotEmpty();
            RuleFor(s => s.Md_URL).NotEmpty().Must(BeValidUrl).WithMessage("Invalid download url format");
        }

        public bool BeValidUrl(string url)
        {
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}