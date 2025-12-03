using SwitchCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Model.Authentication
{
    public class AuthenticationResponseModel
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public Roles Role { get; set; }
        public string? JwtToken { get; set; }
        public string? RefreshToken { get; set; }
        public bool Success { get; set; }
        public int ErrorID { get; set; }
        public string? message { get; set; }
    }
}
