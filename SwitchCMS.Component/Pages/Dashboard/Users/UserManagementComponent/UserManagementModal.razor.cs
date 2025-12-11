using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using SwitchCMS.Client.Services.Interface;
using SwitchCMS.Component.Data;
using SwitchCMS.Model;
using SwitchCMS.Model.Authentication;
using SwitchCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Component.Pages.Dashboard.Users.UserManagementComponent
{
    public partial class UserManagementModal
    {

        #region Parameter and Injections
        [Parameter]
        public OUSR ParmUser { get; set; }

        [Parameter]
        public EventCallback<bool> ModalCreateorUpdate { get; set; }


        [Inject]
        public IOCMPService companyService { get; set; }

        [Inject]
        ISessionStorageService? storageService { get; set; }

        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Inject]
        IOUSRService UserService { get; set; }

        [Inject]
        IOMENService MenuService { get; set; }

        #endregion

        #region Object and Parameter
        private OUSR SelectedUser = new();

        AuthenticationResponseModel Response = new AuthenticationResponseModel();

        private OCMP SelectedCompany = new();

        private Roles SelectedRole;
        private Status userStatus;
        string ErroMessage = string.Empty;


        List<OMEN> MenuList = new List<OMEN>();

        List<OMEN> UserMenus = new List<OMEN>();


        string Message = string.Empty;
        SuccessorFailAlert SucessOrFaild;


        bool MenuAssignorNot;

        #endregion


        #region After Render
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

            if (firstRender)
            {
                Response = await storageService.GetItemAsync<AuthenticationResponseModel>("Token");
                SelectedRole = Utility.Roles.EmployeeUser;
                MenuList = await MenuService.GetAllMenu(Response.JwtToken!);
                MenuList.ForEach(x => x.Selector = "available");
                var LoginUSerDetails = await UserService.GetLoginUserDetails(Response.JwtToken!);
                if (LoginUSerDetails.Role == Roles.CompanyAdmin)
                {
                    SelectedCompany = LoginUSerDetails.Company;
                }
            }

        }
        #endregion


        #region Paremeter Changes
        protected override async Task OnParametersSetAsync()
        {
            if (ParmUser != null)
            {
                if (SelectedUser.ID != ParmUser.ID)
                {
                    SelectedUser = ParmUser;

                    SelectedCompany = SelectedUser.Company;
                    userStatus = SelectedUser.Status;
                    SelectedRole = SelectedUser.Role;

                    if (SelectedUser.ID == Convert.ToInt32(Response.Id))
                    {
                        MenuAssignorNot = true;
                    }
                    else
                    {
                        MenuAssignorNot = false;
                    }
                    if (SelectedUser.ID > 0)
                    {
                        UserMenus = await MenuService.GetMenuByUserID(SelectedUser.ID, Response.JwtToken!);

                        if (UserMenus != null)
                        {
                            if (UserMenus.Count > 0)
                            {
                                foreach (var menu in MenuList)
                                {
                                    if (UserMenus.Any(u => u.ID == menu.ID))
                                    {
                                        menu.Selector = "assigned";
                                    }
                                }
                            }
                        }
                    }

                }
            }

        }
        #endregion

        #region On Company Selected
        private void CompanySelection(OCMP Company)
        {
            SelectedCompany = Company;
            SelectedUser.CompanyID = Company.ID;
            SelectedUser.Company = SelectedCompany;
            this.StateHasChanged();
        }
        #endregion

        #region Close Modal
        private async void CloseModal()
        {
            MenuList.ForEach(x => x.Selector = "available");
            await JSRuntime.InvokeVoidAsync("UsercloseModal");
        }

        #endregion

        #region Create User
        private async void CreeateUpdateUser()
        {
            SelectedUser.Status = userStatus;
            SelectedUser.Role = SelectedRole;

            // if login user is company admin then create/ update user company is same as login user company
            if (Response.Role == Roles.CompanyAdmin)
            {
                var LoginUSerDetails = await UserService.GetLoginUserDetails(Response.JwtToken!);
                SelectedUser.CompanyID = LoginUSerDetails.CompanyID;
            }

            //UserMenu Add
            var UserAssignedMenu = new List<OUMN>();
            foreach (var menu in MenuList.Where(x => x.Selector == "assigned"))
            {
                UserAssignedMenu.Add(new OUMN { UserID = SelectedUser.ID, MenuID = menu.ID });
            }

            SelectedUser.UserMenus = UserAssignedMenu;

            var status = await UserService.CreeateUpdateUser(SelectedUser, Response.JwtToken!);
            if (status != null)
            {
                if (status.Success)
                {
                    SucessOrFaild = SuccessorFailAlert.Success;

                    await JSRuntime.InvokeVoidAsync("UsercloseModal");
                    await ModalCreateorUpdate.InvokeAsync(true);
                }
                else
                {
                    SucessOrFaild = SuccessorFailAlert.Faild;
                    ErroMessage = status.Message;
                }
            }

            Message = status.Message;

            this.StateHasChanged();

            AlertCalling.CallAlert(JSRuntime);
        }
        #endregion

        #region Dragable Item Updates
        private void OnItemDropped(MudItemDropInfo<OMEN> drop)
        {
            drop.Item.Selector = drop.DropzoneIdentifier;

        }
        #endregion
    }
}
