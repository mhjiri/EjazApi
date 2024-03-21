using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Banners;
using Application.Banners.Core;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;

namespace Ejaz.Controllers
{

    
    public class BannerController : BaseApiController
    {
        [Authorize]
        [HttpGet("getBanners")]
        public async Task<IActionResult> GetBanners([FromQuery] BannerParams param)
        {
            return HandlePagedResult(await mdtr.Send(new List.Query { Params = param }));
        }

        [Authorize]
        [HttpGet("getBanner/{id}")]
        public async Task<IActionResult> GetBanner(Guid id)
        {
            return HandleResult(await mdtr.Send(new Get.Query { Id = id }));
        }

        [Authorize]
        [HttpGet("getBannerByLocation/{id}")]
        public async Task<IActionResult> GetBannerByLocation(Guid id)
        {
            return HandleResult(await mdtr.Send(new GetByLocation.Query { Id = id }));
        }



        [Authorize]
        [HttpGet("getCmdBanner/{id}")]
        public async Task<IActionResult> GetCmdBanner(Guid id)
        {
            return HandleResult(await mdtr.Send(new GetCmd.Query { Id = id }));
        }

        [Authorize]
        [HttpPost("createBanner")]
        public async Task<IActionResult> CreateBanner(BannerCmdDto banner)
        {
            return HandleResult(await mdtr.Send(new Create.Command { Banner = banner }));
        }

        [Authorize]
        [HttpPut("updateBanner/{id}")]
        public async Task<IActionResult> UpdateBanner(Guid id, BannerCmdDto banner)
        {
            banner.Bn_ID = id;
            return HandleResult(await mdtr.Send(new Update.Command { Banner = banner }));
        }

        [Authorize]
        [HttpPut("activateBanner/{id}")]
        public async Task<IActionResult> ActivateBanner(Guid id)
        {
            
            return HandleResult(await mdtr.Send(new Activate.Command { Id = id }));
        }

        [Authorize]
        [HttpPut("activateBatchBanner/{id}")]
        public async Task<IActionResult> ActivateBatchBanner(List<Guid> ids)
        {

            return HandleResult(await mdtr.Send(new ActivateBatch.Command { Ids = ids }));
        }

        [Authorize]
        [HttpPut("deactivateBanner/{id}")]
        public async Task<IActionResult> DeactivateBanner(Guid id)
        {

            return HandleResult(await mdtr.Send(new Deactivate.Command { Id = id }));
        }

        [Authorize]
        [HttpPut("deactivateBatchBanner/{id}")]
        public async Task<IActionResult> DeactivateBatchBanner(List<Guid> ids)
        {

            return HandleResult(await mdtr.Send(new DeactivateBatch.Command { Ids = ids }));
        }



    }
}

