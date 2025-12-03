using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Model.Authentication
{
    public class UserClaimModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string? UserType { get; set; }
        public string? UserValue { get; set; }
    }
}
