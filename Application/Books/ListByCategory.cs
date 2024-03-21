using Application.Core;
using Application.Books.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Books
{
    public class ListByCategory
    {
        public class Query : IRequest<Result<PagedList<BookDto>>>
        {
            public Guid Id { get; set; }
            public BookParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<BookDto>>>
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

            public async Task<Result<PagedList<BookDto>>> Handle(Query req, CancellationToken cancellationToken)
            {
                var qry = _ctx.Books
                    .Where(s => s.Categories.Select(z=> z.Ct_ID).Contains(req.Id))
                    .ProjectTo<BookDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
                    .AsQueryable();

                #region Filter
                char[] Chars = new char[] { ' ', '.', '?', '!', ' ', ';', ':', ',' };
                string Search = req.Params.Search;
                if (!String.IsNullOrEmpty(Search))
                {
                    Search = Search.ToLower();
                    List<string> SearchWordList = Search.Split(" ").ToList();

                    qry = qry
                        .Where(s => s.Bk_Name.ToLower().Contains(Search)
                            || s.Bk_Name_Ar.ToLower().Contains(Search)
                            || s.Bk_Title.ToLower().Contains(Search)
                            || s.Bk_Title_Ar.ToLower().Contains(Search)
                            || s.Bk_Desc.ToLower().Contains(Search)
                            || s.Bk_Desc_Ar.ToLower().Contains(Search)
                            //|| s.Bk_Creator.ToLower().Contains(Search)
                            ////|| s.Bk_Modifier.ToLower().Contains(Search)
                            //|| s.Bk_Category.ToLower().Contains(Search)
                            //|| s.Bk_ThematicArea.ToLower().Contains(Search)
                            //|| s.Bk_Title.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Bk_Title_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Bk_Title.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Bk_Title_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Bk_Desc.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Bk_Desc_Ar.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Bk_Category.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            //|| s.Bk_ThematicArea.ToLower().Split(Chars, StringSplitOptions.RemoveEmptyEntries).Select(z => z.Trim()).Distinct().Any(y => SearchWordList.Contains(y))
                            );
                }

                string Language = req.Params.Language;
                if (!String.IsNullOrEmpty(Language))
                {
                    Language = Language.ToLower();
                    qry = qry
                        .Where(s => s.Bk_Language.ToLower().Contains(Language));
                }

                if (!String.IsNullOrEmpty(req.Params.Status))
                {
                    switch (req.Params.Status.ToLower())
                    {
                        case "active":
                            qry = qry.Where(s => s.Bk_Active == true);
                            break;
                        case "disable":
                            qry = qry.Where(s => s.Bk_Active == false);
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
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Bk_Name);
                            else qry = qry.OrderBy(s => s.Bk_Name);
                            break;
                        case "name_ar":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Bk_Name_Ar);
                            else qry = qry.OrderBy(s => s.Bk_Name_Ar);
                            break;
                        case "title":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Bk_Title);
                            else qry = qry.OrderBy(s => s.Bk_Title);
                            break;
                        case "title_ar":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Bk_Title_Ar);
                            else qry = qry.OrderBy(s => s.Bk_Title_Ar);
                            break;
                        //case "category":
                        //    if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Bk_Category);
                        //    else qry = qry.OrderBy(s => s.Bk_Category);
                        //    break;
                        //case "thematicarea":
                        //    if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Bk_ThematicArea);
                        //    else qry = qry.OrderBy(s => s.Bk_ThematicArea);
                        //    break;
                        case "language":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Bk_Language);
                            else qry = qry.OrderBy(s => s.Bk_Language);
                            break;

                        default:
                            qry = qry.OrderBy(s => s.Bk_Title);
                            break;

                    }
                }
                #endregion
                return Result<PagedList<BookDto>>
                    .Success(await PagedList<BookDto>.CreateAsync(qry,
                        req.Params.PageNumber, req.Params.PageSize));
            }
        }
    }
}
