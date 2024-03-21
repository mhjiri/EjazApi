using System.Diagnostics;   
using Application.Core;
using Application.Groups.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Groups
{
    public class Update
    {
        public class Command : IRequest<Result<GroupQryDto>>
        {
            public GroupCmdDto Group { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<GroupQryDto>>
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
                    RuleFor(x => x.Group).SetValidator(new GroupCmdValidator());
                }
            }

            public async Task<Result<GroupQryDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var group = await _ctx.Groups.FindAsync(new object[] { req.Group.Gr_ID }, cancellationToken: cancellationToken);

                if (group == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                DateTime dateTime = DateTime.UtcNow;

                
                _mpr.Map(req.Group, group);
                group.Gr_Modifier = user.Id;
                group.Gr_ModifyOn = dateTime;

                if (req.Group.Categories != null)
                {
                    foreach (var groupCategory in group.Categories.Where(s => !req.Group.CategoryItems.Select(z => z.It_ID).Contains(s.Ct_ID)))
                    {
                        groupCategory.GrCt_Active = false;
                        groupCategory.GrCt_Modifier = user.Id;
                        groupCategory.GrCt_ModifyOn = dateTime;
                    }

                    foreach (ItemDto item in req.Group.CategoryItems)
                    {
                        var category = group.Categories.Where(s => s.Ct_ID == item.It_ID).FirstOrDefault();
                        if (category == null)
                        {
                            var groupCategory = new GroupCategory
                            {
                                Ct_ID = item.It_ID,
                                GrCt_Ordinal = item.It_Ordinal,
                                GrCt_Active = true,
                                GrCt_Creator = user.Id,
                                GrCt_CreatedOn = dateTime
                            };
                            group.Categories.Add(groupCategory);
                        }
                        else
                        {
                            category.GrCt_Ordinal = item.It_Ordinal;
                            if (!category.GrCt_Active) category.GrCt_Active = true;
                            category.GrCt_Modifier = user.Id;
                            category.GrCt_ModifyOn = dateTime;
                        }

                    }
                }
                else
                {
                    foreach (var groupCategory in group.Categories)
                    {
                        groupCategory.GrCt_Active = false;
                        groupCategory.GrCt_Modifier = user.Id;
                        groupCategory.GrCt_ModifyOn = dateTime;
                    }
                }


                if (req.Group.ThematicAreas != null)
                {
                    foreach (var groupThematicArea in group.ThematicAreas.Where(s => !req.Group.ThematicAreaItems.Select(z => z.It_ID).Contains(s.Th_ID)))
                    {
                        groupThematicArea.GrTh_Active = false;
                        groupThematicArea.GrTh_Modifier = user.Id;
                        groupThematicArea.GrTh_ModifyOn = dateTime;
                    }

                    foreach (ItemDto item in req.Group.ThematicAreaItems)
                    {
                        var thematicArea = group.ThematicAreas.Where(s => s.Th_ID == item.It_ID).FirstOrDefault();
                        if (thematicArea == null)
                        {
                            var groupThematicrArea = new GroupThematicArea
                            {
                                Th_ID = item.It_ID,
                                GrTh_Ordinal = item.It_Ordinal,
                                GrTh_Active = true,
                                GrTh_Creator = user.Id,
                                GrTh_CreatedOn = dateTime
                            };
                            group.ThematicAreas.Add(groupThematicrArea);
                        }
                        else
                        {
                            thematicArea.GrTh_Ordinal = item.It_Ordinal;
                            if (!thematicArea.GrTh_Active) thematicArea.GrTh_Active = true;
                            thematicArea.GrTh_Modifier = user.Id;
                            thematicArea.GrTh_ModifyOn = dateTime;
                        }

                    }
                }
                else
                {
                    foreach (var groupThematicrArea in group.ThematicAreas)
                    {
                        groupThematicrArea.GrTh_Active = false;
                        groupThematicrArea.GrTh_Modifier = user.Id;
                        groupThematicrArea.GrTh_ModifyOn = dateTime;
                    }
                }


                if (req.Group.Genres != null)
                {
                    foreach (var groupGenre in group.Genres.Where(s => !req.Group.GenreItems.Select(z => z.It_ID).Contains(s.Gn_ID)))
                    {
                        groupGenre.GrGn_Active = false;
                        groupGenre.GrGn_Modifier = user.Id;
                        groupGenre.GrGn_ModifyOn = dateTime;
                    }

                    foreach (ItemDto item in req.Group.GenreItems)
                    {
                        var genre = group.Genres.Where(s => s.Gn_ID == item.It_ID).FirstOrDefault();
                        if (genre == null)
                        {
                            var groupGenre = new GroupGenre
                            {
                                Gn_ID = item.It_ID,
                                GrGn_Ordinal = item.It_Ordinal,
                                GrGn_Active = true,
                                GrGn_Creator = user.Id,
                                GrGn_CreatedOn = dateTime
                            };
                            group.Genres.Add(groupGenre);
                        }
                        else
                        {
                            genre.GrGn_Ordinal = item.It_Ordinal;
                            if (!genre.GrGn_Active) genre.GrGn_Active = true;
                            genre.GrGn_Modifier = user.Id;
                            genre.GrGn_ModifyOn = dateTime;
                        }

                    }
                }
                else
                {
                    foreach (var groupGenre in group.Genres)
                    {
                        groupGenre.GrGn_Active = false;
                        groupGenre.GrGn_Modifier = user.Id;
                        groupGenre.GrGn_ModifyOn = dateTime;
                    }
                }

                if (req.Group.Tags != null)
                {
                    foreach (var groupTag in group.Tags.Where(s => !req.Group.TagItems.Select(z => z.It_ID).Contains(s.Tg_ID)))
                    {
                        groupTag.GrTg_Active = false;
                        groupTag.GrTg_Modifier = user.Id;
                        groupTag.GrTg_ModifyOn = dateTime;
                    }

                    foreach (ItemDto item in req.Group.TagItems)
                    {
                        var tag = group.Tags.Where(s => s.Tg_ID == item.It_ID).FirstOrDefault();
                        if (tag == null)
                        {
                            var groupTag = new GroupTag
                            {
                                Tg_ID = item.It_ID,
                                GrTg_Ordinal = item.It_Ordinal,
                                GrTg_Active = true,
                                GrTg_Creator = user.Id,
                                GrTg_CreatedOn = dateTime
                            };
                            group.Tags.Add(groupTag);
                        }
                        else
                        {
                            tag.GrTg_Ordinal = item.It_Ordinal;
                            if (!tag.GrTg_Active) tag.GrTg_Active = true;
                            tag.GrTg_Modifier = user.Id;
                            tag.GrTg_ModifyOn = dateTime;
                        }

                    }
                }
                else
                {
                    foreach (var groupTag in group.Tags)
                    {
                        groupTag.GrTg_Active = false;
                        groupTag.GrTg_Modifier = user.Id;
                        groupTag.GrTg_ModifyOn = dateTime;
                    }
                }

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<GroupQryDto>.Failure("Failed to update Group");
                return Result<GroupQryDto>.Success(_mpr.Map<GroupQryDto>(group));
            }
        }
    }
}