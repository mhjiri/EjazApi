using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.PaymentMethods;
using Application.PaymentMethods.Core;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;

namespace Ejaz.Controllers
{

    
    public class PaymentMethodController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet("getPaymentMethods")]
        public async Task<IActionResult> GetPaymentMethods([FromQuery] PaymentMethodParams param)
        {
            return HandlePagedResult(await mdtr.Send(new List.Query { Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getPaymentMethod/{id}")]
        public async Task<IActionResult> GetPaymentMethod(Guid id)
        {
            return HandleResult(await mdtr.Send(new Get.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpGet("getCmdPaymentMethod/{id}")]
        public async Task<IActionResult> GetCmdPaymentMethod(Guid id)
        {
            return HandleResult(await mdtr.Send(new GetCmd.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpPost("createPaymentMethod")]
        public async Task<IActionResult> CreatePaymentMethod(PaymentMethodCmdDto paymentMethod)
        {
            return HandleResult(await mdtr.Send(new Create.Command { PaymentMethod = paymentMethod }));
        }

        [AllowAnonymous]
        [HttpPut("updatePaymentMethod/{id}")]
        public async Task<IActionResult> UpdatePaymentMethod(Guid id, PaymentMethodCmdDto paymentMethod)
        {
            paymentMethod.Py_ID = id;
            return HandleResult(await mdtr.Send(new Update.Command { PaymentMethod = paymentMethod }));
        }

        [AllowAnonymous]
        [HttpPut("activatePaymentMethod/{id}")]
        public async Task<IActionResult> ActivatePaymentMethod(Guid id)
        {
            
            return HandleResult(await mdtr.Send(new Activate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("activatePaymentMethods")]
        public async Task<IActionResult> ActivatePaymentMethods(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new ActivateBatch.Command { Ids = ids }));
        }

        [AllowAnonymous]
        [HttpPut("deactivatePaymentMethod/{id}")]
        public async Task<IActionResult> DeactivatePaymentMethod(Guid id)
        {

            return HandleResult(await mdtr.Send(new Deactivate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("deactivatePaymentMethods")]
        public async Task<IActionResult> DeactivateBatchPaymentMethod(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new DeactivateBatch.Command { Ids = ids }));
        }



    }
}

