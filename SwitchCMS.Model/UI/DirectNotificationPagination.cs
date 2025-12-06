using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Model.UI
{
    public class DirectNotificationPagination
    {
        public int PageIndex { get; set; }
        public int RowCount { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public string FilterSearch { get; set; } = string.Empty;
        public int CompanyID { get; set; }

        public List<HEM2> DirectNotificationList { get; set; } = new();
    }
}
