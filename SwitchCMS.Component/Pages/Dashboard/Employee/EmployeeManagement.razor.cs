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

namespace SwitchCMS.Component.Pages.Dashboard.Employee
{
    public partial class EmployeeManagement
    {
        #region  Service Injections
        [Inject]
        IJSRuntime? jSRuntime { get; set; }
        [Inject]
        IOHEMService? employeeService { get; set; }
        [Inject]
        ISessionStorageService? storageService { get; set; }
        [Inject]
        IOCRYService? countryService { get; set; }
        [Inject]
        IOUSRService? userService { get; set; }
        [Inject]
        IOCMPService? companyService { get; set; }
        #endregion
        #region Object/Datatype Declarations
        AuthenticationResponseModel loginResponse = new AuthenticationResponseModel();
        EmployeePagination employeePagination = new EmployeePagination();


        private OHEM EmployeeModal = new();
        OUSR loginUserDetails = new OUSR();
        string errorMessage = string.Empty;
        string AlertMessage = string.Empty;
        string DeleteConfirmModalMessage = string.Empty;
        int DeletedEmployeeId = 0;
        SuccessorFailAlert SuccessOrFaild;

        OCRY selectedCountry = new OCRY();
        #endregion
        #region Razor Page On After Render
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

            if (firstRender)
            {
                loginResponse = await storageService!.GetItemAsync<AuthenticationResponseModel>("Token");

                loginUserDetails = await userService!.GetLoginUserDetails(loginResponse.JwtToken!);

                employeePagination.RowCount = 10;
                employeePagination.CompanyId = loginUserDetails.CompanyID;
                employeePagination = await employeeService!.GetEmployeeByPagination(employeePagination, loginResponse.JwtToken!);
                this.StateHasChanged();
            }
        }
        #endregion
        #region Pagination Selections
        private async Task PaginationResponse(int PageIndex)
        {
            employeePagination.PageIndex = PageIndex;
            employeePagination = await employeeService!.GetEmployeeByPagination(employeePagination, loginResponse.JwtToken!);
            this.StateHasChanged();

        }
        #endregion


        #region Add/Edit Employee
        private async Task CreateEmployee()
        {
            bool isSuccess = false;
            EmployeeModal.CreatedBy = loginUserDetails.ID;
            EmployeeModal.CompanyID = loginUserDetails.CompanyID;
            if (EmployeeModal.ID == 0)
            {
                isSuccess = await employeeService!.InsertEmployee(EmployeeModal, loginResponse.JwtToken!);
            }
            else
            {
                isSuccess = await employeeService!.UpdateEmployee(EmployeeModal, loginResponse.JwtToken!);
            }
            if (isSuccess)
            {
                EmployeeModal = new OHEM();
                AlertMessage = "Modified successfully";
                SuccessOrFaild = SuccessorFailAlert.Success;
                employeePagination = await employeeService!.GetEmployeeByPagination(employeePagination, loginResponse.JwtToken!);
                await CloseEmployeeModal();
                this.StateHasChanged();
                AlertCalling.CallAlert(jSRuntime!);

            }
        }
        #endregion
        #region Delete Employee
        private async Task DeleteEmployee()
        {
            if (DeletedEmployeeId > 0)
            {
                bool isDelete = await employeeService!.DeleteEmployee(DeletedEmployeeId, loginResponse.JwtToken!);
                if (isDelete)
                {
                    DeletedEmployeeId = 0;
                    employeePagination = await employeeService!.GetEmployeeByPagination(employeePagination, loginResponse.JwtToken!);
                    ConfirmationModalCall.HideModal(jSRuntime!);
                    AlertMessage = "Deleted successfully";
                    SuccessOrFaild = SuccessorFailAlert.Success;
                    this.StateHasChanged();
                    AlertCalling.CallAlert(jSRuntime!);
                }
            }

        }
        #endregion
        #region Call Delete Confirm Modal
        private async void DeleteConfirmModal(int employeeId)
        {
            DeletedEmployeeId = employeeId;
            DeleteConfirmModalMessage = "Are you sure you want to Delete this Employee";
            this.StateHasChanged();
            ConfirmationModalCall.ShowModal(jSRuntime!);
        }
        #endregion
        #region Modal Event Call 
        public async void ModalCreateOrUpdateResult(bool success)
        {
            if (success)
            {
                AlertMessage = "Modified successfully";
                SuccessOrFaild = SuccessorFailAlert.Success;
                employeePagination = await employeeService!.GetEmployeeByPagination(employeePagination, loginResponse.JwtToken!);
                this.StateHasChanged();
                AlertCalling.CallAlert(jSRuntime);
            }
        }
        #endregion
        #region Show Employee Modal
        private async Task ShowEmployeeModal()
        {
            await jSRuntime!.InvokeVoidAsync("ShowEmployeeModal");
        }
        #endregion
        #region Close Employee Modal
        private async Task CloseEmployeeModal()
        {
            await jSRuntime!.InvokeVoidAsync("HideEmployeeModal");
        }
        #endregion
        #region Edit Company
        public async void EditEmployee(OHEM employee)
        {
            EmployeeModal = employee;
            selectedCountry.Code = employee.Nationality;

            await jSRuntime!.InvokeVoidAsync("ShowEmployeeModal");
        }
        #endregion


        #region On Country Selection Event Call Back
        private void CountrySelection(OCRY country)
        {
            selectedCountry = country;
            EmployeeModal.Nationality = selectedCountry.Code;
            this.StateHasChanged();
        }
        #endregion
    }
}
