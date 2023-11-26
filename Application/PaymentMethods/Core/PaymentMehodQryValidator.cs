using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.PaymentMehods.Core
{
    public class PaymentMehodQryValidator : AbstractValidator<PaymentMethodQryDto>
    {
        public PaymentMehodQryValidator()
        {
            RuleFor(s => s.Py_Title).NotEmpty().Length(3, 250);
            RuleFor(s => s.Py_Title_Ar).NotEmpty().Length(3, 250);
        }
    }
}