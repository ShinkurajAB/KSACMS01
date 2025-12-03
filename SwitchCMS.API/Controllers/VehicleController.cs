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
    public class VehicleController : ControllerBase
    {
        private readonly IOVHLService vehicleService;
        public VehicleController(IOVHLService vehicleService)
        {
            this.vehicleService = vehicleService;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateVehicle([FromBody] OVHL Model)
        {
            try
            {
                var data=await vehicleService.CreateVehicle(Model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetVehicleByPageIndex([FromBody] VehiclePagination Pagination)
        {
            try
            {
                var data = await vehicleService.GetVehiclePagination(Pagination);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetVehicleByID(int VehcileID)
        {
            try
            {
                var data = await vehicleService.GetVehicleByID(VehcileID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateVehicle([FromBody] OVHL Model)
        {
            try
            {
                var data = await vehicleService.UpdateVehicle(Model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteVehicle(int VehicleID)
        {
            try
            {
                var data = await vehicleService.DeleteVehicle(VehicleID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
