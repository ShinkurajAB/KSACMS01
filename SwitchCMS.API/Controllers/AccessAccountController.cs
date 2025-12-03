using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwitchCMS.API.Services.Interface;
using SwitchCMS.Model;

namespace SwitchCMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccessAccountController : ControllerBase
    {
        private readonly IOACAService accessService;
        public AccessAccountController(IOACAService accessService)
        {
            this.accessService = accessService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAccessAccountManagers()
        {
            try
            {

                List<OACA> accessList = await accessService.GetAllAccessAccountManagers();
                return Ok(accessList);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAccessAccountManagersByCompanyId(int companyId)
        {
            try
            {

                List<OACA> accessList = await accessService.GetAccessAccountManagersByCompanyId(companyId);
                return Ok(accessList);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> InsertAccessAccountManager([FromBody] OACA accessModal)
        {
            try
            {

                bool isSuccess = await accessService.InsertAccessAccountManager(accessModal);
                return Ok(isSuccess);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateAccessAccountManager([FromBody] OACA accessModal)
        {
            try
            {

                bool isSuccess = await accessService.UpdateAccessAccountManager(accessModal);
                return Ok(isSuccess);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> DeleteAccessAccountManager(int accessId)
        {
            try
            {

                bool isSuccess = await accessService.DeleteAccessAccountManager(accessId);
                return Ok(isSuccess);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

