using System.Linq;
using Application.Core;
using Application.Interfaces;
using Application.ThematicAreas.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.ThematicAreas
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<ThematicAreaDto>>>
        {
            public ThematicAreaParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<ThematicAreaDto>>>
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

            public async Task<Result<PagedList<ThematicAreaDto>>> Handle(Query req, CancellationToken cancellationToken)
            {
                var qry = _ctx.ThematicAreas
                    .ProjectTo<ThematicAreaDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .AsQueryable();

                #region Filter
                char[] Chars = new char[] { ' ', '.', '?', '!', ' ', ';', ':', ',' };
                string Search = req.Params.Search;
                if (!String.IsNullOrEmpty(Search)) {
                    Search = Search.ToLower();
                    List<string> SearchWordList = Search.Split(" ").ToList();

                    qry = qry
                        .Where(s => s.Th_Title.ToLower().Contains(Search)
                            || s.Th_Title_Ar.ToLower().Contains(Search)
                            //|| s.Th_Desc.ToLower().Contains(Search)
                            //|| s.Th_Desc_Ar.ToLower().Contains(Search)
                            //|| s.Th_Creator.ToLower().Contains(Search)
                            //|| s.Th_Modifier.ToLower().Contains(Search)
                            //|| s.Th_Title.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Th_Title_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Th_Desc.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Th_Desc_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            );
                }

                if (!String.IsNullOrEmpty(req.Params.Status)) { 
                    switch (req.Params.Status.ToLower())
                    {
                        case "active":
                            qry = qry.Where(s => s.Th_Active == true);
                            break;
                        case "disable":
                            qry = qry.Where(s => s.Th_Active == false);
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
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Th_Title);
                            else qry = qry.OrderBy(s => s.Th_Title);
                            break;
                        case "title_ar":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Th_Title_Ar);
                            else qry = qry.OrderBy(s => s.Th_Title_Ar);
                            break;
                        case "summeries":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Th_Summaries);
                            else qry = qry.OrderBy(s => s.Th_Summaries);
                            break;

                        default:
                            qry = qry.OrderBy(s => s.Th_Title);
                            break;

                    }
                } 
                #endregion
                return Result<PagedList<ThematicAreaDto>>
                    .Success(await PagedList<ThematicAreaDto>.CreateAsync(qry,
                        req.Params.PageNumber, req.Params.PageSize));
            }
        }
    }
}
