using Application.Core;
using Application.Profiles.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Profiles
{
    public class ListTrialUsers
    {
        public class Query : IRequest<Result<PagedList<ProfileDto>>>
        {
            public ProfileParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<ProfileDto>>>
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

            public async Task<Result<PagedList<ProfileDto>>> Handle(Query req, CancellationToken cancellationToken)
            {
                var qry = _ctx.Users.Where(s => s.Us_Customer == true && !s.Us_Deleted && (s.Us_SubscriptionExpiryDate == null || s.Us_SubscriptionExpiryDate < DateTime.Now))
                    .ProjectTo<ProfileDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .AsQueryable();

                #region Filter
                char[] Chars = new char[] { ' ', '.', '?', '!', ' ', ';', ':', ',' };
                string Search = req.Params.Search;
                if (!String.IsNullOrEmpty(Search)) {
                    Search = Search.ToLower();
                    List<string> SearchWordList = Search.Split(" ").ToList();

                    qry = qry
                        .Where(s => s.Us_DisplayName.ToLower().Contains(Search)
                            || s.Username.ToLower().Contains(Search)
                            || s.PhoneNumber.ToLower().Contains(Search)
                            || s.Email.ToLower().Contains(Search)
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
                            qry = qry.Where(s => s.Us_Active == true);
                            break;
                        case "disable":
                            qry = qry.Where(s => s.Us_Active == false);
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
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Us_DisplayName);
                            else qry = qry.OrderBy(s => s.Us_DisplayName);
                            break;
                        case "username":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Username);
                            else qry = qry.OrderBy(s => s.Username);
                            break;
                        case "phonenumber":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.PhoneNumber);
                            else qry = qry.OrderBy(s => s.PhoneNumber);
                            break;
                        case "email":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Email);
                            else qry = qry.OrderBy(s => s.Email);
                            break;

                        default:
                            qry.OrderBy(s => s.Us_DisplayName);
                            break;

                    }
                }
                #endregion
                return Result<PagedList<ProfileDto>>
                    .Success(await PagedList<ProfileDto>.CreateAsync(qry,
                        req.Params.PageNumber, req.Params.PageSize));
            }
        }
    }
}
