using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.PaymentMethods.Core
{
    public class PaymentMethodCmdValidator : AbstractValidator<PaymentMethodCmdDto>
    {
        public PaymentMethodCmdValidator()
        {
            RuleFor(s => s.Py_Title).NotEmpty().Length(3, 250);
            RuleFor(s => s.Py_Title_Ar).NotEmpty().Length(3, 250);
        }
    }
}