using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Model.UI
{
    public class AdvatisementPagination
    {
        public int PageIndex { get; set; }
        public int RowCount { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }        
        public string CompanyName { get; set; } = string.Empty;
        public List<OADV> AdvatisementList { get; set; } = new();
    }
}
