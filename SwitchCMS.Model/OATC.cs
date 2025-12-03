using SwitchCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Model
{
    /// <summary>
    /// Document Attachment
    /// </summary>
    public class OATC
    {
        public int ID { get; set; }
        public string EntityName { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; } = DateTime.Now;
        public DateTime? ExpiryDate {  get; set; } = DateTime.Now;
        public Status DocStatus {  get; set; }
        public DocumentType documentType {  get; set; }
        public string FileName {  get; set; }=string.Empty;
        public int CompanyID {  get; set; }

    }
}
