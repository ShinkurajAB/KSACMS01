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
    public partial class GeneralWarning
    {
        #region Service Injections
        [Inject]
        IJSRuntime? JSRuntime { get; set; }

        [Inject]
        ISessionStorageService? storageService { get; set; }
        [Inject]
        IHEM5Service? warningService { get; set; }
        [Inject]
        IOUSRService? userService { get; set; }
        #endregion
        #region Object/Datatype Declaration
        GeneralWarningPagination Pagination = new GeneralWarningPagination();
        AuthenticationResponseModel response = new AuthenticationResponseModel();
        OUSR loginUserDetails = new OUSR();
        private int EditedWarningID = 0;
        private int DeletedWarningID = 0;
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
                Pagination.CompanyID = loginUserDetails.CompanyID;
                Pagination = await warningService!.GetGeneralWarningPagination(Pagination, response.JwtToken!);
                this.StateHasChanged();
            }
        }
        #endregion
        #region Create or Update Modal Calling
        private async void CreateOrUpdateModal()
        {
            EditedWarningID = 0;
            await JSRuntime!.InvokeVoidAsync("CreateEmployeeWarningModal");
        }
        #endregion
        #region Modal Event Call 
        public async void ModalCreateOrUpdateResult(ModificationStatus status)
        {
            if (status.Success)
            {
                SuccessOrFaild = SuccessorFailAlert.Success;
                AlertMessage = status.Message;
                Pagination = await warningService!.GetGeneralWarningPagination(Pagination, response.JwtToken!);
                this.StateHasChanged();
                AlertCalling.CallAlert(JSRuntime!);

            }
        }
        #endregion
        #region Pagination Selections
        private async Task PaginationResponse(int PageIndex)
        {
            Pagination.PageIndex = PageIndex;
            Pagination = await warningService!.GetGeneralWarningPagination(Pagination, response.JwtToken!);
            this.StateHasChanged();

        }
        #endregion
        #region Edit Handover Modal Call
        private async Task EditCallModal(int WarningID)
        {
            EditedWarningID = WarningID;
            await JSRuntime!.InvokeVoidAsync("CreateEmployeeWarningModal");

        }
        #endregion
        #region Call Delete Confirm Modal
        private async void DeleteConfirmModal(int ID)
        {
            DeletedWarningID = ID;
            DeleteConfirmModalMessage = "Do you want to Delete this Warning";
            this.StateHasChanged();
            ConfirmationModalCall.ShowModal(JSRuntime!);
        }
        #endregion
        #region Delete Confirmed
        private async Task DeleteConfirmed(bool IsDelete)
        {
            if (IsDelete)
            {
                var status = await warningService!.DeleteGeneralWarning(DeletedWarningID, response.JwtToken!);
                if (status.Success)
                {
                    Pagination = await warningService!.GetGeneralWarningPagination(Pagination, response.JwtToken!);
                    this.StateHasChanged();
                }

                ConfirmationModalCall.HideModal(JSRuntime!);
            }
        }
        #endregion
        #region Print General Warning
        private async Task PrintWarning(int ID)
        {
            string navigate = Utility.RouteData.PrintWarningLetter + "?ID=" + ID + "&token=" + response.JwtToken!;
            await JSRuntime.InvokeVoidAsync("open", navigate, "_blank");
        }
        #endregion
    }
}
