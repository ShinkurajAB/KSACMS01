using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace SwitchCMS.Client.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeFormsController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeFormsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Text.Encoding.GetEncoding("windows-1252");
        }


    }
}
