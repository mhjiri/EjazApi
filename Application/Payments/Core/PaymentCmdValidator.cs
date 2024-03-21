using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.Payments.Core
{
    public class PaymentCmdValidator : AbstractValidator<PaymentCmdDto>
    {
        public PaymentCmdValidator()
        {
            //RuleFor(s => s.Py_ID).NotEmpty();
            //RuleFor(s => s.Sb_ID).NotEmpty();
            RuleFor(s => s.Pm_RefernceID).NotEmpty();
        }
    }
}