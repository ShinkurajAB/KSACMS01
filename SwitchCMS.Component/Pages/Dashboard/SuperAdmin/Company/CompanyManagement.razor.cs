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

namespace SwitchCMS.Component.Pages.Dashboard.SuperAdmin.Company
{
    public partial class CompanyManagement
    {
        #region Service Injections
        [Inject]
        IJSRuntime? JSRuntime { get; set; }

        [Inject]
        IOCMPService? companyService { get; set; }

        [Inject]
        ISessionStorageService? storageService { get; set; }
        #endregion
        #region Object/Datatype Declarations
        CompanyPagination companyPagination = new CompanyPagination();
        AuthenticationResponseModel responseModel = new AuthenticationResponseModel();
        OCMP companyModal = new OCMP();
        string DeleteConfirmModalMessage = string.Empty;
        int DeletedComapnyID = 0;
        string AlertMessage = string.Empty;
        SuccessorFailAlert SuccessOrFaild;
        #endregion
        #region Razor Page Life Cycles
        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                responseModel = await storageService!.GetItemAsync<AuthenticationResponseModel>("Token");
                companyPagination.RowCount = 10;
                companyPagination = await companyService!.GetCompanyByPagination(companyPagination, responseModel.JwtToken!);
                this.StateHasChanged();
            }
        }
        #endregion
        #region Pagination Selections
        private async Task PaginationResponse(int PageIndex)
        {
            companyPagination.PageIndex = PageIndex;
            companyPagination = await companyService!.GetCompanyByPagination(companyPagination, responseModel.JwtToken!);
            this.StateHasChanged();

        }
        #endregion

        #region Approve Company
        private async Task UpdateCompanyStatus(int companyId)
        {
            bool isSuccess = await companyService!.UpdateCompanyStatus(companyId, Utility.CompanyStatus.Active.ToString(), responseModel.JwtToken!);
            if (isSuccess)
            {
                companyPagination = await companyService!.GetCompanyByPagination(companyPagination, responseModel.JwtToken!);
                this.StateHasChanged();
            }
        }
        #endregion

        #region Delete Company
        private async Task DeleteCompany()
        {
            if (DeletedComapnyID > 0)
            {
                bool isDelete = await companyService!.DeleteCompany(DeletedComapnyID, responseModel.JwtToken!);
                if (isDelete)
                {
                    DeletedComapnyID = 0;
                    companyPagination = await companyService!.GetCompanyByPagination(companyPagination, responseModel.JwtToken!);
                    ConfirmationModalCall.HideModal(JSRuntime);
                    AlertMessage = "Deleted successfully";
                    SuccessOrFaild = SuccessorFailAlert.Success;
                    this.StateHasChanged();
                    AlertCalling.CallAlert(JSRuntime);
                }
            }

        }
        #endregion

        #region Call Delete Confirm Modal
        private async void DeleteConfirmModal(int companyId)
        {
            DeletedComapnyID = companyId;
            DeleteConfirmModalMessage = "Are you sure you want to Delete this Company";
            this.StateHasChanged();
            ConfirmationModalCall.ShowModal(JSRuntime);
        }
        #endregion

        #region Plus button click
        private async Task ShowCompanyModal()
        {
            await JSRuntime!.InvokeVoidAsync("ShowCompanyModal");
        }
        #endregion

        #region Modal Event Call 
        public async void ModalCreateOrUpdateResult(bool success)
        {
            if (success)
            {
                AlertMessage = "Modified successfully";
                SuccessOrFaild = SuccessorFailAlert.Success;
                companyPagination = await companyService!.GetCompanyByPagination(companyPagination, responseModel.JwtToken!);
                this.StateHasChanged();
                AlertCalling.CallAlert(JSRuntime);
            }
        }
        #endregion

        #region Edit Company
        public async void EditCompany(OCMP company)
        {
            companyModal = company;
            await JSRuntime!.InvokeVoidAsync("ShowCompanyModal");
        }
        #endregion
    }
}
