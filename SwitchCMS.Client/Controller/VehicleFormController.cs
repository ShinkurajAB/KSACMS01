using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Reporting.NETCore;
using SwitchCMS.Client.Data;
using SwitchCMS.Client.Services.Interface;
using SwitchCMS.Model;
using System.Data;
using System.Globalization;
using System.Text;

namespace SwitchCMS.Client.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VehicleFormController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IVHL1Service vehicleHandoverService;
        ListtoDataTableConverter ListToTable = new ListtoDataTableConverter();

        public VehicleFormController(IWebHostEnvironment webHostEnvironment,
            IVHL1Service _VehicleHandoverService
            )
        {
            _webHostEnvironment = webHostEnvironment;
            vehicleHandoverService = _VehicleHandoverService;
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Text.Encoding.GetEncoding("windows-1252");
        }

        [HttpGet]
        public async Task<IActionResult> VehicleHandoverForms(int ID,string token)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture =
        Thread.CurrentThread.CurrentUICulture =
            CultureInfo.DefaultThreadCurrentCulture =
                CultureInfo.DefaultThreadCurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;

                var handoverDetails = await vehicleHandoverService.GetHandoverByHandoverID(ID,token);
                List<VHL1> data=new List<VHL1> ();
                data.Add(handoverDetails);
                DataTable dt = ListToTable.ToDataTable<VHL1>(data);
                string mimeType = string.Empty;
                int extention = 1;
                string path = $"{_webHostEnvironment.WebRootPath}\\Report\\VehicleHandover.rdlc";

                Stream reportDefinition;
                using var fs = new FileStream(path, FileMode.Open);
                reportDefinition = fs;
                
                LocalReport localReport = new LocalReport();
                localReport.EnableExternalImages = true;
                localReport.LoadReportDefinition(reportDefinition);
                localReport.DataSources.Add(new ReportDataSource("dsVehicleHandover", dt));

                byte[] pdf = localReport.Render("PDF");
                // localReport.AddDataSource("dsPrintInvoice", dt);
                fs.Dispose();
                //var result = localReport.Execute(RenderType.Pdf, extention, null, mimeType);

                return File(pdf, "application/pdf");
                //return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + " " + ex.InnerException + "\n\n" + ex.StackTrace);
            }
        }

    }
}
