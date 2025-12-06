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

namespace SwitchCMS.Component.Pages.Dashboard.Employee.EmployeeResignationComponent
{
    public partial class EmployeeResignationComponent
    {
        #region Parameter and Injections
        [Inject]
        IJSRuntime? JSRuntime { get; set; }

        [Inject]
        ISessionStorageService? storageService { get; set; }

        [Inject]
        IHEM1Service? ResignationService { get; set; }

        [Inject]
        IOUSRService? userService { get; set; }
        [Inject]
        IOHEMService? EmployeeService { get; set; }

        [Parameter]
        public int ParamResignationID { get; set; }
        [Parameter]
        public EventCallback<ModificationStatus> ModalCreateorUpdate { get; set; }
        #endregion

        #region Objects and Datatype

        int ResignationId;
        HEM1 Model = new HEM1();
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
                Model.CompanyId = loginUserDetails.CompanyID;
                this.StateHasChanged();
            }


        }
        #endregion
        #region Parameter Change
        protected override async Task OnParametersSetAsync()
        {
            if (ParamResignationID == 0)
            {
                Model = new HEM1();
                Model.CompanyId = loginUserDetails.CompanyID;
                ResignationId = ParamResignationID;
            }
            else
            {
                if (ParamResignationID != ResignationId)
                {
                    ResignationId = ParamResignationID;
                    Model = await ResignationService!.GetResignationById(ResignationId, response.JwtToken!);
                }
            }




        }
        #endregion
        #region Hide Modal
        private async Task HideModal()
        {
            await JSRuntime!.InvokeVoidAsync("HideEmployeeResignationModal");
        }
        #endregion
        #region Submit Vehicle
        private async Task SubmitData()
        {
            errorMessage = string.Empty;
            if (Model.EmployeeId == 0)
            {
                errorMessage = "Please select employee.";
                return;
            }
            Model.EmployeeName = EmployeeList.Where(x => x.ID == Model.EmployeeId).Select(x => x.Name).FirstOrDefault() ?? string.Empty;
            if (ResignationId > 0)
            {
                Result = await ResignationService!.UpdateResignation(Model, response.JwtToken!);
                if (Result.Success)
                {
                    await ModalCreateorUpdate.InvokeAsync(Result);
                    await HideModal();
                }
            }
            else
            {
                Result = await ResignationService!.InsertResignation(Model, response.JwtToken!);
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
