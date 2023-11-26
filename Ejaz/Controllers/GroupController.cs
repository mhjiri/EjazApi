using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Groups;
using Application.Groups.Core;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;

namespace Ejaz.Controllers
{

    
    public class GroupController : BaseApiController
    {
        [Authorize]
        [HttpGet("getGroups")]
        public async Task<IActionResult> GetGroups([FromQuery] GroupParams param)
        {
            return HandlePagedResult(await mdtr.Send(new List.Query { Params = param }));
        }

        [Authorize]
        [HttpGet("getGroup/{id}")]
        public async Task<IActionResult> GetGroup(Guid id)
        {
            return HandleResult(await mdtr.Send(new Get.Query { Id = id }));
        }

        //[Authorize]
        //[HttpGet("getCmdGroup/{id}")]
        //public async Task<IActionResult> GetCmdGroup(Guid id)
        //{
        //    return HandleResult(await mdtr.Send(new GetCmd.Query { Id = id }));
        //}

        [Authorize]
        [HttpPost("createGroup")]
        public async Task<IActionResult> CreateGroup(GroupCmdDto group)
        {
            return HandleResult(await mdtr.Send(new Create.Command { Group = group }));
        }

        [Authorize]
        [HttpPut("updateGroup/{id}")]
        public async Task<IActionResult> UpdateGroup(Guid id, GroupCmdDto group)
        {
            group.Gr_ID = id;
            return HandleResult(await mdtr.Send(new Update.Command { Group = group }));
        }

        [Authorize]
        [HttpPut("activateGroup/{id}")]
        public async Task<IActionResult> ActivateGroup(Guid id)
        {
            
            return HandleResult(await mdtr.Send(new Activate.Command { Id = id }));
        }

        [Authorize]
        [HttpPut("activateBatchGroup/{id}")]
        public async Task<IActionResult> ActivateBatchGroup(List<Guid> ids)
        {

            return HandleResult(await mdtr.Send(new ActivateBatch.Command { Ids = ids }));
        }

        [Authorize]
        [HttpPut("deactivateGroup/{id}")]
        public async Task<IActionResult> DeactivateGroup(Guid id)
        {

            return HandleResult(await mdtr.Send(new Deactivate.Command { Id = id }));
        }

        [Authorize]
        [HttpPut("deactivateBatchGroup/{id}")]
        public async Task<IActionResult> DeactivateBatchGroup(List<Guid> ids)
        {

            return HandleResult(await mdtr.Send(new DeactivateBatch.Command { Ids = ids }));
        }



    }
}

