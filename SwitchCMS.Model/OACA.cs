using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Model
{
    /// <summary>
    /// My Paswword
    /// </summary>
    public class OACA
    {
        public int ID { get; set; }
        public int CompanyID {  get; set; }
        public string Platform { get; set; } = string.Empty;
        public string URL { get; set; } = string.Empty;
        public string UserName {  get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Remark {  get; set; } = string.Empty;
        public int CreatedBy {  get; set; }
        public DateTime LastUpdatedDate {  get; set; }
        public OCMP Company { get; set; } = new OCMP();

    }
}
