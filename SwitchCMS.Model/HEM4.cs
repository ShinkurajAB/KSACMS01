using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Model
{
    public class HEM4
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string WorkLocation { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public decimal TotalMonthlySalary { get; set; }
        public decimal HousingAllowance { get; set; }
        public decimal TransportationAllowance { get; set; }
        public decimal OtherAllowance { get; set; }
        public DateTime CreatedDate { get; set; }
        public OCMP Company { get; set; } = new();
    }
}
