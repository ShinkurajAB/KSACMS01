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

namespace SwitchCMS.Component.Pages.Dashboard.MyPassword
{
    public partial class MyPassword
    {
        #region Service Injections
        [Inject]
        IJSRuntime? jsRuntime { get; set; }
        [Inject]
        ISessionStorageService? storageService { get; set; }
        [Inject]
        IOACAService? accessService { get; set; }
        [Inject]
        IOUSRService? userService { get; set; }
        #endregion
        #region Object/Datatype Declarations
        AuthenticationResponseModel responseModel = new AuthenticationResponseModel();
        List<OACA> accessCompanyList = new List<OACA>();
        OACA accessCompany = new OACA();
        OUSR loginUserDetails = new OUSR();
        string errorMessage = string.Empty;
        int deleteAccessId = 0;
        string AlertMessage = string.Empty;
        string DeleteConfirmModalMessage = string.Empty;
        SuccessorFailAlert SuccessOrFaild;
        #endregion
        #region On After Render
        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                responseModel = await storageService!.GetItemAsync<AuthenticationResponseModel>("Token");
                loginUserDetails = await userService!.GetLoginUserDetails(responseModel.JwtToken!);
                accessCompanyList = await accessService!.GetAccessAccountManagersByCompanyId(loginUserDetails.CompanyID, responseModel.JwtToken!);
                this.StateHasChanged();
            }
        }
        #endregion
        #region Plus Button Click
        private async Task ShowAccessControlModal()
        {
            await jsRuntime!.InvokeVoidAsync("ShowAccessControlModal");
        }
        #endregion
        #region Close Access Modal
        private async Task HideAccessControlModal()
        {
            accessCompany = new OACA();
            await jsRuntime!.InvokeVoidAsync("HideAccessAccountModal");
        }
        #endregion
        #region Save Access Control
        private async Task SaveAccessControl()
        {
            if (ValidationCheck())
            {
                bool isSuccess = false;
                accessCompany.CompanyID = loginUserDetails.CompanyID;
                accessCompany.CreatedBy = loginUserDetails.ID;
                if (accessCompany.ID == 0)
                {
                    isSuccess = await accessService!.InsertAccessAccountManager(accessCompany, responseModel.JwtToken!);
                    AlertMessage = "Inserted successfully";
                }
                else
                {
                    isSuccess = await accessService!.UpdateAccessAccountManager(accessCompany, responseModel.JwtToken!);
                    AlertMessage = "Updated successfully";
                }

                if (isSuccess)
                {
                    accessCompany = new OACA();
                    accessCompanyList = await accessService!.GetAccessAccountManagersByCompanyId(loginUserDetails.CompanyID, responseModel.JwtToken!);
                    await HideAccessControlModal();
                    SuccessOrFaild = SuccessorFailAlert.Success;
                    this.StateHasChanged();
                    AlertCalling.CallAlert(jsRuntime!);
                }
            }

        }
        #endregion
        #region Validation
        private bool ValidationCheck()
        {
            errorMessage = string.Empty;
            if (string.IsNullOrEmpty(accessCompany.Platform))
            {
                errorMessage = "Please Enter Platform";
                return false;
            }
            if (string.IsNullOrEmpty(accessCompany.URL))
            {
                errorMessage = "Please Enter URL";
                return false;
            }
            if (string.IsNullOrEmpty(accessCompany.UserName))
            {
                errorMessage = "Please Enter UserName";
                return false;
            }
            if (string.IsNullOrEmpty(accessCompany.Password))
            {
                errorMessage = "Please Enter Password";
                return false;
            }
            return true;
        }
        #endregion
        #region Call Delete Confirm Modal
        private async void DeleteConfirmModal(int accessId)
        {
            deleteAccessId = accessId;
            DeleteConfirmModalMessage = "Are you sure you want to Delete??";
            this.StateHasChanged();
            ConfirmationModalCall.ShowModal(jsRuntime);
        }
        #endregion
        #region Delete Access Control
        private async Task DeleteAccessControl()
        {
            if (deleteAccessId > 0)
            {
                bool isDelete = await accessService!.DeleteAccessAccountManager(deleteAccessId, responseModel.JwtToken!);
                if (isDelete)
                {
                    deleteAccessId = 0;
                    accessCompanyList = await accessService!.GetAccessAccountManagersByCompanyId(loginUserDetails.CompanyID, responseModel.JwtToken!);
                    ConfirmationModalCall.HideModal(jsRuntime);
                    AlertMessage = "Deleted successfully";
                    SuccessOrFaild = SuccessorFailAlert.Success;
                    this.StateHasChanged();
                    AlertCalling.CallAlert(jsRuntime);
                }
            }

        }
        #endregion
        #region Edit button click
        private async Task EditAccessControl(OACA access)
        {
            accessCompany = access;
            await jsRuntime!.InvokeVoidAsync("ShowAccessControlModal");
        }
        #endregion

        #region Copy Text
        private async void CopyText(string element)
        {
            await jsRuntime!.InvokeVoidAsync("copyText", element);
        }
        #endregion
    }
}
