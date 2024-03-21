using System.Diagnostics;
using Application.Core;
using Application.Interfaces;
using Application.ThematicAreas.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ThematicAreas
{
    public class Create
    {
        public class Command : IRequest<Result<ThematicAreaDto>>
        {
            public ThematicAreaCmdDto ThematicArea { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<ThematicAreaDto>>
        {
            private readonly DataContext _ctx;
            private readonly IMapper _mpr;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext ctx, IMapper mpr, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _ctx = ctx;
                _mpr = mpr;
            }

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.ThematicArea).SetValidator(new ThematicAreaCmdValidator());
                }
            }

            public async Task<Result<ThematicAreaDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                var thematicArea = _mpr.Map<ThematicArea>(req.ThematicArea);
                thematicArea.Th_Creator = user.Id;

                _ctx.ThematicAreas.Add(thematicArea);

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<ThematicAreaDto>.Failure("Failed to create ThematicArea");
                return Result<ThematicAreaDto>.Success(_mpr.Map<ThematicAreaDto>(thematicArea)); //Result<ThematicAreaDto>.Success(req.ThematicArea);
            }
        }
    }
}