using System.Diagnostics;
using Application.Core;
using Application.Tags.Core;
using Application.Interfaces;
using Application.ThematicAreas.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Tags
{
    public class Create
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
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                var tag = _mpr.Map<Tag>(req.Tag);
                tag.Tg_Creator = user.Id;

                _ctx.Tags.Add(tag);

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<TagDto>.Failure("Failed to create Tag");
                return Result<TagDto>.Success(_mpr.Map<TagDto>(tag)); 
            }
        }
    }
}