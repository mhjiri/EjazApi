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
    public class GetImage
    {
        public class Query : IRequest<Result<Medium>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Medium>>
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

            public async Task<Result<Medium>> Handle(Query req, CancellationToken cancellationToken)
            {
                var medium = await _ctx.Media
                    .FirstOrDefaultAsync(s => s.Md_ID == req.Id, cancellationToken: cancellationToken);

                return Result<Medium>.Success(medium);
            }
        }
    }
}

