﻿using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.Books.Core
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(s => s.Bk_Code).NotEmpty();
            RuleFor(s => s.Bk_Name).NotEmpty().Length(3, 250);
            RuleFor(s => s.Bk_Name_Ar).NotEmpty().Length(3, 250);
            RuleFor(s => s.Bk_Title).NotEmpty().Length(3,250);
            RuleFor(s => s.Bk_Title_Ar).NotEmpty().Length(3, 250);
            RuleFor(s => s.Bk_Summary).NotEmpty();
            RuleFor(s => s.Bk_Summary_Ar).NotEmpty();
            RuleFor(s => s.Creator).NotEmpty();
        }
    }
}