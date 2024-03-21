using System.Diagnostics;
using Application.Core;
using Application.Interfaces;
using Application.Avatars.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Avatars
{
    public class Create
    {
        public class Command : IRequest<Result<AvatarDto>>
        {
            public Avatar Avatar { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<AvatarDto>>
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
                    RuleFor(x => x.Avatar).SetValidator(new AvatarValidator());
                }
            }

            public async Task<Result<AvatarDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                req.Avatar.Av_Creator = user.Id;
                _ctx.Avatars.Add(req.Avatar);

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<AvatarDto>.Failure("Failed to create Avatar");
                return Result<AvatarDto>.Success(_mpr.Map<AvatarDto>(req.Avatar)); //Result<AvatarDto>.Success(req.Avatar);
            }
        }
    }
}