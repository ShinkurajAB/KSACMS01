using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Utility
{
    public class PolicyType
    {
        // Users
        public const string SuperAdmin = "SuperAdmin";
        public const string CompanyAdmin = "CompanyAdmin";
        public const string EmployeeUser = "EmployeeUser";

        // Applications
        public const string AppAll = "AppsAll";
        public const string AppExternal = "AppExternal";
    }
}
