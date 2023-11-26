using System.Linq;
using Application.Core;
using Application.BannerLocations.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.BannerLocations
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<BannerLocationDto>>>
        {
            public BannerLocationParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<BannerLocationDto>>>
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

            public async Task<Result<PagedList<BannerLocationDto>>> Handle(Query req, CancellationToken cancellationToken)
            {
                var qry = _ctx.BannerLocations
                    .ProjectTo<BannerLocationDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .AsQueryable();

                #region Filter
                char[] Chars = new char[] { ' ', '.', '?', '!', ' ', ';', ':', ',' };
                string Search = req.Params.Search;
                if (!String.IsNullOrEmpty(Search)) {
                    Search = Search.ToLower();
                    List<string> SearchWordList = Search.Split(" ").ToList();

                    qry = qry
                        .Where(s => s.Bl_Title.ToLower().Contains(Search)
                            || s.Bl_Title_Ar.ToLower().Contains(Search)
                            //|| s.Bl_Desc.ToLower().Contains(Search)
                            //|| s.Bl_Desc_Ar.ToLower().Contains(Search)
                            //|| s.Bl_Creator.ToLower().Contains(Search)
                            //|| s.Bl_Modifier.ToLower().Contains(Search)
                            //|| s.Bl_Title.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Bl_Title_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Bl_Desc.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Bl_Desc_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            );
                }

                if (!String.IsNullOrEmpty(req.Params.Status)) { 
                    switch (req.Params.Status.ToLower())
                    {
                        case "active":
                            qry = qry.Where(s => s.Bl_Active == true);
                            break;
                        case "disable":
                            qry = qry.Where(s => s.Bl_Active == false);
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
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Bl_Title);
                            else qry = qry.OrderBy(s => s.Bl_Title);
                            break;
                        case "title_ar":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Bl_Title_Ar);
                            else qry = qry.OrderBy(s => s.Bl_Title_Ar);
                            break;
                        case "total":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Bl_Banners);
                            else qry = qry.OrderBy(s => s.Bl_Banners);
                            break;

                        default:
                            qry.OrderBy(s => s.Bl_Title);
                            break;

                    }
                }
                #endregion
                return Result<PagedList<BannerLocationDto>>
                    .Success(await PagedList<BannerLocationDto>.CreateAsync(qry,
                        req.Params.PageNumber, req.Params.PageSize));
            }
        }
    }
}
