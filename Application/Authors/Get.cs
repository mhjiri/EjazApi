﻿using Application.Authors.Core;
using Application.Core;
using Application.Interfaces;
using Application.Media.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Authors
{
    public class Get
    {
        public class Query : IRequest<Result<AuthorQryDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<AuthorQryDto>>
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

            public async Task<Result<AuthorQryDto>> Handle(Query req, CancellationToken cancellationToken)
            {
                var author = await _ctx.Authors
                    .ProjectTo<AuthorQryDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .FirstOrDefaultAsync(s => s.At_ID == req.Id, cancellationToken: cancellationToken);

                return Result<AuthorQryDto>.Success(author);
            }
        }
    }
}
