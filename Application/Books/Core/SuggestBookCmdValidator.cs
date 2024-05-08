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
            RuleFor(s => s.Bk_Code).NotEmpty().When(s => s.Bk_Code != null);
            RuleFor(s => s.Bk_Title).NotEmpty().Length(3, 250).When(s => s.Bk_Title != null);
            RuleFor(s => s.Bk_Author).NotEmpty().Length(3, 250).When(s => s.Bk_Author != null);
            RuleFor(s => s.Bk_Editor).NotEmpty().Length(3, 250).When(s => s.Bk_Editor != null);
            RuleFor(s => s.Bk_Comments).NotEmpty().Length(3, 1000).When(s => s.Bk_Comments != null);
            RuleFor(s => s.Bk_Language).NotEmpty().When(s => s.Bk_Language != null);
        }
    }
}
