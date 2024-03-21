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

namespace Application.Countries
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
                var countries = _ctx.Countries.Where(s => req.Ids.Contains(s.Cn_ID));

                if (countries == null || !countries.Any()) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                foreach(var country in countries)
                {
                    country.Cn_Modifier = user.Id;
                    country.Cn_ModifyOn = DateTime.UtcNow;
                    country.Cn_Active = true;
                }

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failed to activate Countries");
                return Result<Unit>.Success(Unit.Value); 
            }
        }
    }
}