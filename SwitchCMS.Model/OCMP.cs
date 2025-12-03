using SwitchCMS.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Model
{

    /// <summary>
    /// Company Model
    /// </summary>
    public class OCMP
    {
       
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address {  get; set; }=string.Empty;
        public string Email {  get; set; }=string.Empty;
        public string PhoneNumber {  get; set; }=string.Empty;
        public CompanyStatus Status { get; set;} 
        public string CountryCode {  get; set; }=string.Empty;
        public DateTime? ValidationDate {  get; set; }
        public bool IsAll {  get; set; }
        public OCRY Country { get; set; } = new();

    }
}
