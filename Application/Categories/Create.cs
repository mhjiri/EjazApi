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
    public class Create
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
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                Category category = _mpr.Map<Category>(req.Category);
                category.Ct_ClassificationID = (category.Ct_ClassificationID == Guid.Empty) ? null : category.Ct_ClassificationID;
                category.Ct_Creator = user.Id;
                category.Tags = new List<CategoryTag>();

                if (req.Category.TagItems != null) {
                    foreach (ItemDto tag in req.Category.TagItems)
                    {
                        var categoryTag = new CategoryTag
                        {
                            Tg_ID = tag.It_ID,
                            CtTg_Ordinal = tag.It_Ordinal,
                            CtTg_Active = true,
                            CtTg_Creator = user.Id
                        };
                        category.Tags.Add(categoryTag);
                    }
                }

                _ctx.Categories.Add(category);

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<CategoryDto>.Failure("Failed to create Category");

                category = await _ctx.Categories.Include(i => i.Tags).ThenInclude(j => j.Tag).Include(i => i.Classification).Where(s => s.Ct_ID == category.Ct_ID).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                return Result<CategoryDto>.Success(_mpr.Map<CategoryDto>(category)); 
            }
        }
    }
}