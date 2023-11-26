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

namespace Application.Addresses
{
    public class SetDefault
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
                var address = await _ctx.Addresses.FindAsync(new object[] { req.Id }, cancellationToken: cancellationToken);

                if (address == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted);


                address.Ad_Modifier = user.Id;
                address.Ad_ModifyOn = DateTime.UtcNow;
                address.Ad_Active = true;
                address.Ad_IsDefault = true;

                var addresses = _ctx.Addresses.Where(s => s.User == user && s.Ad_IsDefault);
                foreach (var add in addresses)
                {
                    address.Ad_IsDefault = false;
                }

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failed to activate Address");
                return Result<Unit>.Success(Unit.Value); 
            }
        }
    }
}