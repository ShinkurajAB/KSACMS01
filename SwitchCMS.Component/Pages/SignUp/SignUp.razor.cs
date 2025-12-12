using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using SwitchCMS.Client.Services.Interface;
using SwitchCMS.Component.Data;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using SwitchCMS.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Component.Pages.SignUp
{
    public partial class SignUp
    {
        #region Service Injections       
        [Inject]
        public IOCMPService? companyService { get; set; }
        [Inject]
        public IJSRuntime? JSRuntime { get; set; }
        [Inject]
        public IS3Service? s3Service { get; set; }
        #endregion

        #region Object and Datatype 

        OCMP companyModal    = new OCMP();
        OCRY selectedCountry = new OCRY();

        string nameErrorMessage          = string.Empty;
        string emailErrorMessage         = string.Empty;
        string contactErrorMessage       = string.Empty;
        string countryCodeErrorMessage   = string.Empty;
        string UploadCRFileError         = string.Empty;
        string UploadNationalIDFileError = string.Empty;
        string UploadVATFileError        = string.Empty; 
        string contactPersonErrorMessage = string.Empty;
        string crNumberErrorMessage      = string.Empty;
        string termsAndCondtionsErrorMsg = string.Empty;
        string AlertMessage              = string.Empty;
        Stream Image                     = new MemoryStream();
        Stream NationalIDImage           = new MemoryStream();
        Stream VATImage                  = new MemoryStream();
        Stream OthersImage               = new MemoryStream();
        string ContentType               = string.Empty;
        bool isTermsChecked              = false;
        private IBrowserFile SelectCRFile;
        private IBrowserFile SelectNationalIDFile;
        private IBrowserFile SelectVATFile;
        private IBrowserFile SelectOtherFile;
        SuccessorFailAlert SuccessOrFaild;
        #endregion

     

        #region On Country Selection Event Call Back
        private void CountrySelection(OCRY country)
        {
            selectedCountry = country;
            this.StateHasChanged();
        }
        #endregion

        #region SignUp Click
        private async Task SignUpCompany()
        {
            if (ValidateSignUp())
            {
                if(!isTermsChecked)
                {
                    termsAndCondtionsErrorMsg = "*";
                    return;
                }
                companyModal.CountryCode = selectedCountry.Code;
                companyModal.Status = CompanyStatus.Pending;
                ModificationStatus status = await companyService!.SignUpCompany(companyModal);
                if (status.Success)
                {
                    termsAndCondtionsErrorMsg = string.Empty;
                    await s3Service!.UploadFile(Image, companyModal.CRCertificate, SelectCRFile.ContentType, status.ID.ToString());
                    await s3Service!.UploadFile(NationalIDImage, companyModal.NationalID, SelectNationalIDFile.ContentType, status.ID.ToString());
                    await s3Service!.UploadFile(VATImage, companyModal.VATCertificate, SelectVATFile.ContentType, status.ID.ToString());
                    if (!string.IsNullOrEmpty(companyModal.Other))
                        await s3Service!.UploadFile(OthersImage, companyModal.Other, SelectOtherFile.ContentType, status.ID.ToString());
                    selectedCountry = new OCRY();
                    companyModal = new OCMP();
                    nameErrorMessage = string.Empty;
                    emailErrorMessage = string.Empty;
                    contactErrorMessage = string.Empty;
                    countryCodeErrorMessage = string.Empty;
                    contactPersonErrorMessage = string.Empty;
                    crNumberErrorMessage = string.Empty;
                    UploadVATFileError = string.Empty;
                    UploadCRFileError = string.Empty;
                    UploadNationalIDFileError = string.Empty;
                    AlertMessage = "Signup request sent. Awaiting admin approval!";
                    SuccessOrFaild = SuccessorFailAlert.Success;
                    this.StateHasChanged();
                    AlertCalling.CallAlert(JSRuntime!);


                }
            }
            this.StateHasChanged();
        }
        #endregion 
        #region Validation Check
        private bool ValidateSignUp()
        {
            nameErrorMessage       = string.Empty;
            emailErrorMessage       = string.Empty;
            contactErrorMessage     = string.Empty;
            countryCodeErrorMessage = string.Empty;
            contactPersonErrorMessage = string.Empty;
            crNumberErrorMessage = string.Empty;
            UploadVATFileError = string.Empty;
            UploadCRFileError = string.Empty;
            UploadNationalIDFileError = string.Empty;
            if (string.IsNullOrEmpty(companyModal.Name))
            {
                nameErrorMessage = "Please Enter Name";
            }
            if (string.IsNullOrEmpty(companyModal.Email))
            {
                emailErrorMessage = "Please Enter Email";
            }
            if (string.IsNullOrEmpty(companyModal.PhoneNumber))
            {
                contactErrorMessage = "Please Enter Phone Number";
            }
            if (string.IsNullOrEmpty(selectedCountry.Code))
            {
                countryCodeErrorMessage = "Please Select Country";
            }
            if (string.IsNullOrEmpty(companyModal.ContactPerson))
            {
                countryCodeErrorMessage = "Please Enter Contact Person";
            }
            if (string.IsNullOrEmpty(companyModal.CRNumber))
            {
                crNumberErrorMessage = "Please Enter CR Number";
            }
            if (string.IsNullOrEmpty(companyModal.CRCertificate))
            {
                UploadCRFileError = "Please Select CR Certificate";
            }
            if (string.IsNullOrEmpty(companyModal.NationalID))
            {
                UploadNationalIDFileError = "Please Select National ID";
            }
            if (string.IsNullOrEmpty(companyModal.VATCertificate))
            {
                UploadVATFileError = "Please Select VAT Certificate";
            }
            if (!String.IsNullOrEmpty(nameErrorMessage) || !String.IsNullOrEmpty(contactErrorMessage) || !String.IsNullOrEmpty(emailErrorMessage) || !String.IsNullOrEmpty(countryCodeErrorMessage) || !String.IsNullOrEmpty(contactPersonErrorMessage) || !String.IsNullOrEmpty(crNumberErrorMessage) || !String.IsNullOrEmpty(UploadVATFileError) || !String.IsNullOrEmpty(UploadCRFileError) || !String.IsNullOrEmpty(UploadNationalIDFileError))
                return false;
            else
                return true;

        }
        #endregion
        #region File Change
        private async Task UploadCRFiles(InputFileChangeEventArgs file)
        {
            try
            {
                SelectCRFile = file.File;
                long maxFileSize = 10 * 1024 * 1024; // 10MB

                // Read file into MemoryStream
                var ms = new MemoryStream();
                using var fileStream = SelectCRFile.OpenReadStream(maxFileSize);
                await fileStream.CopyToAsync(ms);


                ms.Position = 0;
                Image       = ms;
                companyModal.CRCertificate = SelectCRFile.Name;

                ContentType = SelectCRFile.ContentType;
               
                //// await s3Service.UploadFile(stream, companyModal.CRCertificate, selectedFile.ContentType,companyModal.Name);
               

                this.StateHasChanged();
            }
            catch (Exception ex)
            {
                UploadCRFileError = "File Size Exceeded";
            }
        }
        private async Task UploadNationalIdFiles(InputFileChangeEventArgs file)
        {
            try
            {
                SelectNationalIDFile = file.File;
                long maxFileSize = 10 * 1024 * 1024; // 10MB

               
                // Read file into MemoryStream
                var ms = new MemoryStream();
                using var fileStream = SelectCRFile.OpenReadStream(maxFileSize);
                await fileStream.CopyToAsync(ms);
                ms.Position = 0;
                NationalIDImage = ms;
                companyModal.NationalID = SelectNationalIDFile.Name;

                ContentType = SelectNationalIDFile.ContentType;

                //// await s3Service.UploadFile(stream, companyModal.NationalID, SelectNationalIDFile.ContentType,companyModal.Name);


                this.StateHasChanged();
            }
            catch (Exception ex)
            {
                UploadNationalIDFileError ="File Size Exceeded";
            }
        }
        private async Task UploadVatFiles(InputFileChangeEventArgs file)
        {
            try
            {
                SelectVATFile = file.File;
                long maxFileSize = 10 * 1024 * 1024; // 10MB

                // Read file into MemoryStream
                var ms = new MemoryStream();
                using var fileStream = SelectCRFile.OpenReadStream(maxFileSize);
                await fileStream.CopyToAsync(ms);
                ms.Position = 0;
                VATImage = ms;
                companyModal.VATCertificate = SelectVATFile.Name;

                ContentType = SelectVATFile.ContentType;

                //// await s3Service.UploadFile(stream, companyModal.VATCertificate, SelectVATFile.ContentType,companyModal.Name);


                this.StateHasChanged();
            }
            catch (Exception ex)
            {
                UploadVATFileError = "File Size Exceeded";
            }
        }
        private async Task UploadOtherFiles(InputFileChangeEventArgs file)
        {
            try
            {
                SelectOtherFile = file.File;
                long maxFileSize = 10 * 1024 * 1024; // 10MB

                // Read file into MemoryStream
                var ms = new MemoryStream();
                using var fileStream = SelectCRFile.OpenReadStream(maxFileSize);
                await fileStream.CopyToAsync(ms);
                ms.Position = 0;
                OthersImage = ms;
                companyModal.Other = SelectOtherFile.Name;

                ContentType = SelectOtherFile.ContentType;

                //// await s3Service.UploadFile(stream, companyModal.VATCertificate, SelectOtherFile.ContentType,companyModal.Name);


                this.StateHasChanged();
            }
            catch (Exception ex)
            {
               
            }
        }
        #endregion
    }
}
