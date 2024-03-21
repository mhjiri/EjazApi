using Application.Publishers.Core;
using Application.Core;
using Application.Interfaces;
using Application.Media.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Publishers
{
    public class Get
    {
        public class Query : IRequest<Result<PublisherQryDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PublisherQryDto>>
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

            public async Task<Result<PublisherQryDto>> Handle(Query req, CancellationToken cancellationToken)
            {
                var publisher = await _ctx.Publishers
                    .ProjectTo<PublisherQryDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .FirstOrDefaultAsync(s => s.Pb_ID == req.Id, cancellationToken: cancellationToken);

                return Result<PublisherQryDto>.Success(publisher);
            }
        }
    }
}

