using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dashboard;
using Application.Dashboard.Core;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;

namespace Ejaz.Controllers
{

    
    public class DashboardController : BaseApiController
    {
        

        [AllowAnonymous]
        [HttpGet("GetUserData")]
        public async Task<IActionResult> GetUserData()
        {
            return HandleResult(await mdtr.Send(new GetUserData.Query { }));
        }

        [AllowAnonymous]
        [HttpGet("GetBookData")]
        public async Task<IActionResult> GetBookData()
        {
            return HandleResult(await mdtr.Send(new GetBookData.Query { }));
        }

        [AllowAnonymous]
        [HttpGet("GetAuthorData")]
        public async Task<IActionResult> GetAuthorData()
        {
            return HandleResult(await mdtr.Send(new GetAuthorData.Query { }));
        }

        [AllowAnonymous]
        [HttpGet("GetActiveMembersData")]
        public async Task<IActionResult> GetActiveMembersData()
        {
            return HandleResult(await mdtr.Send(new GetActiveMembersData.Query { }));
        }

        [AllowAnonymous]
        [HttpGet("GetNewMembersData")]
        public async Task<IActionResult> GetNewMembersData()
        {
            return HandleResult(await mdtr.Send(new GetNewMembersData.Query { }));
        }

        [AllowAnonymous]
        [HttpGet("GetExpiredMembersData")]
        public async Task<IActionResult> GetExpiredMembersData()
        {
            return HandleResult(await mdtr.Send(new GetExpiredMembersData.Query { }));
        }



    }
}

