using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwitchCMS.API.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;

namespace SwitchCMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OfferLetterController : ControllerBase
    {
        private readonly IHEM4Service OfferLetterService;
        public OfferLetterController(IHEM4Service OfferLetterService)
        {
            this.OfferLetterService = OfferLetterService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetOfferLetterByPagination([FromBody] OfferLetterPagination pagination)
        {
            try
            {

                var data = await OfferLetterService.GetOfferLetterByCompanyId(pagination);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetOfferLetterById(int Id)
        {
            try
            {

                var data = await OfferLetterService.GetOfferLetterById(Id);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> DeleteOfferLetter(int offerId)
        {
            try
            {

                bool isSuccess = await OfferLetterService.DeleteOfferLetter(offerId);
                return Ok(isSuccess);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> InsertOfferLetter([FromBody] HEM4 modal)
        {
            try
            {

                var data = await OfferLetterService.InsertOfferLetter(modal);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateOfferLetter([FromBody] HEM4 modal)
        {
            try
            {

                var data = await OfferLetterService.UpdateOfferLetter(modal);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
