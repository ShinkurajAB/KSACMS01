using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using SwitchCMS.Client.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Component.CommonComponents
{
    public partial class CompanyDropdown
    {
        #region Parameter and Injections
        [Parameter]
        public OCMP ParmSelectedCompany { get; set; }

        [Inject]
        IOCMPService CompanyService { get; set; }

        [Inject]
        ISessionStorageService? storageService { get; set; }

        [Parameter]
        public EventCallback<OCMP> SelectedCompanyCallBack { get; set; }

        #endregion

        #region Onjects and Datatype
        private List<OCMP> CompanyList = new List<OCMP>();
        AuthenticationResponseModel Response = new AuthenticationResponseModel();
        private OCMP SelectedCompany = new OCMP();
        #endregion

        #region OnRenderAfter
        protected async override void OnAfterRender(bool firstRender)
        {

            if (firstRender)
            {
                Response = await storageService.GetItemAsync<AuthenticationResponseModel>("Token");
                CompanyList = await CompanyService.GetAllCompany(Response.JwtToken!);
            }

        }
        #endregion

        #region Parameter Change
        protected override void OnParametersSet()
        {
            if (SelectedCompany.ID != ParmSelectedCompany.ID)
            {
                SelectedCompany = ParmSelectedCompany;
            }
        }
        #endregion

        #region Search Data
        private async Task<IEnumerable<OCMP>> FilterCompany(string value, CancellationToken toke)
        {
            if (string.IsNullOrWhiteSpace(value))
                return CompanyList;

            return CompanyList.Where(x => x.Name.Contains(value, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        #endregion

        #region Event CallBack Selected Country
        private async Task OnCompanySelected(OCMP company)
        {
            if (company is null)
                company = new();

            // Do whatever you need here
            Console.WriteLine($"Selected company: {company.Name}");

            // Example: trigger another method
            await SelectedCompanyCallBack.InvokeAsync(company);
        }
        #endregion
    }
}
