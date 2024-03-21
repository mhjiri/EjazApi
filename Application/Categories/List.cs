using Application.Core;
using Application.Categories.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<CategoryDto>>>
        {
            public CategoryParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<CategoryDto>>>
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

            public async Task<Result<PagedList<CategoryDto>>> Handle(Query req, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername(), cancellationToken: cancellationToken);


                var qry = _ctx.Categories
                    .ProjectTo<CategoryDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .AsQueryable();

                #region Filter
                char[] Chars = new char[] { ' ', '.', '?', '!', ' ', ';', ':', ',' };
                string Search = req.Params.Search;
                if (!String.IsNullOrEmpty(Search)) {
                    Search = Search.ToLower();
                    List<string> SearchWordList = Search.Split(" ").ToList();

                    qry = qry
                        .Where(s => s.Ct_Title.ToLower().Contains(Search)
                            || s.Ct_Title_Ar.ToLower().Contains(Search)
                            || s.Ct_Desc.ToLower().Contains(Search)
                            || s.Ct_Desc_Ar.ToLower().Contains(Search)
                            //|| s.Ct_Creator.ToLower().Contains(Search)
                            //|| s.Ct_Modifier.ToLower().Contains(Search)
                            //|| s.Ct_Title.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Ct_Title_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Ct_Desc.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Ct_Desc_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            );
                }

                if (!String.IsNullOrEmpty(req.Params.Status)) { 
                    switch (req.Params.Status.ToLower())
                    {
                        case "active":
                            qry = qry.Where(s => s.Ct_Active == true);
                            break;
                        case "disable":
                            qry = qry.Where(s => s.Ct_Active == false);
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
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Ct_Title);
                            else qry = qry.OrderBy(s => s.Ct_Title);
                            break;
                        case "title_ar":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Ct_Title_Ar);
                            else qry = qry.OrderBy(s => s.Ct_Title_Ar);
                            break;
                        case "summeries":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Ct_Summaries);
                            else qry = qry.OrderBy(s => s.Ct_Summaries);
                            break;

                        default:
                            qry.OrderBy(s => s.Ct_Title);
                            break;

                    }
                }
                #endregion
                return Result<PagedList<CategoryDto>>
                    .Success(await PagedList<CategoryDto>.CreateAsync(qry,
                        req.Params.PageNumber, req.Params.PageSize));
            }
        }
    }
}
