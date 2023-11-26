using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.Addresses.Core
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(s => s.Ad_Title).NotEmpty().Length(3,250);
            RuleFor(s => s.Ad_Title_Ar).NotEmpty().Length(3, 250);
            RuleFor(s => s.Ad_Building).NotEmpty();
            RuleFor(s => s.Ad_Street).NotEmpty();
            RuleFor(s => s.Ad_Zone).NotEmpty();
            RuleFor(s => s.Ad_State).NotEmpty();
            RuleFor(s => s.Cn_ID).NotEmpty();
            RuleFor(s => s.Us_ID).NotEmpty();
        }
    }
}