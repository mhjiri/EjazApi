using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Rewards;
using Application.Rewards.Core;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;

namespace Ejaz.Controllers
{

    
    public class RewardController : BaseApiController
    {
        [Authorize]
        [HttpGet("getRewards")]
        public async Task<IActionResult> GetRewards([FromQuery] RewardParams param)
        {
            return HandlePagedResult(await mdtr.Send(new List.Query { Params = param }));
        }

        //[Authorize]
        //[HttpGet("getReward/{id}")]
        //public async Task<IActionResult> GetReward(Guid id)
        //{
        //    return HandleResult(await mdtr.Send(new Get.Query { Id = id }));
        //}

        //[Authorize]
        //[HttpGet("getCmdReward/{id}")]
        //public async Task<IActionResult> GetCmdReward(Guid id)
        //{
        //    return HandleResult(await mdtr.Send(new GetCmd.Query { Id = id }));
        //}

        [Authorize]
        [HttpPost("createReward")]
        public async Task<IActionResult> CreateReward(RewardCmdDto reward)
        {
            return HandleResult(await mdtr.Send(new Create.Command { Reward = reward }));
        }
        

        [Authorize]
        [HttpPut("activateReward/{id}")]
        public async Task<IActionResult> ActivateReward(Guid id)
        {
            
            return HandleResult(await mdtr.Send(new Activate.Command { Id = id }));
        }

        [Authorize]
        [HttpPut("activateBatchReward/{id}")]
        public async Task<IActionResult> ActivateBatchReward(List<Guid> ids)
        {

            return HandleResult(await mdtr.Send(new ActivateBatch.Command { Ids = ids }));
        }

        [Authorize]
        [HttpPut("deactivateReward/{id}")]
        public async Task<IActionResult> DeactivateReward(Guid id)
        {

            return HandleResult(await mdtr.Send(new Deactivate.Command { Id = id }));
        }

        [Authorize]
        [HttpPut("deactivateBatchReward/{id}")]
        public async Task<IActionResult> DeactivateBatchReward(List<Guid> ids)
        {

            return HandleResult(await mdtr.Send(new DeactivateBatch.Command { Ids = ids }));
        }



    }
}

