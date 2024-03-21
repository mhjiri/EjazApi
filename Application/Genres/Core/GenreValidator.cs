using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.Genres.Core
{
    public class GenreValidator : AbstractValidator<Genre>
    {
        public GenreValidator()
        {
            RuleFor(s => s.Gn_Title).NotEmpty().Length(3,250);
            RuleFor(s => s.Gn_Title_Ar).NotEmpty().Length(3, 250);
        }
    }
}