using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
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

namespace SwitchCMS.Component.Pages.Dashboard.SuperAdmin.Company.CompanyManagementComponent
{
    public partial class CompanyManagementComponent
    {

        #region Parameters
        [Parameter]
        public OCMP ParamCompany { get; set; }

        [Parameter]
        public EventCallback<bool> ModalCreateorUpdate { get; set; }
        #endregion
        #region Service Injections
        [Inject]
        public IOCMPService? companyService { get; set; }

        [Inject]
        ISessionStorageService? storageService { get; set; }

        [Inject]
        IJSRuntime? JSRuntime { get; set; }
        [Inject]
        IOCRYService? countryService { get; set; }
        #endregion
        #region Object/Datatype Declarations
        AuthenticationResponseModel loginResponse = new AuthenticationResponseModel();

        private OCMP SelectedCompany = new();
        string errorMessage = string.Empty;
        string AlertMessage = string.Empty;
        OCRY selectedCountry = new OCRY();
        SuccessorFailAlert SuccessOrFaild;
        #endregion
        #region Razor Page On After Render
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

            if (firstRender)
            {
                loginResponse = await storageService!.GetItemAsync<AuthenticationResponseModel>("Token");

                SelectedCompany.Status = Utility.CompanyStatus.Active;
                SelectedCompany.ValidationDate = DateTime.Today;
                this.StateHasChanged();
            }
        }
        #endregion
        #region Parameter Changes
        protected override async Task OnParametersSetAsync()
        {
            if (ParamCompany.ID != SelectedCompany.ID)
            {
                SelectedCompany = ParamCompany;
                selectedCountry.Code = SelectedCompany.CountryCode;
            }
        }
        #endregion
        #region Close Add Company Modal
        private async Task CloseAddCompanyModal()
        {
            await JSRuntime!.InvokeVoidAsync("HideCompanyModal");
        }
        #endregion
        #region Validation
        private bool Validation()
        {
            errorMessage = string.Empty;
            if (string.IsNullOrEmpty(SelectedCompany.Name))
            {
                errorMessage = "Please Enter Company Name";
                return false;
            }
            if (string.IsNullOrEmpty(SelectedCompany.Address))
            {
                errorMessage = "Please Enter Address";
                return false;
            }
            if (string.IsNullOrEmpty(SelectedCompany.Email))
            {
                errorMessage = "Please Enter Email";
                return false;
            }
            if (string.IsNullOrEmpty(SelectedCompany.PhoneNumber))
            {
                errorMessage = "Please Enter Phone Number";
                return false;
            }
            if (string.IsNullOrEmpty(SelectedCompany.CountryCode))
            {
                errorMessage = "Please Select Country";
                return false;
            }
            if (string.IsNullOrEmpty(SelectedCompany.Status.ToString()))
            {
                errorMessage = "Please Select Status";
                return false;
            }
            if (SelectedCompany.ValidationDate!.Value.Date < DateTime.Today.Date)
            {
                errorMessage = "Please Select Validation Date Greater Than Today";
                return false;
            }
            return true;
        }
        #endregion
        #region Save Company
        private async Task SaveCompany()
        {
            if (Validation())
            {
                bool isSuccess = false;
                if (SelectedCompany.ID == 0)
                {
                    isSuccess = await companyService!.SignUpCompany(SelectedCompany);
                }
                else
                {
                    isSuccess = await companyService!.UpdateCompany(SelectedCompany, loginResponse.JwtToken!);
                }
                if (isSuccess)
                {
                    SelectedCompany = new OCMP();
                    AlertMessage = "Saved successfully";
                    SuccessOrFaild = SuccessorFailAlert.Success;
                    await JSRuntime!.InvokeVoidAsync("HideCompanyModal");
                    await ModalCreateorUpdate.InvokeAsync(true);
                    this.StateHasChanged();
                    AlertCalling.CallAlert(JSRuntime!);

                }

            }
        }
        #endregion

        #region On Country Selection Event Call Back
        private void CountrySelection(OCRY country)
        {
            selectedCountry = country;
            SelectedCompany.CountryCode = selectedCountry.Code;
            this.StateHasChanged();
        }
        #endregion
    }
}
