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
    public class CompanyController : ControllerBase
    {

        private readonly IOCMPService CompanyService;
        public CompanyController(IOCMPService CompanyService)
        {
            this.CompanyService = CompanyService;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> GetAllCompany()
        {
            try
            {

                var Data = await CompanyService.GetAllCompany();
                return Ok(Data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> SignUpCompany([FromBody] OCMP Company)
        {
            try
            {

                bool isSuccess = await CompanyService.SignUpCompany(Company);
                return Ok(isSuccess);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetCompanyByPagination([FromBody] CompanyPagination pagination)
        {
            try
            {

                var data = await CompanyService.GetCompanyByPagination(pagination);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateCompany([FromBody] OCMP Company)
        {
            try
            {

                bool isSuccess = await CompanyService.UpdateCompany(Company);
                return Ok(isSuccess);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateCompanyStatus(int companyId,string companyStatus)
        {
            try
            {

                bool isSuccess = await CompanyService.UpdateCompanyStatus(companyId,companyStatus);
                return Ok(isSuccess);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> DeleteCompany(int companyId)
        {
            try
            {

                bool isSuccess = await CompanyService.DeleteCompany(companyId);
                return Ok(isSuccess);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
