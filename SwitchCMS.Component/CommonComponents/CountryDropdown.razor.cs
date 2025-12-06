using Microsoft.AspNetCore.Components;
using SwitchCMS.Client.Services.Interface;
using SwitchCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Component.CommonComponents
{
    public partial class CountryDropdown
    {
        #region Parameters
        [Parameter]
        public OCRY? ParamSelectedCountry { get; set; }
        [Parameter]
        public EventCallback<OCRY> SelectedCountryCallBack { get; set; }
        #endregion
        #region Service Injections

        [Inject]
        IOCRYService? CountryService { get; set; }

        #endregion
        #region Object and Datatype Declarations
        private List<OCRY> countryList = new List<OCRY>();
        private OCRY SelectedCountry = new OCRY();
        #endregion
        #region Razor Page Life Cycles
        protected async override void OnAfterRender(bool firstRender)
        {

            if (firstRender)
            {
                countryList = await CountryService!.GetAllCountries();
            }

        }
        #endregion
        #region Parameter Change
        protected override void OnParametersSet()
        {
            if (SelectedCountry != null)
            {
                if (SelectedCountry.Code != ParamSelectedCountry!.Code)
                {
                    SelectedCountry = countryList.FirstOrDefault(x => x.Code == ParamSelectedCountry.Code)!;
                }
            }
        }
        #endregion
        #region Search Data
        private async Task<IEnumerable<OCRY>> FilterCountry(string value, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(value))
                return countryList;

            return countryList.Where(x => x.CountryName.Contains(value, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        #endregion
        #region Event Callback For Selected Country
        private async Task OnCountrySelected(OCRY country)
        {
            if (country is null)
                country = new();


            await SelectedCountryCallBack.InvokeAsync(country);
        }
        #endregion
    }
}
