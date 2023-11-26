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

namespace Application.ThematicAreas
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
                var thematicArea = await _ctx.ThematicAreas.FindAsync(new object[] { req.Id }, cancellationToken: cancellationToken);

                if (thematicArea == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted);


                thematicArea.Th_Modifier = user.Id;
                thematicArea.Th_ModifyOn = DateTime.UtcNow;
                thematicArea.Th_Active = false;

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failed to deactivate ThematicArea");
                return Result<Unit>.Success(Unit.Value); //Result<ThematicAreaDto>.Success(req.ThematicArea);
            }
        }
    }
}