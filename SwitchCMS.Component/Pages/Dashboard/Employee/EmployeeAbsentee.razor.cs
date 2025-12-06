using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SwitchCMS.Client.Services.Interface;
using SwitchCMS.Component.Data;
using SwitchCMS.Model;
using SwitchCMS.Model.Authentication;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Component.Pages.Dashboard.Employee
{
    public partial class EmployeeAbsentee
    {
        #region Service Injections
        [Inject]
        IJSRuntime? JSRuntime { get; set; }

        [Inject]
        ISessionStorageService? storageService { get; set; }
        [Inject]
        IHEM3Service? absenteeService { get; set; }
        [Inject]
        IOUSRService? userService { get; set; }
        #endregion
        #region Object/Datatype Declaration
        EmployeeAbsencePagination Pagination = new EmployeeAbsencePagination();
        AuthenticationResponseModel response = new AuthenticationResponseModel();
        OUSR loginUserDetails = new OUSR();
        private int EditedAbsenteeID = 0;
        private int DeletedAbsenteeID = 0;
        string DeleteConfirmModalMessage = string.Empty;
        string AlertMessage = string.Empty;
        SuccessorFailAlert SuccessOrFaild;
        #endregion
        #region Razor Page On After Render Method
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                response = await storageService!.GetItemAsync<AuthenticationResponseModel>("Token");
                loginUserDetails = await userService!.GetLoginUserDetails(response.JwtToken!);
                Pagination.RowCount = 10;
                Pagination.CompanyId = loginUserDetails.CompanyID;
                Pagination = await absenteeService!.GetAbsenteesByCompanyId(Pagination, response.JwtToken!);
                this.StateHasChanged();
            }
        }
        #endregion
        #region Create or Update Modal Calling
        private async void CreateOrUpdateModal()
        {
            EditedAbsenteeID = 0;
            await JSRuntime!.InvokeVoidAsync("CreateEmployeeAbsenteeModal");
        }
        #endregion
        #region Modal Event Call 
        public async void ModalCreateOrUpdateResult(ModificationStatus status)
        {
            if (status.Success)
            {
                SuccessOrFaild = SuccessorFailAlert.Success;
                AlertMessage = status.Message;
                Pagination = await absenteeService!.GetAbsenteesByCompanyId(Pagination, response.JwtToken!);
                this.StateHasChanged();
                AlertCalling.CallAlert(JSRuntime!);

            }
        }
        #endregion
        #region Pagination Selections
        private async Task PaginationResponse(int PageIndex)
        {
            Pagination.PageIndex = PageIndex;
            Pagination = await absenteeService!.GetAbsenteesByCompanyId(Pagination, response.JwtToken!);
            this.StateHasChanged();

        }
        #endregion
        #region Edit Handover Modal Call
        private async Task EditCallModal(int HandoverID)
        {
            EditedAbsenteeID = HandoverID;
            await JSRuntime!.InvokeVoidAsync("CreateEmployeeAbsenteeModal");

        }
        #endregion
        #region Call Delete Confirm Modal
        private async void DeleteConfirmModal(int ID)
        {
            DeletedAbsenteeID = ID;
            DeleteConfirmModalMessage = "Do you want to Delete this Absentee";
            this.StateHasChanged();
            ConfirmationModalCall.ShowModal(JSRuntime!);
        }
        #endregion
        #region Delete Confirmed
        private async Task DeleteConfirmed(bool IsDeleete)
        {
            if (IsDeleete)
            {
                var success = await absenteeService!.DeleteAbsentee(DeletedAbsenteeID, response.JwtToken!);
                if (success)
                {
                    Pagination = await absenteeService!.GetAbsenteesByCompanyId(Pagination, response.JwtToken!);
                    this.StateHasChanged();
                }

                ConfirmationModalCall.HideModal(JSRuntime!);
            }
        }
        #endregion
        #region Print EmployeeAbsentee
        private async Task PrintAbsentee(int ID)
        {
            string navigate = Utility.RouteData.PrintEmployeeAbsentee + "?ID=" + ID + "&token=" + response.JwtToken!;
            await JSRuntime.InvokeVoidAsync("open", navigate, "_blank");
        }
        #endregion
    }
}
