using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Model.UI
{
    public class OfferLetterPagination
    {
        public int PageIndex { get; set; }
        public int RowCount { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public int CompanyId { get; set; }

        public List<HEM4> OfferLetterList { get; set; } = new();
    }
}
