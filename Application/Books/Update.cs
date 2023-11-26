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

namespace Application.Books
{
    public class Update
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
                var book = await _ctx.Books
                    .Include(i => i.Genres).ThenInclude(j => j.Genre)
                    .Include(i => i.Categories).ThenInclude(j => j.Category)
                    .Include(i => i.ThematicAreas).ThenInclude(j => j.ThematicArea)
                    .Include(i => i.Tags).ThenInclude(j => j.Tag)
                    .Include(i => i.Authors).ThenInclude(j => j.Author)
                    .Include(i => i.Publishers).ThenInclude(j => j.Publisher)
                    .Include(i => i.Media)
                    .Include(i => i.Creator)
                    .Where(s => s.Bk_ID == req.Book.Bk_ID).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (book == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                DateTime dateTime = DateTime.UtcNow;
                
                _mpr.Map(req.Book, book);

                book.Bk_Modifier = user.Id;
                book.Bk_ModifyOn = dateTime;


                //Book Genre
                if (req.Book.GenreItems != null) {
                    foreach (var bookGenre in book.Genres.Where(s => !req.Book.GenreItems.Select(z => z.It_ID).Contains(s.Gn_ID)))
                    {
                        bookGenre.BkGn_Active = false;
                        bookGenre.BkGn_Modifier = user.Id;
                        bookGenre.BkGn_ModifyOn = dateTime;
                    }

                    foreach (ItemDto item in req.Book.GenreItems)
                    {
                        var genre = book.Genres.Where(s => s.Gn_ID == item.It_ID).FirstOrDefault();
                        if (genre == null)
                        {
                            var bookGenre = new BookGenre
                            {
                                Gn_ID = item.It_ID,
                                BkGn_Ordinal = item.It_Ordinal,
                                BkGn_Active = true,
                                BkGn_Creator = user.Id,
                                BkGn_CreatedOn = dateTime
                            };
                            book.Genres.Add(bookGenre);
                        } else
                        {
                            genre.BkGn_Ordinal = item.It_Ordinal;
                            if (!genre.BkGn_Active) genre.BkGn_Active = true;
                            genre.BkGn_Modifier = user.Id;
                            genre.BkGn_ModifyOn = dateTime;
                        }

                    }
                } else
                {
                    foreach (var bookGenre in book.Genres)
                    {
                        bookGenre.BkGn_Active = false;
                        bookGenre.BkGn_Modifier = user.Id;
                        bookGenre.BkGn_ModifyOn = dateTime;
                    }
                }

                //Category
                if (req.Book.CategoryItems != null) {
                    foreach (var bookCategory in book.Categories.Where(s => !req.Book.CategoryItems.Select(z => z.It_ID).Contains(s.Ct_ID)))
                    {
                        bookCategory.BkCt_Active = false;
                        bookCategory.BkCt_Modifier = user.Id;
                        bookCategory.BkCt_ModifyOn = dateTime;
                    }

                    foreach (ItemDto item in req.Book.CategoryItems)
                    {
                        var category = book.Categories.Where(s => s.Ct_ID == item.It_ID).FirstOrDefault();
                        if (category == null)
                        {
                            var bookCategory = new BookCategory
                            {
                                Ct_ID = item.It_ID,
                                BkCt_Ordinal = item.It_Ordinal,
                                BkCt_Active = true,
                                BkCt_Creator = user.Id,
                                BkCt_CreatedOn = dateTime
                            };
                            book.Categories.Add(bookCategory);
                        }
                        else
                        {
                            category.BkCt_Ordinal = item.It_Ordinal;
                            if (!category.BkCt_Active) category.BkCt_Active = true;
                            category.BkCt_Modifier = user.Id;
                            category.BkCt_ModifyOn = dateTime;
                        }

                    }
                } else
                {
                    foreach (var bookCategory in book.Categories)
                    {
                        bookCategory.BkCt_Active = false;
                        bookCategory.BkCt_Modifier = user.Id;
                        bookCategory.BkCt_ModifyOn = dateTime;
                    }
                }

                //ThematicArea
                if (req.Book.ThematicAreaItems != null) {
                    foreach (var bookThematicArea in book.ThematicAreas.Where(s => !req.Book.ThematicAreaItems.Select(z => z.It_ID).Contains(s.Th_ID)))
                    {
                        bookThematicArea.BkTh_Active = false;
                        bookThematicArea.BkTh_Modifier = user.Id;
                        bookThematicArea.BkTh_ModifyOn = dateTime;
                    }

                    foreach (ItemDto item in req.Book.ThematicAreaItems)
                    {
                        var thematicArea = book.ThematicAreas.Where(s => s.Th_ID == item.It_ID).FirstOrDefault();
                        if (thematicArea == null)
                        {
                            var bookThematicArea = new BookThematicArea
                            {
                                Th_ID = item.It_ID,
                                BkTh_Ordinal = item.It_Ordinal,
                                BkTh_Active = true,
                                BkTh_Creator = user.Id,
                                BkTh_CreatedOn = dateTime
                            };
                            book.ThematicAreas.Add(bookThematicArea);
                        }
                        else
                        {
                            thematicArea.BkTh_Ordinal = item.It_Ordinal;
                            if (!thematicArea.BkTh_Active) thematicArea.BkTh_Active = true;
                            thematicArea.BkTh_Modifier = user.Id;
                            thematicArea.BkTh_ModifyOn = dateTime;
                        }

                    }
                } else
                {
                    foreach (var bookThematicArea in book.ThematicAreas)
                    {
                        bookThematicArea.BkTh_Active = false;
                        bookThematicArea.BkTh_Modifier = user.Id;
                        bookThematicArea.BkTh_ModifyOn = dateTime;
                    }
                }

                //Tag
                if(req.Book.TagItems != null)
                {
                    foreach (var bookTag in book.Tags.Where(s => !req.Book.TagItems.Select(z => z.It_ID).Contains(s.Tg_ID)))
                    {
                        bookTag.BkTg_Active = false;
                        bookTag.BkTg_Modifier = user.Id;
                        bookTag.BkTg_ModifyOn = dateTime;
                    }

                    foreach (ItemDto item in req.Book.TagItems)
                    {
                        var tag = book.Tags.Where(s => s.Tg_ID == item.It_ID).FirstOrDefault();
                        if (tag == null)
                        {
                            var bookTag = new BookTag
                            {
                                Tg_ID = item.It_ID,
                                BkTg_Ordinal = item.It_Ordinal,
                                BkTg_Active = true,
                                BkTg_Creator = user.Id,
                                BkTg_CreatedOn = dateTime
                            };
                            book.Tags.Add(bookTag);
                        }
                        else
                        {
                            tag.BkTg_Ordinal = item.It_Ordinal;
                            if (!tag.BkTg_Active) tag.BkTg_Active = true;
                            tag.BkTg_Modifier = user.Id;
                            tag.BkTg_ModifyOn = dateTime;
                        }

                    }
                } else
                {
                    foreach (var bookTag in book.Tags)
                    {
                        bookTag.BkTg_Active = false;
                        bookTag.BkTg_Modifier = user.Id;
                        bookTag.BkTg_ModifyOn = dateTime;
                    }
                }
                

                //Publisher
                if(req.Book.PublisherItems != null)
                {
                    foreach (var bookPublisher in book.Publishers.Where(s => !req.Book.PublisherItems.Select(z => z.It_ID).Contains(s.Pb_ID)))
                    {
                        bookPublisher.BkPb_Active = false;
                        bookPublisher.BkPb_Modifier = user.Id;
                        bookPublisher.BkPb_ModifyOn = dateTime;
                    }

                    foreach (ItemDto item in req.Book.PublisherItems)
                    {
                        var publisher = book.Publishers.Where(s => s.Pb_ID == item.It_ID).FirstOrDefault();
                        if (publisher == null)
                        {
                            var bookPublisher = new BookPublisher
                            {
                                Pb_ID = item.It_ID,
                                BkPb_Ordinal = item.It_Ordinal,
                                BkPb_Active = true,
                                BkPb_Creator = user.Id,
                                BkPb_CreatedOn = dateTime
                            };
                            book.Publishers.Add(bookPublisher);
                        }
                        else
                        {
                            publisher.BkPb_Ordinal = item.It_Ordinal;
                            if (!publisher.BkPb_Active) publisher.BkPb_Active = true;
                            publisher.BkPb_Modifier = user.Id;
                            publisher.BkPb_ModifyOn = dateTime;
                        }

                    }
                } else
                {

                    foreach (var bookPublisher in book.Publishers)
                    {
                        bookPublisher.BkPb_Active = false;
                        bookPublisher.BkPb_Modifier = user.Id;
                        bookPublisher.BkPb_ModifyOn = dateTime;
                    }
                }
                

                //Author
                if(req.Book.AuthorItems != null)
                {
                    foreach (var bookAuthor in book.Authors.Where(s => !req.Book.AuthorItems.Select(z => z.It_ID).Contains(s.At_ID)))
                    {
                        bookAuthor.BkAt_Active = false;
                        bookAuthor.BkAt_Modifier = user.Id;
                        bookAuthor.BkAt_ModifyOn = dateTime;
                    }

                    foreach (ItemDto item in req.Book.AuthorItems)
                    {
                        var author = book.Authors.Where(s => s.At_ID == item.It_ID).FirstOrDefault();
                        if (author == null)
                        {
                            var bookAuthor = new BookAuthor
                            {
                                At_ID = item.It_ID,
                                BkAt_Ordinal = item.It_Ordinal,
                                BkAt_Active = true,
                                BkAt_Creator = user.Id,
                                BkAt_CreatedOn = dateTime
                            };
                            book.Authors.Add(bookAuthor);
                        }
                        else
                        {
                            author.BkAt_Ordinal = item.It_Ordinal;
                            if (!author.BkAt_Active) author.BkAt_Active = true;
                            author.BkAt_Modifier = user.Id;
                            author.BkAt_ModifyOn = dateTime;
                        }

                    }
                } else
                {
                    foreach (var bookAuthor in book.Authors)
                    {
                        bookAuthor.BkAt_Active = false;
                        bookAuthor.BkAt_Modifier = user.Id;
                        bookAuthor.BkAt_ModifyOn = dateTime;
                    }
                }
                

                

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;
                if (!result) return Result<BookQryDto>.Failure("Failed to update Book");

                //Book Genre
                if (req.Book.MediumItems != null)
                {
                    foreach (var bookMedia in book.Media.Where(s => !req.Book.MediumItems.Select(z => z.It_ID).Contains(s.Md_ID)))
                    {
                        bookMedia.Bk_ID = null;
                        result = await _ctx.SaveChangesAsync(cancellationToken) > 0;
                    }

                    foreach (ItemDto item in req.Book.MediumItems)
                    {
                        var medium = _ctx.Media.Where(s => s.Md_ID == item.It_ID).FirstOrDefault();
                        if (medium != null)
                        {
                            medium.Bk_ID = book.Bk_ID;
                            result = await _ctx.SaveChangesAsync(cancellationToken) > 0;
                        }

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