using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwitchCMS.API.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;

namespace SwitchCMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeAbsenceController : ControllerBase
    {
        private readonly IHEM3Service EmployeeAbsenteeService;
        public EmployeeAbsenceController(IHEM3Service EmployeeAbsenteeService)
        {
            this.EmployeeAbsenteeService = EmployeeAbsenteeService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetAbsenteesByPagination([FromBody] EmployeeAbsencePagination pagination)
        {
            try
            {

                var data = await EmployeeAbsenteeService.GetAbsenteesByCompanyId(pagination);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAbsenteeById(int Id)
        {
            try
            {

                var data = await EmployeeAbsenteeService.GetAbsenteeById(Id);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> DeleteAbsentee(int absenteeId)
        {
            try
            {

                bool isSuccess = await EmployeeAbsenteeService.DeleteAbsentee(absenteeId);
                return Ok(isSuccess);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> InsertAbsentee([FromBody] HEM3 modal)
        {
            try
            {

                var data = await EmployeeAbsenteeService.InsertAbsentee(modal);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateAbsentee([FromBody] HEM3 modal)
        {
            try
            {

                var data = await EmployeeAbsenteeService.UpdateAbsentee(modal);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
