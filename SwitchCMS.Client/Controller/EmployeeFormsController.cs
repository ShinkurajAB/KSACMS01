using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class EmployeeFormsController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHEM1Service employeeResignationService;
        private readonly IHEM3Service employeeAbsenceService;
        private readonly IHEM4Service offerLetterService;
        ListtoDataTableConverter ListToTable = new ListtoDataTableConverter();

        public EmployeeFormsController(IWebHostEnvironment webHostEnvironment,
            IHEM1Service employeeResignationService, IHEM3Service employeeAbsenceService, IHEM4Service offerLetterService)
        {
            _webHostEnvironment = webHostEnvironment;
            this.employeeResignationService = employeeResignationService;
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            System.Text.Encoding.GetEncoding("windows-1252");
            this.employeeAbsenceService = employeeAbsenceService;
            this.offerLetterService = offerLetterService;
        }


        [HttpGet]
        public async Task<IActionResult> GetResignationForms(int ID, string token)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture =
        Thread.CurrentThread.CurrentUICulture =
            CultureInfo.DefaultThreadCurrentCulture =
                CultureInfo.DefaultThreadCurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;

                var resignationDetails = await employeeResignationService.GetResignationById(ID, token);
                List<HEM1> data = new List<HEM1>();
                data.Add(resignationDetails);
                DataTable dt = ListToTable.ToDataTable<HEM1>(data);
                string mimeType = string.Empty;
                int extention = 1;
                string path = $"{_webHostEnvironment.WebRootPath}\\Report\\EmployeeResignation.rdlc";

                Stream reportDefinition;
                using var fs = new FileStream(path, FileMode.Open);
                reportDefinition = fs;

                LocalReport localReport = new LocalReport();
                localReport.EnableExternalImages = true;
                localReport.LoadReportDefinition(reportDefinition);
                localReport.DataSources.Add(new ReportDataSource("dsEmployeeResignation", dt));

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

        [HttpGet]
        public async Task<IActionResult> GetAbsenceForms(int ID, string token)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture =
                Thread.CurrentThread.CurrentUICulture =
                CultureInfo.DefaultThreadCurrentCulture =
                CultureInfo.DefaultThreadCurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;

                var absenceDetails = await employeeAbsenceService.GetAbsenteeById(ID, token);
                List<HEM3> data = new List<HEM3>();
                data.Add(absenceDetails);
                DataTable dt = ListToTable.ToDataTable<HEM3>(data);
                string mimeType = string.Empty;
                int extention = 1;
                string path = $"{_webHostEnvironment.WebRootPath}\\Report\\EmployeeAbsence.rdlc";

                Stream reportDefinition;
                using var fs = new FileStream(path, FileMode.Open);
                reportDefinition = fs;

                LocalReport localReport = new LocalReport();
                localReport.EnableExternalImages = true;
                localReport.LoadReportDefinition(reportDefinition);
                localReport.DataSources.Add(new ReportDataSource("dsEmployeeAbsence", dt));

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
        [HttpGet]
        public async Task<IActionResult> GetOfferLetterForms(int ID, string token)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture =
                Thread.CurrentThread.CurrentUICulture =
                CultureInfo.DefaultThreadCurrentCulture =
                CultureInfo.DefaultThreadCurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;

                var offerDetails = await offerLetterService.GetOfferLetterById(ID, token);
                List<HEM4> data = new List<HEM4>();
                data.Add(offerDetails);
                DataTable dt = ListToTable.ToDataTable<HEM4>(data);
                string mimeType = string.Empty;
                int extention = 1;
                string path = $"{_webHostEnvironment.WebRootPath}\\Report\\OfferLetter.rdlc";

                Stream reportDefinition;
                using var fs = new FileStream(path, FileMode.Open);
                reportDefinition = fs;

                LocalReport localReport = new LocalReport();
                localReport.EnableExternalImages = true;
                localReport.LoadReportDefinition(reportDefinition);
                localReport.DataSources.Add(new ReportDataSource("dsOfferLetter", dt));

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
