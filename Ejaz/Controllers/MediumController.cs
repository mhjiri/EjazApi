using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Application.Media;
using Application.Media.Core;

namespace Ejaz.Controllers
{
    public class MediumController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet("getMedium/{id}")]
        public async Task<IActionResult> GetMedium(Guid id)
        {
            return HandleResult(await mdtr.Send(new Get.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpGet("getImage/{id}")]
        public async Task<IActionResult> GetImage(Guid id)
        {
            return HandleFileResult<Medium>(await mdtr.Send(new GetImage.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpGet("getAudio/{id}")]
        public async Task<IActionResult> GetAudio(Guid id)
        {
            return HandleFileResult<Medium>(await mdtr.Send(new GetImage.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpPost("createMedium")]
        public async Task<IActionResult> CreateMedium([FromForm] MediumFileCmdDto medium)
        {
            return HandleResult(await mdtr.Send(new Create.Command { Medium = medium }));

        }

        [AllowAnonymous]
        [HttpPut("updateMedium/{id}")]
        public async Task<IActionResult> UpdateThematicArea(Guid id, MediumCmdDto medium)
        {
            medium.Md_ID = id;
            return HandleResult(await mdtr.Send(new Update.Command { Medium = medium }));
        }

        [AllowAnonymous]
        [HttpPut("activateMedium/{id}")]
        public async Task<IActionResult> ActivateMedium(Guid id)
        {
            return HandleResult(await mdtr.Send(new Activate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("deactivateMedium/{id}")]
        public async Task<IActionResult> DeactivateMedium(Guid id)
        {
            return HandleResult(await mdtr.Send(new Deactivate.Command { Id = id }));
        }



    }
}

