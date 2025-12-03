using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Utility
{
    public class ApplicationTokenKey
    {
        public const string Application = "Application";

        private static readonly Dictionary<string, string> keys = new()
        {
            { Application, "F0B4A9232031408E89B09FAE6D48B1D6" }
        };

        public static bool IsValidApplication(string userName, string password)
        {
            bool isValid = false;
            if (keys.ContainsKey(userName))
            {
                if (keys[userName] == password)
                    isValid = true;
            }
            return isValid;
        }
    }
}
