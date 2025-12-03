using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SwitchCMS.Client.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Component.Shared.Layout
{
    public partial class SideMenus
    {
        #region Injection and Parameter
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        [Inject]
        ISessionStorageService session { get; set; }
        [Inject]
        IOUSRService userService { get; set; }

        [Inject]
        IOMENService MenuService { get; set; }
        #endregion

        #region Objects and Datatypes
        AuthenticationResponseModel loginResponse = new AuthenticationResponseModel();
        OUSR UserDetails = new OUSR();
        List<OMEN> Menus = new List<OMEN>();
        #endregion


        #region OnAfter Render
        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                var data = await session.GetItemAsync<AuthenticationResponseModel>("Token");
                loginResponse = data!;

                UserDetails = await userService.GetLoginUserDetails(loginResponse.JwtToken!);


                Menus = await MenuService.GetAllMenu(loginResponse.JwtToken!);


                this.StateHasChanged();

                //await JSRuntime.InvokeVoidAsync("SideBarToggle");
            }

        }
        #endregion
    }
}
