using System.Security.Claims;
using Ejaz.DTOs;
using Ejaz.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json.Linq;
using System.Threading;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;

namespace Ejaz.Controllers
{
    [ApiController]
    [Route("ejaz/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, TokenService tokenService, RoleManager<IdentityRole> roleManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpPost("oauth")]
        public async Task<ActionResult<UserDto>> OAuth(OAuthDto oAuthDto)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.NormalizedEmail== oAuthDto.Email.ToUpper() && x.Us_Active == true && !x.Us_Deleted);

            if (user == null) return Unauthorized();

            var resultLogin = await _userManager.CheckPasswordAsync(user, oAuthDto.Password);

            if (!resultLogin)
            {
                FirebaseToken firebaseDecodedToken = await FirebaseAuth.DefaultInstance
                                            .VerifyIdTokenAsync(oAuthDto.FirebaseUID);
                string firebaseUID = firebaseDecodedToken.Uid;
                UserRecord firebaseUser = await FirebaseAuth.DefaultInstance.GetUserAsync(firebaseUID);

                if (oAuthDto.Email != firebaseUser.Email) return Unauthorized();
            }

            if(!String.IsNullOrEmpty(oAuthDto.FirebaseUID)) user.Us_FirebaseUID = oAuthDto.FirebaseUID;
            if (!String.IsNullOrEmpty(oAuthDto.FirebaseToken)) user.Us_FirebaseToken = oAuthDto.FirebaseToken;
            var result = await _userManager.UpdateAsync(user);

            
            return CreateUserObject(user);
        }

        [AllowAnonymous]
        [HttpPost("oauthfree")]
        public async Task<ActionResult<UserDto>> OAuthFree(OAuthDto oAuthDto)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.NormalizedEmail == oAuthDto.Email.ToUpper() && x.Us_Active == true && !x.Us_Deleted);

            if (user == null) return Unauthorized();

            var resultLogin = await _userManager.CheckPasswordAsync(user, oAuthDto.Password);

            if (!resultLogin)
            {
                //FirebaseToken firebaseDecodedToken = await FirebaseAuth.DefaultInstance
                //                            .VerifyIdTokenAsync(oAuthDto.FirebaseUID);
                //string firebaseUID = firebaseDecodedToken.Uid;
                //UserRecord firebaseUser = await FirebaseAuth.DefaultInstance.GetUserAsync(firebaseUID);

                //if (oAuthDto.Email != firebaseUser.Email) return Unauthorized();
                return Unauthorized("Incorrect Password. Please check the password and try again later.");
            }

            if (!String.IsNullOrEmpty(oAuthDto.FirebaseUID)) user.Us_FirebaseUID = oAuthDto.FirebaseUID;
            if (!String.IsNullOrEmpty(oAuthDto.FirebaseToken)) user.Us_FirebaseToken = oAuthDto.FirebaseToken;
            var result = await _userManager.UpdateAsync(user);


            return CreateUserObject(user);
        }

        [AllowAnonymous]
        [HttpPost("isUserExist")]
        public async Task<ActionResult<Boolean>> IsUserExist(UserKeyDto userKeyDto)
        {
            
            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => ((!String.IsNullOrEmpty(userKeyDto.Email) && x.NormalizedEmail == userKeyDto.Email.ToUpper()) || (!String.IsNullOrEmpty(userKeyDto.PhoneNumber) && x.PhoneNumber == userKeyDto.PhoneNumber.ToUpper()) || (!String.IsNullOrEmpty(userKeyDto.Username) && x.UserName.ToLower() == userKeyDto.Username.ToLower())) && x.Us_Active == true && !x.Us_Deleted);


            if (user == null) return false;
            else return true;
        }

        [Authorize]
        [HttpPost("updatePassword")]
        public async Task<ActionResult<UserDto>> UpdatePasword(UserUpdatePasswordDto oAuthDto)
        {
            //var user = await _userManager.Users
            //    .FirstOrDefaultAsync(x => (x.NormalizedEmail == oAuthDto.Email.ToUpper() || x.PhoneNumber == oAuthDto.PhoneNumber || x.UserName.ToUpper() == oAuthDto.Username.ToUpper()) && x.Us_Active == true);

            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.UserName == User.FindFirstValue(ClaimTypes.Name) && !x.Us_Deleted);

            if (user == null) return Unauthorized();

            var resultLogin = await _userManager.CheckPasswordAsync(user, oAuthDto.CurrentPassword);

            if (resultLogin)
            {
                var result = await _userManager.ChangePasswordAsync(user, oAuthDto.CurrentPassword, oAuthDto.NewPassword);

                
                return CreateUserObject(user);
            } else return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("updateForgotPassword")]
        public async Task<ActionResult<UserDto>> UpdateForgotPasword(UserUpdatePasswordDto oAuthDto)
        {
            AppUser user = null; ;
            if (!String.IsNullOrEmpty(oAuthDto.Email)) user = await _userManager.Users.FirstOrDefaultAsync(x => (x.NormalizedEmail == oAuthDto.Email.ToUpper()) && !x.Us_Deleted && x.Us_Active == true);
            else if(!String.IsNullOrEmpty(oAuthDto.PhoneNumber)) user = await _userManager.Users.FirstOrDefaultAsync(x => (x.PhoneNumber == oAuthDto.PhoneNumber) && !x.Us_Deleted && x.Us_Active == true);
            else if(!String.IsNullOrEmpty(oAuthDto.Username)) user = await _userManager.Users.FirstOrDefaultAsync(x => (x.NormalizedEmail == oAuthDto.Username.ToUpper()) && !x.Us_Deleted && x.Us_Active == true);
            //var user = await _userManager.Users
            //    .FirstOrDefaultAsync(x => x.UserName == User.FindFirstValue(ClaimTypes.Name) && !x.Us_Deleted);

            if (user == null) return Unauthorized();

            if (!String.IsNullOrEmpty(oAuthDto.NewPassword))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                IdentityResult res = await _userManager.ResetPasswordAsync(user, token, oAuthDto.NewPassword);

                if(res.Succeeded) return CreateUserObject(user);
            } 
            
            return BadRequest();
        }

        [Authorize]
        [HttpPost("updatePhoneNumber")]
        public async Task<ActionResult<UserDto>> UpdatePhoneNumber(UserUpdateKeyDto oAuthDto)
        {
            //var user = await _userManager.Users
            //    .FirstOrDefaultAsync(x => (x.NormalizedEmail == oAuthDto.Email.ToUpper() || x.PhoneNumber == oAuthDto.PhoneNumber || x.UserName.ToUpper() == oAuthDto.Username.ToUpper()) && x.Us_Active == true);

            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.UserName == User.FindFirstValue(ClaimTypes.Name) && !x.Us_Deleted);

            if (user == null) return Unauthorized();

            if (!String.IsNullOrEmpty(oAuthDto.NewPhoneNumber)) user.PhoneNumber = oAuthDto.NewPhoneNumber;
            var result = await _userManager.UpdateAsync(user);


            return CreateUserObject(user);
        }

        [Authorize]
        [HttpPost("updateEmail")]
        public async Task<ActionResult<UserDto>> UpdateEmail(UserUpdateKeyDto oAuthDto)
        {
            //var user = await _userManager.Users
            //    .FirstOrDefaultAsync(x => (x.NormalizedEmail == oAuthDto.Email.ToUpper() || x.PhoneNumber == oAuthDto.PhoneNumber || x.UserName.ToUpper() == oAuthDto.Username.ToUpper()) && x.Us_Active == true);

            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.UserName == User.FindFirstValue(ClaimTypes.Name) && !x.Us_Deleted);

            if (user == null) return Unauthorized();

            if (!String.IsNullOrEmpty(oAuthDto.NewEmail)) {
                user.Email = oAuthDto.NewEmail.ToLower();
                user.NormalizedEmail = oAuthDto.NewEmail.ToUpper();
            }
            var result = await _userManager.UpdateAsync(user);


            return CreateUserObject(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.NormalizedEmail == loginDto.Email.ToUpper() && x.Us_Active == true && !x.Us_Deleted);

            if (user == null) return Unauthorized();

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (result)
            {
                return CreateUserObject(user);
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("registerOAuth")]
        public async Task<ActionResult<UserDto>> RegisterOAuth(RegisterOAuthDto registerOAuthDto)
        {
            

            if (await _userManager.Users.AnyAsync(x => x.UserName == registerOAuthDto.Username && !x.Us_Deleted))
            {
                ModelState.AddModelError("username", "Username taken");
                return ValidationProblem();
            }

            if (await _userManager.Users.AnyAsync(x => x.Email == registerOAuthDto.Email && !x.Us_Deleted))
            {
                ModelState.AddModelError("email", "Email taken");
                return ValidationProblem();
            }

            if (await _userManager.Users.AnyAsync(x => x.PhoneNumber == registerOAuthDto.PhoneNumber && !x.Us_Deleted))
            {
                ModelState.AddModelError("phonenumber", "PhoneNumber taken");
                return ValidationProblem();
            }

            var user = new AppUser
            {
                Us_DisplayName = registerOAuthDto.DisplayName,
                Email = registerOAuthDto.Email,
                UserName = registerOAuthDto.Username,
                PhoneNumber = registerOAuthDto.PhoneNumber,
                Us_language = registerOAuthDto.Language,
                Us_FirebaseUID = registerOAuthDto.FirebaseUID,
                Us_FirebaseToken = registerOAuthDto.FirebaseToken,
                Us_Active = true,
                Us_Customer = true
                
            };

            FirebaseToken firebaseDecodedToken = await FirebaseAuth.DefaultInstance
                                            .VerifyIdTokenAsync(registerOAuthDto.FirebaseUID);
            string firebaseUID = firebaseDecodedToken.Uid;
            UserRecord firebaseUser = await FirebaseAuth.DefaultInstance.GetUserAsync(firebaseUID);

            if ((registerOAuthDto.Email != firebaseUser.Email) || (registerOAuthDto.PhoneNumber != firebaseUser.PhoneNumber)) return Unauthorized();

            var result = await _userManager.CreateAsync(user, registerOAuthDto.Password);



            if (result.Succeeded)
            {
                var customerRole = _roleManager.FindByNameAsync("Customer").Result;

                if (customerRole != null)
                {
                    IdentityResult customerRoleResult = await _userManager.AddToRoleAsync(user, customerRole.Name);
                }
                return CreateUserObject(user);
            }

            return BadRequest(result.Errors);
        }

        [AllowAnonymous]
        [HttpPost("registerOAuthFree")]
        public async Task<ActionResult<UserDto>> RegisterOAuthFree(RegisterOAuthDto registerOAuthDto)
        {


            if (await _userManager.Users.AnyAsync(x => x.UserName == registerOAuthDto.Username && !x.Us_Deleted))
            {
                ModelState.AddModelError("username", "Username taken");
                return ValidationProblem();
            }

            if (await _userManager.Users.AnyAsync(x => x.Email == registerOAuthDto.Email && !x.Us_Deleted))
            {
                ModelState.AddModelError("email", "Email taken");
                return ValidationProblem();
            }

            if (await _userManager.Users.AnyAsync(x => x.PhoneNumber == registerOAuthDto.PhoneNumber && !x.Us_Deleted))
            {
                ModelState.AddModelError("phonenumber", "PhoneNumber taken");
                return ValidationProblem();
            }

            var user = new AppUser
            {
                Us_DisplayName = registerOAuthDto.DisplayName,
                Email = registerOAuthDto.Email,
                UserName = registerOAuthDto.Username,
                PhoneNumber = registerOAuthDto.PhoneNumber,
                Us_language = registerOAuthDto.Language,
                Us_FirebaseUID = registerOAuthDto.FirebaseUID,
                Us_FirebaseToken = registerOAuthDto.FirebaseToken,
                Us_Active = true,
                Us_Customer = true
            };

            //FirebaseToken firebaseDecodedToken = await FirebaseAuth.DefaultInstance
            //                                .VerifyIdTokenAsync(registerOAuthDto.FirebaseUID);
            //string firebaseUID = firebaseDecodedToken.Uid;
            //UserRecord firebaseUser = await FirebaseAuth.DefaultInstance.GetUserAsync(firebaseUID);

            //if ((registerOAuthDto.Email != firebaseUser.Email) || (registerOAuthDto.PhoneNumber != firebaseUser.PhoneNumber)) return Unauthorized();

            var result = await _userManager.CreateAsync(user, registerOAuthDto.Password);

            if (result.Succeeded)
            {
                var customerRole = _roleManager.FindByNameAsync("Customer").Result;

                if (customerRole != null)
                {
                    IdentityResult customerRoleResult = await _userManager.AddToRoleAsync(user, customerRole.Name);
                }
                return CreateUserObject(user);
            }

            return BadRequest(result.Errors);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.Username && !x.Us_Deleted))
            {
                ModelState.AddModelError("username", "Username taken");
                return ValidationProblem();
            }

            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email && !x.Us_Deleted))
            {
                ModelState.AddModelError("email", "Email taken");
                return ValidationProblem();
            }

            if (await _userManager.Users.AnyAsync(x => x.PhoneNumber == registerDto.PhoneNumber && !x.Us_Deleted))
            {
                ModelState.AddModelError("phonenumber", "PhoneNumber taken");
                return ValidationProblem();
            }

            var user = new AppUser
            {
                Us_DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username,
                PhoneNumber = registerDto.PhoneNumber,
                Us_language = registerDto.Language,
                Us_Active = true,
                Us_Admin = true,
                Us_SuperAdmin = registerDto.Role.ToUpper() == "SUPERADMIN"

            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                var adminRole = _roleManager.FindByNameAsync("Admin").Result;

                if (adminRole != null)
                {
                    IdentityResult adminRoleResult = await _userManager.AddToRoleAsync(user, adminRole.Name);
                }

                if (registerDto.Role.ToUpper() == "SUPERADMIN")
                {
                    var superAdminRole = _roleManager.FindByNameAsync("SuperAdmin").Result;

                    if (superAdminRole != null)
                    {
                        IdentityResult superAdminRoleResult = await _userManager.AddToRoleAsync(user, superAdminRole.Name);
                    }
                }

                return CreateUserObject(user);
            }

            return BadRequest(result.Errors);
        }

        [AllowAnonymous]
        [HttpGet("getCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Name) && !x.Us_Deleted);

            if (user == null) return Unauthorized();
                
            return CreateUserObject(user);
        }

        [Authorize]
        [HttpPut("updateFirebaseToken")]
        public async Task<ActionResult<UserDto>> UpdateToken(UserDto userDto)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.UserName == User.FindFirstValue(ClaimTypes.Name) && !x.Us_Deleted);

            if (!String.IsNullOrEmpty(userDto.FirebaseToken)) user.Us_FirebaseToken = userDto.FirebaseToken;
            var result = await _userManager.UpdateAsync(user);


            return CreateUserObject(user);
        }

        private UserDto CreateUserObject(AppUser user)
        {
            return new UserDto
            {
                DisplayName = user.Us_DisplayName,
                //Image = user?.Photos?.FirstOrDefault(x => x.IsMain)?.Url,
                Image = user.Md_ID.ToString(),
                Token = _tokenService.CreateToken(user),
                Username = user.UserName,
                FirebaseUID = user.Us_FirebaseUID,
                FirebaseToken = user.Us_FirebaseToken,
                Language = user.Us_language,
                IsSubscribed = (user.Us_SubscriptionExpiryDate != null && user.Us_SubscriptionExpiryDate > DateTime.Now)
            };
        }
    }
}