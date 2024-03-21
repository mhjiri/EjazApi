using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Publishers;
using Application.Publishers.Core;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;

namespace Ejaz.Controllers
{

    
    public class PublisherController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet("getPublishers")]
        public async Task<IActionResult> GetPublishers([FromQuery] PublisherParams param)
        {
            return HandlePagedResult(await mdtr.Send(new List.Query { Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getPublisher/{id}")]
        public async Task<IActionResult> GetPublisher(Guid id)
        {
            return HandleResult(await mdtr.Send(new Get.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpGet("getCmdPublisher/{id}")]
        public async Task<IActionResult> GetCmdPublisher(Guid id)
        {
            return HandleResult(await mdtr.Send(new GetCmd.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpPost("createPublisher")]
        public async Task<IActionResult> CreatePublisher(PublisherCmdDto publisher)
        {
            return HandleResult(await mdtr.Send(new Create.Command { Publisher = publisher }));
        }

        [AllowAnonymous]
        [HttpPut("updatePublisher/{id}")]
        public async Task<IActionResult> UpdatePublisher(Guid id, PublisherCmdDto publisher)
        {
            publisher.Pb_ID = id;
            return HandleResult(await mdtr.Send(new Update.Command { Publisher = publisher }));
        }

        [AllowAnonymous]
        [HttpPut("activatePublisher/{id}")]
        public async Task<IActionResult> ActivatePublisher(Guid id)
        {
            
            return HandleResult(await mdtr.Send(new Activate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("activatePublishers")]
        public async Task<IActionResult> ActivatePublishers(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new ActivateBatch.Command { Ids = ids }));
        }

        [AllowAnonymous]
        [HttpPut("deactivatePublisher/{id}")]
        public async Task<IActionResult> DeactivatePublisher(Guid id)
        {

            return HandleResult(await mdtr.Send(new Deactivate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("deactivatePublishers")]
        public async Task<IActionResult> DeactivatePublishers(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new DeactivateBatch.Command { Ids = ids }));
        }



    }
}

