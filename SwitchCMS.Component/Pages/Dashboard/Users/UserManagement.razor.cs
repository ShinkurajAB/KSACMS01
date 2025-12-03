using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SwitchCMS.Client.Services.Interface;
using SwitchCMS.Component.Data;
using SwitchCMS.Model;
using SwitchCMS.Model.Authentication;
using SwitchCMS.Model.UI;
using SwitchCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Component.Pages.Dashboard.Users
{
    public partial class UserManagement
    {
        #region Injection and Parameter
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Inject]
        IOUSRService userService { get; set; }

        [Inject]
        ISessionStorageService? storageService { get; set; }



        #endregion

        #region Objects and Datattype
        OUSR User = new OUSR();
        UsersPagination Pagination = new UsersPagination();
        AuthenticationResponseModel Response = new AuthenticationResponseModel();
        List<OCMP> ComapnyList = new List<OCMP>();


        private OCMP SelectedCompany = new OCMP();


        // Delete Modal Message
        string DeleteConfirmModalMessage = string.Empty;
        int DeletedUserID;
        #endregion

        #region after render
        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                Response = await storageService.GetItemAsync<AuthenticationResponseModel>("Token");
                Pagination.RowCount = 10;
                var LoginUSerDetails = await userService.GetLoginUserDetails(Response.JwtToken!);
                if (Response.Role != Roles.SuperAdmin)
                {
                    Pagination.SelectedCompany = LoginUSerDetails.Company;
                }

                Pagination = await userService.GetUserByPagination(Pagination, Response.JwtToken!);



                this.StateHasChanged();
            }

        }
        #endregion


        #region Pagination Selections
        private async void PaginationResponse(int PageIndex)
        {
            Pagination.PageIndex = PageIndex;
            Pagination = await userService.GetUserByPagination(Pagination, Response.JwtToken!);
            this.StateHasChanged();

        }
        #endregion

        #region On Company Selected
        private async void CompanySelection(OCMP Company)
        {
            SelectedCompany = Company;
            Pagination.SelectedCompany = Company;
            Pagination = await userService.GetUserByPagination(Pagination, Response.JwtToken!);
            this.StateHasChanged();
        }
        #endregion

        #region Grid Edit Button to Show Modal
        private async Task SelectedUser(OUSR SelectedUser)
        {
            User = SelectedUser;
            this.StateHasChanged();
            await JSRuntime.InvokeVoidAsync("UseropenModal");

        }
        #endregion

        #region Open Modal New User Create
        private async void CreateNewUserModal()
        {
            User = new OUSR();
            if (Response.Role == Roles.SuperAdmin)
            {
                User.Role = Utility.Roles.CompanyAdmin;
                User.Status = Status.Active;
            }
            else
            {
                User.Role = Utility.Roles.EmployeeUser;
                User.Status = Status.Active;
            }
            this.StateHasChanged();
            await JSRuntime.InvokeVoidAsync("UseropenModal");
        }
        #endregion

        #region Modal Event Call 
        public async void ModalCreateOrUpdateResult(bool success)
        {
            if (success)
            {
                Pagination = await userService.GetUserByPagination(Pagination, Response.JwtToken!);
                this.StateHasChanged();
            }
        }
        #endregion

        #region Call Delete Confirm Modal
        private async void DeleteConfirmModal(int userID)
        {
            DeletedUserID = userID;
            DeleteConfirmModalMessage = "Do you want to Delete this User";
            this.StateHasChanged();
            ConfirmationModalCall.ShowModal(JSRuntime);
        }
        #endregion


        #region Delete Confirmed
        private async void DeleteConfirmed(bool IsDeleete)
        {
            if (IsDeleete)
            {
                var Sucess = await userService.DeleteUserByUserID(DeletedUserID, Response?.JwtToken!);
                if (Sucess.Success)
                {
                    Pagination = await userService.GetUserByPagination(Pagination, Response.JwtToken!);
                    ConfirmationModalCall.HideModal(JSRuntime);
                    this.StateHasChanged();
                }
            }
        }
        #endregion
    }
}
