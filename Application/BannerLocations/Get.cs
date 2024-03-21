using Application.BannerLocations.Core;
using Application.Core;
using Application.Interfaces;
using Application.Media.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.BannerLocations
{
    public class Get
    {
        public class Query : IRequest<Result<BannerLocationDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<BannerLocationDto>>
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

            public async Task<Result<BannerLocationDto>> Handle(Query req, CancellationToken cancellationToken)
            {
                var bannerLocation = await _ctx.BannerLocations
                    .ProjectTo<BannerLocationDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .FirstOrDefaultAsync(s => s.Bl_ID == req.Id, cancellationToken: cancellationToken);

                return Result<BannerLocationDto>.Success(bannerLocation);
            }
        }
    }
}

