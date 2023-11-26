using Application.Core;
using Application.Rewards.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Rewards
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<RewardDto>>>
        {
            public RewardParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<RewardDto>>>
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

            public async Task<Result<PagedList<RewardDto>>> Handle(Query req, CancellationToken cancellationToken)
            {
                var qry = _ctx.Rewards
                    .ProjectTo<RewardDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .AsQueryable();

                #region Filter
                char[] Chars = new char[] { ' ', '.', '?', '!', ' ', ';', ':', ',' };
                string Search = req.Params.Search;
                if (!String.IsNullOrEmpty(Search)) {
                    Search = Search.ToLower();
                    List<string> SearchWordList = Search.Split(" ").ToList();

                    qry = qry
                        .Where(s => s.Rw_Name.ToLower().Contains(Search)
                            || s.Rw_Name_Ar.ToLower().Contains(Search)
                            || s.Rw_Title.ToLower().Contains(Search)
                            || s.Rw_Title_Ar.ToLower().Contains(Search)
                            || s.Rw_Desc.ToLower().Contains(Search)
                            || s.Rw_Desc_Ar.ToLower().Contains(Search)
                            || s.Rw_Creator.ToLower().Contains(Search)
                            || s.Rw_Modifier.ToLower().Contains(Search)
                            || s.Rw_Name.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            || s.Rw_Name_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            || s.Rw_Title.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            || s.Rw_Title_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            || s.Rw_Desc.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            || s.Rw_Desc_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            );
                }

                if (!String.IsNullOrEmpty(req.Params.Status)) { 
                    switch (req.Params.Status.ToLower())
                    {
                        case "active":
                            qry = qry.Where(s => s.Rw_Active == true);
                            break;
                        case "disable":
                            qry = qry.Where(s => s.Rw_Active == false);
                            break;
                    }
                }
                #endregion
                #region Order
                if (!String.IsNullOrEmpty(req.Params.OrderBy))
                {
                    switch (req.Params.OrderBy.ToLower())
                    {
                        case "name":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Rw_Name);
                            else qry = qry.OrderBy(s => s.Rw_Name);
                            break;
                        case "name_ar":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Rw_Name_Ar);
                            else qry = qry.OrderBy(s => s.Rw_Name_Ar);
                            break;
                        case "title":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Rw_Title);
                            else qry = qry.OrderBy(s => s.Rw_Title);
                            break;
                        case "title_ar":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Rw_Title_Ar);
                            else qry = qry.OrderBy(s => s.Rw_Title_Ar);
                            break;
                        case "groups":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Rw_Groups);
                            else qry = qry.OrderBy(s => s.Rw_Groups);
                            break;
                        case "customers":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Rw_Customers);
                            else qry = qry.OrderBy(s => s.Rw_Customers);
                            break;

                        default:
                            qry.OrderBy(s => s.Rw_Title);
                            break;

                    }
                }
                #endregion
                return Result<PagedList<RewardDto>>
                    .Success(await PagedList<RewardDto>.CreateAsync(qry,
                        req.Params.PageNumber, req.Params.PageSize));
            }
        }
    }
}
