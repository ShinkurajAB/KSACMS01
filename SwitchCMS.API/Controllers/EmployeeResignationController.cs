using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwitchCMS.API.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;

namespace SwitchCMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeResignationController : ControllerBase
    {
        private readonly IHEM1Service EmployeeResignationService;
        public EmployeeResignationController(IHEM1Service EmployeeResignationService)
        {
            this.EmployeeResignationService = EmployeeResignationService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetEmployeeResignationsByPagination([FromBody] EmployeeResignationPagination pagination)
        {
            try
            {

                var data = await EmployeeResignationService.GetResignationsByCompanyId(pagination);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetResignationById(int Id)
        {
            try
            {

                var data = await EmployeeResignationService.GetResignationById(Id);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> DeleteResignation(int resignationId)
        {
            try
            {

                bool isSuccess = await EmployeeResignationService.DeleteResignation(resignationId);
                return Ok(isSuccess);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> InsertResignation([FromBody] HEM1 modal)
        {
            try
            {

                var data = await EmployeeResignationService.InsertResignation(modal);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateResignation([FromBody] HEM1 modal)
        {
            try
            {

                var data = await EmployeeResignationService.UpdateResignation(modal);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
