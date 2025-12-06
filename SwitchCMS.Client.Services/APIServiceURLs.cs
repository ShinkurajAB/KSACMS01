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
            public static string DeleteUserByUserID(int UserID) => $"/api/Users/DeleteUser?UserID={UserID}";
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
            public static string GetAllEmployessByCompany(int companyId) => $"/api/Employee/GetAllEmployessByCompany?companyId={companyId}";
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
        public class HEM1Url
        {
            public static string InsertResignation() => "/api/EmployeeResignation/InsertResignation";
            public static string UpdateResignation() => "/api/EmployeeResignation/UpdateResignation";
            public static string GetResignationsByPagination() => "/api/EmployeeResignation/GetEmployeeResignationsByPagination";
            public static string DeleteResignation(int resigId) => $"/api/EmployeeResignation/DeleteResignation?resignationId={resigId}";
            public static string GetResignationById(int resigId) => $"/api/EmployeeResignation/GetResignationById?Id={resigId}";
        }

        public class HEM2Url
        {
            public static string CreateDirectNotification() => "/api/VehicleHandover/CreateHandover";
            public static string GetDirectNotificationByPageIndex() => $"/api/VehicleHandover/GetHandoverByPageIndex";
            public static string GetDirectNotificationByID(int ID) => $"/api/VehicleHandover/GetHandoverByID?HandoverID={ID}";
            public static string UpdateDirectNotification() => $"/api/VehicleHandover/UpdateHandover";
            public static string DeleteDirectNotification(int ID) => $"/api/VehicleHandover/DeleteHandover?HandoverID={ID}";
        }
        public class HEM3Url
        {
            public static string InsertAbsentee() => "/api/EmployeeAbsence/InsertAbsentee";
            public static string UpdateAbsentee() => "/api/EmployeeAbsence/UpdateAbsentee";
            public static string GetAbsenteesByPagination() => "/api/EmployeeAbsence/GetAbsenteesByPagination";
            public static string DeleteAbsentee(int absenteeId) => $"/api/EmployeeAbsence/DeleteAbsentee?absenteeId={absenteeId}";
            public static string GetAbsenteeById(int id) => $"/api/EmployeeAbsence/GetAbsenteeById?Id={id}";
        }
        public class HEM4Url
        {
            public static string InsertOfferLetter() => "/api/OfferLetter/InsertOfferLetter";
            public static string UpdateOfferLetter() => "/api/OfferLetter/UpdateOfferLetter";
            public static string GetOfferLetterByPagination() => "/api/OfferLetter/GetOfferLetterByPagination";
            public static string DeleteOfferLetter(int offerId) => $"/api/OfferLetter/DeleteOfferLetter?offerId={offerId}";
            public static string GetOfferLetterById(int id) => $"/api/OfferLetter/GetOfferLetterById?Id={id}";
        }
    }
}
