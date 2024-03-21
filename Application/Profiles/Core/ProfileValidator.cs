using System.Diagnostics;
using Application.Profiles.Core;
using Domain;
using FluentValidation;

namespace Application.Profiles.Core
{
    public class ProfileValidator : AbstractValidator<ProfileDto>
    {
        public ProfileValidator()
        {

            RuleFor(s => s.Username).NotNull().NotEmpty();
            RuleFor(s => s.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(s => s.Us_DOB).NotEmpty().LessThan(DateTime.Now);
            RuleFor(s => s.Us_Gender).NotEmpty().Must(ValidateGender)
                .WithMessage("Gender must be either Male or Female");
            RuleFor(s => s.Us_language).NotEmpty().Must(ValidateLanguage)
                .WithMessage("Language must be either all, Arabic or English");
            RuleFor(s => s.Us_DOB).NotEmpty().LessThan(DateTime.Now);
        }

        private static bool ValidateLanguage(string language)
        {
            List<string> Languages = new List<string>() { "all", "arabic", "english" };
            return Languages.Contains(language.ToLower());
        }

        private static bool ValidateGender(string gender)
        {
            List<string> Genders = new List<string>() { "male", "female" };
            return Genders.Contains(gender.ToLower());
        }
    }
}