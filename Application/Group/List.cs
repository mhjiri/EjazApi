using Application.Core;
using Application.Groups.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Groups
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<GroupDto>>>
        {
            public GroupParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<GroupDto>>>
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

            public async Task<Result<PagedList<GroupDto>>> Handle(Query req, CancellationToken cancellationToken)
            {
                var qry = _ctx.Groups
                    .ProjectTo<GroupDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .AsQueryable();

                #region Filter
                char[] Chars = new char[] { ' ', '.', '?', '!', ' ', ';', ':', ',' };
                string Search = req.Params.Search;
                if (!String.IsNullOrEmpty(Search)) {
                    Search = Search.ToLower();
                    List<string> SearchWordList = Search.Split(" ").ToList();

                    qry = qry
                        .Where(s => s.Gr_Title.ToLower().Contains(Search)
                            || s.Gr_Title_Ar.ToLower().Contains(Search)
                            || s.Gr_Desc.ToLower().Contains(Search)
                            //|| s.Gr_Desc_Ar.ToLower().Contains(Search)
                            //|| s.Gr_Creator.ToLower().Contains(Search)
                            //|| s.Gr_Modifier.ToLower().Contains(Search)
                            //|| s.Gr_Title.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Gr_Title_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Gr_Desc.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Gr_Desc_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            );
                }

                if (!String.IsNullOrEmpty(req.Params.Status)) { 
                    switch (req.Params.Status.ToLower())
                    {
                        case "active":
                            qry = qry.Where(s => s.Gr_Active == true);
                            break;
                        case "disable":
                            qry = qry.Where(s => s.Gr_Active == false);
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
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Gr_Title);
                            else qry = qry.OrderBy(s => s.Gr_Title);
                            break;
                        case "title_ar":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Gr_Title_Ar);
                            else qry = qry.OrderBy(s => s.Gr_Title_Ar);
                            break;

                        default:
                            qry.OrderBy(s => s.Gr_Title);
                            break;

                    }
                }
                #endregion
                return Result<PagedList<GroupDto>>
                    .Success(await PagedList<GroupDto>.CreateAsync(qry,
                        req.Params.PageNumber, req.Params.PageSize));
            }
        }
    }
}
