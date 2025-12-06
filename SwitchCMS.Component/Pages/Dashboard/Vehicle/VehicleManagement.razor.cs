using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SwitchCMS.Client.Services;
using SwitchCMS.Client.Services.Interface;
using SwitchCMS.Component.Data;
using SwitchCMS.Model;
using SwitchCMS.Model.Authentication;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static MudBlazor.CategoryTypes;

namespace SwitchCMS.Component.Pages.Dashboard.Vehicle
{
    public partial class VehicleManagement
    {

        #region Parameter and Injections
        [Inject]
        IJSRuntime? JSRuntime { get; set; }

        [Inject]
        ISessionStorageService? storageService { get; set; }

        [Inject]
        IOVHLService? VehicleService { get; set; }


        [Inject]
        IOUSRService? userService { get; set; }
        #endregion

        #region Objects and Declaration


        VehiclePagination Pagination=new VehiclePagination();
        AuthenticationResponseModel response = new AuthenticationResponseModel();
        OUSR loginUserDetails = new OUSR();

        private int EditedVehicleID;
        private int DeletedVehicleID;
        string DeleteConfirmModalMessage = string.Empty;


        string AlertMessage = string.Empty;
        SuccessorFailAlert SuccessOrFaild;
        #endregion


        #region Onrender After
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                response = await storageService!.GetItemAsync<AuthenticationResponseModel>("Token");
                loginUserDetails = await userService.GetLoginUserDetails(response.JwtToken!);
                Pagination.RowCount = 10;
                Pagination.CompanyID = loginUserDetails.CompanyID;
                Pagination = await VehicleService.GetVehicleByPageIndex(Pagination, response.JwtToken!);
                this.StateHasChanged();
            }
             
        }
        #endregion


        #region Create or Update Modal Calling
        private async void CreateOrUpdateModal()
        {
            EditedVehicleID = 0;
            await JSRuntime.InvokeVoidAsync("CreateVehicleModal");
        }
        #endregion

        #region Modal Event Call 
        public async void ModalCreateOrUpdateResult(ModificationStatus status)
        {
            if (status.Success)
            {
                SuccessOrFaild=SuccessorFailAlert.Success;
                AlertMessage = status.Message;
                Pagination = await VehicleService.GetVehicleByPageIndex(Pagination, response.JwtToken!);
                this.StateHasChanged();
                AlertCalling.CallAlert(JSRuntime);
               
            }
        }
        #endregion

        #region Pagination Selections
        private async Task PaginationResponse(int PageIndex)
        {
            Pagination.PageIndex = PageIndex;
            Pagination = await VehicleService.GetVehicleByPageIndex(Pagination, response.JwtToken!);
            this.StateHasChanged();

        }
        #endregion

        #region Edit Vehicle Modal Call
        private async Task EditCallModal(int VehicleID)
        {
            EditedVehicleID = VehicleID;
            await JSRuntime.InvokeVoidAsync("CreateVehicleModal");

        }
        #endregion

        #region Call Delete Confirm Modal
        private async void DeleteConfirmModal(int ID)
        {
            DeletedVehicleID = ID;
            DeleteConfirmModalMessage = "Do you want to Delete this Vehicle";
            this.StateHasChanged();
            ConfirmationModalCall.ShowModal(JSRuntime);
        }
        #endregion

        #region Delete Confirmed
        private async Task DeleteConfirmed(bool IsDeleete)
        {
            if (IsDeleete)
            {
                var success = await VehicleService.DeleteVehicle(DeletedVehicleID, response.JwtToken!);
                if (success.Success)
                {
                    Pagination = await VehicleService.GetVehicleByPageIndex(Pagination, response.JwtToken!);
                    this.StateHasChanged();
                }
                              
                ConfirmationModalCall.HideModal(JSRuntime);
            }
        }
        #endregion
    }
}
