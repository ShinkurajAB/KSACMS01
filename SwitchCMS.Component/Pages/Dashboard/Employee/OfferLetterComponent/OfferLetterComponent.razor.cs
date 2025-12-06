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

namespace SwitchCMS.Component.Pages.Dashboard.Employee.OfferLetterComponent
{
    public partial class OfferLetterComponent
    {
        #region Parameter and Injections
        [Inject]
        IJSRuntime? JSRuntime { get; set; }

        [Inject]
        ISessionStorageService? storageService { get; set; }

        [Inject]
        IHEM4Service? OfferLetterService { get; set; }

        [Inject]
        IOUSRService? userService { get; set; }


        [Parameter]
        public int ParamOfferID { get; set; }
        [Parameter]
        public EventCallback<ModificationStatus> ModalCreateorUpdate { get; set; }
        #endregion

        #region Objects and Datatype

        int offerId;
        HEM4 Model = new HEM4();
        AuthenticationResponseModel response = new AuthenticationResponseModel();
        ModificationStatus Result = new ModificationStatus();
        OUSR loginUserDetails = new OUSR();

        #endregion
        #region OnrenderAfter
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                response = await storageService!.GetItemAsync<AuthenticationResponseModel>("Token");
                loginUserDetails = await userService!.GetLoginUserDetails(response.JwtToken!);
                Model.CompanyID = loginUserDetails.CompanyID;
                this.StateHasChanged();
            }


        }
        #endregion
        #region Parameter Change
        protected override async Task OnParametersSetAsync()
        {
            if (ParamOfferID == 0)
            {
                Model = new HEM4();
                Model.CompanyID = loginUserDetails.CompanyID;
                offerId = ParamOfferID;
            }
            else
            {
                if (ParamOfferID != offerId)
                {
                    offerId = ParamOfferID;
                    Model = await OfferLetterService!.GetOfferLetterById(offerId, response.JwtToken!);
                }
            }
        }
        #endregion
        #region Hide Modal
        private async Task HideModal()
        {
            await JSRuntime!.InvokeVoidAsync("HideOfferLetterModal");
        }
        #endregion
        #region Submit Offer Letter Data
        private async Task SubmitData()
        {
            if (offerId > 0)
            {
                Result = await OfferLetterService!.UpdateOfferLetter(Model, response.JwtToken!);
                if (Result.Success)
                {
                    await ModalCreateorUpdate.InvokeAsync(Result);
                    await HideModal();
                }
            }
            else
            {
                Result = await OfferLetterService!.InsertOfferLetter(Model, response.JwtToken!);
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
