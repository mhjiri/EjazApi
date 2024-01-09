using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Payments;
using Application.Payments.Core;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;

namespace Ejaz.Controllers
{
   
    public class PaymentController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet("getPayments")]
        public async Task<IActionResult> GetPayments([FromQuery] PaymentParams param)
        {
            return HandlePagedResult(await mdtr.Send(new List.Query { Params = param }));
        }

        [AllowAnonymous]
        [HttpGet("getPayment/{id}")]
        public async Task<IActionResult> GetPayment(Guid id)
        {
            return HandleResult(await mdtr.Send(new Get.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpGet("getCmdPayment/{id}")]
        public async Task<IActionResult> GetCmdPayment(Guid id)
        {
            return HandleResult(await mdtr.Send(new GetCmd.Query { Id = id }));
        }

        [AllowAnonymous]
        [HttpPost("doPayment")]
        public async Task<IActionResult> DoPayment(PaymentCmdDto payment)
        {
            return HandleResult(await mdtr.Send(new Create.Command { Payment = payment }));
        }

        [AllowAnonymous]
        [HttpPut("activatePayment/{id}")]
        public async Task<IActionResult> ActivatePayment(Guid id)
        {
            
            return HandleResult(await mdtr.Send(new Activate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("activatePayments")]
        public async Task<IActionResult> ActivatePayments(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new ActivateBatch.Command { Ids = ids }));
        }

        [AllowAnonymous]
        [HttpPut("deactivatePayment/{id}")]
        public async Task<IActionResult> DeactivatePayment(Guid id)
        {

            return HandleResult(await mdtr.Send(new Deactivate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("deactivatePayments")]
        public async Task<IActionResult> DeactivateBatchPayment(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new DeactivateBatch.Command { Ids = ids }));
        }

        [AllowAnonymous]
        [HttpPost("giftPayment")]
        public async Task<IActionResult> GiftPayment(GiftPaymentCmdDto giftPayment)
        {
            return HandleResult(await mdtr.Send(new CreateGift.Command { GiftPayment = giftPayment }));
        }

        [AllowAnonymous]
        [HttpPut("activateGiftPayment/{couponCode}")]
        public async Task<IActionResult> ActivateGiftPayment(string couponCode)
        {
            if (string.IsNullOrEmpty(couponCode))
            {
                return BadRequest("Please enter a valid Coupon code");
            }
            else
            {
                return HandleResult(await mdtr.Send(new ActivateGift.Command { CouponCode = couponCode }));
            }
        }

    }
}

