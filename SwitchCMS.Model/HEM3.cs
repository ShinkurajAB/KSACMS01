using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Model
{
    public class HEM3
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime? AbsenceDate { get; set; } = DateTime.Now;
        public string Notes { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CompanyID { get; set; }
        public OHEM Employee { get; set; } = new();
        public OCMP Company { get; set; } = new();
    }
}
