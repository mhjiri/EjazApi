using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ThematicAreas;
using Application.ThematicAreas.Core;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;

namespace Ejaz.Controllers
{

    
    public class ThematicAreaController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet("getThematicAreas")]
        public async Task<IActionResult> GetThematicAreas([FromQuery] ThematicAreaParams param)
        {
            return HandlePagedResult(await mdtr.Send(new List.Query { Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getThematicArea/{id}")]
        public async Task<IActionResult> GetThematicArea(Guid id)
        {
            return HandleResult(await mdtr.Send(new Get.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpPost("createThematicArea")]
        public async Task<IActionResult> CreateThematicArea(ThematicAreaCmdDto thematicArea)
        {
            return HandleResult(await mdtr.Send(new Create.Command { ThematicArea = thematicArea }));
        }

        [AllowAnonymous]
        [HttpPut("updateThematicArea/{id}")]
        public async Task<IActionResult> UpdateThematicArea(Guid id, ThematicAreaCmdDto thematicArea)
        {
            thematicArea.Th_ID = id;
            return HandleResult(await mdtr.Send(new Update.Command { ThematicArea = thematicArea }));
        }

        [AllowAnonymous]
        [HttpPut("activateThematicArea/{id}")]
        public async Task<IActionResult> ActivateThematicArea(Guid id)
        {
            
            return HandleResult(await mdtr.Send(new Activate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("deactivateThematicArea/{id}")]
        public async Task<IActionResult> DeactivateThematicArea(Guid id)
        {

            return HandleResult(await mdtr.Send(new Deactivate.Command { Id = id }));
        }

        [Authorize]
        [HttpPut("activateThematicAreas")]
        public async Task<IActionResult> ActivateThematicAreas(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new ActivateBatch.Command { Ids = ids }));
        }

        [Authorize]
        [HttpPut("deactivateThematicAreas")]
        public async Task<IActionResult> DeactivateThematicAreas(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new DeactivateBatch.Command { Ids = ids }));
        }



    }
}

