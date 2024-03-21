using Application.PaymentMethods.Core;
using Application.Core;
using Application.Interfaces;
using Application.Media.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Application.PaymentMehods.Core;

namespace Application.PaymentMethods
{
    public class Get
    {
        public class Query : IRequest<Result<PaymentMethodQryDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PaymentMethodQryDto>>
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

            public async Task<Result<PaymentMethodQryDto>> Handle(Query req, CancellationToken cancellationToken)
            {
                var paymentMethod = await _ctx.PaymentMethods
                    .ProjectTo<PaymentMethodQryDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .FirstOrDefaultAsync(s => s.Py_ID == req.Id, cancellationToken: cancellationToken);

                return Result<PaymentMethodQryDto>.Success(paymentMethod);
            }
        }
    }
}

