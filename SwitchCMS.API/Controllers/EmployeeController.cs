using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwitchCMS.API.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;

namespace SwitchCMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IOHEMService EmployeeService;
        public EmployeeController(IOHEMService EmployeeService)
        {
            this.EmployeeService = EmployeeService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetEmployeeByPagination([FromBody] EmployeePagination pagination)
        {
            try
            {

                var data = await EmployeeService.GetEmployeeByPagination(pagination);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            try
            {

                bool isSuccess = await EmployeeService.DeleteEmployee(employeeId);
                return Ok(isSuccess);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> InsertEmployee([FromBody] OHEM modal)
        {
            try
            {

                bool isSuccess = await EmployeeService.InsertEmployee(modal);
                return Ok(isSuccess);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateEmployee([FromBody] OHEM modal)
        {
            try
            {

                bool isSuccess = await EmployeeService.UpdateEmployee(modal);
                return Ok(isSuccess);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EmployeeBulkUpload([FromBody] List<OHEM> employeeList)
        {
            try
            {

                var data = await EmployeeService.EmployeeBulkUpload(employeeList);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployessByCompany(int companyId)
        {
            try
            {

                var data = await EmployeeService.GetAllEmployessByCompany(companyId);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
