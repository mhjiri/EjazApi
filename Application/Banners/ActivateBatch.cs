using System.Diagnostics;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Azure.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Banners
{
    public class ActivateBatch
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
                var banners = _ctx.Banners.Where(s => req.Ids.Contains(s.Bn_ID));

                if (banners == null || !banners.Any()) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                foreach(var banner in banners)
                {
                    banner.Bn_Modifier = user.Id;
                    banner.Bn_ModifyOn = DateTime.UtcNow;
                    banner.Bn_Active = true;
                }

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failed to activate Banners");
                return Result<Unit>.Success(Unit.Value); 
            }
        }
    }
}