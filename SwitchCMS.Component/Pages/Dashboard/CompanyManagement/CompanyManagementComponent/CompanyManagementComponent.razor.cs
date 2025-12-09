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

namespace SwitchCMS.Component.Pages.Dashboard.CompanyManagement.CompanyManagementComponent
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
            await JSRuntime!.InvokeVoidAsync("hideEditCompanyManagementModal");
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
           
            return true;
        }
        #endregion
        #region Save Company
        private async Task SaveCompany()
        {
            if (Validation())
            {
               ModificationStatus modificationStatus = new ModificationStatus();
                SelectedCompany.Address = "Address";
                if (SelectedCompany.ID == 0)
                {
                    modificationStatus = await companyService!.SignUpCompany(SelectedCompany);
                }
                else
                {
                    modificationStatus.Success = await companyService!.UpdateCompany(SelectedCompany, loginResponse.JwtToken!);
                }
                if (modificationStatus.Success)
                {
                    SelectedCompany.ID = 0;
                    SelectedCompany.Address= string.Empty;
                    SelectedCompany.CRNumber= string.Empty;
                    SelectedCompany.ContactPerson= string.Empty;
                    SelectedCompany.CRCertificate= string.Empty;
                    SelectedCompany.Email= string.Empty;
                    SelectedCompany.Name= string.Empty;
                    SelectedCompany.NationalID= string.Empty;
                    SelectedCompany.PhoneNumber= string.Empty;
                    SelectedCompany.Other = string.Empty;
                    SelectedCompany.Status = Utility.CompanyStatus.Active;
                    SelectedCompany.VATCertificate= string.Empty;
                    SelectedCompany.Country= new OCRY();
                    SelectedCompany.CountryCode= string.Empty;
                    SelectedCompany.ValidationDate= DateTime.Today;
                    

                    AlertMessage = "Saved successfully";
                    SuccessOrFaild = SuccessorFailAlert.Success;
                    await JSRuntime!.InvokeVoidAsync("hideEditCompanyManagementModal");
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
