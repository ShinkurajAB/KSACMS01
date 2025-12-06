using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Model
{
    /// <summary>
    /// Employee Direction Forms
    /// </summary>
    public class HEM2
    {
        public int ID {  get; set; }
        public int EmployeeID {  get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string JobNumber { get; set; } = string.Empty;
        public string Management {  get; set; } = string.Empty;
        public DateTime? StartDate {  get; set; }=DateTime.Now;
        public int CompanyID {  get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public OHEM Employee { get; set; } = new();
        public OCMP Company {  get; set; }= new();


    }
}
