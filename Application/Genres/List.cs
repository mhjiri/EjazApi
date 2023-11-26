using System.Linq;
using Application.Core;
using Application.Genres.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Genres
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<GenreDto>>>
        {
            public GenreParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<GenreDto>>>
        {
            private readonly DataContext _ctx;
            private readonly IMapper _mpr;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext ctx, IMapper mpr, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _mpr = mpr;
                _ctx= ctx;
            }

            public async Task<Result<PagedList<GenreDto>>> Handle(Query req, CancellationToken cancellationToken)
            {
                var qry = _ctx.Genres
                    .ProjectTo<GenreDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .AsQueryable();

                #region Filter
                char[] Chars = new char[] { ' ', '.', '?', '!', ' ', ';', ':', ',' };
                string Search = req.Params.Search;
                if (!String.IsNullOrEmpty(Search)) {
                    Search = Search.ToLower();
                    List<string> SearchWordList = Search.Split(" ").ToList();

                    qry = qry
                        .Where(s => s.Gn_Title.ToLower().Contains(Search)
                            || s.Gn_Title_Ar.ToLower().Contains(Search)
                            //|| s.Gn_Desc.ToLower().Contains(Search)
                            //|| s.Gn_Desc_Ar.ToLower().Contains(Search)
                            //|| s.Gn_Creator.ToLower().Contains(Search)
                            //|| s.Gn_Modifier.ToLower().Contains(Search)
                            //|| s.Gn_Title.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Gn_Title_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Gn_Desc.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Gn_Desc_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            );
                }

                if (!String.IsNullOrEmpty(req.Params.Status)) { 
                    switch (req.Params.Status.ToLower())
                    {
                        case "active":
                            qry = qry.Where(s => s.Gn_Active == true);
                            break;
                        case "disable":
                            qry = qry.Where(s => s.Gn_Active == false);
                            break;
                    }
                }
                #endregion
                #region Order
                if (!String.IsNullOrEmpty(req.Params.OrderBy))
                {
                    switch (req.Params.OrderBy.ToLower())
                    {
                        case "title":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Gn_Title);
                            else qry = qry.OrderBy(s => s.Gn_Title);
                            break;
                        case "title_ar":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Gn_Title_Ar);
                            else qry = qry.OrderBy(s => s.Gn_Title_Ar);
                            break;
                        case "total":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Gn_Total);
                            else qry = qry.OrderBy(s => s.Gn_Total);
                            break;
                        case "summeries":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Gn_Summaries);
                            else qry = qry.OrderBy(s => s.Gn_Summaries);
                            break;
                        case "authors":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Gn_Authors);
                            else qry = qry.OrderBy(s => s.Gn_Authors);
                            break;
                        case "publishers":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Gn_Publishers);
                            else qry = qry.OrderBy(s => s.Gn_Publishers);
                            break;

                        default:
                            qry.OrderBy(s => s.Gn_Title);
                            break;

                    }
                }
                #endregion
                return Result<PagedList<GenreDto>>
                    .Success(await PagedList<GenreDto>.CreateAsync(qry,
                        req.Params.PageNumber, req.Params.PageSize));
            }
        }
    }
}
