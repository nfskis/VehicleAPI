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
using VehicleAPI.ViewModels.Users;

namespace VehicleAPI.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        private readonly AccountProcessor AccountProcessor;

        public AccountController(IConfiguration configuration)
        {
            AccountProcessor = new AccountProcessor(configuration);
        }

        /// <summary>
        /// register user
        /// </summary>
        /// <param name="value">user model</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("api/user/")]
        public ActionResult RegisterUser([FromForm] ResisterUserViewModel value)
        {
            var user = new LoginUserViewModel(value.Email, value.Password);
            bool isExisted = AccountProcessor.LoginUser(user) != null 
                ? true 
                : false;

            if (isExisted)
            {
                return BadRequest($"the givens user eamil existed in user database: '{value.Email}'");
            }

            AccountProcessor.RegisterUser(value);
            return Ok($"Regiser user successful: {value.Email}");
        }

        /// <summary>
        /// update user 
        /// </summary>
        /// <param name="value">user view Model</param>
        [HttpPut]
        [Route("api/user")]
        [Authorize]
        public ActionResult UpdateUser([FromForm] UpdateUserViewModel value)
        {
            AccountProcessor.UpdateUser(User.Claims.ElementAt(1).Value, value);
            return Ok($"Updated user : {User.Identity.Name}");
        }

        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="userEmail">user email address</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/user")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUser([FromForm] DeleteUserViewModel deleteUser)
        {
            AccountProcessor.DeleteUser(deleteUser.Email);
            return Ok($"Deleted user: {deleteUser.Email}");
        }

        /// <summary>
        /// get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/user")]
        [Authorize(Roles = "Admin")]
        public List<GetUsersViewModel> GetUser(int pageNumber)
        {
            int rowsOfPage = 10;
            return AccountProcessor.GetUsers(pageNumber, rowsOfPage);
        }

        /// <summary>
        /// user login 
        /// </summary>
        /// <param name="value">Login User View Model</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("api/user/login")]
        public async Task<ActionResult> UserLoginAsync([FromForm] LoginUserViewModel value)
        {
            var loginUser = AccountProcessor.LoginUser(value);
            if (loginUser == null)
            {
                // redirection to login page
                return NotFound($"User doen't existe in our database: {value.Email}");
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

                return Ok($"Successful login user: {loginUser.Email}");
            }
            catch (Exception ex)
            {
                // login failed or redirection to error page
                return NotFound($@"{ex.Message}");
            }
        }

        /// <summary>
        /// user log out 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/user/logout")]
        [Authorize]
        public async Task<ActionResult> UesrLogOutAsync()
        {
            await HttpContext.SignOutAsync();
            return Ok($@"Logout Successful: {User.Identity.Name}");
        }


    }
}