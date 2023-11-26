using Application.Profiles.Core;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles
{
    public class Get
    {
        public class Query : IRequest<Result<ProfileDto>>
        {
            
        }

        public class Handler : IRequestHandler<Query, Result<ProfileDto>>
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

            public async Task<Result<ProfileDto>> Handle(Query req, CancellationToken cancellationToken)
            {
                var profile = await _ctx.Users
                    .ProjectTo<ProfileDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .SingleOrDefaultAsync(s => s.Username == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                return Result<ProfileDto>.Success(profile);
            }
        }
    }
}

