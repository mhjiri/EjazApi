using System.Diagnostics;
using Application.Core;
using Application.Tags.Core;
using Application.Interfaces;
using Application.ThematicAreas.Core;
using AutoMapper;
using Azure.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Tags
{
    public class Update
    {
        public class Command : IRequest<Result<TagDto>>
        {
            public TagCmdDto Tag { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<TagDto>>
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
                    RuleFor(x => x.Tag).SetValidator(new TagCmdValidator());
                }
            }

            public async Task<Result<TagDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var tag = await _ctx.Tags.FindAsync(new object[] { req.Tag.Tg_ID }, cancellationToken: cancellationToken);

                if (tag == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted);

                _mpr.Map(req.Tag, tag);

                tag.Tg_Modifier = user.Id;
                tag.Tg_ModifyOn = DateTime.UtcNow;

                

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<TagDto>.Failure("Failed to update Tag");
                return Result<TagDto>.Success(_mpr.Map<TagDto>(tag)); //Result<ThematicAreaDto>.Success(req.ThematicArea);
            }
        }
    }
}