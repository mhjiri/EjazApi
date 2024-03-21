using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Categories;
using Application.Categories.Core;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;

namespace Ejaz.Controllers
{

    
    public class CategoryController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet("getCategories")]
        public async Task<IActionResult> GetCategories([FromQuery] CategoryParams param)
        {
            return HandlePagedResult(await mdtr.Send(new List.Query { Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getCategory/{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            return HandleResult(await mdtr.Send(new Get.Query { Id = id }));
        }


        //[Authorize]
        //[HttpGet("getCmdCategory/{id}")]
        //public async Task<IActionResult> GetCmdCategory(Guid id)
        //{
        //    return HandleResult(await mdtr.Send(new GetCmd.Query { Id = id }));
        //}

        [AllowAnonymous]
        [HttpPost("createCategory")]
        public async Task<IActionResult> CreateCategory(CategoryCmdDto category)
        {
            return HandleResult(await mdtr.Send(new Create.Command { Category = category }));
        }

        [AllowAnonymous]
        [HttpPut("updateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryCmdDto category)
        {
            category.Ct_ID = id;
            return HandleResult(await mdtr.Send(new Update.Command { Category = category }));
        }

        [AllowAnonymous]
        [HttpPut("activateCategory/{id}")]
        public async Task<IActionResult> ActivateCategory(Guid id)
        {
            
            return HandleResult(await mdtr.Send(new Activate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("activateCategories")]
        public async Task<IActionResult> ActivateCategories(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new ActivateBatch.Command { Ids = ids }));
        }

        [AllowAnonymous]
        [HttpPut("deactivateCategory/{id}")]
        public async Task<IActionResult> DeactivateCategory(Guid id)
        {

            return HandleResult(await mdtr.Send(new Deactivate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("deactivateCategories")]
        public async Task<IActionResult> DeactivateBatchCategory(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new DeactivateBatch.Command { Ids = ids }));
        }



    }
}

