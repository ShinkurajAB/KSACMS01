using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.JSInterop;
using SwitchCMS.Client.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Component.Pages
{
    public partial class Index
    {
        #region Parameter and Injections
        [CascadingParameter]
        public Task<AuthenticationState>? AuthenticationStateTask { get; set; }

        [Inject]
        NavigationManager navigate { get; set; }

        [Inject]
        AuthenticationStateProvider? AuthenticationProvider { get; set; }

        [Inject]
        ISessionStorageService? storageService { get; set; }

        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Inject]
        IOUSRService UserService { get; set; }
        #endregion

        #region Objects and Data type
        string ErrorMessage = String.Empty;
        ClaimsPrincipal user = new ClaimsPrincipal();
        OUSR login = new OUSR();
        AuthenticationResponseModel Response = new AuthenticationResponseModel();
        bool ShowLoginSpinner = false;
        #endregion


        #region On Render after 
        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                user = (await AuthenticationStateTask!).User;
                if (user.Identity!.IsAuthenticated)
                    navigate!.NavigateTo(Utility.RouteData.Dashboard, forceLoad: false);
                ErrorMessage = String.Empty;
                 
            }

        }
        #endregion


        #region Login Button Click


        private async Task LoginUser()
        {
            ShowLoginSpinner = true;
            if (!String.IsNullOrEmpty(login.UserName) && !String.IsNullOrEmpty(login.Password))
            {
                //await JSRuntime.InvokeVoidAsync("ShowLoginSpinner");
                Response = await UserService!.LoginUser(login);
                if (Response.Success)
                {
                    await storageService!.SetItemAsync("Token", Response);
                    AuthenticationStateTask = AuthenticationProvider!.GetAuthenticationStateAsync();
                    var returnUrl = navigate!.ToAbsoluteUri(navigate.Uri);
                    StringValues returnurlValue;
                    QueryHelpers.ParseQuery(returnUrl.Query).TryGetValue("returnUrl", out returnurlValue);
                    if (string.IsNullOrWhiteSpace(returnurlValue))
                        navigate.NavigateTo(Utility.RouteData.Dashboard);
                    else
                        navigate.NavigateTo(returnurlValue!, forceLoad: false);
                }
                else
                {
                    ErrorMessage = Response.message!;

                }
            }
            else
            {
                ErrorMessage = "Please enter all credentials";
            }
            ShowLoginSpinner = false;
            this.StateHasChanged();
        }
        #endregion
    }
}
