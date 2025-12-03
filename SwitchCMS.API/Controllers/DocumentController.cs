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
    public class DocumentController : ControllerBase
    {

        private readonly IOATCService attachementService;
        public DocumentController(IOATCService _attachementService)
        {
            attachementService= _attachementService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateDocument([FromBody]OATC Model )
        {
            try
            {
                var data = await attachementService.CreateDocument(Model);
                return Ok(data);
            }
            catch (Exception ex)
            {
                var data = new ModificationStatus { Message = ex.Message, Success = false };
                return BadRequest(data);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetDocumentByPageIndex([FromBody] DocumentPagination Pagination)
        {
            try
            {
                var data = await attachementService.GetDocumentPagination(Pagination);
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
        public async Task<IActionResult> GetDocumentByID(int ID)
        {
            try
            {
                var data = await attachementService.GetDocumentByID(ID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                var data = new ModificationStatus { Message = ex.Message, Success = false };
                return BadRequest(data);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteDocumentByID(int ID)
        {
            try
            {
                var data = await attachementService.DeleteDocumentByID(ID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                var data = new ModificationStatus { Message = ex.Message, Success = false };
                return BadRequest(data);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateDocument([FromBody] OATC Model)
        {
            try
            {
                var data = await attachementService.UpdateDocument(Model);
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
