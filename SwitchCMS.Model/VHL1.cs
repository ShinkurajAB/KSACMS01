using SwitchCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Model
{
    /// <summary>
    /// Vehicle Handover Form
    /// </summary>
    public class VHL1
    {
        public int ID {  get; set; }
        public DateTime? CreatedDate {  get; set; }
        public string? EmployeeName {  get; set; }
        public string? JobTitle {  get; set; }
        public string? Department {  get; set; }
        public DateTime? EmploymentDate { get; set; } = DateTime.Now;
        public string? VehicleType {  get; set; }
        public string? VehicleModel {  get; set; }
        public string? PlateNumber {  get; set; }
        public string? ChasisNumber {  get; set; }
        public string? VehicleColor { get; set; }
        public string? License {  get; set; }

        public DateTime? IssueDate { get; set; } = DateTime.Now;
        public DateTime? ExpiryDate { get; set; } = DateTime.Now;

        public string? InsurancePolicyNumber { get; set; }
        public DateTime? InsuranceDate { get; set; } = DateTime.Now;
        public decimal Milage {  get; set; }
        public string? InteriorCondition {  get; set; }
        public string? ExteriorCondition { get; set; }

        public string? Dameges { get; set; }

        public string? TooolsAndSpareTire { get; set; }

        public string? GeneralNotes { get; set; }

        public string? FinanceDepartmentNotes { get; set; }

        public string? HRDepartmentNotes { get; set; }

        public int CompanyID {  get; set; }
    }
}
