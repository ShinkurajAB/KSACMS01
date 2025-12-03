using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Model.Authentication
{
    public class UserAccount
    {
        public OUSR User { get; set; }
        public List<UserClaimModel> Claims { get; set; }
    }
}
