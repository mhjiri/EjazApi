using System.Diagnostics;
using Application.Core;
using Domain;
using FluentValidation;

namespace Application.Media.Core
{
    public class MediumCmdValidator : AbstractValidator<MediumCmdDto>
    {
        public MediumCmdValidator()
        {
            RuleFor(s => s.Md_Title).NotEmpty().Length(3,250);
            RuleFor(s => s.Md_Title_Ar).NotEmpty().Length(3, 250);
            //RuleFor(s => s.Md_Creator).NotEmpty();
            //RuleFor(x => x.D).SetValidator(new FileAudioValidator());
        }
    }
}