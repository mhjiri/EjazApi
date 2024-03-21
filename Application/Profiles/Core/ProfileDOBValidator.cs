using System.Diagnostics;
using Application.Profiles.Core;
using Domain;
using FluentValidation;

namespace Application.Profiles.Core
{
    public class ProfileDOBValidator : AbstractValidator<ProfileDOBDto>
    {
        public ProfileDOBValidator()
        {
            
            RuleFor(s => s.Us_DOB).NotEmpty().LessThan(DateTime.Now);
        }
    }
}