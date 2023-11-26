using System.Diagnostics;
using Application.Core;
using Application.Profiles.Core;
using Application.Interfaces;
using Application.ThematicAreas.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles
{
    public class UpdateLanguage
    {
        public class Command : IRequest<Result<ProfileDto>>
        {
            public ProfileLanguageDto Profile { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<ProfileDto>>
        {
            private readonly DataContext _ctx;
            private readonly IMapper _mpr;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext ctx, IMapper mpr, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _ctx = ctx;
                _mpr = mpr;
            }

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.Profile).SetValidator(new ProfileLanguageValidator());
                }
            }

            public async Task<Result<ProfileDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                if (user == null) return null;

                DateTime dateTime = DateTime.UtcNow;


                user.Us_language = req.Profile.Us_language;
                user.Us_Modifier = user.Id;
                user.Us_ModifyOn = dateTime;

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<ProfileDto>.Failure("Failed to update Profile");
                return Result<ProfileDto>.Success(_mpr.Map<ProfileDto>(user));
            }
        }
    }
}