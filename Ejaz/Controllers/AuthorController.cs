using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Authors;
using Application.Authors.Core;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;

namespace Ejaz.Controllers
{

    
    public class AuthorController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet("getAuthors")]
        public async Task<IActionResult> GetAuthors([FromQuery] AuthorParams param)
        {
            return HandlePagedResult(await mdtr.Send(new List.Query { Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getAuthor/{id}")]
        public async Task<IActionResult> GetAuthor(Guid id)
        {
            return HandleResult(await mdtr.Send(new Get.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpGet("getCmdAuthor/{id}")]
        public async Task<IActionResult> GetCmdAuthor(Guid id)
        {
            return HandleResult(await mdtr.Send(new GetCmd.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpPost("createAuthor")]
        public async Task<IActionResult> CreateAuthor(AuthorCmdDto author)
        {
            return HandleResult(await mdtr.Send(new Create.Command { Author = author }));
        }

        [AllowAnonymous]
        [HttpPut("updateAuthor/{id}")]
        public async Task<IActionResult> UpdateAuthor(Guid id, AuthorCmdDto author)
        {
            author.At_ID = id;
            return HandleResult(await mdtr.Send(new Update.Command { Author = author }));
        }

        [AllowAnonymous]
        [HttpPut("activateAuthor/{id}")]
        public async Task<IActionResult> ActivateAuthor(Guid id)
        {
            
            return HandleResult(await mdtr.Send(new Activate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("activateAuthors")]
        public async Task<IActionResult> ActivateAuthors(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new ActivateBatch.Command { Ids = ids }));
        }

        [AllowAnonymous]
        [HttpPut("deactivateAuthor/{id}")]
        public async Task<IActionResult> DeactivateAuthor(Guid id)
        {

            return HandleResult(await mdtr.Send(new Deactivate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("deactivateAuthors")]
        public async Task<IActionResult> DeactivateBatchAuthor(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new DeactivateBatch.Command { Ids = ids }));
        }



    }
}

