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
    public partial class VehocleCreateorUpdate
    {


        #region Parameter and Injections
        [Inject]
        IJSRuntime? JSRuntime { get; set; }

        [Inject]
        ISessionStorageService? storageService { get; set; }

        [Inject]
        IOVHLService? VehicleService {  get; set; }

        [Inject]
        IOUSRService? userService { get; set; }

        [Parameter]
        public int ParamVehicleID {  get; set; }
        [Parameter]
        public EventCallback<ModificationStatus> ModalCreateorUpdate { get; set; }
        #endregion

        #region Objects and Datatype

        int VehicleID;
        OVHL Model = new OVHL();
        AuthenticationResponseModel response = new AuthenticationResponseModel();
        ModificationStatus Result=new ModificationStatus();
        OUSR loginUserDetails = new OUSR();

        #endregion


        #region OnrenderAfter
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                response = await storageService!.GetItemAsync<AuthenticationResponseModel>("Token");
                loginUserDetails = await userService.GetLoginUserDetails(response.JwtToken!);
                Model.CompanyID=loginUserDetails.CompanyID;
                this.StateHasChanged();
            }

           
        }
        #endregion

        #region Parameter Change
        protected override async Task OnParametersSetAsync()
        {
            if(ParamVehicleID==0)
            {
                Model=new OVHL();   
                Model.CompanyID= loginUserDetails.CompanyID;
                VehicleID = ParamVehicleID;
            }
            else
            {
                if (ParamVehicleID != VehicleID)
                {
                    VehicleID = ParamVehicleID;
                    Model=await VehicleService.GetVehicleByVehcileID(VehicleID, response.JwtToken!);
                }
            }

           
            
             
        }
        #endregion


        #region Hide Modal
        private async Task HideModal()
        {
            await JSRuntime.InvokeVoidAsync("HideVehicleModal");
        }
        #endregion

        #region Submit Vehicle
        private async Task SubmitData()
        {
            if(VehicleID>0)
            {
                Result = await VehicleService.UpdateVehicle(Model, response.JwtToken!);
                if(Result.Success)
                {
                    await ModalCreateorUpdate.InvokeAsync(Result);
                    await HideModal();
                }
            }
            else
            {
                Result = await VehicleService.CreateVehicle(Model, response.JwtToken!);
                if (Result.Success)
                {
                   await  ModalCreateorUpdate.InvokeAsync(Result);
                   await HideModal();
                }
            }
        }
        #endregion

    }
}
