using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books
{
    public class BookViews
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid BookId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _ctx;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext ctx, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _ctx = ctx;
            }

            public async Task<Result<Unit>> Handle(Command req, CancellationToken cancellationToken)
            {
                var book = await _ctx.Books.FindAsync(new object[] { req.BookId }, cancellationToken: cancellationToken);

                if (book == null) return null;

                book.Bk_ViewCount++;
                book.Bk_LastViewedOn = DateTime.Now;

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failed to update Book viewed status.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
