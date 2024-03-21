using System.Diagnostics;
using System.Security.Policy;
using Application.Core;
using Application.Groups.Core;
using Application.Interfaces;
using Application.ThematicAreas.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Groups
{
    public class Create
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
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                Group group = _mpr.Map<Group>(req.Group);
                group.Gr_Creator = user.Id;

                group.Categories = new List<GroupCategory>();
                if (req.Group.CategoryItems != null)
                {
                    foreach (ItemDto category in req.Group.CategoryItems)
                    {
                        var groupCategory = new GroupCategory
                        {
                            Ct_ID = category.It_ID,
                            GrCt_Ordinal = category.It_Ordinal,
                            GrCt_Active = true,
                            GrCt_Creator = user.Id
                        };
                        group.Categories.Add(groupCategory);
                    }
                }

                group.ThematicAreas = new List<GroupThematicArea>();
                if (req.Group.ThematicAreaItems != null)
                {
                    foreach (ItemDto thematicArea in req.Group.ThematicAreaItems)
                    {
                        var groupThematicArea= new GroupThematicArea
                        {
                            Th_ID = thematicArea.It_ID,
                            GrTh_Ordinal = thematicArea.It_Ordinal,
                            GrTh_Active = true,
                            GrTh_Creator = user.Id
                        };
                        group.ThematicAreas.Add(groupThematicArea);
                    }
                }

                group.Genres = new List<GroupGenre>();
                if (req.Group.GenreItems != null)
                {
                    foreach (ItemDto genre in req.Group.GenreItems)
                    {
                        var groupGenre = new GroupGenre
                        {
                            Gn_ID = genre.It_ID,
                            GrGn_Ordinal = genre.It_Ordinal,
                            GrGn_Active = true,
                            GrGn_Creator = user.Id
                        };
                        group.Genres.Add(groupGenre);
                    }
                }

                group.Tags= new List<GroupTag>();
                if (req.Group.TagItems != null)
                {
                    foreach (ItemDto tag in req.Group.TagItems)
                    {
                        var groupTag = new GroupTag
                        {
                            Tg_ID = tag.It_ID,
                            GrTg_Ordinal = tag.It_Ordinal,
                            GrTg_Active = true,
                            GrTg_Creator = user.Id
                        };
                        group.Tags.Add(groupTag);
                    }
                }


                _ctx.Groups.Add(group);

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<GroupQryDto>.Failure("Failed to create Group");

                group = await _ctx.Groups
                    .Include(i => i.Categories).ThenInclude(j => j.Category)
                    .Include(i => i.ThematicAreas).ThenInclude(j => j.ThematicArea)
                    .Include(i => i.Genres).ThenInclude(j => j.Genre)
                    .Include(i => i.Tags).ThenInclude(j => j.Tag)
                    .Include(i => i.Creator)
                    .Where(s => s.Gr_ID == group.Gr_ID).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                return Result<GroupQryDto>.Success(_mpr.Map<GroupQryDto>(group)); 
            }
        }
    }
}