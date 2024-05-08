using System.Diagnostics;
using Application.Core;
using Domain;
using FluentValidation;

namespace Application.Media.Core
{
    public class MediumFileCmdValidator : AbstractValidator<MediumFileCmdDto>
    {
        public MediumFileCmdValidator()
        {
            RuleFor(s => s.Md_Title).NotEmpty().Length(3,250);
            RuleFor(s => s.Md_Title_Ar).NotEmpty().Length(3, 250);
            //RuleFor(s => s.Md_Creator).NotEmpty();
            RuleFor(x => x.Md_URL).SetValidator(new FileAudioValidator());
        }
    }
}