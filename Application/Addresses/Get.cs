using Application.Addresses.Core;
using Application.Core;
using Application.Interfaces;
using Application.Media.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Addresses
{
    public class Get
    {
        public class Query : IRequest<Result<AddressDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<AddressDto>>
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

            public async Task<Result<AddressDto>> Handle(Query req, CancellationToken cancellationToken)
            {
                var address = await _ctx.Addresses
                    .ProjectTo<AddressDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .FirstOrDefaultAsync(s => s.Ad_ID == req.Id, cancellationToken: cancellationToken);

                return Result<AddressDto>.Success(address);
            }
        }
    }
}

