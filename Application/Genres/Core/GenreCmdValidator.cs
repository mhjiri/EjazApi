using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.Genres.Core
{
    public class GenreCmdValidator : AbstractValidator<GenreCmdDto>
    {
        public GenreCmdValidator()
        {
            RuleFor(s => s.Gn_Title).NotEmpty().Length(3,250);
            RuleFor(s => s.Gn_Title_Ar).NotEmpty().Length(3, 250);
        }
    }
}