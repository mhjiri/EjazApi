using System.Diagnostics;
using Domain;
using FluentValidation;

namespace Application.Avatars.Core
{
    public class AvatarValidator : AbstractValidator<Avatar>
    {
        public AvatarValidator()
        {
            RuleFor(s => s.Av_Title).NotEmpty().Length(3,250);
            RuleFor(s => s.Av_Title_Ar).NotEmpty().Length(3, 250);
            RuleFor(s => s.Md_ID).NotEmpty();
            RuleFor(s => s.Creator).NotEmpty();
        }
    }
}