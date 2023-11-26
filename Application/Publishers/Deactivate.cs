﻿using System.Diagnostics;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Azure.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Publishers
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
                var publisher = await _ctx.Publishers.FindAsync(new object[] { req.Id }, cancellationToken: cancellationToken);

                if (publisher == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);


                publisher.Pb_Modifier = user.Id;
                publisher.Pb_ModifyOn = DateTime.UtcNow;
                publisher.Pb_Active = false;

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failed to deactivate Publisher");
                return Result<Unit>.Success(Unit.Value); 
            }
        }
    }
}