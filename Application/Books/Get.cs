using Application.Books.Core;
using Application.Core;
using Application.Interfaces;
using Application.Media.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Books
{
    public class Get
    {
        public class Query : IRequest<Result<BookQryDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<BookQryDto>>
        {
            private readonly DataContext _ctx;
            private readonly IMapper _mpr;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext ctx, IMapper mpr, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _mpr = mpr;
                _ctx = ctx;
            }

            public async Task<Result<BookQryDto>> Handle(Query req, CancellationToken cancellationToken)
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
                    .Include(i => i.Modifier)
                    .Where(s => s.Bk_ID == req.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                //var book = await _ctx.Books
                //    .ProjectTo<BookDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                //    .FirstOrDefaultAsync(s => s.Bk_ID == req.Id, cancellationToken: cancellationToken);

                return Result<BookQryDto>.Success(_mpr.Map<BookQryDto>(book));
                //return Result<BookDto>.Success(book);
            }
        }
    }
}

