using System.Linq;
using Application.Core;
using Application.Countries.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Countries
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<CountryDto>>>
        {
            public CountryParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<CountryDto>>>
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

            public async Task<Result<PagedList<CountryDto>>> Handle(Query req, CancellationToken cancellationToken)
            {
                var qry = _ctx.Countries
                    .ProjectTo<CountryDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .AsQueryable();

                #region Filter
                char[] Chars = new char[] { ' ', '.', '?', '!', ' ', ';', ':', ',' };
                string Search = req.Params.Search;
                if (!String.IsNullOrEmpty(Search)) {
                    Search = Search.ToLower();
                    List<string> SearchWordList = Search.Split(" ").ToList();

                    qry = qry
                        .Where(s => s.Cn_Code.ToString().Contains(Search)
                            || s.Cn_Title.ToLower().Contains(Search)
                            || s.Cn_Title_Ar.ToLower().Contains(Search)
                            || s.Cn_Desc.ToLower().Contains(Search)
                            || s.Cn_Desc_Ar.ToLower().Contains(Search)
                            || s.Cn_Creator.ToLower().Contains(Search)
                            || s.Cn_Modifier.ToLower().Contains(Search)
                            || s.Cn_Title.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            || s.Cn_Title_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            || s.Cn_Desc.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            || s.Cn_Desc_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            );
                }

                if (!String.IsNullOrEmpty(req.Params.Status)) { 
                    switch (req.Params.Status.ToLower())
                    {
                        case "active":
                            qry = qry.Where(s => s.Cn_Active == true);
                            break;
                        case "disable":
                            qry = qry.Where(s => s.Cn_Active == false);
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
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Cn_Title);
                            else qry = qry.OrderBy(s => s.Cn_Title);
                            break;
                        case "title_ar":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Cn_Title_Ar);
                            else qry = qry.OrderBy(s => s.Cn_Title_Ar);
                            break;
                        case "code":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Cn_Code);
                            else qry = qry.OrderBy(s => s.Cn_Code);
                            break;

                        default:
                            qry.OrderBy(s => s.Cn_Code);
                            break;

                    }
                }
                #endregion
                return Result<PagedList<CountryDto>>
                    .Success(await PagedList<CountryDto>.CreateAsync(qry,
                        req.Params.PageNumber, req.Params.PageSize));
            }
        }
    }
}
