using Application.BookCollections.Core;
using Application.Core;
using Application.Interfaces;
using Application.Media.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.BookCollections
{
    public class Get
    {
        public class Query : IRequest<Result<BookCollectionQryDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<BookCollectionQryDto>>
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

            public async Task<Result<BookCollectionQryDto>> Handle(Query req, CancellationToken cancellationToken)
            {
                var bookCollection = await _ctx.BookCollections
                    .ProjectTo<BookCollectionQryDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .FirstOrDefaultAsync(s => s.Bc_ID == req.Id, cancellationToken: cancellationToken);

                return Result<BookCollectionQryDto>.Success(bookCollection);
            }
        }
    }
}

