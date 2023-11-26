using System.Diagnostics;
using Application.Core;
using Application.Interfaces;
using Application.ThematicAreas.Core;
using AutoMapper;
using Azure.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Genres
{
    public class Activate
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
                var genre = await _ctx.Genres.FindAsync(new object[] { req.Id }, cancellationToken: cancellationToken);

                if (genre == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted);


                genre.Gn_Modifier = user.Id;
                genre.Gn_ModifyOn = DateTime.UtcNow;
                genre.Gn_Active = true;

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failed to activate Genre");
                return Result<Unit>.Success(Unit.Value); 
            }
        }
    }
}