using SwitchCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Model
{
    //Employee Model
    public class OHEM
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string JobTitle {  get; set; } = string.Empty;
        public string ProjectName {  get; set; } = string.Empty;
        public string Email {  get; set; }= string.Empty;
        public string PassportNumber {  get; set; } = string.Empty;
        public DateTime DOB { get; set; } = DateTime.Now;
        public string MobileNumber { get; set; } = string.Empty;
        public Gender Gender {  get; set; }
        public string Nationality {  get; set; } = string.Empty;
        public int CompanyID { get; set; }
        public string IqamaID {  get; set; } = string.Empty;
        public DateTime IqamaExpiry {  get; set; }= DateTime.Now;
        public Status EmployeeStatus {  get; set; }
        public ContractType ContractType {  get; set; }
        public string Sponsor {  get; set; }=string.Empty;
        public string NationalUnifiedNumber {  get; set; } = string.Empty;
        public DateTime JoiningDate {  get; set; }=DateTime.Now;
        public string BankAccount {  get; set; } = string.Empty;
        public DateTime ContractExpiry {  get; set; } =DateTime.Now;
        public Status ContractStatus {  get; set; }
        public string InsuranceCompanyName {  get; set; } = string.Empty;
        public DateTime InsuranceStartDate { get; set; } = DateTime.Now;
        public DateTime InsuranceExpiryDate { get; set; } = DateTime.Now;
        public Status InsuranceStatus { get; set; }
        public int CreatedBy {  get; set; }
        public OCMP Company { get; set; } = new OCMP();
        public string? BulkUploadStatus { get; set; }

    }
}
