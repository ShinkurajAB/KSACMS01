using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using SwitchCMS.Client.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Component.CommonComponents
{
    public partial class Carousel
    {
        #region Parameter and Injections
        [Inject]
        ISessionStorageService? storageService { get; set; }

        [Inject]
        IOADVService AdvatismentService { get; set; }

        [Inject]
        IConfiguration configuration { get; set; }

        [Inject]
        IOUSRService userService { get; set; }
        #endregion


        #region Objects and Datatypes
        AuthenticationResponseModel response = new AuthenticationResponseModel();
        string FolderPath = string.Empty;
        string UrlBasePath = string.Empty;
        string FilePath = string.Empty;

        List<OADV> AdvatisementList = new List<OADV>();
        #endregion


        #region On render After
        protected async override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                FolderPath = configuration.GetSection("S3:AdvatisementPath").Value!;
                UrlBasePath = configuration.GetSection("S3:FileBaseUrl").Value!;
                FilePath = configuration.GetSection("S3:FileSavePath").Value!;
                response = await storageService!.GetItemAsync<AuthenticationResponseModel>("Token");

                var User = await userService.GetLoginUserDetails(response.JwtToken!);

                AdvatisementList = await AdvatismentService.GetAdvatisementByCustID(User.Company.ID, response.JwtToken!);

                this.StateHasChanged();
            }

        }
        #endregion
    }
}
