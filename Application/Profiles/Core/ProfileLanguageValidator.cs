using System.Diagnostics;
using Application.Profiles.Core;
using Domain;
using FluentValidation;

namespace Application.Profiles.Core
{
    public class ProfileLanguageValidator : AbstractValidator<ProfileLanguageDto>
    {
        public ProfileLanguageValidator()
        {

            RuleFor(s => s.Us_language).NotEmpty().Must(ValidateLanguage)
                .WithMessage("Language must be either all, Arabic or English");
        }

        private static bool ValidateLanguage(string language)
        {
            List<string> Languages = new List<string>() { "all", "arabic", "english" };
            return Languages.Contains(language.ToLower());
        }
    }
}