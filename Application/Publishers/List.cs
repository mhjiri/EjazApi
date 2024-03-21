using Application.Core;
using Application.Publishers.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Publishers
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<PublisherDto>>>
        {
            public PublisherParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<PublisherDto>>>
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

            public async Task<Result<PagedList<PublisherDto>>> Handle(Query req, CancellationToken cancellationToken)
            {
                var qry = _ctx.Publishers
                    .ProjectTo<PublisherDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .AsQueryable();

                #region Filter
                char[] Chars = new char[] { ' ', '.', '?', '!', ' ', ';', ':', ',' };
                string Search = req.Params.Search;
                if (!String.IsNullOrEmpty(Search)) {
                    Search = Search.ToLower();
                    List<string> SearchWordList = Search.Split(" ").ToList();

                    qry = qry
                        .Where(s => s.Pb_Name.ToLower().Contains(Search)
                            || s.Pb_Name_Ar.ToLower().Contains(Search)
                            || s.Pb_Title.ToLower().Contains(Search)
                            || s.Pb_Title_Ar.ToLower().Contains(Search)
                            || s.Pb_Desc.ToLower().Contains(Search)
                            || s.Pb_Desc_Ar.ToLower().Contains(Search)
                            //|| s.Pb_Creator.ToLower().Contains(Search)
                            //|| s.Pb_Modifier.ToLower().Contains(Search)
                            //|| s.Pb_Title.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Pb_Title_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Pb_Title.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Pb_Title_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Pb_Desc.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Pb_Desc_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            );
                }

                if (!String.IsNullOrEmpty(req.Params.Status)) { 
                    switch (req.Params.Status.ToLower())
                    {
                        case "active":
                            qry = qry.Where(s => s.Pb_Active == true);
                            break;
                        case "disable":
                            qry = qry.Where(s => s.Pb_Active == false);
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
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Pb_Name);
                            else qry = qry.OrderBy(s => s.Pb_Name);
                            break;
                        case "name_ar":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Pb_Name_Ar);
                            else qry = qry.OrderBy(s => s.Pb_Name_Ar);
                            break;
                        case "title":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Pb_Title);
                            else qry = qry.OrderBy(s => s.Pb_Title);
                            break;
                        case "title_ar":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Pb_Title_Ar);
                            else qry = qry.OrderBy(s => s.Pb_Title_Ar);
                            break;
                        case "customers":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Pb_Summaries);
                            else qry = qry.OrderBy(s => s.Pb_Summaries);
                            break;

                        default:
                            qry.OrderBy(s => s.Pb_Title);
                            break;

                    }
                }
                #endregion
                return Result<PagedList<PublisherDto>>
                    .Success(await PagedList<PublisherDto>.CreateAsync(qry,
                        req.Params.PageNumber, req.Params.PageSize));
            }
        }
    }
}
