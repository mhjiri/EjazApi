using Application.Books.Core;
using Application.Core;
using Application.Interfaces;
using Application.Media.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Books
{
    public class GetSuggestedBook
    {
        public class Query : IRequest<Result<SuggestBookQryDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<SuggestBookQryDto>>
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

            public async Task<Result<SuggestBookQryDto>> Handle(Query req, CancellationToken cancellationToken)
            {
                var book = await _ctx.SugggestBook
                           .FirstOrDefaultAsync(s => s.Bk_ID == req.Id, cancellationToken: cancellationToken);

                if (book == null)
                    return Result<SuggestBookQryDto>.Failure("Book not found.");

                return Result<SuggestBookQryDto>.Success(_mpr.Map<SuggestBookQryDto>(book));
            }
        
        }
    }
}
