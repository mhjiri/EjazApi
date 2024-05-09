using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Books;
using Application.Books.Core;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Persistence;

namespace Ejaz.Controllers
{

    [ResponseCache(CacheProfileName = "Default604800")]
    public class BookController : BaseApiController
    {

        private readonly ILogger<BookController> _logger;
        private readonly IDistributedCache _cache;

        public BookController(IDistributedCache cache, ILogger<BookController> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("getBooks")]
        [Produces("application/json")]
        [OutputCache(Duration = 604800)]
        public async Task<IActionResult> GetBooks([FromQuery] BookParams param)
        {
            HttpContext.Response.GetTypedHeaders().CacheControl =
             new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
             {
                 Public = true, // Cache response in public caches (like browsers)
                 MaxAge = TimeSpan.FromSeconds(10) // Cache duration (10 seconds in this example)
             };
            HttpContext.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
                new string[] { "Accept-Encoding" };
            _logger.LogInformation("Alhamdulillah. Starting getBooks endpoint....");
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                var cacheKey = $"Books_{param.Status}_{param.PageSize}_{param.OrderBy}_{param.OrderAs}";
                var cachedData = await _cache.GetStringAsync(cacheKey);
                if (cachedData != null)
                {
                    var books = JsonConvert.DeserializeObject<List<BookDto>>(cachedData);
                    return Ok(books);
                }

                var result = await mdtr.Send(new List.Query { Params = param });

                if (result.IsSuccess && result.Value != null)
                {
                    var pagedList = (PagedList<BookDto>)result.Value;

                    var items = pagedList.ToList();

                    var serializedBooks = JsonConvert.SerializeObject(items);

                    await _cache.SetStringAsync(cacheKey, serializedBooks, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7)
                    });

                    return Ok(items);
                }
                else
                {
                    return BadRequest("Failed to retrieve books.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation("getBooks endpoints completed in ${Elapsedmilliseconds} ms", stopwatch.ElapsedMilliseconds);
            }
        }

        [AllowAnonymous]
        [HttpGet("getBookList")]
        [OutputCache(Duration = 604800)]
        public async Task<IActionResult> GetBookList([FromQuery] BookParams param)
        {
            try
            {
                var cacheKey = $"BookList_{param.Status}_{param.PageSize}_{param.OrderBy}_{param.OrderAs}";
                var cachedData = await _cache.GetStringAsync(cacheKey);

                if (!string.IsNullOrEmpty(cachedData))
                {
                    var books = JsonConvert.DeserializeObject<List<BookListDto>>(cachedData);
                    return Ok(books);
                }

                var result = await mdtr.Send(new ListBooks.Query { Params = param });

                if (result.IsSuccess && result.Value != null)
                {
                    var books = result.Value;

                    var serializedBooks = JsonConvert.SerializeObject(books);

                    await _cache.SetStringAsync(cacheKey, serializedBooks, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7)
                    });

                    return Ok(books);
                }
                else
                {
                    return BadRequest("Failed to retrieve the book list");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [AllowAnonymous]
        [HttpGet("getBook/{id}")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            return HandleResult(await mdtr.Send(new Get.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpGet("getBookByAuthor/{id}")]
        public async Task<IActionResult> GetBookByAuthor(Guid id, [FromQuery] BookParams param)
        {
            return HandlePagedResult(await mdtr.Send(new ListByAuthor.Query { Id = id, Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getBookByCategory/{id}")]
        public async Task<IActionResult> GetBookByCategory(Guid id, [FromQuery] BookParams param)
        {
            return HandlePagedResult(await mdtr.Send(new ListByCategory.Query { Id = id, Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getBookByGenre/{id}")]
        public async Task<IActionResult> GetBookByGenre(Guid id, [FromQuery] BookParams param)
        {
            return HandlePagedResult(await mdtr.Send(new ListByGenre.Query { Id = id, Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getBookByPublisher/{id}")]
        public async Task<IActionResult> GetBookByPublisher(Guid id, [FromQuery] BookParams param)
        {
            return HandlePagedResult(await mdtr.Send(new ListByPublisher.Query { Id = id, Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getBookByTag/{id}")]
        public async Task<IActionResult> GetBookByTag(Guid id, [FromQuery] BookParams param)
        {
            return HandlePagedResult(await mdtr.Send(new ListByTag.Query { Id = id, Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getBookByThematicArea/{id}")]
        public async Task<IActionResult> GetBookByThematicArea(Guid id, [FromQuery] BookParams param)
        {
            return HandlePagedResult(await mdtr.Send(new ListByThematicArea.Query { Id = id, Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getCmdBook/{id}")]
        public async Task<IActionResult> GetCmdBook(Guid id)
        {
            return HandleResult(await mdtr.Send(new GetCmd.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpPost("createBook")]
        public async Task<IActionResult> CreateBook(BookCmdDto book)
        {
            return HandleResult(await mdtr.Send(new Create.Command { Book = book }));
        }

        [AllowAnonymous]
        [HttpPut("updateBook/{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, BookCmdDto book)
        {
            book.Bk_ID = id;
            return HandleResult(await mdtr.Send(new Update.Command { Book = book }));
        }

        [AllowAnonymous]
        [HttpPut("activateBook/{id}")]
        public async Task<IActionResult> ActivateBook(Guid id)
        {

            return HandleResult(await mdtr.Send(new Activate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("activateBooks")]
        public async Task<IActionResult> ActivateBatchBook(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new ActivateBatch.Command { Ids = ids }));
        }

        [AllowAnonymous]
        [HttpPut("deactivateBook/{id}")]
        public async Task<IActionResult> DeactivateBook(Guid id)
        {

            return HandleResult(await mdtr.Send(new Deactivate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("deactivateBooks")]
        public async Task<IActionResult> DeactivateBatchBook(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new DeactivateBatch.Command { Ids = ids }));
        }

        //Suggest Book start here
        [HttpGet("getsuggestbooks")]
        public async Task<IActionResult> GetSuggestedBooks([FromQuery] BookParams param)
        {
            return HandlePagedResult(await mdtr.Send(new ListOfSuggestedBooks.Query { Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getsuggestbooks/{id}")]
        public async Task<IActionResult> GetSuggestedBook(Guid id)
        {
            return HandleResult(await mdtr.Send(new GetSuggestedBook.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpPost("suggestBook")]
        public async Task<IActionResult> SuggestBook(SuggestBookCmd book)
        {
            return HandleResult(await mdtr.Send(new Application.Books.SuggestBook.Command { Book = book }));
        }

        [AllowAnonymous]
        [HttpPut("suggestBook/{bookId}")]
        public async Task<IActionResult> UpdateSuggestedBook(Guid bookId, [FromBody] UpdateSuggestBookCmd updatedBook)
        {
            return HandleResult(await mdtr.Send(new Application.Books.UpdateSuggestedBook.Command { BookId = bookId,  Book = updatedBook }));
        }

        [AllowAnonymous]
        [HttpDelete("removesuggestBook/{bookId}")]
        public async Task<IActionResult> DeleteSuggestBook(Guid bookId)
        {
            return HandleResult(await mdtr.Send(new DeleteSuggestedBook.Command { BookId = bookId }));
        }

        [AllowAnonymous]
        [HttpPut("bookViews/{bookId}")]
        public async Task<IActionResult> BookViews(Guid bookId)
        {
            return HandleResult(await mdtr.Send(new BookViews.Command { BookId = bookId }));
        }


        [AllowAnonymous]
        [HttpGet("getTrendingBooks")]
        public async Task<IActionResult> GetTrendingBooks()
        {
            return HandleResult(await mdtr.Send(new TrendingBooks.Command { }));
        }
    }
}

