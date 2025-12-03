using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Model.UI
{
    public class UsersPagination
    {

        public string UsersName { get; set; } = string.Empty;
        public OCMP SelectedCompany { get; set; } = new();
        public int PageIndex { get; set; }
        public int RowCount { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }

        public List<OUSR> UserList { get; set; } = new();
    }
}
