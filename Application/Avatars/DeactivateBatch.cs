using System.Diagnostics;
using Application.Core;
using Application.Interfaces;
using Application.Avatars.Core;
using AutoMapper;
using Azure.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Avatars
{
    public class DeactivateBatch
    {
        public class Command : IRequest<Result<Unit>>
        {
            public ICollection<Guid> Ids { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _ctx;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext ctx, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _ctx = ctx;
            }

            public async Task<Result<Unit>> Handle(Command req, CancellationToken cancellationToken)
            {
                var avatars = _ctx.Avatars.Where(s => req.Ids.Contains(s.Av_ID));

                if (avatars == null || !avatars.Any()) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                foreach (var avatar in avatars)
                {
                    avatar.Av_Modifier = user.Id;
                    avatar.Av_ModifyOn = DateTime.UtcNow;
                    avatar.Av_Active = false;
                }

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failed to deactivate Avatars");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}