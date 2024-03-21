using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Countries;
using Application.Countries.Core;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;

namespace Ejaz.Controllers
{

    
    public class CountryController : BaseApiController
    {
        [Authorize]
        [HttpGet("getCountries")]
        public async Task<IActionResult> GetCountries([FromQuery] CountryParams param)
        {
            return HandlePagedResult(await mdtr.Send(new List.Query { Params = param }));
        }

        //[Authorize]
        //[HttpGet("getCountry/{id}")]
        //public async Task<IActionResult> GetCountry(Guid id)
        //{
        //    return HandleResult(await mdtr.Send(new Get.Query { Id = id }));
        //}
        

        //[Authorize]
        //[HttpGet("getCmdCountry/{id}")]
        //public async Task<IActionResult> GetCmdCountry(Guid id)
        //{
        //    return HandleResult(await mdtr.Send(new GetCmd.Query { Id = id }));
        //}

        [Authorize]
        [HttpPost("createCountry")]
        public async Task<IActionResult> CreateCountry(Country country)
        {
            return HandleResult(await mdtr.Send(new Create.Command { Country = country }));
        }

        [Authorize]
        [HttpPut("updateCountry/{id}")]
        public async Task<IActionResult> UpdateCountry(Guid id, Country country)
        {
            country.Cn_ID = id;
            return HandleResult(await mdtr.Send(new Update.Command { Country = country }));
        }

        [Authorize]
        [HttpPut("activateCountry/{id}")]
        public async Task<IActionResult> ActivateCountry(Guid id)
        {
            
            return HandleResult(await mdtr.Send(new Activate.Command { Id = id }));
        }

        [Authorize]
        [HttpPut("activateBatchCountry/{id}")]
        public async Task<IActionResult> ActivateBatchCountry(List<Guid> ids)
        {

            return HandleResult(await mdtr.Send(new ActivateBatch.Command { Ids = ids }));
        }

        [Authorize]
        [HttpPut("deactivateCountry/{id}")]
        public async Task<IActionResult> DeactivateCountry(Guid id)
        {

            return HandleResult(await mdtr.Send(new Deactivate.Command { Id = id }));
        }

        [Authorize]
        [HttpPut("deactivateBatchCountry/{id}")]
        public async Task<IActionResult> DeactivateBatchCountry(List<Guid> ids)
        {

            return HandleResult(await mdtr.Send(new DeactivateBatch.Command { Ids = ids }));
        }



    }
}

