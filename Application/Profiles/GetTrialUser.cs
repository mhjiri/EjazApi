using Application.Profiles.Core;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Application.Books.Core;

namespace Application.Profiles
{
    public class GetTrialUser
    {
        public class Query : IRequest<Result<ProfileQryDto>>
        {
            public string Username { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ProfileQryDto>>
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

            public async Task<Result<ProfileQryDto>> Handle(Query req, CancellationToken cancellationToken)
            {
                var profile = await _ctx.Users
                    .Include(i => i.Genres).ThenInclude(j => j.Genre)
                    .Include(i => i.Categories).ThenInclude(j => j.Category)
                    .Include(i => i.ThematicAreas).ThenInclude(j => j.ThematicArea)
                    .Include(i => i.Tags).ThenInclude(j => j.Tag)
                    .Include(i => i.Creator)
                    //.ProjectTo<ProfileQryDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .SingleOrDefaultAsync(s => s.UserName == req.Username && s.Us_Customer == true && !s.Us_Deleted && (s.Us_SubscriptionExpiryDate == null || s.Us_SubscriptionExpiryDate < DateTime.Now), cancellationToken: cancellationToken);

                return Result<ProfileQryDto>.Success(_mpr.Map<ProfileQryDto>(profile));
                //return Result<ProfileQryDto>.Success(profile);
            }
        }
    }
}

