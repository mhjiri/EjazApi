using System.Diagnostics;
using Application.Core;
using Application.Categories.Core;
using Application.Interfaces;
using Application.ThematicAreas.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Categories
{
    public class Update
    {
        public class Command : IRequest<Result<CategoryDto>>
        {
            public CategoryCmdDto Category { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<CategoryDto>>
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
                    RuleFor(x => x.Category).SetValidator(new CategoryCmdValidator());
                }
            }

            public async Task<Result<CategoryDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var category = await _ctx.Categories.Include(i => i.Tags).ThenInclude(j => j.Tag).Include(i => i.Classification).Where(s => s.Ct_ID == req.Category.Ct_ID).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (category == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                DateTime dateTime = DateTime.UtcNow;

                _mpr.Map(req.Category, category);

                category.Ct_ClassificationID = (category.Ct_ClassificationID == Guid.Empty) ? null : category.Ct_ClassificationID;
                category.Ct_Modifier = user.Id;
                category.Ct_ModifyOn = dateTime;



                if (req.Category.TagItems != null)
                {
                    foreach (var tag in category.Tags.Where(s => !req.Category.TagItems.Select(z => z.It_ID).Contains(s.Tg_ID)))
                    {
                        tag.CtTg_Active = false;
                        tag.CtTg_Modifier = user.Id;
                        tag.CtTg_ModifyOn = dateTime;
                    }

                    foreach (ItemDto item in req.Category.TagItems)
                    {
                        var tag = category.Tags.Where(s => s.Tg_ID == item.It_ID).FirstOrDefault();
                        if (tag == null)
                        {
                            var CategoryTag = new CategoryTag
                            {
                                Tg_ID = item.It_ID,
                                CtTg_Ordinal = item.It_Ordinal,
                                CtTg_Active = true,
                                CtTg_Creator = user.Id,
                                CtTg_CreatedOn = dateTime
                            };
                            category.Tags.Add(CategoryTag);
                        }
                        else
                        {
                            tag.CtTg_Ordinal = item.It_Ordinal;
                            if (!tag.CtTg_Active) tag.CtTg_Active = true;
                            tag.CtTg_Modifier = user.Id;
                            tag.CtTg_ModifyOn = dateTime;
                        }

                    }
                } else
                {
                    foreach (var categoryTag in category.Tags)
                    {
                        categoryTag.CtTg_Active = false;
                        categoryTag.CtTg_Modifier = user.Id;
                        categoryTag.CtTg_ModifyOn = dateTime;
                    }
                }

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<CategoryDto>.Failure("Failed to update Category");
                return Result<CategoryDto>.Success(_mpr.Map<CategoryDto>(category));
            }
        }
    }
}