using System.Diagnostics;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Azure.Core;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Books
{
    public class DeactivateBatch
    {
        public class Command : IRequest<Result<Unit>>
        {
            public ListGuidDto Ids { get; set; }
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
                var books = _ctx.Books.Where(s => req.Ids.Ids.Contains(s.Bk_ID));

                if (books == null || !books.Any()) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                foreach (var book in books)
                {
                    book.Bk_Modifier = user.Id;
                    book.Bk_ModifyOn = DateTime.UtcNow;
                    book.Bk_Active = false;
                }

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<Unit>.Failure("Failed to deactivate Books");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}