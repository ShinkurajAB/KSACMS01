using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwitchCMS.API.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;

namespace SwitchCMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DirectNotificationController : ControllerBase
    {

        private readonly IHEM2Service DirectService;
        public DirectNotificationController(IHEM2Service DirectService)
        {
            this.DirectService = DirectService;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateDirectNotification([FromBody] HEM2 Model)
        {
            try
            {
                var data = await DirectService.CreateDirectNotification(Model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetDirectNotificationByPageIndex([FromBody] DirectNotificationPagination Pagination)
        {
            try
            {
                var data = await DirectService.GetDirectNotificationPagination(Pagination);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetDirectNotificationByID(int ID)
        {
            try
            {
                var data = await DirectService.GetDirectNotificationByID(ID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateDirectNotification([FromBody] HEM2 Model)
        {
            try
            {
                var data = await DirectService.UpdateDirectNotification(Model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteDirectNotification(int ID)
        {
            try
            {
                var data = await DirectService.DeleteDirectNotification(ID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
