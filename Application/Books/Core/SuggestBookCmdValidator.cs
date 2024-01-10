using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Core
{
    public class SuggestBookCmdValidator : AbstractValidator<SuggestBookCmd>
    {
        public SuggestBookCmdValidator()
        {
            RuleFor(s => s.Bk_Code).NotEmpty();
            RuleFor(s => s.Bk_Title).NotEmpty().Length(3, 250);
            RuleFor(s => s.Bk_Author).NotEmpty().Length(3, 250);
            RuleFor(s => s.Bk_Editor).NotEmpty().Length(3, 250);
            RuleFor(s => s.Bk_Comments).NotEmpty().Length(3, 1000);
            RuleFor(s => s.Bk_Language).NotEmpty();
        }
    }
}
