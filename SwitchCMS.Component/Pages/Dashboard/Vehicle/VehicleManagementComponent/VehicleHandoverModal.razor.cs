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

namespace SwitchCMS.Component.Pages.Dashboard.Vehicle.VehicleManagementComponent
{
    public partial class VehicleHandoverModal
    {
        #region Parameter and Injections
        [Inject]
        IJSRuntime? JSRuntime { get; set; }

        [Inject]
        ISessionStorageService? storageService { get; set; }


        [Inject]
        IOUSRService? userService { get; set; }

        [Inject]
        IVHL1Service? HandoverService { get; set; }

        [Parameter]
        public int ParmHandoverID { get; set; }
        [Parameter]
        public EventCallback<ModificationStatus> ModalCreateorUpdate { get; set; }
        #endregion



        #region Objects and Datatype
        AuthenticationResponseModel response = new AuthenticationResponseModel();
        ModificationStatus Result = new ModificationStatus();
        OUSR loginUserDetails = new OUSR();
        VHL1 Model = new VHL1();
        int HandoverID;
        #endregion


        #region OnrenderAfter
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                response = await storageService!.GetItemAsync<AuthenticationResponseModel>("Token");
                loginUserDetails = await userService.GetLoginUserDetails(response.JwtToken!);
                Model.CompanyID = loginUserDetails.CompanyID;
                this.StateHasChanged();
            }


        }
        #endregion

        #region Parameter Change
        protected override async Task OnParametersSetAsync()
        {
            if (ParmHandoverID == 0)
            {
                Model = new VHL1();
                Model.CompanyID = loginUserDetails.CompanyID;
                HandoverID = ParmHandoverID;
            }
            else
            {
                if (ParmHandoverID != HandoverID)
                {
                    HandoverID = ParmHandoverID;
                    Model = await HandoverService.GetHandoverByHandoverID(HandoverID, response.JwtToken!);
                }
            }




        }
        #endregion


        #region Hide Modal
        private async Task HideModal()
        {
            await JSRuntime.InvokeVoidAsync("HideHandoverModal");
        }
        #endregion

        #region Submit Handover
        private async Task SubmitData()
        {
            if (HandoverID > 0)
            {
                Result = await HandoverService.UpdateHandover(Model, response.JwtToken!);
                if (Result.Success)
                {
                    await ModalCreateorUpdate.InvokeAsync(Result);
                    await HideModal();
                }
            }
            else
            {
                Result = await HandoverService.CreateHandover(Model, response.JwtToken!);
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
