using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Model.UI
{
    public class ModificationStatus
    {
        public int ID {  get; set; }
        public bool Success {  get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
