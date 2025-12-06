using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;
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

namespace SwitchCMS.Component.Pages.Dashboard.AdvManagement.AdvtisementComponent
{
    public partial class AdvatiseCreateUpdateModal
    {

        #region Injections
        [Inject]
        IJSRuntime jSRuntime { get; set; }
        [Inject]
        IS3Service s3service { get; set; }
        [Inject]
        IConfiguration configuration { get; set; }
        [Inject]
        ISessionStorageService? storageService { get; set; }

        [Inject]
        IOADVService AdvatisementService { get; set; }

        [Parameter]
        public int ParamAdID { get; set; }

        [Parameter]
        public EventCallback<ModificationStatus> ParmChanged { get; set; }


        #endregion


        #region Objects and Datatype
        OADV Advatisement = new OADV();
        Stream Image = new MemoryStream();
        string FolderPath = string.Empty;
        string UploadImageError = string.Empty;
        string ErrorMessage = string.Empty;

        bool FileExistOrNot = false;
        private List<OCMP> SelectedCompany = new List<OCMP>();
        string ContentType = string.Empty;

        AuthenticationResponseModel Response = new AuthenticationResponseModel();




        private IBrowserFile UploadFile;
        #endregion

        #region Onafter Render
        protected async override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                FolderPath = configuration.GetSection("S3:AdvatisementPath").Value;
                Response = await storageService.GetItemAsync<AuthenticationResponseModel>("Token");

            }

        }
        #endregion

        #region OnParameter Chnage
        protected override async Task OnParametersSetAsync()
        {
            if (ParamAdID > 0)
            {
                Advatisement = await AdvatisementService.GetAdvatisementByID(ParamAdID, Response.JwtToken!);
                if (Advatisement.CustList.Count > 0)
                {
                    if (Advatisement.CustList.Count == 1)
                    {
                        if (Advatisement.CustList[0].ID > 1)
                        {
                            SelectedCompany = Advatisement.CustList.Select(x => new OCMP { ID = x.CustomerID }).ToList();
                        }
                        else
                        {
                            SelectedCompany = new List<OCMP>();
                        }
                    }
                    else
                    {
                        SelectedCompany = Advatisement.CustList.Select(x => new OCMP { ID = x.CustomerID }).ToList();
                    }

                }
            }
            else
            {
                Advatisement = new OADV();
                SelectedCompany = new List<OCMP>();
            }

        }
        #endregion


        #region Hide Advatise Modal
        protected async Task HideAdvatiseModal()
        {
            await jSRuntime.InvokeVoidAsync("HideAdvatiseModal");
        }
        #endregion


        #region FileUplad Event
        private async Task UploadFiles(IBrowserFile file)
        {
            try
            {
                UploadFile = file;
                //long maxFileSize = 10 * 1024 * 1024;
                //using var stream = file.OpenReadStream(maxFileSize);
                //MemoryStream ms = new MemoryStream();
                //await stream.CopyToAsync(ms);
                //ms.Position = 0;
                //Image = ms;

                Advatisement.ImagePath = file.Name;
                ContentType = file.ContentType;
                FileExistOrNot = await s3service.FIleExistorNot(FolderPath, Advatisement.ImagePath);
                // await s3service.UploadFile(stream, Advatisement.ImagePath, file.ContentType, FolderPath);
                if (FileExistOrNot)
                {
                    UploadImageError = "This Image Already Exist in CDN";
                }
                else
                {
                    UploadImageError = string.Empty;
                }

                this.StateHasChanged();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        #endregion

        #region company selected List
        public void SelectedCompanyList(List<OCMP> Companys)
        {
            SelectedCompany = Companys;
        }
        #endregion

        #region Create Banner
        private async Task CreateAdvatisement()
        {
            bool ImageUploaded = false;
            if (string.IsNullOrEmpty(UploadImageError))
            {
                if (!string.IsNullOrEmpty(Advatisement.ImagePath))
                {
                    if (UploadFile != null)
                    {
                        using var stream = UploadFile.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
                        using var ms = new MemoryStream();
                        await stream.CopyToAsync(ms);
                        Image.Position = 0;
                        ImageUploaded = await s3service.UploadFile(ms, Advatisement.ImagePath, ContentType, FolderPath);


                    }


                    if (SelectedCompany.Count > 0)
                    {
                        Advatisement.CustList = SelectedCompany.Select(x => new ADV1 { CustomerID = x.ID }).ToList();

                    }
                    else
                    {
                        Advatisement.CustList = new List<ADV1>();
                    }


                    if (Advatisement.ID > 0)
                    {
                        Advatisement.CustList.ForEach(x => x.OADVID = Advatisement.ID);
                        var status = await AdvatisementService.UpdateAdvatisement(Advatisement, Response.JwtToken!);
                        if (status != null)
                        {
                            if (status.Success)
                            {


                                await ParmChanged.InvokeAsync(status);
                                await HideAdvatiseModal();

                            }

                        }
                    }
                    else
                    {
                        if (Image != null)
                        {
                            var status = await AdvatisementService.CreateAdvatisement(Advatisement, Response.JwtToken!);
                            if (status != null)
                            {
                                if (status.Success)
                                {


                                    await ParmChanged.InvokeAsync(status);
                                    await HideAdvatiseModal();
                                }

                            }
                        }



                    }
                }
            }
        }


        #endregion

    }
}
