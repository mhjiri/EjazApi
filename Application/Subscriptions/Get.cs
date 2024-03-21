﻿using Application.Subscriptions.Core;
using Application.Core;
using Application.Interfaces;
using Application.Media.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Subscriptions
{
    public class Get
    {
        public class Query : IRequest<Result<SubscriptionQryDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<SubscriptionQryDto>>
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

            public async Task<Result<SubscriptionQryDto>> Handle(Query req, CancellationToken cancellationToken)
            {
                var subscription = await _ctx.Subscriptions
                    .ProjectTo<SubscriptionQryDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .FirstOrDefaultAsync(s => s.Sb_ID == req.Id, cancellationToken: cancellationToken);

                return Result<SubscriptionQryDto>.Success(subscription);
            }
        }
    }
}

