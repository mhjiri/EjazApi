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
    public class UpdateTrialUser
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
                var user = await _ctx.Users
                    .Include(i => i.Creator).FirstOrDefaultAsync(s =>
                   s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                var profile = await _ctx.Users
                    .Include(i => i.Genres).ThenInclude(j => j.Genre)
                    .Include(i => i.Categories).ThenInclude(j => j.Category)
                    .Include(i => i.ThematicAreas).ThenInclude(j => j.ThematicArea)
                    .Include(i => i.Tags).ThenInclude(j => j.Tag)
                    .FirstOrDefaultAsync(s =>
                    s.UserName == req.Profile.Username && s.Us_Customer == true && !s.Us_Deleted && (s.Us_SubscriptionExpiryDate == null || s.Us_SubscriptionExpiryDate < DateTime.Now), cancellationToken: cancellationToken);

                if (profile == null) return null;

                if (await _ctx.Users.AnyAsync(x => x.Email == req.Profile.Email && x.UserName != profile.UserName && !x.Us_Deleted))
                {
                    return Result<ProfileQryDto>.Failure("Failed to update Trial User - Email already  taken!");
                }

                if (await _ctx.Users.AnyAsync(x => x.PhoneNumber == req.Profile.PhoneNumber && x.UserName != profile.UserName && !x.Us_Deleted))
                {
                    return Result<ProfileQryDto>.Failure("Failed to update Trial User - Phone Number already taken!");
                }

                DateTime dateTime = DateTime.UtcNow;
                req.Profile.Us_Creator = profile.Us_Creator;
                _mpr.Map(req.Profile, profile);
                profile.Us_Modifier = user.Id;
                profile.Us_ModifyOn = dateTime;

                //Book Genre
                if (req.Profile.GenreItems != null)
                {
                    foreach (var customerGenre in profile.Genres.Where(s => !req.Profile.GenreItems.Select(z => z.It_ID).Contains(s.Gn_ID)))
                    {
                        customerGenre.CsGn_Active = false;
                        customerGenre.CsGn_Modifier = user.Id;
                        customerGenre.CsGn_ModifyOn = dateTime;
                    }

                    foreach (ItemDto item in req.Profile.GenreItems)
                    {
                        var genre = profile.Genres.Where(s => s.Gn_ID == item.It_ID).FirstOrDefault();
                        if (genre == null)
                        {
                            var customerGenre = new CustomerGenre
                            {
                                Gn_ID = item.It_ID,
                                CsGn_Ordinal = item.It_Ordinal,
                                CsGn_Active = true,
                                CsGn_Creator = user.Id,
                                CsGn_CreatedOn = dateTime
                            };
                            profile.Genres.Add(customerGenre);
                        }
                        else
                        {
                            genre.CsGn_Ordinal = item.It_Ordinal;
                            if (!genre.CsGn_Active) genre.CsGn_Active = true;
                            genre.CsGn_Modifier = user.Id;
                            genre.CsGn_ModifyOn = dateTime;
                        }

                    }
                }
                else
                {
                    foreach (var customerGenre in profile.Genres)
                    {
                        customerGenre.CsGn_Active = false;
                        customerGenre.CsGn_Modifier = user.Id;
                        customerGenre.CsGn_ModifyOn = dateTime;
                    }
                }

                //Category
                if (req.Profile.CategoryItems != null)
                {
                    foreach (var customerCategory in profile.Categories.Where(s => !req.Profile.CategoryItems.Select(z => z.It_ID).Contains(s.Ct_ID)))
                    {
                        customerCategory.CsCt_Active = false;
                        customerCategory.CsCt_Modifier = user.Id;
                        customerCategory.CsCt_ModifyOn = dateTime;
                    }

                    foreach (ItemDto item in req.Profile.CategoryItems)
                    {
                        var category = profile.Categories.Where(s => s.Ct_ID == item.It_ID).FirstOrDefault();
                        if (category == null)
                        {
                            var customerCategory = new CustomerCategory
                            {
                                Ct_ID = item.It_ID,
                                CsCt_Ordinal = item.It_Ordinal,
                                CsCt_Active = true,
                                CsCt_Creator = user.Id,
                                CsCt_CreatedOn = dateTime
                            };
                            profile.Categories.Add(customerCategory);
                        }
                        else
                        {
                            category.CsCt_Ordinal = item.It_Ordinal;
                            if (!category.CsCt_Active) category.CsCt_Active = true;
                            category.CsCt_Modifier = user.Id;
                            category.CsCt_ModifyOn = dateTime;
                        }

                    }
                }
                else
                {
                    foreach (var customerCategory in profile.Categories)
                    {
                        customerCategory.CsCt_Active = false;
                        customerCategory.CsCt_Modifier = user.Id;
                        customerCategory.CsCt_ModifyOn = dateTime;
                    }
                }

                //ThematicArea
                if (req.Profile.ThematicAreaItems != null)
                {
                    foreach (var customerThematicArea in profile.ThematicAreas.Where(s => !req.Profile.ThematicAreaItems.Select(z => z.It_ID).Contains(s.Th_ID)))
                    {
                        customerThematicArea.CsTh_Active = false;
                        customerThematicArea.CsTh_Modifier = user.Id;
                        customerThematicArea.CsTh_ModifyOn = dateTime;
                    }

                    foreach (ItemDto item in req.Profile.ThematicAreaItems)
                    {
                        var thematicArea = profile.ThematicAreas.Where(s => s.Th_ID == item.It_ID).FirstOrDefault();
                        if (thematicArea == null)
                        {
                            var customerThematicArea = new CustomerThematicArea
                            {
                                Th_ID = item.It_ID,
                                CsTh_Ordinal = item.It_Ordinal,
                                CsTh_Active = true,
                                CsTh_Creator = user.Id,
                                CsTh_CreatedOn = dateTime
                            };
                            profile.ThematicAreas.Add(customerThematicArea);
                        }
                        else
                        {
                            thematicArea.CsTh_Ordinal = item.It_Ordinal;
                            if (!thematicArea.CsTh_Active) thematicArea.CsTh_Active = true;
                            thematicArea.CsTh_Modifier = user.Id;
                            thematicArea.CsTh_ModifyOn = dateTime;
                        }

                    }
                }
                else
                {
                    foreach (var customerThematicArea in profile.ThematicAreas)
                    {
                        customerThematicArea.CsTh_Active = false;
                        customerThematicArea.CsTh_Modifier = user.Id;
                        customerThematicArea.CsTh_ModifyOn = dateTime;
                    }
                }

                //Tag
                if (req.Profile.TagItems != null)
                {
                    foreach (var customerTag in profile.Tags.Where(s => !req.Profile.TagItems.Select(z => z.It_ID).Contains(s.Tg_ID)))
                    {
                        customerTag.CsTg_Active = false;
                        customerTag.CsTg_Modifier = user.Id;
                        customerTag.CsTg_ModifyOn = dateTime;
                    }

                    foreach (ItemDto item in req.Profile.TagItems)
                    {
                        var tag = profile.Tags.Where(s => s.Tg_ID == item.It_ID).FirstOrDefault();
                        if (tag == null)
                        {
                            var customerTag = new CustomerTag
                            {
                                Tg_ID = item.It_ID,
                                CsTg_Ordinal = item.It_Ordinal,
                                CsTg_Active = true,
                                CsTg_Creator = user.Id,
                                CsTg_CreatedOn = dateTime
                            };
                            profile.Tags.Add(customerTag);
                        }
                        else
                        {
                            tag.CsTg_Ordinal = item.It_Ordinal;
                            if (!tag.CsTg_Active) tag.CsTg_Active = true;
                            tag.CsTg_Modifier = user.Id;
                            tag.CsTg_ModifyOn = dateTime;
                        }

                    }
                }
                else
                {
                    foreach (var customerTag in profile.Tags)
                    {
                        customerTag.CsTg_Active = false;
                        customerTag.CsTg_Modifier = user.Id;
                        customerTag.CsTg_ModifyOn = dateTime;
                    }
                }


                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<ProfileQryDto>.Failure("Failed to update Trial User");

                if(!String.IsNullOrEmpty(req.Profile.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(profile);

                    var res = await _userManager.ResetPasswordAsync(profile, token, req.Profile.Password);
                }

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