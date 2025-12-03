using Microsoft.AspNetCore.Mvc;
using SwitchCMS.API.Services.Interface;
using SwitchCMS.Model;

namespace SwitchCMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountryController:ControllerBase
    {
        private readonly IOCRYService CountryService;
        public CountryController(IOCRYService CountryService)
        {
            this.CountryService = CountryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            try
            {

                List<OCRY> countryList = await CountryService.GetAllCountries();
                return Ok(countryList);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
