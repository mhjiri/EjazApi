using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.Tags.Core
{
    public class TagCmdValidator : AbstractValidator<TagCmdDto>
    {
        public TagCmdValidator()
        {
            RuleFor(s => s.Tg_Title).NotEmpty().Length(3,250);
            RuleFor(s => s.Tg_Title_Ar).NotEmpty().Length(3, 250);
        }
    }
}