using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SwitchCMS.Client.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.Authentication;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Component.Pages.Dashboard.Employee.EmployeeAbsenteeComponent
{
    public partial class EmployeeAbsenteeComponent
    {
        #region Parameter and Injections
        [Inject]
        IJSRuntime? JSRuntime { get; set; }

        [Inject]
        ISessionStorageService? storageService { get; set; }

        [Inject]
        IHEM3Service? AbsenteeService { get; set; }

        [Inject]
        IOUSRService? userService { get; set; }
        [Inject]
        IOHEMService? EmployeeService { get; set; }

        [Parameter]
        public int ParamAbsenteeID { get; set; }
        [Parameter]
        public EventCallback<ModificationStatus> ModalCreateorUpdate { get; set; }
        #endregion

        #region Objects and Datatype

        int AbsenteeId;
        HEM3 Model = new HEM3();
        AuthenticationResponseModel response = new AuthenticationResponseModel();
        ModificationStatus Result = new ModificationStatus();
        OUSR loginUserDetails = new OUSR();
        List<OHEM> EmployeeList = new List<OHEM>();
        string errorMessage = string.Empty;
        #endregion
        #region OnrenderAfter
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                response = await storageService!.GetItemAsync<AuthenticationResponseModel>("Token");
                loginUserDetails = await userService!.GetLoginUserDetails(response.JwtToken!);
                EmployeeList = await EmployeeService!.GetAllEmployessByCompany(loginUserDetails.CompanyID, response.JwtToken!);
                Model.CompanyID = loginUserDetails.CompanyID;
                this.StateHasChanged();
            }


        }
        #endregion
        #region Parameter Change
        protected override async Task OnParametersSetAsync()
        {
            if (ParamAbsenteeID == 0)
            {
                Model = new HEM3();
                Model.CompanyID = loginUserDetails.CompanyID;
                AbsenteeId = ParamAbsenteeID;
            }
            else
            {
                if (ParamAbsenteeID != AbsenteeId)
                {
                    AbsenteeId = ParamAbsenteeID;
                    Model = await AbsenteeService!.GetAbsenteeById(AbsenteeId, response.JwtToken!);
                }
            }




        }
        #endregion
        #region Hide Modal
        private async Task HideModal()
        {
            await JSRuntime!.InvokeVoidAsync("HideEmployeeAbsenteeModal");
        }
        #endregion
        #region Submit Absence Data
        private async Task SubmitData()
        {
            errorMessage = string.Empty;
            if (Model.EmployeeID == 0)
            {
                errorMessage = "Please select employee.";
                return;
            }
            Model.EmployeeName = EmployeeList.Where(x => x.ID == Model.EmployeeID).Select(x => x.Name).FirstOrDefault() ?? string.Empty;
            if (AbsenteeId > 0)
            {
                Result = await AbsenteeService!.UpdateAbsentee(Model, response.JwtToken!);
                if (Result.Success)
                {
                    await ModalCreateorUpdate.InvokeAsync(Result);
                    await HideModal();
                }
            }
            else
            {
                Result = await AbsenteeService!.InsertAbsentee(Model, response.JwtToken!);
                if (Result.Success)
                {
                    await ModalCreateorUpdate.InvokeAsync(Result);
                    await HideModal();
                }
            }
        }
        #endregion
    }
}
