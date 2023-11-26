using Application.Core;
using Application.Interfaces;
using Application.Media.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Media
{
    public class Get
    {
        public class Query : IRequest<Result<MediumDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<MediumDto>>
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

            public async Task<Result<MediumDto>> Handle(Query req, CancellationToken cancellationToken)
            {
                var medium = await _ctx.Media
                    .ProjectTo<MediumDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .FirstOrDefaultAsync(s => s.Md_ID == req.Id, cancellationToken: cancellationToken);

                return Result<MediumDto>.Success(medium);
            }
        }
    }
}

