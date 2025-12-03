using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using SwitchCMS.Client.Services.Interface;
using SwitchCMS.Component.Auth;
using SwitchCMS.Model;
using SwitchCMS.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Component.Shared.Layout
{
    public partial class TopBar
    {
        #region Parameters and Injections

        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Inject]
        AuthenticationStateProvider AuthenticationProvider { get; set; }

        [Inject]
        ISessionStorageService session { get; set; }

        [Inject]
        NavigationManager nav { get; set; }

        [Inject]
        IOUSRService userService { get; set; }

        #endregion

        #region Objects and Datatypes
        AuthenticationResponseModel loginResponse = new AuthenticationResponseModel();
        OUSR UserDetails = new OUSR();
        #endregion



        #region OnAfter Render
        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                var data = await session.GetItemAsync<AuthenticationResponseModel>("Token");
                loginResponse = data!;

                UserDetails = await userService.GetLoginUserDetails(loginResponse.JwtToken!);

                this.StateHasChanged();
            }

        }
        #endregion


        #region Sidebar toggles
        private async void SidebarToggle()
        {
            await JSRuntime.InvokeVoidAsync("SideBarToggle");
        }
        #endregion

        #region logout button
        async void LogOut()
        {
            //await jscall.InvokeVoidAsync("HideLogoutModal");
            var customAuthState = (CustomAuthenticationStateProvider)AuthenticationProvider;

            string absurl = nav.Uri;
            await customAuthState.UpdateAuthenticationState(null);

            Navigatesame(absurl);
        }
        #endregion


        #region Navigate when logout click
        void Navigatesame(string absurl = "")
        {
            if (string.IsNullOrEmpty(absurl))
            {
                string uri = nav.Uri;
                if (nav.Uri.EndsWith("#"))
                {
                    uri = nav.Uri.Remove(nav.Uri.Length - 1);
                }

                nav.NavigateTo(uri, forceLoad: false);
            }
            else
            {
                nav.NavigateTo(absurl, forceLoad: false);
            }
        }
        #endregion

        #region Down Menu Signout show
        private async Task ShowProfileandSignout()
        {
            await JSRuntime.InvokeVoidAsync("showUserDetailDrodown");
        }
        #endregion
    }
}
