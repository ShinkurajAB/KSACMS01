using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using SwitchCMS.Client.Services.Interface;
using SwitchCMS.Component.Data;
using SwitchCMS.Model;
using SwitchCMS.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Component.Pages.Dashboard.Employee
{
    public partial class EmployeeBulkRegistration
    {
        #region Service Injections
        [Inject]
        IJSRuntime? JSRuntime { get; set; }
        [Inject]
        IOHEMService? employeeService { get; set; }
        [Inject]
        ISessionStorageService? storageService { get; set; }
        [Inject]
        IOUSRService? userService { get; set; }
        #endregion
        #region Object / Data type Declare


        AuthenticationResponseModel loginResponseModel = new AuthenticationResponseModel();
        List<OHEM> EmployeeModalList = new();
        OUSR loginUserDetails = new OUSR();

        string AlertMessage = string.Empty;
        string ErrorMessage = string.Empty;
        SuccessorFailAlert SuccessOrFaild;
        #endregion
        #region Razor Page On After Render
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

            if (firstRender)
            {
                loginResponseModel = await storageService!.GetItemAsync<AuthenticationResponseModel>("Token");
                loginUserDetails = await userService!.GetLoginUserDetails(loginResponseModel.JwtToken!);

                this.StateHasChanged();
            }
        }
        #endregion
        #region LoadData        

        private async void LoadFile(IBrowserFile file)
        {
            try
            {
                var uploadedFile = file;
                if (uploadedFile == null)
                {
                    ErrorMessage = "No file selected.";
                    return;
                }

                var uploadsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "EmployeeBulkRegister");
                Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, uploadedFile.Name);

                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadedFile.OpenReadStream(maxAllowedSize: 10_000_000).CopyToAsync(stream);
                }

                using var workbook = new ClosedXML.Excel.XLWorkbook(filePath);
                var worksheet = workbook.Worksheets.First();
                var rows = worksheet.RangeUsed()!.RowsUsed().Skip(1);

                EmployeeModalList.Clear();

                foreach (var row in rows)
                {
                    var emp = new OHEM
                    {
                        Name = row.Cell(1).GetString(),
                        JobTitle = row.Cell(2).GetString(),
                        ProjectName = row.Cell(3).GetString(),
                        Email = row.Cell(4).GetString(),

                        PassportNumber = row.Cell(5).GetString(),
                        DOB = row.Cell(6).TryGetValue<DateTime>(out var dob) ? dob : dob,
                        MobileNumber = row.Cell(7).GetString(),
                        Gender = Enum.TryParse<SwitchCMS.Utility.Gender>(row.Cell(8).GetString(), true, out var gender) ? gender
                                  : SwitchCMS.Utility.Gender.Other,
                        Nationality = row.Cell(9).GetString(),
                        IqamaID = row.Cell(10).GetString(),
                        EmployeeStatus = Enum.TryParse<SwitchCMS.Utility.Status>(row.Cell(11).GetString(), true, out var status)
                                         ? status : SwitchCMS.Utility.Status.InActive,
                        ContractType = Enum.TryParse<SwitchCMS.Utility.ContractType>(row.Cell(12).GetString(), true, out var contracttype)
                                         ? contracttype : SwitchCMS.Utility.ContractType.None,
                        Sponsor = row.Cell(13).GetString(),
                        NationalUnifiedNumber = row.Cell(14).GetString(),
                        JoiningDate = row.Cell(15).TryGetValue<DateTime>(out var jd) ? jd : jd,
                        BankAccount = row.Cell(16).GetString(),
                        ContractExpiry = row.Cell(17).TryGetValue<DateTime>(out var ced) ? ced : ced,
                        ContractStatus = Enum.TryParse<SwitchCMS.Utility.Status>(row.Cell(18).GetString(), true, out var cnstatus)
                                         ? cnstatus : SwitchCMS.Utility.Status.InActive,
                        IqamaExpiry = row.Cell(19).TryGetValue<DateTime>(out var iex) ? iex : iex,
                        InsuranceCompanyName = row.Cell(20).GetString(),
                        InsuranceStartDate = row.Cell(21).TryGetValue<DateTime>(out var ist) ? ist : ist,
                        InsuranceExpiryDate = row.Cell(22).TryGetValue<DateTime>(out var ied) ? ied : ied,
                        InsuranceStatus = Enum.TryParse<SwitchCMS.Utility.Status>(row.Cell(23).GetString(), true, out var instatus)
                                         ? instatus : SwitchCMS.Utility.Status.InActive,
                        CompanyID = loginUserDetails.Company.ID,
                        CreatedBy = loginUserDetails.ID
                    };

                    EmployeeModalList.Add(emp);
                }

                ErrorMessage = string.Empty;

                await InvokeAsync(StateHasChanged);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error reading file: " + ex.Message;
                await InvokeAsync(StateHasChanged);
            }
        }

        #endregion
        #region Sample DownLoad
        private async Task SampleDownload()
        {
            ErrorMessage = string.Empty;
            string stringBytes = string.Empty;
            string file = AppDomain.CurrentDomain.BaseDirectory + $"wwwroot\\EmployeeBulkRegister\\EmployeeBulkUpload.xlsx";

            if (File.Exists(file))
            {
                using (var fileInpu = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    var memory = new MemoryStream();
                    await fileInpu.CopyToAsync(memory);
                    var buffer = memory.ToArray();
                    stringBytes = Convert.ToBase64String(buffer);
                }
            }
            await JSRuntime!.InvokeVoidAsync("DownLoadFile", "application/vnd.ms-excel", stringBytes, "sampledata.xlsx");
        }
        #endregion
        #region Upload Employees
        private async Task BulkUploadEmployees()
        {
            if (EmployeeModalList.Count == 0)
            {
                ErrorMessage = "Please load a file before uploading.";
                return;
            }
            else
            {
                List<OHEM> result = await employeeService!.EmployeeBulkUpload(EmployeeModalList, loginResponseModel.JwtToken!);
                ErrorMessage = string.Empty;
                if (result != null)
                {
                    if (result.Count > 0)
                    {
                        string stringBytes = string.Empty;
                        string file = AppDomain.CurrentDomain.BaseDirectory + $"\\wwwroot\\EmployeeBulkRegister\\Temp{loginResponseModel.Id}.xlsx";

                        using (StreamWriter writer = new StreamWriter(file))
                        {
                            writer.WriteLine("Name,Email,JobTitle,Status");
                            foreach (var item in result)
                            {
                                writer.WriteLine($"{item.Name},{item.Email},{item.JobTitle},{item.BulkUploadStatus}");

                            }
                            if (result.Any(x => x.BulkUploadStatus != "Success"))
                            {
                                AlertMessage = "Some employees could not be entered.Check the response file";
                                SuccessOrFaild = SuccessorFailAlert.Faild;
                                this.StateHasChanged();
                                AlertCalling.CallAlert(JSRuntime!);
                            }
                            else
                            {
                                AlertMessage = "All employees have been entered successfully.";
                                SuccessOrFaild = SuccessorFailAlert.Success;
                                this.StateHasChanged();
                                AlertCalling.CallAlert(JSRuntime!);
                            }
                        }
                        using (var fileInpu = new FileStream(file, FileMode.Open, FileAccess.Read))
                        {
                            var memory = new MemoryStream();
                            await fileInpu.CopyToAsync(memory);
                            var buffer = memory.ToArray();
                            stringBytes = Convert.ToBase64String(buffer);
                        }
                        if (File.Exists(file))
                        {
                            File.Delete(file);
                        }

                        await JSRuntime!.InvokeVoidAsync("DownLoadFile", "text/csv", stringBytes, "response.csv");
                        EmployeeModalList = new List<OHEM>();
                        this.StateHasChanged();
                    }


                }
            }
        }
        #endregion
    }
}
