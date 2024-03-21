using Application.Core;
using Application.Subscriptions.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Subscriptions
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<SubscriptionDto>>>
        {
            public SubscriptionParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<SubscriptionDto>>>
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

            public async Task<Result<PagedList<SubscriptionDto>>> Handle(Query req, CancellationToken cancellationToken)
            {
                var qry = _ctx.Subscriptions
                    .ProjectTo<SubscriptionDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .AsQueryable();

                #region Filter
                char[] Chars = new char[] { ' ', '.', '?', '!', ' ', ';', ':', ',' };
                string Search = req.Params.Search;
                if (!String.IsNullOrEmpty(Search)) {
                    Search = Search.ToLower();
                    List<string> SearchWordList = Search.Split(" ").ToList();

                    qry = qry
                        .Where(s => s.Sb_Name.ToLower().Contains(Search)
                            || s.Sb_Name_Ar.ToLower().Contains(Search)
                            || s.Sb_Title.ToLower().Contains(Search)
                            || s.Sb_Title_Ar.ToLower().Contains(Search)
                            || s.Sb_Desc.ToLower().Contains(Search)
                            || s.Sb_Desc_Ar.ToLower().Contains(Search)
                            || s.Sb_Price.ToString().Contains(Search)
                            || s.Sb_Days.ToString().Contains(Search)
                            //|| s.At_Creator.ToLower().Contains(Search)
                            //|| s.At_Modifier.ToLower().Contains(Search)
                            //|| s.At_Title.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.At_Title_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.At_Title.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.At_Title_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.At_Desc.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.At_Desc_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            );
                }

                if (!String.IsNullOrEmpty(req.Params.Status)) { 
                    switch (req.Params.Status.ToLower())
                    {
                        case "active":
                            qry = qry.Where(s => s.Sb_Active == true);
                            break;
                        case "disable":
                            qry = qry.Where(s => s.Sb_Active == false);
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
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Sb_Name);
                            else qry = qry.OrderBy(s => s.Sb_Name);
                            break;
                        case "name_ar":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Sb_Name_Ar);
                            else qry = qry.OrderBy(s => s.Sb_Name_Ar);
                            break;
                        case "title":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Sb_Title);
                            else qry = qry.OrderBy(s => s.Sb_Title);
                            break;
                        case "title_ar":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Sb_Title_Ar);
                            else qry = qry.OrderBy(s => s.Sb_Title_Ar);
                            break;
                        case "price":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Sb_Price);
                            else qry = qry.OrderBy(s => s.Sb_Price);
                            break;
                        case "days":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Sb_Days);
                            else qry = qry.OrderBy(s => s.Sb_Days);
                            break;

                        default:
                            qry.OrderBy(s => s.Sb_Title);
                            break;

                    }
                }
                #endregion
                return Result<PagedList<SubscriptionDto>>
                    .Success(await PagedList<SubscriptionDto>.CreateAsync(qry,
                        req.Params.PageNumber, req.Params.PageSize));
            }
        }
    }
}
