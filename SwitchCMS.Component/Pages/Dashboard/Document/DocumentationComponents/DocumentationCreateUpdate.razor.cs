using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using SwitchCMS.Client.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.Authentication;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Component.Pages.Dashboard.Document.DocumentationComponents
{
    public partial class DocumentationCreateUpdate
    {
        #region Injection and Datatype
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

        [Parameter]
        public EventCallback<ModificationStatus> CreateOrNoEvent { get; set; }

        [Parameter]
        public int ParmDocID { get; set; }
        #endregion


        #region Datatype and Objects
        OATC attachement = new OATC();
        string UploadFileError = string.Empty;
        Stream Image = new MemoryStream();
        string ContentType = string.Empty;
        OUSR LoginUser = new OUSR();
        string ErrorMessage = string.Empty;

        AuthenticationResponseModel response = new AuthenticationResponseModel();

        string OldFileName = string.Empty;


        int SelectedID;

        private IBrowserFile UploadFile;
        #endregion

        #region On Render After
        protected async override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                response = await storageService.GetItemAsync<AuthenticationResponseModel>("Token");
                LoginUser = await userService.GetLoginUserDetails(response.JwtToken!);
                attachement.CompanyID = LoginUser.CompanyID;
            }


            base.OnAfterRender(firstRender);
        }
        #endregion

        #region On Parameter
        protected override async Task OnParametersSetAsync()
        {
            if (ParmDocID > 0)
            {
                attachement = await attachService.GetDocumentByID(ParmDocID, response.JwtToken!);
                OldFileName = attachement.FileName;
                UploadFile = null;
            }
            else
            {
                attachement = new OATC();
                attachement.CompanyID = LoginUser.CompanyID;
                UploadFile = null;
            }


        }
        #endregion

        #region Hide Modal
        private async Task HideDocument()
        {
            await JSRuntime.InvokeVoidAsync("HideDocumentModal");
        }
        #endregion

        #region File Change
        private async Task UploadFiles(IBrowserFile file)
        {
            try
            {
                //long maxFileSize = 10 * 1024 * 1024;
                //using var stream = file.OpenReadStream(maxFileSize);
                //MemoryStream ms = new MemoryStream();
                //await stream.CopyToAsync(ms);
                //ms.Position = 0;
                //Image = ms;
                UploadFile = file;

                attachement.FileName = file.Name;


                ContentType = file.ContentType;
                bool FileExistOrNot = await s3Service.FIleExistorNot(LoginUser.CompanyID.ToString(), attachement.FileName);
                //// await s3Service.UploadFile(stream, attachement.FileName, file.ContentType, LoginUser.CompanyID.ToString());
                if (FileExistOrNot)
                {
                    UploadFileError = "This File Already Exist in CDN";
                }
                else
                {
                    UploadFileError = string.Empty;
                }

                this.StateHasChanged();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        #endregion

        #region Create Document
        private async Task CreateOrUpdateDocument()
        {
            bool ImageUploaded = false;

            if (string.IsNullOrEmpty(UploadFileError))
            {
                if (!string.IsNullOrEmpty(attachement.FileName))
                {
                    if (UploadFile != null)
                    {
                        using var stream = UploadFile.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
                        using var ms = new MemoryStream();
                        await stream.CopyToAsync(ms);
                        Image.Position = 0;
                        ImageUploaded = await s3Service.UploadFile(ms, attachement.FileName, ContentType, LoginUser.CompanyID.ToString());


                    }

                    if (attachement.ID > 0)
                    {
                        if (OldFileName != attachement.FileName)
                        {
                            await s3Service.DeleteFileAsync(OldFileName, LoginUser.CompanyID.ToString());
                        }

                        var status = await attachService.UpdateDocumentByID(attachement, response.JwtToken!);
                        if (status.Success)
                        {
                            attachement = new OATC();
                            attachement.CompanyID = LoginUser.CompanyID;
                            await CreateOrNoEvent.InvokeAsync(status);
                            await HideDocument();
                        }

                    }
                    else
                    {
                        var Status = await attachService.CreateDocument(attachement, response.JwtToken!);
                        if (Status.Success)
                        {
                            attachement = new OATC();
                            attachement.CompanyID = LoginUser.CompanyID;
                            await CreateOrNoEvent.InvokeAsync(Status);
                            await HideDocument();
                        }
                    }
                }
                else
                {
                    ErrorMessage = "Upload File Is Mandatory";
                }
            }


        }
        #endregion
    }
}
