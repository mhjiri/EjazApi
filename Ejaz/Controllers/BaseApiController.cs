using Ejaz.Extensions;
using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Application.Media.Core;
using Microsoft.Extensions.Caching.Memory;

namespace Ejaz.Controllers
{
    [ApiController]
    [Route("ejaz/v1/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mdtr;
        private IMemoryCache _cache;

        protected IMediator mdtr => _mdtr ??=
            HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) return NotFound();

            if (result.IsSuccess && result.Value != null)
                return Ok(result.Value);

            if (result.IsSuccess && result.Value == null)
                return NotFound();

            return BadRequest(result.Error);
        }

        //protected ActionResult HandleFileResult<T>(Result<Medium> result)
        //{
        //    if (result == null) return NotFound();

        //    if (result != null && result.Value.Md_Medium != null && !String.IsNullOrEmpty(result.Value.Md_FileType))
        //    {
        //        return File(result.Value.Md_Medium, result.Value.Md_FileType);
        //    }


        //    return BadRequest();
        //}

        protected ActionResult HandleFileResult<T>(Result<Medium> result)
        {
            if (result == null) return NotFound();

            if (result.Value != null && !string.IsNullOrEmpty(result.Value.Md_URL))
            {
                // Redirect to the Firebase Storage URL
                return Redirect(result.Value.Md_URL);
            }


            return BadRequest();
        }




        protected ActionResult HandlePagedResult<T>(Result<PagedList<T>> result)
        {
            if (result == null) return NotFound();
            if (result.IsSuccess && result.Value != null)
            {
                Response.AddPaginationHeader(result.Value.CurrentPage, result.Value.PageSize,
                    result.Value.TotalCount, result.Value.TotalPages);
                return Ok(result.Value);
            }

            if (result.IsSuccess && result.Value == null)
                return NotFound();
            return BadRequest(result.Error);
        }
    }
}