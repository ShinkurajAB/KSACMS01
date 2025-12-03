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
    public class AdvatisementController : ControllerBase
    {
        private readonly IOADVService AdvatisementService;

        public AdvatisementController(IOADVService _AdvatisementService)
        {
            AdvatisementService = _AdvatisementService;
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateAdvatisement([FromBody] OADV Model)
        {
            try
            {
                var data = await AdvatisementService.CreateAdvatisement(Model);
                return Ok(data);    
            }
            catch (Exception ex) 
            {
                var data = new ModificationStatus { Message = ex.Message, Success = false };
                return BadRequest(data);
            }
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAdvatisementByPagination([FromBody] AdvatisementPagination pagination)
        {
            try
            {

                var data = await AdvatisementService.GetAdvatisementPagination(pagination);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAdvatisementByID(int ID)
        {
            try
            {

                var data = await AdvatisementService.GetAdvatisementByID(ID);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> UpdateAdvatisement([FromBody] OADV Model)
        {
            try
            {
                var data = await AdvatisementService.updateAdvatisement(Model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                var data = new ModificationStatus { Message = ex.Message, Success = false };
                return BadRequest(data);
            }
        }

        
        [HttpDelete]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteAdvatisement(int ID)
        {
            try
            {
                var data = await AdvatisementService.DeleteAdvatisement(ID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                var data = new ModificationStatus { Message = ex.Message, Success = false };
                return BadRequest(data);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAdvatisementByCustID(int ID)
        {
            try
            {
                var data = await AdvatisementService.GetAdvatisementByCustID(ID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                var data = new ModificationStatus { Message = ex.Message, Success = false };
                return BadRequest(data);
            }
        }
    }
}
