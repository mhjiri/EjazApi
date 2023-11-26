using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Addresses;
using Application.Addresses.Core;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;

namespace Ejaz.Controllers
{

    
    public class AddressController : BaseApiController
    {
        [Authorize]
        [HttpPut("getAddress/{id}")]
        public async Task<IActionResult> GetAddress(Guid id)
        {

            return HandleResult(await mdtr.Send(new Get.Query { Id = id }));
        }

        [Authorize]
        [HttpPost("createAddress")]
        public async Task<IActionResult> CreateAddress(Address address)
        {
            return HandleResult(await mdtr.Send(new Create.Command { Address = address }));
        }

        [Authorize]
        [HttpPut("updateAddress/{id}")]
        public async Task<IActionResult> UpdateAddress(Guid id, Address address)
        {
            address.Ad_ID = id;
            return HandleResult(await mdtr.Send(new Update.Command { Address = address }));
        }

        [Authorize]
        [HttpPut("activateAddress/{id}")]
        public async Task<IActionResult> ActivateAddress(Guid id)
        {
            
            return HandleResult(await mdtr.Send(new Activate.Command { Id = id }));
        }

        [Authorize]
        [HttpPut("deactivateAddress/{id}")]
        public async Task<IActionResult> DeactivateAddress(Guid id)
        {

            return HandleResult(await mdtr.Send(new Deactivate.Command { Id = id }));
        }

        [Authorize]
        [HttpPut("setDefaultAddress/{id}")]
        public async Task<IActionResult> SetDefaultAddress(Guid id)
        {

            return HandleResult(await mdtr.Send(new SetDefault.Command { Id = id }));
        }



    }
}

