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
    public class Get
    {
        public class Query : IRequest<Result<PaymentDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PaymentDto>>
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

            public async Task<Result<PaymentDto>> Handle(Query req, CancellationToken cancellationToken)
            {
                var payment = await _ctx.Payments
                    .ProjectTo<PaymentDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .FirstOrDefaultAsync(s => s.Pm_ID == req.Id, cancellationToken: cancellationToken);

                return Result<PaymentDto>.Success(payment);
            }
        }
    }
}

