using Application.Banners.Core;
using Application.Core;
using Application.Interfaces;
using Application.Media.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Banners
{
    public class GetCmd
    {
        public class Query : IRequest<Result<BannerCmdDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<BannerCmdDto>>
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

            public async Task<Result<BannerCmdDto>> Handle(Query req, CancellationToken cancellationToken)
            {
                var banner = await _ctx.Banners
                    .ProjectTo<BannerCmdDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .FirstOrDefaultAsync(s => s.Bn_ID == req.Id, cancellationToken: cancellationToken);

                return Result<BannerCmdDto>.Success(banner);
            }
        }
    }
}

