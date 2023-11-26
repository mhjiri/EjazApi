using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.BookCollections.Core
{
    public class BookCollectionValidator : AbstractValidator<BookCollection>
    {
        public BookCollectionValidator()
        {
            RuleFor(s => s.Bc_Title).NotEmpty().Length(3,250);
            RuleFor(s => s.Bc_Title_Ar).NotEmpty().Length(3, 250);
        }
    }
}