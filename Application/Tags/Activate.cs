using System.Diagnostics;
using Application.Core;
using Application.Interfaces;
using Application.Tags.Core;
using AutoMapper;
using Azure.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Tags
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
                var tag = await _ctx.Tags.FindAsync(new object[] { req.Id }, cancellationToken: cancellationToken);

                if (tag == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted);


                tag.Tg_Modifier = user.Id;
                tag.Tg_ModifyOn = DateTime.UtcNow;
                tag.Tg_Active = true;

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failed to activate Tag");
                return Result<Unit>.Success(Unit.Value); 
            }
        }
    }
}