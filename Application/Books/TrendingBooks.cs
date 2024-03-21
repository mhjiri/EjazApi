using Application.Core;
using Application.Interfaces;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books
{
    public class TrendingBooks
    {
        public class Command : IRequest<Result<List<Guid>>> {  }

        public class Handler : IRequestHandler<Command, Result<List<Guid>>>
        {
            private readonly DataContext _ctx;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext ctx, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _ctx = ctx;
            }

            public async Task<Result<List<Guid>>> Handle(Command req, CancellationToken cancellationToken)
            {
                var books = _ctx.Books.AsQueryable()
                    .Where(b => b.Bk_ViewCount > 0)
                    .OrderByDescending(b => b.Bk_ViewCount)
                    .Select(s => s.Bk_ID).ToList();

                if (books == null || books?.Count == 0) return Result<List<Guid>>.Failure("No Trending books available."); 

                return Result<List<Guid>>.Success(books);
            }
        }
    }
}
