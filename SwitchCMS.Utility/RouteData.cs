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

        #region Vehicle 
        public const string VehicleManagement = "/Vehicle-management";
        public const string VehicleMaintanance = "/Vehicle-maintanance";
        public const string VehicleHandover = "/Vehicle-handover";
        #endregion
        public const string FormRegistration = "/Form-registration";
        
        public const string FormFinancialPledge = "/Form-financialpledge";
        public const string FormSalaryDefinition = "/Form-salaryDefinition";

        public const string MyPassword = "/MyPassword";
        public const string MyDocumentation = "/Mydocumentation";

        public const string AddManagement = "/Add-Managemnt";
        #endregion


        #region Print Urls
        public const string PrintVehicleHandover = "api/VehicleForm/VehicleHandoverForms";
        #endregion
    }
}
