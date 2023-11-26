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

namespace Application.BookCollections
{
    public class Update
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
                var bookCollection = await _ctx.BookCollections.Include(i => i.Books).ThenInclude(j => j.Book).Include(i => i.Creator).Where(s => s.Bc_ID == req.BookCollection.Bc_ID).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (bookCollection == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                DateTime dateTime = DateTime.UtcNow;

                _mpr.Map(req.BookCollection, bookCollection);

                bookCollection.Bc_Modifier = user.Id;
                bookCollection.Bc_ModifyOn = dateTime;




                //BookCollection Book
                if (req.BookCollection.Books != null)
                {
                    foreach (var bookBookCollection in bookCollection.Books.Where(s => !req.BookCollection.BookItems.Select(z => z.It_ID).Contains(s.Bk_ID)))
                    {
                        bookBookCollection.BkBc_Active = false;
                        bookBookCollection.BkBc_Modifier = user.Id;
                        bookBookCollection.BkBc_ModifyOn = dateTime;
                    }

                    foreach (ItemDto item in req.BookCollection.BookItems)
                    {
                        var book = bookCollection.Books.Where(s => s.Bk_ID == item.It_ID).FirstOrDefault();
                        if (book == null)
                        {
                            var bookBookCollection = new BookBookCollection
                            {
                                Bk_ID = item.It_ID,
                                BkBc_Ordinal = item.It_Ordinal,
                                BkBc_Active = true,
                                BkBc_Creator = user.Id,
                                BkBc_CreatedOn = dateTime
                            };
                            bookCollection.Books.Add(bookBookCollection);
                        }
                        else
                        {
                            book.BkBc_Ordinal = item.It_Ordinal;
                            if (!book.BkBc_Active) book.BkBc_Active = true;
                            book.BkBc_Modifier = user.Id;
                            book.BkBc_ModifyOn = dateTime;
                        }

                    }
                } else
                {
                    foreach (var bookBookCollection in bookCollection.Books)
                    {
                        bookBookCollection.BkBc_Active = false;
                        bookBookCollection.BkBc_Modifier = user.Id;
                        bookBookCollection.BkBc_ModifyOn = dateTime;
                    }
                }

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<BookCollectionQryDto>.Failure("Failed to update Book Collection");
                return Result<BookCollectionQryDto>.Success(_mpr.Map<BookCollectionQryDto>(bookCollection));
            }
        }
    }
}