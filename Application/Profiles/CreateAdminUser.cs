using System.Diagnostics;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Application.Profiles.Core;
using System.Security.Policy;
using Microsoft.AspNetCore.Identity;

namespace Application.Profiles
{
    public class CreateAdminUser
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

                if (await _ctx.Users.AnyAsync(x => x.UserName == req.Profile.Username && !x.Us_Deleted))
                {
                    return Result<ProfileDto>.Failure("Failed to register Trial User - Username already taken!");
                }

                if (await _ctx.Users.AnyAsync(x => x.Email == req.Profile.Email && !x.Us_Deleted))
                {
                    return Result<ProfileDto>.Failure("Failed to resgister Trial User - Email already  taken!");
                }

                if (await _ctx.Users.AnyAsync(x => x.PhoneNumber == req.Profile.PhoneNumber))
                {
                    return Result<ProfileDto>.Failure("Failed to register Trial User - Phone Number already taken!");
                }

                AppUser profile = _mpr.Map<AppUser>(req.Profile);

                _mpr.Map(req.Profile, profile);

                profile.Us_Creator = user.Id;
                profile.Us_CreatedOn = DateTime.UtcNow;
                profile.Us_Modifier = user.Id;
                profile.Us_ModifyOn = DateTime.UtcNow;
                profile.Us_Admin = true;
                profile.Us_Customer = false;

                var result = await _userManager.CreateAsync(profile, req.Profile.Password);

                if (!result.Succeeded) return Result<ProfileDto>.Failure("Failed to update Trial User");

                if(profile.Us_SuperAdmin)
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

                profile = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == req.Profile.Username && !s.Us_Deleted, cancellationToken: cancellationToken);

                return Result<ProfileDto>.Success(_mpr.Map<ProfileDto>(profile)); 
            }
        }
    }
}