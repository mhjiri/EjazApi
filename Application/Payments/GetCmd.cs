using Application.Payments.Core;
using Application.Core;
using Application.Interfaces;
using Application.Media.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Payments
{
    public class GetCmd
    {
        public class Query : IRequest<Result<PaymentCmdDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PaymentCmdDto>>
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

            public async Task<Result<PaymentCmdDto>> Handle(Query req, CancellationToken cancellationToken)
            {
                var author = await _ctx.Payments
                    .ProjectTo<PaymentCmdDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .FirstOrDefaultAsync(s => s.Pm_ID == req.Id, cancellationToken: cancellationToken);

                return Result<PaymentCmdDto>.Success(author);
            }
        }
    }
}

