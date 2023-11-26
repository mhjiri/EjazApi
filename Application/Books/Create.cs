using System.Diagnostics;
using Application.Core;
using Application.Books.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Security.Policy;
using Application.Publishers.Core;
using AutoMapper.QueryableExtensions;

namespace Application.Books
{
    public class Create
    {
        public class Command : IRequest<Result<BookQryDto>>
        {
            public BookCmdDto Book { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<BookQryDto>>
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
                    RuleFor(x => x.Book).SetValidator(new BookCmdValidator());
                }
            }

            public async Task<Result<BookQryDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                Book book = _mpr.Map<Book>(req.Book);
                book.Bk_Creator = user.Id;

                book.Genres = new List<BookGenre>();
                if (req.Book.GenreItems != null) {
                    foreach (ItemDto genre in req.Book.GenreItems)
                    {
                        var bookGenre = new BookGenre
                        {
                            Gn_ID = genre.It_ID,
                            BkGn_Ordinal = genre.It_Ordinal,
                            BkGn_Active = true,
                            BkGn_Creator = user.Id
                        };
                        book.Genres.Add(bookGenre);
                    }
                }

                book.Categories = new List<BookCategory>();
                if (req.Book.CategoryItems != null) {
                    foreach (ItemDto catgory in req.Book.CategoryItems)
                    {
                        var bookCategory = new BookCategory
                        {
                            Ct_ID = catgory.It_ID,
                            BkCt_Ordinal = catgory.It_Ordinal,
                            BkCt_Active = true,
                            BkCt_Creator = user.Id
                        };
                        book.Categories.Add(bookCategory);
                    }
                }

                book.ThematicAreas = new List<BookThematicArea>();
                if (req.Book.ThematicAreaItems != null) {
                    foreach (ItemDto thematicArea in req.Book.ThematicAreaItems)
                    {
                        var bookThematicArea = new BookThematicArea
                        {
                            Th_ID = thematicArea.It_ID,
                            BkTh_Ordinal = thematicArea.It_Ordinal,
                            BkTh_Active = true,
                            BkTh_Creator = user.Id
                        };
                        book.ThematicAreas.Add(bookThematicArea);
                    }
                }

                book.Tags = new List<BookTag>();
                if(req.Book.TagItems != null)
                {
                    foreach (ItemDto tag in req.Book.TagItems)
                    {
                        var bookTag = new BookTag
                        {
                            Tg_ID = tag.It_ID,
                            BkTg_Ordinal = tag.It_Ordinal,
                            BkTg_Active = true,
                            BkTg_Creator = user.Id
                        };
                        book.Tags.Add(bookTag);
                    }
                }
                

                book.Authors = new List<BookAuthor>();
                if(req.Book.AuthorItems != null)
                {
                    foreach (ItemDto author in req.Book.AuthorItems)
                    {
                        var bookAuthor = new BookAuthor
                        {
                            At_ID = author.It_ID,
                            BkAt_Ordinal = author.It_Ordinal,
                            BkAt_Active = true,
                            BkAt_Creator = user.Id
                        };
                        book.Authors.Add(bookAuthor);
                    }
                }
                

                book.Publishers = new List<BookPublisher>();
                if(req.Book.PublisherItems != null)
                {
                    foreach (ItemDto publisher in req.Book.PublisherItems)
                    {
                        var bookPublisher = new BookPublisher
                        {
                            Pb_ID = publisher.It_ID,
                            BkPb_Ordinal = publisher.It_Ordinal,
                            BkPb_Active = true,
                            BkPb_Creator = user.Id
                        };
                        book.Publishers.Add(bookPublisher);
                    }
                }
                

                _ctx.Books.Add(book);

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<BookQryDto>.Failure("Failed to create Book");


                if (req.Book.MediumItems != null) {
                    foreach (ItemDto bookMedium in req.Book.MediumItems)
                    {
                        var medium = await _ctx.Media
                        .FirstOrDefaultAsync(s => s.Md_ID == bookMedium.It_ID, cancellationToken: cancellationToken);
                        medium.Bk_ID = book.Bk_ID;
                        result = await _ctx.SaveChangesAsync(cancellationToken) > 0;
                    }
                }

                book = await _ctx.Books
                    .Include(i => i.Genres).ThenInclude(j => j.Genre)
                    .Include(i => i.Categories).ThenInclude(j => j.Category)
                    .Include(i => i.ThematicAreas).ThenInclude(j => j.ThematicArea)
                    .Include(i => i.Tags).ThenInclude(j => j.Tag)
                    .Include(i => i.Authors).ThenInclude(j => j.Author)
                    .Include(i => i.Publishers).ThenInclude(j => j.Publisher)
                    .Include(i => i.Media)
                    .Include(i => i.Creator)
                    .Where(s => s.Bk_ID == book.Bk_ID).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                return Result<BookQryDto>.Success(_mpr.Map<BookQryDto>(book)); 
            }
        }
    }
}