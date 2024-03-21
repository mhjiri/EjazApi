using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.BannerLocations;
using Application.BannerLocations.Core;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;

namespace Ejaz.Controllers
{

    
    public class BannerLocationController : BaseApiController
    {
        [Authorize]
        [HttpGet("getBannerLocations")]
        public async Task<IActionResult> GetBannerLocations([FromQuery] BannerLocationParams param)
        {
            return HandlePagedResult(await mdtr.Send(new List.Query { Params = param }));
        }

        [Authorize]
        [HttpGet("getBannerLocation/{id}")]
        public async Task<IActionResult> GetBannerLocation(Guid id)
        {
            return HandleResult(await mdtr.Send(new Get.Query { Id = id }));
        }

        [Authorize]
        [HttpPost("createBannerLocation")]
        public async Task<IActionResult> CreateBannerLocation(BannerLocationCmdDto bannerLocation)
        {
            return HandleResult(await mdtr.Send(new Create.Command { BannerLocation = bannerLocation }));
        }

        [Authorize]
        [HttpPut("updateBannerLocation/{id}")]
        public async Task<IActionResult> UpdateBannerLocation(Guid id, BannerLocationCmdDto bannerLocation)
        {
            bannerLocation.Bl_ID = id;
            return HandleResult(await mdtr.Send(new Update.Command { BannerLocation = bannerLocation }));
        }

        [Authorize]
        [HttpPut("activateBannerLocation/{id}")]
        public async Task<IActionResult> ActivateBannerLocation(Guid id)
        {
            
            return HandleResult(await mdtr.Send(new Activate.Command { Id = id }));
        }

        [Authorize]
        [HttpPut("activateBatchBannerLocation/{id}")]
        public async Task<IActionResult> ActivateBatchBannerLocation(List<Guid> ids)
        {

            return HandleResult(await mdtr.Send(new ActivateBatch.Command { Ids = ids }));
        }

        [Authorize]
        [HttpPut("deactivateBannerLocation/{id}")]
        public async Task<IActionResult> DeactivateBannerLocation(Guid id)
        {

            return HandleResult(await mdtr.Send(new Deactivate.Command { Id = id }));
        }

        [Authorize]
        [HttpPut("deactivateBatchBannerLocation/{id}")]
        public async Task<IActionResult> DeactivateBatchBannerLocation(List<Guid> ids)
        {

            return HandleResult(await mdtr.Send(new DeactivateBatch.Command { Ids = ids }));
        }



    }
}

