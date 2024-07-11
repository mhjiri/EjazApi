using Application.Core;
using Application.Books.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Domain;
using Newtonsoft.Json.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace Application.Books
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<BookDto>>>
        {
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
                _ctx = ctx;
            }

            //private static readonly Func<DataContext, string, string, string, bool, bool, int, int, Task<List<Book>>> _getBooksQuery =
            //    EF.CompileAsyncQuery((DataContext context, string username, string search, string language, bool isActive, bool orderByDesc, int skip, int take) =>
            //        context.Books
            //            .Where(b => b.Bk_Active == isActive)
            //            .OrderBy(b => orderByDesc ? b.Bk_Title : b.Bk_Title)
            //            .Skip(skip)
            //            .Take(take)
            //            .ToList());

            //private static readonly Func<DataContext, Guid?, string> _getAudioUrl = EF.CompileQuery((DataContext context, Guid? mediaId) => 
            //    context.Media.Where(m => m.Md_ID == mediaId).Select(m => m.Md_URL).FirstOrDefault()
            //);

            //private static readonly Func<DataContext, string, string, string, IQueryable<Book>> _filterQuery =
            //    (Func<DataContext, string, string, string, IQueryable<Book>>)EF.CompileQuery((DataContext context, string searchTerm, string language, string status) =>
            //        context.Books
            //            .Where(s =>
            //                (string.IsNullOrEmpty(searchTerm) ||
            //                    s.Bk_Name.ToLower().Contains(searchTerm.ToLower()) ||
            //                    s.Bk_Name_Ar.ToLower().Contains(searchTerm.ToLower()) ||
            //                    s.Bk_Title.ToLower().Contains(searchTerm.ToLower()) ||
            //                    s.Bk_Title_Ar.ToLower().Contains(searchTerm.ToLower()) ||
            //                    s.Bk_Desc.ToLower().Contains(searchTerm.ToLower()) ||
            //                    s.Bk_Desc_Ar.ToLower().Contains(searchTerm.ToLower()))
            //                && (string.IsNullOrEmpty(language) || s.Bk_Language.ToLower().Contains(language.ToLower()))
            //                && (string.IsNullOrEmpty(status) ||
            //                    (status.ToLower() == "active" && s.Bk_Active) ||
            //                    (status.ToLower() == "disable" && !s.Bk_Active))
            //           ).AsQueryable());


            //    private static readonly Func<DataContext, string, string, IQueryable<BookDto>> _orderQuery =
            //(Func<DataContext, string, string, IQueryable<BookDto>>)EF.CompileAsyncQuery((DataContext context, string orderBy, string orderAs) =>
            //{
            //    var query = context.Books
            //        .Select(b => new BookDto
            //        {
            //            Bk_ID = b.Bk_ID,
            //            // Add other properties as needed
            //        });

            //    switch (orderBy.ToLower())
            //    {
            //        case "name":
            //            query = orderAs.ToLower() == "desc" ? query.OrderByDescending(s => s.Bk_Name) : query.OrderBy(s => s.Bk_Name);
            //            break;
            //        case "name_ar":
            //            query = orderAs.ToLower() == "desc" ? query.OrderByDescending(s => s.Bk_Name_Ar) : query.OrderBy(s => s.Bk_Name_Ar);
            //            break;
            //        case "title":
            //            query = orderAs.ToLower() == "desc" ? query.OrderByDescending(s => s.Bk_Title) : query.OrderBy(s => s.Bk_Title);
            //            break;
            //        case "title_ar":
            //            query = orderAs.ToLower() == "desc" ? query.OrderByDescending(s => s.Bk_Title_Ar) : query.OrderBy(s => s.Bk_Title_Ar);
            //            break;
            //        case "trial":
            //            query = orderAs.ToLower() == "desc" ? query.OrderByDescending(s => s.Bk_Trial) : query.OrderBy(s => s.Bk_Trial);
            //            break;
            //        case "language":
            //            query = orderAs.ToLower() == "desc" ? query.OrderByDescending(s => s.Bk_Language) : query.OrderBy(s => s.Bk_Language);
            //            break;
            //        default:
            //            query = query.OrderBy(s => s.Bk_Title);
            //            break;
            //    }

            //    return query.AsQueryable();
            //});


            //    public async Task<Result<PagedList<BookDto>>> Handle(Query req, CancellationToken cancellationToken)
            //    {
            //        var user = await _ctx.Users.FirstOrDefaultAsync(s =>
            //            s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

            //        bool Subscribed = (user != null && user.Us_SubscriptionExpiryDate > DateTime.Now);

            //        string property = string.Empty;
            //        if (Subscribed) property = "Bk_Trial";
            //        var qry = _ctx.Books.AsQueryable();



            //        #region Filter
            //        //char[] Chars = new char[] { ' ', '.', '?', '!', ' ', ';', ':', ',' };
            //        string Search = req.Params.Search;
            //        if (!String.IsNullOrEmpty(Search)) {
            //            Search = Search.ToLower();
            //            List<string> SearchWordList = Search.Split(" ").ToList();

            //            qry = qry
            //                .Where(s => s.Bk_Name.ToLower().Contains(Search)
            //                    || s.Bk_Name_Ar.ToLower().Contains(Search)
            //                    || s.Bk_Title.ToLower().Contains(Search)
            //                    || s.Bk_Title_Ar.ToLower().Contains(Search)
            //                    || s.Bk_Desc.ToLower().Contains(Search)
            //                    || s.Bk_Desc_Ar.ToLower().Contains(Search));

            //        }

            //        string Language = req.Params.Language;
            //        if (!String.IsNullOrEmpty(Language))
            //        {
            //            Language = Language.ToLower();
            //            qry = qry
            //                .Where(s => s.Bk_Language.ToLower().Contains(Language));
            //        }

            //        if (!String.IsNullOrEmpty(req.Params.Status)) { 
            //            switch (req.Params.Status.ToLower())
            //            {
            //                case "active":
            //                    qry = qry.Where(s => s.Bk_Active == true);
            //                    break;
            //                case "disable":
            //                    qry = qry.Where(s => s.Bk_Active == false);
            //                    break;
            //            }
            //        }
            //        #endregion
            //        #region Order
            //        if (!String.IsNullOrEmpty(req.Params.OrderBy))
            //        {
            //            switch (req.Params.OrderBy.ToLower())
            //            {
            //                case "name":
            //                    if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Bk_Name);
            //                    else qry = qry.OrderBy(s => s.Bk_Name);
            //                    break;
            //                case "name_ar":
            //                    if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Bk_Name_Ar);
            //                    else qry = qry.OrderBy(s => s.Bk_Name_Ar);
            //                    break;
            //                case "title":
            //                    if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Bk_Title);
            //                    else qry = qry.OrderBy(s => s.Bk_Title);
            //                    break;
            //                case "title_ar":
            //                    if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Bk_Title_Ar);
            //                    else qry = qry.OrderBy(s => s.Bk_Title_Ar);
            //                    break;

            //                case "trial":
            //                    if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Bk_Trial);
            //                    else qry = qry.OrderBy(s => s.Bk_Trial);
            //                    break;
            //                case "language":
            //                    if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Bk_Language);
            //                    else qry = qry.OrderBy(s => s.Bk_Language);
            //                    break;

            //                default:
            //                    qry = qry.OrderBy(s => s.Bk_Title);
            //                    break;

            //            }
            //        }

            //        #endregion

            //        var books = await qry.ToListAsync(cancellationToken);


            //        foreach (var book in books)
            //        {
            //            book.Md_AudioEn_URL = await GetAudioUrlAsync(book.Md_AudioEn_ID);
            //            book.Md_AudioAr_URL = await GetAudioUrlAsync(book.Md_AudioAr_ID);

            //        }

            //        var bookDtos = books.Select(book => _mpr.Map<BookDto>(book)).AsQueryable();

            //        //            qry = _ctx.Books
            //        //.ProjectTo<BookDto>(_mpr.ConfigurationProvider, new { currentUsername = _userAccessor.GetUsername() })
            //        //.AsQueryable();


            //        //var bookDtos = books.Select(book => new BookDto
            //        //{
            //        //    Bk_Name = book.Bk_Name,
            //        //    Bk_Name_Ar = book.Bk_Name_Ar,
            //        //    Bk_Title = book.Bk_Title,
            //        //    Bk_Title_Ar = book.Bk_Title_Ar,
            //        //    Bk_Desc = book.Bk_Desc,
            //        //    Bk_Desc_Ar = book.Bk_Desc_Ar,
            //        //    Bk_Language = book.Bk_Language,
            //        //    Bk_Active = book.Bk_Active,
            //        //    Md_AudioEn_ID = book.Md_AudioEn_ID,
            //        //    Md_AudioAr_ID = book.Md_AudioAr_ID,
            //        //    Md_AudioEn_URL = book.Md_AudioEn_URL,
            //        //    Md_AudioAr_URL = book.Md_AudioAr_URL,
            //        //    Bk_ID = book.Bk_ID,
            //        //    Md_ID = null,
            //        //    Bk_Introduction= book.Bk_Introduction,
            //        //    Bk_Introduction_Ar = book.Bk_Introduction_Ar,
            //        //    Bk_Summary=book.Bk_Summary,
            //        //    Bk_Summary_Ar= book.Bk_Summary_Ar,
            //        //    Bk_Language_Ar= "الكل",
            //        //    Bk_Trial= book.Bk_Trial,
            //        //    Bk_Characters= book.Bk_Characters,
            //        //    Bk_Characters_Ar=book.Bk_Characters_Ar,
            //        //    Bk_CreatedOn= book.Bk_CreatedOn,
            //        //    Bk_Creator= book.Bk_Creator,
            //        //    Bk_Code=book.Bk_Code,
            //        //    Bk_Modifier=book.Bk_Modifier,
            //        //    Bk_ModifyOn=book.Bk_ModifyOn,

            //        //}).AsQueryable();

            //        return Result<PagedList<BookDto>>
            //            .Success(await PagedList<BookDto>.CreateAsync(bookDtos,
            //                req.Params.PageNumber, req.Params.PageSize, property, Subscribed));



            //    }
            //    private async Task<string> GetAudioUrlAsync(Guid? mediaId)
            //    {
            //        if (mediaId.HasValue)
            //        {
            //            return await _ctx.Media
            //                .Where(m => m.Md_ID == mediaId)
            //                .Select(m => m.Md_URL)
            //                .FirstOrDefaultAsync();
            //        }
            //        return null;
            //    }
            //}

            public async Task<Result<PagedList<BookDto>>> Handle(Query req, CancellationToken cancellationToken)
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(s =>
                    s.UserName == _userAccessor.GetUsername() && !s.Us_Deleted, cancellationToken: cancellationToken);

                bool Subscribed = (user != null && user.Us_SubscriptionExpiryDate > DateTime.Now);

                string property = string.Empty;
                if (Subscribed) property = "Bk_Trial";

                var qry = _ctx.Books
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
                            //|| s.Bk_Modifier.ToLower().Contains(Search)
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
                        case "trial":
                            if (req.Params.OrderAs.ToLower() == "desc") qry = qry.OrderByDescending(s => s.Bk_Trial);
                            else qry = qry.OrderBy(s => s.Bk_Trial);
                            break;
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
                //return Result<PagedList<BookDto>>.Success(_mpr.Map<PagedList<BookDto>>(await qry.ToListAsync()));
                return Result<PagedList<BookDto>>
                    .Success(await PagedList<BookDto>.CreateAsync(qry,
                        req.Params.PageNumber, req.Params.PageSize, property, Subscribed));
            }



        }
    }
}
