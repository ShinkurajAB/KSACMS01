using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwitchCMS.API.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;

namespace SwitchCMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GeneralWarningController : ControllerBase
    {
        private readonly IHEM5Service WarningService;
        public GeneralWarningController(IHEM5Service WarningService)
        {
            this.WarningService = WarningService;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateGeneralWarning([FromBody] HEM5 Model)
        {
            try
            {
                var data = await WarningService.CreateGeneralWarning(Model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetGeneralWarningByPageIndex([FromBody] GeneralWarningPagination Pagination)
        {
            try
            {
                var data = await WarningService.GetGeneralWarningPagination(Pagination);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetGeneralWarningByID(int ID)
        {
            try
            {
                var data = await WarningService.GetGeneralWarningByID(ID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateGeneralWarning([FromBody] HEM5 Model)
        {
            try
            {
                var data = await WarningService.UpdateGeneralWarning(Model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> DeleteGeneralWarning(int ID)
        {
            try
            {
                var data = await WarningService.DeleteGeneralWarning(ID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
