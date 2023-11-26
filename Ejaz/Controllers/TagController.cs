using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Tags;
using Application.Tags.Core;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;

namespace Ejaz.Controllers
{

    
    public class TagController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet("getTags")]
        public async Task<IActionResult> GetTags([FromQuery] TagParams param)
        {
            return HandlePagedResult(await mdtr.Send(new List.Query { Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getTag/{id}")]
        public async Task<IActionResult> GetTag(Guid id)
        {
            return HandleResult(await mdtr.Send(new Get.Query { Id = id }));
        }

        //[Authorize]
        //[HttpGet("getCmdTag/{id}")]
        //public async Task<IActionResult> GetCmdTag(Guid id)
        //{
        //    return HandleResult(await mdtr.Send(new GetCmd.Query { Id = id }));
        //}

        [AllowAnonymous]
        [HttpPost("createTag")]
        public async Task<IActionResult> CreateTag(TagCmdDto tag)
        {
            return HandleResult(await mdtr.Send(new Create.Command { Tag = tag }));
        }

        [AllowAnonymous]
        [HttpPut("updateTag/{id}")]
        public async Task<IActionResult> UpdateTag(Guid id, TagCmdDto tag)
        {
            tag.Tg_ID = id;
            return HandleResult(await mdtr.Send(new Update.Command { Tag = tag }));
        }

        [AllowAnonymous]
        [HttpPut("activateTag/{id}")]
        public async Task<IActionResult> ActivateTag(Guid id)
        {
            
            return HandleResult(await mdtr.Send(new Activate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("activateTags")]
        public async Task<IActionResult> ActivateBatchTag(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new ActivateBatch.Command { Ids = ids }));
        }

        [AllowAnonymous]
        [HttpPut("deactivateTag/{id}")]
        public async Task<IActionResult> DeactivateTag(Guid id)
        {

            return HandleResult(await mdtr.Send(new Deactivate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("deactivateTags")]
        public async Task<IActionResult> DeactivateBatchTag(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new DeactivateBatch.Command { Ids = ids }));
        }



    }
}

