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

namespace VehicleAPI.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private LoginProcessor LoginProcessor;

        public AccountController(IConfiguration configuration)
        {
            LoginProcessor = new LoginProcessor(configuration);
        }

        /// <summary>
        /// Register Track of Vehicle 
        /// https://localhost:44309/api/Login/Register/
        /// </summary>
        /// <param name="value">Vehicle Tracking Model</param>
        /// <param name="vehicleID">Vehicle ID</param>
        [HttpPost]
        [Route("api/Account/register/")]
        public void RegisterUser([FromForm] UserModel value)
        {
            LoginProcessor.RegisterUser(value);
        }

        /// <summary>
        /// Register Track of Vehicle 
        /// https://localhost:44309/api/Login/Login/
        /// </summary>
        /// <param name="value">Vehicle Tracking Model</param>
        /// <param name="vehicleID">Vehicle ID</param>
        [HttpPost]
        [Route("api/Account/login/")]
        public async Task<UserModel> UserLoginAsync([FromForm] UserModel value)
        {
            var loginUser = LoginProcessor.LoginUser(value);
            if (loginUser == null)
            {
                // redirection to login page
                throw new Exception("User doen't existed in our database");
            }

            try
            {
                var indentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme,
                                                    ClaimTypes.Name,
                                                    ClaimTypes.Role);
                indentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, loginUser.Email));
                indentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, loginUser.UserSeqID));
                

                var princiapl = new ClaimsPrincipal(indentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, princiapl,
                    new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTime.Now.AddHours(1),
                        AllowRefresh = true                        
                    });

                return loginUser;
            }
            catch(Exception ex)
            {
                // login failed or redirection to error page
                throw new Exception("Failed Login: " + ex.Message);
            }
        }

        /// <summary>
        /// User Logout
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [Route("api/Account/logOut/")]
        [Authorize]
        public async void UesrLogOutAsync()
        {
            await HttpContext.SignOutAsync();
        }


    }
}