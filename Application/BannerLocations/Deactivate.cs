using System.Diagnostics;
using Application.Core;
using Application.Interfaces;
using Application.BannerLocations.Core;
using AutoMapper;
using Azure.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.BannerLocations
{
    public class Deactivate
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
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
                var bannerLocation = await _ctx.BannerLocations.FindAsync(new object[] { req.Id }, cancellationToken: cancellationToken);

                if (bannerLocation == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);


                bannerLocation.Bl_Modifier = user.Id;
                bannerLocation.Bl_ModifyOn = DateTime.UtcNow;
                bannerLocation.Bl_Active = false;

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failed to deactivate Banner Location");
                return Result<Unit>.Success(Unit.Value); 
            }
        }
    }
}