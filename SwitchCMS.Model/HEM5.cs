using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Model
{
    public class HEM5
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int CompanyID { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string Violation { get; set; } = string.Empty;
        public string AdditionalDetails { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public OHEM Employee { get; set; } = new OHEM();
        public OCMP Company{ get; set; } = new OCMP();

    }
}
