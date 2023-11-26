using System.Diagnostics;
using Application.Core;
using Application.Interfaces;
using Application.Profiles.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Id { get; set; }
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
                var profile = await _ctx.Users
                    .SingleOrDefaultAsync(s => s.UserName == req.Id && !s.Us_Deleted, cancellationToken: cancellationToken);


                if (profile == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                Random random = new Random();
                int randomNumber = random.Next(0, 1000);

                profile.UserName = String.Format("{0}{1}_{2}", "d", randomNumber.ToString(), profile.UserName);
                profile.NormalizedUserName = String.Format("{0}{1}_{2}", "d", randomNumber.ToString(), profile.NormalizedUserName);
                profile.Email = String.Format("{0}{1}_{2}", "d", randomNumber.ToString(), profile.Email);
                profile.NormalizedEmail = String.Format("{0}{1}_{2}", "d", randomNumber.ToString(), profile.NormalizedEmail);
                profile.PhoneNumber = String.Format("{0}{1}_{2}", "d", randomNumber.ToString(), profile.PhoneNumber);
                profile.Us_Modifier = user.Id;
                profile.Us_ModifyOn= DateTime.UtcNow;
                profile.Us_Active = false;
                profile.Us_Deleted = true;

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete User");
                return Result<Unit>.Success(Unit.Value); 
            }
        }
    }
}