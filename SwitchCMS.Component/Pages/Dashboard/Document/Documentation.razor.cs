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

namespace SwitchCMS.Component.Pages.Dashboard.Document
{
    public partial class Documentation
    {
        #region Objects and Parameter
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Inject]
        IOATCService attachService { get; set; }

        [Inject]
        IS3Service s3Service { get; set; }

        [Inject]
        IOUSRService userService { get; set; }

        [Inject]
        ISessionStorageService? storageService { get; set; }
        #endregion

        #region Object and DataType
        AuthenticationResponseModel response = new AuthenticationResponseModel();
        DocumentPagination pagination = new DocumentPagination();
        OUSR LoginUser = new OUSR();

        int EditedID;
        string DeleteConfirmModalMessage = string.Empty;

        int DeletedID;

        string AlertMessage = string.Empty;
        SuccessorFailAlert SuccessOrFaild;
        #endregion

        #region On render After
        protected async override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                response = await storageService!.GetItemAsync<AuthenticationResponseModel>("Token");
                LoginUser = await userService.GetLoginUserDetails(response.JwtToken!);
                pagination.CompanyID = LoginUser.CompanyID;
                pagination.RowCount = 10;
                pagination = await attachService.GetDocumentByPageIndex(pagination, response.JwtToken!);
                this.StateHasChanged();
            }
            base.OnAfterRender(firstRender);
        }
        #endregion

        #region Create Modal Call
        private async Task CreateDocument()
        {
            EditedID = 0;
            await JSRuntime.InvokeVoidAsync("CreateDocumentModal");
        }
        #endregion

        #region Pagination Selections
        private async Task PaginationResponse(int PageIndex)
        {
            pagination.PageIndex = PageIndex;
            pagination = await attachService!.GetDocumentByPageIndex(pagination, response.JwtToken!);
            this.StateHasChanged();

        }
        #endregion

        #region Modal Event CallBack
        private async void ModalResponse(ModificationStatus respon)
        {
            if (respon.Success)
            {
                SuccessOrFaild = SuccessorFailAlert.Success;
                AlertMessage = respon.Message;
                pagination = await attachService!.GetDocumentByPageIndex(pagination, response.JwtToken!);
                this.StateHasChanged();
                AlertCalling.CallAlert(JSRuntime);
            }
        }
        #endregion

        #region Edit Modal Call
        private async Task EditModalCall(int ID)
        {
            EditedID = ID;
            await JSRuntime.InvokeVoidAsync("CreateDocumentModal");
        }
        #endregion

        #region Call Delete Confirm Modal
        private async void DeleteConfirmModal(int ID)
        {
            DeletedID = ID;
            DeleteConfirmModalMessage = "Do you want to Delete this User";
            this.StateHasChanged();
            ConfirmationModalCall.ShowModal(JSRuntime);
        }
        #endregion

        #region Delete Modal Call
        private async void DeleteConfirmed(bool IsDeleete)
        {
            if (IsDeleete)
            {
                var deleteData = pagination.AttachmentDetails.FirstOrDefault(x => x.ID == DeletedID);
                var successorDelete = await attachService.DeleteDocumentByID(deleteData.ID, response.JwtToken!);
                if (successorDelete.Success)
                {
                    await s3Service.DeleteFileAsync(deleteData.FileName, deleteData.CompanyID.ToString());
                    pagination = await attachService!.GetDocumentByPageIndex(pagination, response.JwtToken!);
                    ConfirmationModalCall.HideModal(JSRuntime);

                    SuccessOrFaild = SuccessorFailAlert.Success;
                    AlertMessage = successorDelete.Message;
                    this.StateHasChanged();
                    AlertCalling.CallAlert(JSRuntime);
                }
            }
        }
        #endregion

        #region DownloadFile
        private async Task DownloadFile(string FileName)
        {
            var data = await s3Service.DownloadFile(FileName, LoginUser.CompanyID.ToString());
            var stringBytes = Convert.ToBase64String(data);
            await JSRuntime!.InvokeVoidAsync("DownLoadFile", "application/octet-stream", stringBytes, FileName);
        }
        #endregion
    }
}
