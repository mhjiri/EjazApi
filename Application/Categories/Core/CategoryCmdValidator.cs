using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.Categories.Core
{
    public class CategoryCmdValidator : AbstractValidator<CategoryCmdDto>
    {
        public CategoryCmdValidator()
        {
            RuleFor(s => s.Ct_Name).NotEmpty().Length(3, 250);
            RuleFor(s => s.Ct_Name_Ar).NotEmpty().Length(3, 250);
            RuleFor(s => s.Ct_Title).NotEmpty().Length(3,250);
            RuleFor(s => s.Ct_Title_Ar).NotEmpty().Length(3, 250);
        }
    }
}