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
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Profiles
{
    public class Update
    {
        public class Command : IRequest<Result<ProfileDto>>
        {
            public ProfileDto Profile { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<ProfileDto>>
        {
            private readonly DataContext _ctx;
            private readonly IMapper _mpr;
            private readonly IUserAccessor _userAccessor;
            private readonly UserManager<AppUser> _userManager;

            public Handler(DataContext ctx, IMapper mpr, IUserAccessor userAccessor, UserManager<AppUser> userManager)
            {
                _userAccessor = userAccessor;
                _ctx = ctx;
                _mpr = mpr;
                _userManager = userManager;
            }

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.Profile).SetValidator(new ProfileValidator());
                }
            }

            public async Task<Result<ProfileDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                if (user == null) return null;

                if (await _ctx.Users.AnyAsync(x => x.Email == req.Profile.Email && x.UserName != user.UserName))
                {
                    return Result<ProfileDto>.Failure("Failed to update Admin User - Email already  taken!");
                }

                if (await _ctx.Users.AnyAsync(x => x.PhoneNumber == req.Profile.PhoneNumber && x.UserName != user.UserName))
                {
                    return Result<ProfileDto>.Failure("Failed to update Admin User - Phone Number already taken!");
                }

                DateTime dateTime = DateTime.UtcNow;

                _mpr.Map(req.Profile, user);
                user.Us_Modifier = user.Id;
                user.Us_ModifyOn = dateTime;


                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<ProfileDto>.Failure("Failed to update Profile");

                if (!string.IsNullOrEmpty(req.Profile.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var res = await _userManager.ResetPasswordAsync(user, token, req.Profile.Password);
                }

                return Result<ProfileDto>.Success(_mpr.Map<ProfileDto>(user));
            }
        }
    }
}