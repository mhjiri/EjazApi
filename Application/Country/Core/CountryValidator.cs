using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.Countries.Core
{
    public class CountryValidator : AbstractValidator<Country>
    {
        public CountryValidator()
        {
            RuleFor(s => s.Cn_Title).NotEmpty().Length(3,250);
            RuleFor(s => s.Cn_Title_Ar).NotEmpty().Length(3, 250);
            RuleFor(s => s.Cn_Code).NotEmpty();
            RuleFor(s => s.Creator).NotEmpty();
        }
    }
}