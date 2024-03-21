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

namespace Application.Profiles
{
    public class UpdateAdminUser
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
            private readonly RoleManager<IdentityRole> _roleManager;

            public Handler(DataContext ctx, IMapper mpr, IUserAccessor userAccessor, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userAccessor = userAccessor;
                _ctx = ctx;
                _mpr = mpr;
                _userManager = userManager;
                _roleManager = roleManager;
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

                var profile = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == req.Profile.Username && s.Us_Admin == true && !s.Us_Deleted, cancellationToken: cancellationToken);

                if (profile == null) return null;

                if (await _ctx.Users.AnyAsync(x => x.Email == req.Profile.Email && x.UserName != profile.UserName))
                {
                    return Result<ProfileDto>.Failure("Failed to update Admin User - Email already  taken!");
                }

                if (await _ctx.Users.AnyAsync(x => x.PhoneNumber == req.Profile.PhoneNumber && x.UserName != profile.UserName))
                {
                    return Result<ProfileDto>.Failure("Failed to update Admin User - Phone Number already taken!");
                }

                DateTime dateTime = DateTime.UtcNow;

                _mpr.Map(req.Profile, profile);
                profile.Us_Modifier = user.Id;
                profile.Us_ModifyOn = dateTime;


                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<ProfileDto>.Failure("Failed to update Admin User");

                if(!String.IsNullOrEmpty(req.Profile.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(profile);

                    var res = await _userManager.ResetPasswordAsync(profile, token, req.Profile.Password);
                }

                if (profile.Us_SuperAdmin)
                {
                    var superAdminRole = _roleManager.FindByNameAsync("SuperAdmin").Result;

                    if (superAdminRole != null)
                    {
                        IdentityResult superAdminRoleResult = await _userManager.AddToRoleAsync(profile, superAdminRole.Name);
                    }
                }

                var adminRole = _roleManager.FindByNameAsync("Admin").Result;

                if (adminRole != null)
                {
                    IdentityResult adminRoleResult = await _userManager.AddToRoleAsync(profile, adminRole.Name);
                }



                return Result<ProfileDto>.Success(_mpr.Map<ProfileDto>(req.Profile));
            }
        }
    }
}