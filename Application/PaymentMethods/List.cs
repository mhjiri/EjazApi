using Application.Core;
using Application.PaymentMethods.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.PaymentMethods
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<PaymentMethodDto>>>
        {
            public PaymentMethodParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<PaymentMethodDto>>>
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

            public async Task<Result<PagedList<PaymentMethodDto>>> Handle(Query req, CancellationToken cancellationToken)
            {
                var qry = _ctx.PaymentMethods
                    .ProjectTo<PaymentMethodDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .AsQueryable();

                #region Filter
                char[] Chars = new char[] { ' ', '.', '?', '!', ' ', ';', ':', ',' };
                string Search = req.Params.Search;
                if (!String.IsNullOrEmpty(Search)) {
                    Search = Search.ToLower();
                    List<string> SearchWordList = Search.Split(" ").ToList();

                    qry = qry
                        .Where(s => s.Py_Title.ToLower().Contains(Search)
                            || s.Py_Title_Ar.ToLower().Contains(Search)
                            || s.Py_Desc.ToLower().Contains(Search)
                            || s.Py_Desc_Ar.ToLower().Contains(Search)
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
                            qry = qry.Where(s => s.Py_Active == true);
                            break;
                        case "disable":
                            qry = qry.Where(s => s.Py_Active == false);
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
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Py_Title);
                            else qry = qry.OrderBy(s => s.Py_Title);
                            break;
                        case "title_ar":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Py_Title_Ar);
                            else qry = qry.OrderBy(s => s.Py_Title_Ar);
                            break;

                        default:
                            qry.OrderBy(s => s.Py_Title);
                            break;

                    }
                }
                #endregion
                return Result<PagedList<PaymentMethodDto>>
                    .Success(await PagedList<PaymentMethodDto>.CreateAsync(qry,
                        req.Params.PageNumber, req.Params.PageSize));
            }
        }
    }
}
