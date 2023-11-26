using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Genres;
using Application.Genres.Core;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;

namespace Ejaz.Controllers
{

    
    public class GenreController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet("getGenres")]
        public async Task<IActionResult> GetGenres([FromQuery] GenreParams param)
        {
            return HandlePagedResult(await mdtr.Send(new List.Query { Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getGenre/{id}")]
        public async Task<IActionResult> GetGenre(Guid id)
        {
            return HandleResult(await mdtr.Send(new Get.Query { Id = id }));
        }


        //[Authorize]
        //[HttpGet("getCmdGenre/{id}")]
        //public async Task<IActionResult> GetCmdGenre(Guid id)
        //{
        //    return HandleResult(await mdtr.Send(new GetCmd.Query { Id = id }));
        //}

        [AllowAnonymous]
        [HttpPost("createGenre")]
        public async Task<IActionResult> CreateGenre(GenreCmdDto genre)
        {
            return HandleResult(await mdtr.Send(new Create.Command { Genre = genre }));
        }

        [AllowAnonymous]
        [HttpPut("updateGenre/{id}")]
        public async Task<IActionResult> UpdateGenre(Guid id, GenreCmdDto genre)
        {
            genre.Gn_ID = id;
            return HandleResult(await mdtr.Send(new Update.Command { Genre = genre }));
        }

        [AllowAnonymous]
        [HttpPut("activateGenre/{id}")]
        public async Task<IActionResult> ActivateGenre(Guid id)
        {
            
            return HandleResult(await mdtr.Send(new Activate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("activateGenres")]
        public async Task<IActionResult> ActivateBatchGenre(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new ActivateBatch.Command { Ids = ids }));
        }

        [AllowAnonymous]
        [HttpPut("deactivateGenre/{id}")]
        public async Task<IActionResult> DeactivateGenre(Guid id)
        {

            return HandleResult(await mdtr.Send(new Deactivate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("deactivateGenres")]
        public async Task<IActionResult> DeactivateBatchGenre(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new DeactivateBatch.Command { Ids = ids }));
        }



    }
}

