using System.Diagnostics;
using Application.PaymentMethods.Core;
using Domain;
using FluentValidation;

namespace Application.Authors.Core
{
    public class PaymentMethodValidator : AbstractValidator<PaymentMethodDto>
    {
        public PaymentMethodValidator()
        {
            RuleFor(s => s.Py_Title).NotEmpty().Length(3, 250);
            RuleFor(s => s.Py_Title_Ar).NotEmpty().Length(3, 250);
        }
    }
}