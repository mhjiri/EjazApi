using Application.ThematicAreas.Core;
using Application.Core;
using Application.Interfaces;
using Application.Media.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ThematicAreas
{
    public class Get
    {
        public class Query : IRequest<Result<ThematicAreaDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ThematicAreaDto>>
        {
            private readonly DataContext _ctx;
            private readonly IMapper _mpr;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext ctx, IMapper mpr, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _mpr = mpr;
                _ctx = ctx;
            }

            public async Task<Result<ThematicAreaDto>> Handle(Query req, CancellationToken cancellationToken)
            {
                var thematicArea = await _ctx.ThematicAreas
                    .ProjectTo<ThematicAreaDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .FirstOrDefaultAsync(s => s.Th_ID == req.Id, cancellationToken: cancellationToken);

                return Result<ThematicAreaDto>.Success(thematicArea);
            }
        }
    }
}

