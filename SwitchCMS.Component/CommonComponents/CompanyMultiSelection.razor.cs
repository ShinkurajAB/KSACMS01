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
    public partial class CompanyMultiSelection
    {
        #region Parameter and Injections
        [Parameter]
        public List<OCMP> ParmSelectedCompanys { get; set; }

        [Inject]
        IOCMPService CompanyService { get; set; }

        [Inject]
        ISessionStorageService? storageService { get; set; }

        [Parameter]
        public EventCallback<List<OCMP>> SelectedCompanyCallBack { get; set; }

        #endregion


        #region Onjects and Datatype
        private List<OCMP> CompanyList = new List<OCMP>();
        AuthenticationResponseModel Response = new AuthenticationResponseModel();
        private IList<OCMP> SelectedCompany = new List<OCMP>();
        #endregion


        #region OnRenderAfter
        protected async override void OnAfterRender(bool firstRender)
        {

            if (firstRender)
            {
                Response = await storageService.GetItemAsync<AuthenticationResponseModel>("Token");
                CompanyList = await CompanyService.GetAllCompany(Response.JwtToken!);
                this.StateHasChanged();
            }

        }
        #endregion


        #region Parameter Change
        protected override void OnParametersSet()
        {
            if (ParmSelectedCompanys != null)
            {
                if (SelectedCompany.Count != ParmSelectedCompanys.Count)
                {
                    SelectedCompany = CompanyList.Where(a => ParmSelectedCompanys.Any(g => g.ID == a.ID)).ToList(); ;
                }

            }

        }
        #endregion

        #region Company List Search
        private Task<IEnumerable<OCMP>> AutoSearchBP(string SearchBpName)
        {
            var result = CompanyList.Where(x => x.Name.Contains(SearchBpName, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(result);

        }
        #endregion

        private async Task OnSelectedCompanyChanged()
        {

            await SelectedCompanyCallBack.InvokeAsync(SelectedCompany.ToList());
        }
    }
}
