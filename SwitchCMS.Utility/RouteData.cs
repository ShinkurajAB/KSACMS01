using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Utility
{
    public class RouteData
    {
        public const string Index = "/";
        public const string Dashboard = "/dashboard";
        public const string Signup = "/signup";

        #region Common Control
        public const string UserManagement = "/Users/User-Management";
        #endregion


        #region SuperAdmin
        public const string CompanyManagement = "/superadmin/company-management";
        public const string AccessControl = "/superadmin/access-control";
        #endregion

        #region Menus
        public const string EmployeeManagement = "/Employee-management";
        public const string EmployeeBulkRegistration = "/Employee-bulkregistration";
        public const string EmployeeLeave = "/Employee-leave";
        public const string EmployeeIqamaRenewal = "/Employee-iqamaRenewal";
        public const string EmployeeResignation = "/Form-resignation";
        public const string EmployeeDirectNotification = "/Employee-directNotification";
        public const string EmployeeAbsentee = "/Form-absentee";
        public const string EmployeeOfferLetter = "/Form-offerletter";
        public const string EmployeeWarningLetter = "/Form-warningletter";

        #region Vehicle 
        public const string VehicleManagement = "/Vehicle-management";
        public const string VehicleMaintanance = "/Vehicle-maintanance";
        public const string VehicleHandover = "/Vehicle-handover";
        #endregion
       

        public const string FormFinancialPledge = "/Form-financialpledge";
        public const string FormSalaryDefinition = "/Form-salaryDefinition";

        public const string MyPassword = "/MyPassword";
        public const string MyDocumentation = "/Mydocumentation";

        public const string AddManagement = "/Add-Managemnt";
        #endregion


        #region Print Urls
        public const string PrintVehicleHandover = "api/VehicleForm/VehicleHandoverForms";
        public const string PrintEmployeeResignation = "api/EmployeeForms/GetResignationForms";
        public const string PrintEmployeeAbsentee = "api/EmployeeForms/GetAbsenceForms";
        public const string PrintOfferLetter = "api/EmployeeForms/GetOfferLetterForms";
        public const string PrintWarningLetter = "api/EmployeeForms/GetGeneralWarningForms";
        #endregion
    }
}
