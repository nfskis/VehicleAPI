using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using VehicleAPI.BusinessLogic;
using VehicleAPI.Models;
using VehicleAPI.ViewModels;

namespace VehicleAPI.Controllers
{
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly AccountProcessor AccountProcessor;

        public AccountController(IConfiguration configuration)
        {
            AccountProcessor = new AccountProcessor(configuration);
        }

        /// <summary>
        /// Register Track of Vehicle 
        /// </summary>
        /// <param name="value">Vehicle Tracking Model</param>
        /// <param name="vehicleID">Vehicle ID</param>
        [HttpPost]
        [AllowAnonymous]
        [Route("api/account/user/")]
        public ApiResultMessage RegisterUser([FromForm] UserViewModel value)
        {
            AccountProcessor.RegisterUser(value);
            return new ApiResultMessage("Regiser user successful", "Success");
        }

        /// <summary>
        /// Register Track of Vehicle 
        /// </summary>
        /// <param name="value">Vehicle Tracking Model</param>
        /// <param name="vehicleID">Vehicle ID</param>
        [HttpPut]
        [Route("api/account/user")]
        public ApiResultMessage UpdateUser([FromForm] UpdateUserViewModel value)
        {
            AccountProcessor.UpdateUser(value);
            return new ApiResultMessage("Regiser user successful", "Success");
        }

        /// <summary>
        /// Register Track of Vehicle 
        /// </summary>
        /// <param name="value">Vehicle Tracking Model</param>
        /// <param name="vehicleID">Vehicle ID</param>
        [HttpDelete]
        [Route("api/account/user")]
        [Authorize(Roles = "Admin")]
        public ApiResultMessage DeleteUser(string userEmail)
        {
            var userId = AccountProcessor.GetUsers()
                    .FirstOrDefault(curr => curr.Email == userEmail)?.UserSeqID;
            AccountProcessor.DeleteUser(userId);
            return new ApiResultMessage("Regiser user successful", "Success");
        }

        /// <summary>
        /// Register Track of Vehicle 
        /// </summary>
        /// <param name="value">Vehicle Tracking Model</param>
        /// <param name="vehicleID">Vehicle ID</param>
        [HttpGet]
        [Route("api/account/user")]
        [Authorize(Roles = "Admin")]
        public List<UserViewModel> GetUser()
        {
            return AccountProcessor.GetUsers();
        }

        /// <summary>
        /// Register Track of Vehicle 
        /// </summary>
        /// <param name="value">Vehicle Tracking Model</param>
        /// <param name="vehicleID">Vehicle ID</param>
        [HttpPost]
        [AllowAnonymous]
        [Route("api/account/login/")]
        public async Task<ApiResultMessage> UserLoginAsync([FromForm] LoginUserViewModel value)
        {
            var loginUser = AccountProcessor.LoginUser(value);
            if (loginUser == null)
            {
                // redirection to login page
                return new ApiResultMessage("User doen't existed in our database", "Failed");
            }

            try
            {
                var indentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme,
                                                    ClaimTypes.Name,
                                                    ClaimTypes.Role);
                indentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, loginUser.UserSeqID));
                indentity.AddClaim(new Claim(ClaimTypes.Email, loginUser.Email));
                indentity.AddClaim(new Claim(ClaimTypes.Name, $@"{loginUser.FirstName} {loginUser.LastName}"));
                indentity.AddClaim(new Claim(ClaimTypes.Role, $@"{loginUser.Role}"));

                var princiapl = new ClaimsPrincipal(indentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, princiapl,
                    new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTime.Now.AddHours(1),
                        AllowRefresh = true
                    });

                return new ApiResultMessage($@"Successful login user:{loginUser.FirstName}", "Successful");
            }
            catch (Exception ex)
            {
                // login failed or redirection to error page
                return new ApiResultMessage($@"{ex.Message}", "Failed");
            }
        }

        /// <summary>
        /// User Logout
        /// </summary>
        /// <param name="value"></param>
        [HttpGet]
        [Route("api/account/logOut/")]
        public async Task<ApiResultMessage> UesrLogOutAsync()
        {
            await HttpContext.SignOutAsync();
            return new ApiResultMessage($@"Logout Successful: {User.Identity.Name}", "Success");
        }


    }
}