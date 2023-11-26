using System.Diagnostics;   
using Application.Core;
using Application.Authors.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Authors
{
    public class Update
    {
        public class Command : IRequest<Result<AuthorDto>>
        {
            public AuthorCmdDto Author { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<AuthorDto>>
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
                    RuleFor(x => x.Author).SetValidator(new AuthorCmdValidator());
                }
            }

            public async Task<Result<AuthorDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var author = await _ctx.Authors.Include(i => i.Genres).ThenInclude(j => j.Genre).Include(i => i.Creator).Where(s => s.At_ID == req.Author.At_ID).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (author == null) return null;

                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                DateTime dateTime = DateTime.UtcNow;

                _mpr.Map(req.Author, author);

                author.At_Modifier = user.Id;
                author.At_ModifyOn = dateTime;




                //Author Genre
                if (req.Author.Genres != null)
                {
                    foreach (var authorGenre in author.Genres.Where(s => !req.Author.GenreItems.Select(z => z.It_ID).Contains(s.Gn_ID)))
                    {
                        authorGenre.AtGn_Active = false;
                        authorGenre.AtGn_Modifier = user.Id;
                        authorGenre.AtGn_ModifiedOn = dateTime;
                    }

                    foreach (ItemDto item in req.Author.GenreItems)
                    {
                        var genre = author.Genres.Where(s => s.Gn_ID == item.It_ID).FirstOrDefault();
                        if (genre == null)
                        {
                            var authorGenre = new AuthorGenre
                            {
                                Gn_ID = item.It_ID,
                                AtGn_Ordinal = item.It_Ordinal,
                                AtGn_Active = true,
                                AtGn_Creator = user.Id,
                                AtGn_CreatedOn = dateTime
                            };
                            author.Genres.Add(authorGenre);
                        }
                        else
                        {
                            genre.AtGn_Ordinal = item.It_Ordinal;
                            if (!genre.AtGn_Active) genre.AtGn_Active = true;
                            genre.AtGn_Modifier = user.Id;
                            genre.AtGn_ModifiedOn = dateTime;
                        }

                    }
                } else
                {
                    foreach (var authorGenre in author.Genres)
                    {
                        authorGenre.AtGn_Active = false;
                        authorGenre.AtGn_Modifier = user.Id;
                        authorGenre.AtGn_ModifiedOn = dateTime;
                    }
                }

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<AuthorDto>.Failure("Failed to update Author");
                return Result<AuthorDto>.Success(_mpr.Map<AuthorDto>(author));
            }
        }
    }
}