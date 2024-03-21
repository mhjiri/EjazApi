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

namespace Application.PaymentMethods
{
    public class GetCmd
    {
        public class Query : IRequest<Result<PaymentMethodCmdDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PaymentMethodCmdDto>>
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

            public async Task<Result<PaymentMethodCmdDto>> Handle(Query req, CancellationToken cancellationToken)
            {
                var author = await _ctx.PaymentMethods
                    .ProjectTo<PaymentMethodCmdDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .FirstOrDefaultAsync(s => s.Py_ID == req.Id, cancellationToken: cancellationToken);

                return Result<PaymentMethodCmdDto>.Success(author);
            }
        }
    }
}

