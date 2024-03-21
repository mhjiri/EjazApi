﻿using System.Diagnostics;
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

namespace Application.Banners
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
                var banner = await _ctx.Banners.FindAsync(new object[] { req.Id }, cancellationToken: cancellationToken);

                if (banner == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);


                banner.Bn_Modifier = user.Id;
                banner.Bn_ModifyOn = DateTime.UtcNow;
                banner.Bn_Active = true;

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failed to activate Banner");
                return Result<Unit>.Success(Unit.Value); 
            }
        }
    }
}