using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Client.Services
{
    internal class APIServiceURLs
    {
        public const string BaseAddressSection = "APIService";
        public const string SAPAddressSection = "SAPAPIUrl";
        public const string AuthScheme = "Bearer";

        public class OUSRUrls
        {
            public static string LoginUser() => $"/api/Users/LoginUser";
            public static string GetLoginDetails() => $"/api/Users/GetLoginUserDetails";
            public static string GetUserByPageIndex() => $"/api/Users/GetUsersByPageIndex";
            public static string CreateUser() => $"/api/Users/CreeateUpdateUser";
            public static string DeleteUserByUserID(int UserID) => $"/api/Users/DeleteUser?UserID={ UserID }";
        }

        public class OCMPUrls
        {

            public static string GetAllCompany() => $"/api/Company/GetAllCompany";
            public static string SignUpCompany() => "/api/Company/SignUpCompany";
            public static string GetCompanyByPagination() => "/api/Company/GetCompanyByPagination";
            public static string UpdateCompany() => "/api/Company/UpdateCompany";
            public static string UpdateCompanyStatus(int companyId, string companyStatus) => $"/api/Company/UpdateCompanyStatus?companyId={companyId}&companyStatus={companyStatus}";
            public static string DeleteCompany(int companyId) => $"/api/Company/DeleteCompany?companyId={companyId}";
        }
        public class OCRYUrls
        {
            public static string GetAllCountries() => $"/api/Country/GetAllCountries";
        }

        public class OMENUrls
        {
            public static string GetAllMenus() => $"/api/Menu/GetAllMenus";
            public static string GetMenuByUserID(int UserID) => $"/api/Menu/GetMenuByUserID?UserID={UserID}";
        }
        public class OACAUrls
        {
            public static string InsertAccessAccountManager() => "/api/AccessAccount/InsertAccessAccountManager";
            public static string UpdateAccessAccountManager() => "/api/AccessAccount/UpdateAccessAccountManager";
            public static string DeleteAccessAccountManager(int accountId) => $"/api/AccessAccount/DeleteAccessAccountManager?accessId={accountId}";
            public static string GetAllAccessAccountManagers() => "/api/AccessAccount/GetAllAccessAccountManagers";
            public static string GetAccessAccountManagersByCompanyId(int companyId) => $"/api/AccessAccount/GetAccessAccountManagersByCompanyId?companyId={companyId}";
        }
        public class OHEMUrls
        {
            public static string InsertEmployee() => "/api/Employee/InsertEmployee";
            public static string UpdateEmployee() => "/api/Employee/UpdateEmployee";
            public static string GetEmployeeByPagination() => "/api/Employee/GetEmployeeByPagination";
            public static string DeleteEmployee(int empId) => $"/api/Employee/DeleteEmployee?employeeId={empId}";
            public static string EmployeeBulkUpload() => "/api/Employee/EmployeeBulkUpload";
        }

        public class OADVUrls
        {
            public static string CreateAdvatisement() => $"/api/Advatisement/CreateAdvatisement";
            public static string GetAdvatisementByPageIndex() => $"/api/Advatisement/GetAdvatisementByPagination";
            public static string GetAdvatisementByID(int ID) => $"/api/Advatisement/GetAdvatisementByID?ID={ID}";
            public static string UpdateAdvatisement() => $"/api/Advatisement/UpdateAdvatisement";
            public static string DeleteAdvatisementID(int ID) => $"/api/Advatisement/DeleteAdvatisement?ID={ID}";
            public static string GetAdvatisementByCustID(int CustID) => $"/api/Advatisement/GetAdvatisementByCustID?ID={CustID}";
        }

        public class OVHLUrl
        {
            public static string CreateVehicle() => "/api/Vehicle/CreateVehicle";
            public static string GetVehicleByPageIndex() => $"/api/Vehicle/GetVehicleByPageIndex";
            public static string GetVehicleByID(int ID) => $"/api/Vehicle/GetVehicleByID?VehcileID={ID}";
            public static string UpdateVehicle() => $"/api/Vehicle/UpdateVehicle";
            public static string DeleteVehicle(int ID) => $"/api/Vehicle/DeleteVehicle?VehicleID={ID}";     
            
        }

        public class OATCUrl
        {
            public static string CreateDocument() => $"/api/Document/CreateDocument";
            public static string GetDocumentPageIndex() => $"/api/Document/GetDocumentByPageIndex";
            public static string GetDocumentByID(int ID) => $"/api/Document/GetDocumentByID?ID={ID}";
            public static string DeleteDocumentByID(int ID) => $"/api/Document/DeleteDocumentByID?ID={ID}";
            public static string UpdateDocument() => $"/api/Document/UpdateDocument";
        }

        public class VHL1Url
        {
            public static string CreateHandover() => "/api/VehicleHandover/CreateHandover";
            public static string GetHandoverByPageIndex() => $"/api/VehicleHandover/GetHandoverByPageIndex";
            public static string GetHandoverByID(int ID) => $"/api/VehicleHandover/GetHandoverByID?HandoverID={ID}";
            public static string UpdateHandover() => $"/api/VehicleHandover/UpdateHandover";
            public static string DeleteHandover(int ID) => $"/api/VehicleHandover/DeleteHandover?HandoverID={ID}";
        }
    }
}
