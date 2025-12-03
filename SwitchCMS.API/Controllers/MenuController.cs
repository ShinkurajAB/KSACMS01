using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwitchCMS.API.Services.Interface;
using SwitchCMS.Utility;
using System.Security.Claims;

namespace SwitchCMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : ControllerBase
    {

        private readonly IOMENService MenusService;
        public MenuController(IOMENService MenusService)
        {
            this.MenusService = MenusService;
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllMenus()
        {
            try
            {

                var userIdClaim = User.FindFirst(UserClaimTypes.Id);
                var userId = userIdClaim?.Value;

                // 2️⃣ Get Role(s)
                var roleClaim = User.FindFirst(UserClaimTypes.Role);
                var role = roleClaim?.Value;

                var data= await MenusService.GetAllMenus(userId!,role!);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMenuByUserID(int UserID)
        {
            try
            {

             

                var data = await MenusService.GetMenuUserID(UserID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
