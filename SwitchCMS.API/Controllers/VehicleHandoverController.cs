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
    public class VehicleHandoverController : ControllerBase
    {

        private readonly IVHL1Service HandoverService;
        public VehicleHandoverController(IVHL1Service HandoverService)
        {
            this.HandoverService = HandoverService;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateHandover([FromBody] VHL1 Model)
        {
            try
            {
                var data = await HandoverService.CreateHandover(Model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetHandoverByPageIndex([FromBody] HandoverPagination Pagination)
        {
            try
            {
                var data = await HandoverService.GetVehiclePagination(Pagination);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetHandoverByID(int HandoverID)
        {
            try
            {
                var data = await HandoverService.GetHandoverByID(HandoverID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateHandover([FromBody] VHL1 Model)
        {
            try
            {
                var data = await HandoverService.UpdateHandover(Model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteHandover(int HandoverID)
        {
            try
            {
                var data = await HandoverService.DeleteHandover(HandoverID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
