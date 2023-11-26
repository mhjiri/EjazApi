using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Subscriptions;
using Application.Subscriptions.Core;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;

namespace Ejaz.Controllers
{

    
    public class SubscriptionController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet("getSubscriptions")]
        public async Task<IActionResult> GetSubscriptions([FromQuery] SubscriptionParams param)
        {
            return HandlePagedResult(await mdtr.Send(new List.Query { Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getSubscription/{id}")]
        public async Task<IActionResult> GetSubscription(Guid id)
        {
            return HandleResult(await mdtr.Send(new Get.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpGet("getCmdSubscription/{id}")]
        public async Task<IActionResult> GetCmdSubscription(Guid id)
        {
            return HandleResult(await mdtr.Send(new GetCmd.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpPost("createSubscription")]
        public async Task<IActionResult> CreateSubscription(SubscriptionCmdDto subscription)
        {
            return HandleResult(await mdtr.Send(new Create.Command { Subscription = subscription }));
        }

        [AllowAnonymous]
        [HttpPut("updateSubscription/{id}")]
        public async Task<IActionResult> UpdateSubscription(Guid id, SubscriptionCmdDto subscription)
        {
            subscription.Sb_ID = id;
            return HandleResult(await mdtr.Send(new Update.Command { Subscription = subscription }));
        }

        [AllowAnonymous]
        [HttpPut("activateSubscription/{id}")]
        public async Task<IActionResult> ActivateSubscription(Guid id)
        {
            
            return HandleResult(await mdtr.Send(new Activate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("activateSubscriptions")]
        public async Task<IActionResult> ActivateSubscriptions(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new ActivateBatch.Command { Ids = ids }));
        }

        [AllowAnonymous]
        [HttpPut("deactivateSubscription/{id}")]
        public async Task<IActionResult> DeactivateSubscription(Guid id)
        {

            return HandleResult(await mdtr.Send(new Deactivate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("deactivateSubscriptions")]
        public async Task<IActionResult> DeactivateBatchSubscription(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new DeactivateBatch.Command { Ids = ids }));
        }



    }
}

