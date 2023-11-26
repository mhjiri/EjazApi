using Application.Core;
using Application.Authors.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Authors
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<AuthorDto>>>
        {
            public AuthorParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<AuthorDto>>>
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

            public async Task<Result<PagedList<AuthorDto>>> Handle(Query req, CancellationToken cancellationToken)
            {
                var qry = _ctx.Authors
                    .ProjectTo<AuthorDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .AsQueryable();

                #region Filter
                char[] Chars = new char[] { ' ', '.', '?', '!', ' ', ';', ':', ',' };
                string Search = req.Params.Search;
                if (!String.IsNullOrEmpty(Search)) {
                    Search = Search.ToLower();
                    List<string> SearchWordList = Search.Split(" ").ToList();

                    qry = qry
                        .Where(s => s.At_Name.ToLower().Contains(Search)
                            || s.At_Name_Ar.ToLower().Contains(Search)
                            || s.At_Title.ToLower().Contains(Search)
                            || s.At_Title_Ar.ToLower().Contains(Search)
                            || s.At_Desc.ToLower().Contains(Search)
                            || s.At_Desc_Ar.ToLower().Contains(Search)
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
                            qry = qry.Where(s => s.At_Active == true);
                            break;
                        case "disable":
                            qry = qry.Where(s => s.At_Active == false);
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
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.At_Name);
                            else qry = qry.OrderBy(s => s.At_Name);
                            break;
                        case "name_ar":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.At_Name_Ar);
                            else qry = qry.OrderBy(s => s.At_Name_Ar);
                            break;
                        case "title":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.At_Title);
                            else qry = qry.OrderBy(s => s.At_Title);
                            break;
                        case "title_ar":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.At_Title_Ar);
                            else qry = qry.OrderBy(s => s.At_Title_Ar);
                            break;
                        case "summeries":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.At_Summaries);
                            else qry = qry.OrderBy(s => s.At_Summaries);
                            break;

                        default:
                            qry.OrderBy(s => s.At_Title);
                            break;

                    }
                }
                #endregion
                return Result<PagedList<AuthorDto>>
                    .Success(await PagedList<AuthorDto>.CreateAsync(qry,
                        req.Params.PageNumber, req.Params.PageSize));
            }
        }
    }
}
