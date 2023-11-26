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
    public class ActivateBatch
    {
        public class Command : IRequest<Result<Unit>>
        {
            public ListGuidDto Ids { get; set; }
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
                var thematicAreas = _ctx.ThematicAreas.Where(s => req.Ids.Ids.Contains(s.Th_ID));

                if (thematicAreas == null || !thematicAreas.Any()) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                foreach(var thematicArea in thematicAreas)
                {
                    thematicArea.Th_Modifier = user.Id;
                    thematicArea.Th_ModifyOn = DateTime.UtcNow;
                    thematicArea.Th_Active = true;
                }

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failed to activate ThematicAreas");
                return Result<Unit>.Success(Unit.Value); 
            }
        }
    }
}