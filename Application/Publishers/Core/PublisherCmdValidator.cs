using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.Publishers.Core
{
    public class PublisherCmdValidator : AbstractValidator<PublisherCmdDto>
    {
        public PublisherCmdValidator()
        {
            RuleFor(s => s.Pb_Name).NotEmpty().Length(3, 250);
            RuleFor(s => s.Pb_Name_Ar).NotEmpty().Length(3, 250);
            RuleFor(s => s.Pb_Title).NotEmpty().Length(3, 250);
            RuleFor(s => s.Pb_Title_Ar).NotEmpty().Length(3, 250);
        }
    }
}