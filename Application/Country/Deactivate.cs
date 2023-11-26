using System.Diagnostics;
using Application.Core;
using Application.Interfaces;
using Application.Countries.Core;
using AutoMapper;
using Azure.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Countries
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
                var country = await _ctx.Countries.FindAsync(new object[] { req.Id }, cancellationToken: cancellationToken);

                if (country == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);


                country.Cn_Modifier = user.Id;
                country.Cn_ModifyOn = DateTime.UtcNow;
                country.Cn_Active = false;

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failed to deactivate Country");
                return Result<Unit>.Success(Unit.Value); 
            }
        }
    }
}