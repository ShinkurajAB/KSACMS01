using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using SwitchCMS.Client.Services.Interface;
using SwitchCMS.Component.Data;
using SwitchCMS.Model.Authentication;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Component.Pages.Dashboard.AdvManagement
{
    public partial class AdvManagement
    {
        #region Injection 

        [Inject]
        IOADVService AdvatismentService { get; set; }

        [Inject]
        IS3Service S3Service { get; set; }

        [Inject]
        IJSRuntime jSRuntime { get; set; }

        [Inject]
        IConfiguration configuration { get; set; }
        [Inject]
        ISessionStorageService? storageService { get; set; }
        #endregion

        #region Objects and Datatypes
        AuthenticationResponseModel response = new AuthenticationResponseModel();
        AdvatisementPagination pagination = new AdvatisementPagination();
        string FolderPath = string.Empty;
        string UrlBasePath = string.Empty;
        string FilePath = string.Empty;

        int EditedID;
        string DeleteConfirmModalMessage = string.Empty;

        int DeletedID;


        string AlertMessage = string.Empty;
        SuccessorFailAlert SuccessOrFaild;


        #endregion

        #region Onafter Render
        protected async override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                FolderPath = configuration.GetSection("S3:AdvatisementPath").Value!;
                UrlBasePath = configuration.GetSection("S3:FileBaseUrl").Value!;
                FilePath = configuration.GetSection("S3:FileSavePath").Value!;
                response = await storageService!.GetItemAsync<AuthenticationResponseModel>("Token");

                pagination.RowCount = 10;
                pagination = await AdvatismentService.GetAdvatisementPagination(pagination, response.JwtToken);
                this.StateHasChanged();

            }
        }
        #endregion


        #region Create AdvatiseModal
        protected async Task CreateAdvatiseModal()
        {
            EditedID = 0;
            this.StateHasChanged();
            await jSRuntime.InvokeVoidAsync("CreateAdvatiseModal");
        }
        #endregion


        #region Hide Advatise Modal
        protected async Task HideAdvatiseModal()
        {
            await jSRuntime.InvokeVoidAsync("HideAdvatiseModal");
        }
        #endregion

        #region Pagination Selections
        private async Task PaginationResponse(int PageIndex)
        {
            pagination.PageIndex = PageIndex;
            pagination = await AdvatismentService!.GetAdvatisementPagination(pagination, response.JwtToken!);
            this.StateHasChanged();

        }
        #endregion

        #region Edit Modal Calling
        private async void EditModal(int ID)
        {
            EditedID = ID;
            this.StateHasChanged();
            await jSRuntime.InvokeVoidAsync("CreateAdvatiseModal");

        }
        #endregion


        #region Modal Event CallBack
        private async Task ModalResponse(ModificationStatus respon)
        {
            if (respon.Success)
            {
                SuccessOrFaild = SuccessorFailAlert.Success;
                AlertMessage = respon.Message;
                pagination = await AdvatismentService.GetAdvatisementPagination(pagination, response.JwtToken!);
                this.StateHasChanged();
                AlertCalling.CallAlert(jSRuntime);

            }
        }
        #endregion

        #region Call Delete Confirm Modal
        private async void DeleteConfirmModal(int ID)
        {
            DeletedID = ID;
            DeleteConfirmModalMessage = "Do you want to Delete this User";
            this.StateHasChanged();
            ConfirmationModalCall.ShowModal(jSRuntime);
        }
        #endregion

        #region Delete Confirmed
        private async void DeleteConfirmed(bool IsDeleete)
        {
            if (IsDeleete)
            {
                var imagepath = pagination.AdvatisementList.FirstOrDefault(x => x.ID == DeletedID);


                var success = await AdvatismentService.DeleteAdvatisement(DeletedID, response.JwtToken!);
                if (success.Success)
                {
                    await S3Service.DeleteFileAsync(imagepath.ImagePath, FolderPath);
                    pagination = await AdvatismentService.GetAdvatisementPagination(pagination, response.JwtToken!);
                    ConfirmationModalCall.HideModal(jSRuntime);

                    SuccessOrFaild = SuccessorFailAlert.Success;
                    AlertMessage = success.Message;
                    this.StateHasChanged();
                    AlertCalling.CallAlert(jSRuntime);
                }

            }
        }
        #endregion
    }
}
