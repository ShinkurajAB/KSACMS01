using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwitchCMS.API.AppService;
using SwitchCMS.API.Services.Authentication;
using SwitchCMS.API.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using SwitchCMS.Utility;
using System.Security.Claims;

namespace SwitchCMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserLoginService userLoginService;
        private readonly IOUSRService userService;
        public UsersController(IUserLoginService _userLoginService, IOUSRService _userService)
        {
            userLoginService= _userLoginService;
            userService= _userService;
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] OUSR user)
        {
            try
            {
                string browseData= Request.Headers["User-Agent"];
                var data = await userLoginService.Authenticate(user.UserName, user.Password, browseData);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetLoginUserDetails() 
        {
            try
            {
                var userID = User.FindFirstValue(UserClaimTypes.Id);

                var Data = await userService.GetUserByUserID(Convert.ToInt32(userID));
                return Ok(Data);

            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetUsersByPageIndex([FromBody]UsersPagination Pagination)
        {
            try
            {               

                var Data = await userService.GetUserByPagination(Pagination);
                return Ok(Data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpGet]
        public IActionResult AuthorizeCheck()
        {
            return Ok("Authorize SUccesss");
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public IActionResult AdminCheck()
        {
            return Ok("Authorize admin SUccesss");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreeateUpdateUser([FromBody] OUSR User)
        {
            try
            {

                var Data = await userService.CreeateUpdateUser(User);
                return Ok(Data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int UserID)
        {
            try
            {

                var Data = await userService.DeleteUserByUserID(UserID);
                return Ok(Data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
