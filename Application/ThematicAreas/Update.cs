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
    public class Update
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
                var thematicArea = await _ctx.ThematicAreas.Include(i => i.Creator).Where(s => s.Th_ID == req.ThematicArea.Th_ID).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (thematicArea == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted);

                _mpr.Map(req.ThematicArea, thematicArea);

                thematicArea.Th_Modifier = user.Id;
                thematicArea.Th_ModifyOn = DateTime.UtcNow;

                
                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<ThematicAreaDto>.Failure("Failed to update ThematicArea");
                return Result<ThematicAreaDto>.Success(_mpr.Map<ThematicAreaDto>(thematicArea)); //Result<ThematicAreaDto>.Success(req.ThematicArea);
            }
        }
    }
}