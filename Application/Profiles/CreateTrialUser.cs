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
    public class CreateTrialUser
    {
        public class Command : IRequest<Result<ProfileQryDto>>
        {
            public ProfileQryDto Profile { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<ProfileQryDto>>
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
                    RuleFor(x => x.Profile).SetValidator(new ProfileQryValidator());
                }
            }

            public async Task<Result<ProfileQryDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                if (await _ctx.Users.AnyAsync(x => x.UserName == req.Profile.Username && !x.Us_Deleted))
                {
                    return Result<ProfileQryDto>.Failure("Failed to register Trial User - Username already taken!");
                }

                if (await _ctx.Users.AnyAsync(x => x.Email == req.Profile.Email && !x.Us_Deleted))
                {
                    return Result<ProfileQryDto>.Failure("Failed to resgister Trial User - Email already  taken!");
                }

                if (await _ctx.Users.AnyAsync(x => x.PhoneNumber == req.Profile.PhoneNumber && !x.Us_Deleted))
                {
                    return Result<ProfileQryDto>.Failure("Failed to register Trial User - Phone Number already taken!");
                }

                AppUser profile = _mpr.Map<AppUser>(req.Profile);

                _mpr.Map(req.Profile, profile);

                profile.Us_Creator = user.Id;
                profile.Us_CreatedOn = DateTime.UtcNow;
                profile.Us_Modifier = user.Id;
                profile.Us_ModifyOn = DateTime.UtcNow;
                profile.Us_Customer = true;
                profile.Us_Admin = false;
                profile.Us_SubscriptionDays = 0;
                profile.Us_Modifier = null;
                profile.Us_SuperAdmin = false;

                profile.Genres = new List<CustomerGenre>();
                if (req.Profile.GenreItems != null)
                {
                    foreach (ItemDto genre in req.Profile.GenreItems)
                    {
                        var customerGenre = new CustomerGenre
                        {
                            Gn_ID = genre.It_ID,
                            CsGn_Ordinal = genre.It_Ordinal,
                            CsGn_Active = true,
                            CsGn_Creator = user.Id
                        };
                        profile.Genres.Add(customerGenre);
                    }
                }

                profile.Categories = new List<CustomerCategory>();
                if (req.Profile.CategoryItems != null)
                {
                    foreach (ItemDto catgory in req.Profile.CategoryItems)
                    {
                        var customerCategory = new CustomerCategory
                        {
                            Ct_ID = catgory.It_ID,
                            CsCt_Ordinal = catgory.It_Ordinal,
                            CsCt_Active = true,
                            CsCt_Creator = user.Id
                        };
                        profile.Categories.Add(customerCategory);
                    }
                }

                profile.ThematicAreas = new List<CustomerThematicArea>();
                if (req.Profile.ThematicAreaItems != null)
                {
                    foreach (ItemDto thematicArea in req.Profile.ThematicAreaItems)
                    {
                        var customerThematicArea = new CustomerThematicArea
                        {
                            Th_ID = thematicArea.It_ID,
                            CsTh_Ordinal = thematicArea.It_Ordinal,
                            CsTh_Active = true,
                            CsTh_Creator = user.Id
                        };
                        profile.ThematicAreas.Add(customerThematicArea);
                    }
                }

                profile.Tags = new List<CustomerTag>();
                if (req.Profile.TagItems != null)
                {
                    foreach (ItemDto tag in req.Profile.TagItems)
                    {
                        var customerTag = new CustomerTag
                        {
                            Tg_ID = tag.It_ID,
                            CsTg_Ordinal = tag.It_Ordinal,
                            CsTg_Active = true,
                            CsTg_Creator = user.Id
                        };
                        profile.Tags.Add(customerTag);
                    }
                }


                var result = await _userManager.CreateAsync(profile, req.Profile.Password);

                if (!result.Succeeded) return Result<ProfileQryDto>.Failure("Failed to create Trial User");

                profile = await _ctx.Users
                    .Include(i => i.Genres).ThenInclude(j => j.Genre)
                    .Include(i => i.Categories).ThenInclude(j => j.Category)
                    .Include(i => i.ThematicAreas).ThenInclude(j => j.ThematicArea)
                    .Include(i => i.Tags).ThenInclude(j => j.Tag)
                    .Include(i => i.Creator)
                    .FirstOrDefaultAsync(s =>
                    s.UserName == req.Profile.Username && !s.Us_Deleted, cancellationToken: cancellationToken);

                return Result<ProfileQryDto>.Success(_mpr.Map<ProfileQryDto>(profile)); 
            }
        }
    }
}