using System.Linq;
using Application.Core;
using Application.Genres.Core;
using Application.Interfaces;
using Application.Tags.Core;
using Application.ThematicAreas.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Tags
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<TagDto>>>
        {
            public TagParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<TagDto>>>
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

            public async Task<Result<PagedList<TagDto>>> Handle(Query req, CancellationToken cancellationToken)
            {
                var qry = _ctx.Tags
                    .ProjectTo<TagDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .AsQueryable();

                #region Filter
                char[] Chars = new char[] { ' ', '.', '?', '!', ' ', ';', ':', ',' };
                string Search = req.Params.Search;
                if (!String.IsNullOrEmpty(Search)) {
                    Search = Search.ToLower();
                    List<string> SearchWordList = Search.Split(" ").ToList();

                    qry = qry
                        .Where(s => s.Tg_Title.ToLower().Contains(Search)
                            || s.Tg_Title_Ar.ToLower().Contains(Search)
                            || s.Tg_Desc.ToLower().Contains(Search)
                            || s.Tg_Desc_Ar.ToLower().Contains(Search)
                            //|| s.Tg_Creator.ToLower().Contains(Search)
                            //|| s.Tg_Modifier.ToLower().Contains(Search)
                            //|| s.Tg_Title.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Tg_Title_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Tg_Desc.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Tg_Desc_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            );
                }

                if (!String.IsNullOrEmpty(req.Params.Status)) { 
                    switch (req.Params.Status.ToLower())
                    {
                        case "active":
                            qry = qry.Where(s => s.Tg_Active == true);
                            break;
                        case "disable":
                            qry = qry.Where(s => s.Tg_Active == false);
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
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Tg_Title);
                            else qry = qry.OrderBy(s => s.Tg_Title);
                            break;
                        case "title_ar":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Tg_Title_Ar);
                            else qry = qry.OrderBy(s => s.Tg_Title_Ar);
                            break;
                        case "total":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Tg_Total);
                            else qry = qry.OrderBy(s => s.Tg_Total);
                            break;
                        case "summeries":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Tg_Summaries);
                            else qry = qry.OrderBy(s => s.Tg_Summaries);
                            break;
                        case "categories":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Tg_Categories);
                            else qry = qry.OrderBy(s => s.Tg_Categories);
                            break;

                        default:
                            qry.OrderBy(s => s.Tg_Title);
                            break;

                    }
                }
                #endregion
                return Result<PagedList<TagDto>>
                    .Success(await PagedList<TagDto>.CreateAsync(qry,
                        req.Params.PageNumber, req.Params.PageSize));
            }
        }
    }
}
