using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Model
{
    /// <summary>
    /// Employee Resignation Forms
    /// </summary>
    public class HEM1
    {
        public int ID { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public DateTime? SubmissionDate { get; set; } = DateTime.Now;
        public string EmployeeName { get; set; } = string.Empty;       
        public DateTime? LastWorkingDay { get; set; }=DateTime.Now;
        public OHEM Employee { get; set; } = new OHEM();
        public OCMP Company { get; set; } = new OCMP();

        public DateTime? CreatedDate {  get; set; } = DateTime.Now;
    }
}
