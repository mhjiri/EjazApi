using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.Authors.Core
{
    public class AuthorQryValidator : AbstractValidator<AuthorQryDto>
    {
        public AuthorQryValidator()
        {
            RuleFor(s => s.At_Name).NotEmpty().Length(3, 250);
            RuleFor(s => s.At_Name_Ar).NotEmpty().Length(3, 250);
            RuleFor(s => s.At_Title).NotEmpty().Length(3, 250);
            RuleFor(s => s.At_Title_Ar).NotEmpty().Length(3, 250);
        }
    }
}