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
using System.Security.Policy;

namespace Application.Authors
{
    public class Create
    {
        public class Command : IRequest<Result<AuthorQryDto>>
        {
            public AuthorCmdDto Author { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<AuthorQryDto>>
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

            public async Task<Result<AuthorQryDto>> Handle(Command req, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                Author author = _mpr.Map<Author>(req.Author);
                author.At_Creator = user.Id;
                author.Genres = new List<AuthorGenre>();
                if (req.Author.GenreItems != null) {
                    foreach (ItemDto genre in req.Author.GenreItems)
                    {
                        var authorGenre = new AuthorGenre
                        {
                            Gn_ID = genre.It_ID,
                            AtGn_Ordinal = genre.It_Ordinal,
                            AtGn_Active = true,
                            AtGn_Creator = user.Id
                        };
                        author.Genres.Add(authorGenre);
                    }
                }


                _ctx.Authors.Add(author);

                var result = await _ctx.SaveChangesAsync(cancellationToken) > 0;

                if (!result) return Result<AuthorQryDto>.Failure("Failed to create Author");
                author = await _ctx.Authors.Include(i => i.Genres).ThenInclude(j => j.Genre).Where(s => s.At_ID == author.At_ID).FirstOrDefaultAsync(cancellationToken: cancellationToken);
                return Result<AuthorQryDto>.Success(_mpr.Map<AuthorQryDto>(author)); 
            }
        }
    }
}