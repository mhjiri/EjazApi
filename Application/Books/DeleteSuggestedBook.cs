using Application.Books.Core;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
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
    public class DeleteSuggestedBook
    {


        public class Command : IRequest<Result<Unit>>
        {
            public Guid BookId { get; set; }
        }

        public class DeleteHandler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _ctx;
            private readonly IUserAccessor _userAccessor;

            public DeleteHandler(DataContext ctx, IUserAccessor userAccessor)
            {
                _ctx = ctx;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var book = await _ctx.SugggestBook.FindAsync(request.BookId);

                if (book == null)
                {
                    return Result<Unit>.Failure("Book not found");
                }

                _ctx.SugggestBook.Remove(book);

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                {
                    return Result<Unit>.Failure("Failed to delete the book");
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
