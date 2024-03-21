using Application.Core;
using Application.Payments.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Payments
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<PaymentDto>>>
        {
            public PaymentParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<PaymentDto>>>
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

            public async Task<Result<PagedList<PaymentDto>>> Handle(Query req, CancellationToken cancellationToken)
            {
                var qry = _ctx.Payments
                    .ProjectTo<PaymentDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .AsQueryable();

                #region Filter
                char[] Chars = new char[] { ' ', '.', '?', '!', ' ', ';', ':', ',' };
                string Search = req.Params.Search;
                if (!String.IsNullOrEmpty(Search)) {
                    Search = Search.ToLower();
                    List<string> SearchWordList = Search.Split(" ").ToList();

                    qry = qry
                        .Where(s => s.Pm_RefernceID.ToLower().Contains(Search)
                            || s.SubscriberName.ToLower().Contains(Search)
                            || s.SubscriberName.ToLower().Contains(Search)
                            || s.SubscriberEmail.ToLower().Contains(Search)
                            || s.Subscription.ToLower().Contains(Search)
                            || s.Subscription_Ar.ToLower().Contains(Search)
                            || s.Pm_Price.ToString().Contains(Search)
                            || s.Pm_Days.ToString().Contains(Search)
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
                            qry = qry.Where(s => s.Pm_Active == true);
                            break;
                        case "disable":
                            qry = qry.Where(s => s.Pm_Active == false);
                            break;
                    }
                }
                #endregion
                #region Order
                if (!String.IsNullOrEmpty(req.Params.OrderBy))
                {
                    switch (req.Params.OrderBy.ToLower())
                    {
                        case "referenceid":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Pm_RefernceID);
                            else qry = qry.OrderBy(s => s.Pm_RefernceID);
                            break;
                        case "subscribername":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.SubscriberName);
                            else qry = qry.OrderBy(s => s.SubscriberName);
                            break;
                        case "subscription":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Subscription);
                            else qry = qry.OrderBy(s => s.Subscription);
                            break;
                        case "subscription_ar":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Subscription_Ar);
                            else qry = qry.OrderBy(s => s.Subscription_Ar);
                            break;
                        case "days":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Pm_Days);
                            else qry = qry.OrderBy(s => s.Pm_Days);
                            break;
                        case "price":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Pm_Price);
                            else qry = qry.OrderBy(s => s.Pm_Price);
                            break;

                        default:
                            qry.OrderBy(s => s.Pm_RefernceID);
                            break;

                    }
                }
                #endregion
                return Result<PagedList<PaymentDto>>
                    .Success(await PagedList<PaymentDto>.CreateAsync(qry,
                        req.Params.PageNumber, req.Params.PageSize));
            }
        }
    }
}
