using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Profiles;
using Application.Profiles.Core;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Diagnostics;
using Application.Books.Core;
using Application.Authors.Core;

namespace Ejaz.Controllers
{

    
    public class ProfileController : BaseApiController
    {
        
       
        [Authorize]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetProfile()
        {
            return HandleResult(await mdtr.Send(new Get.Query { }));
        }

        //[Authorize]
        //[HttpGet("getProfile/{id}")]
        //public async Task<IActionResult> GetProfile(Guid id)
        //{
        //    return HandleResult(await mdtr.Send(new GetProfile.Query { Id = id }));
        //}

        [AllowAnonymous]
        [HttpGet("getCustomer/{id}")]
        public async Task<IActionResult> GetCustomer(string id)
        {
            return HandleResult(await mdtr.Send(new GetCustomer.Query { Username = id }));
        }


        [AllowAnonymous]
        [HttpGet("getCustomers")]
        public async Task<IActionResult> GetCustomers([FromQuery] ProfileParams param)
        {
            return HandlePagedResult(await mdtr.Send(new ListCustomers.Query { Params = param }));
        }

        [AllowAnonymous]
        [HttpPut("updateCustomer/{id}")]
        public async Task<IActionResult> UpdateCustomer(string id, ProfileQryDto profile)
        {
            profile.Username = id;
            return HandleResult(await mdtr.Send(new UpdateCustomer.Command { Profile = profile }));
        }

        [AllowAnonymous]
        [HttpGet("getTrialUser/{id}")]
        public async Task<IActionResult> GetTrialUser(string id)
        {
            return HandleResult(await mdtr.Send(new GetTrialUser.Query { Username = id }));
        }


        [AllowAnonymous]
        [HttpGet("getTrialUsers")]
        public async Task<IActionResult> GetTrialUsers([FromQuery] ProfileParams param)
        {
            return HandlePagedResult(await mdtr.Send(new ListTrialUsers.Query { Params = param }));
        }

        [AllowAnonymous]
        [HttpPut("updateTrialUser/{id}")]
        public async Task<IActionResult> UpdateTrialUser(string id, ProfileQryDto profile)
        {
            profile.Username = id;
            return HandleResult(await mdtr.Send(new UpdateTrialUser.Command { Profile = profile }));
        }

        [AllowAnonymous]
        [HttpPost("registerTrialUser")]
        public async Task<IActionResult> RegisterTrialUser(ProfileQryDto profile)
        {
            return HandleResult(await mdtr.Send(new CreateTrialUser.Command { Profile = profile }));
        }

        [AllowAnonymous]
        [HttpGet("getAdminUser/{id}")]
        public async Task<IActionResult> GetAdminUser(string id)
        {
            return HandleResult(await mdtr.Send(new GetAdminUser.Query { Username = id }));
        }


        [AllowAnonymous]
        [HttpGet("getAdminUsers")]
        public async Task<IActionResult> GetAdminUsers([FromQuery] ProfileParams param)
        {
            return HandlePagedResult(await mdtr.Send(new ListAdminUsers.Query { Params = param }));
        }

        [AllowAnonymous]
        [HttpPut("updateAdminUser/{id}")]
        public async Task<IActionResult> UpdateAdminUser(string id, ProfileDto profile)
        {
            profile.Username = id;
            return HandleResult(await mdtr.Send(new UpdateAdminUser.Command { Profile = profile }));
        }

        [AllowAnonymous]
        [HttpPost("registerAdminUser")]
        public async Task<IActionResult> RegisterAdminUser(ProfileDto profile)
        {
            return HandleResult(await mdtr.Send(new CreateAdminUser.Command { Profile = profile }));
        }


        [Authorize]
        [HttpPut("updateProfile")]
        public async Task<IActionResult> UpdateProfile(ProfileDto profile)
        {
      
            return HandleResult(await mdtr.Send(new Update.Command { Profile = profile }));
        }

        [AllowAnonymous]
        [HttpPut("updateLanguage")]
        public async Task<IActionResult> UpdateLanguage(ProfileLanguageDto profile)
        { 
            return HandleResult(await mdtr.Send(new UpdateLanguage.Command { Profile = profile }));
        }

        [AllowAnonymous]
        [HttpPut("updateDOB")]
        public async Task<IActionResult> UpdateDOB(ProfileDOBDto profile)
        {
            return HandleResult(await mdtr.Send(new UpdateDOB.Command { Profile = profile }));
        }

        [AllowAnonymous]
        [HttpPut("assignAvatar")]
        public async Task<IActionResult> AssignAvatar(ProfileAvatarDto profile)
        {
            return HandleResult(await mdtr.Send(new AssignAvatar.Command { Profile = profile }));
        }

        [AllowAnonymous]
        [HttpPut("activateProfile/{id}")]
        public async Task<IActionResult> ActivateProfile(string id)
        {

            return HandleResult(await mdtr.Send(new Activate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("deactivateProfile/{id}")]
        public async Task<IActionResult> DeactivateProfile(string id)
        {

            return HandleResult(await mdtr.Send(new Deactivate.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("softdeleteProfile/{id}")]
        public async Task<IActionResult> SoftDeleteProfile(string id)
        {

            return HandleResult(await mdtr.Send(new SoftDelete.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("deleteProfile/{id}")]
        public async Task<IActionResult> DeleteProfile(string id)
        {

            return HandleResult(await mdtr.Send(new Delete.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpPut("assignCategories")]
        public async Task<IActionResult> AssignCategories(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new AssignCategories.Command { Ids = ids }));
        }

        [AllowAnonymous]
        [HttpPut("assignThematicAreas")]
        public async Task<IActionResult> AssignThematicAreas(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new AssignThematicAreas.Command { Ids = ids }));
        }

        [AllowAnonymous]
        [HttpPut("assignGenres")]
        public async Task<IActionResult> AssignGenres(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new AssignGenres.Command { Ids = ids }));
        }

        [AllowAnonymous]
        [HttpPut("assignTags")]
        public async Task<IActionResult> AssignTags(ListGuidDto ids)
        {

            return HandleResult(await mdtr.Send(new AssignTags.Command { Ids = ids }));
        }




    }
}

