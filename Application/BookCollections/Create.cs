using System.Diagnostics;
using Application.Core;
using Application.BookCollections.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Security.Policy;

namespace Application.BookCollections
{
    public class Create
    {
        public class Command : IRequest<Result<BookCollectionQryDto>>
        {
            public BookCollectionCmdDto BookCollection { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<BookCollectionQryDto>>
        {
            private readonly DataContext _ctx;
            private readonly IMapper _mpr;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext ctx, IMapper mpr, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _ctx = ctx;
                _mpr = mpr;
            }

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.BookCollection).SetValidator(new BookCollectionCmdValidator());
                }
            }

            public async Task<Result<BookCollectionQryDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                BookCollection bookCollection = _mpr.Map<BookCollection>(req.BookCollection);
                bookCollection.Bc_Creator = user.Id;
                bookCollection.Books = new List<BookBookCollection>();
                if (req.BookCollection.BookItems != null) {
                    foreach (ItemDto book in req.BookCollection.BookItems)
                    {
                        var bookBookCollection = new BookBookCollection
                        {
                            Bk_ID = book.It_ID,
                            BkBc_Ordinal = book.It_Ordinal,
                            BkBc_Active = true,
                            BkBc_Creator = user.Id
                        };
                        bookCollection.Books.Add(bookBookCollection);
                    }
                }


                _ctx.BookCollections.Add(bookCollection);

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<BookCollectionQryDto>.Failure("Failed to create Book Collection");
                bookCollection = await _ctx.BookCollections.Include(i => i.Books).ThenInclude(j => j.Book).Where(s => s.Bc_ID == bookCollection.Bc_ID).FirstOrDefaultAsync(cancellationToken: cancellationToken);
                return Result<BookCollectionQryDto>.Success(_mpr.Map<BookCollectionQryDto>(bookCollection)); 
            }
        }
    }
}