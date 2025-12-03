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
    /// User Model
    /// </summary>
    public class OUSR
    {
        
        public int ID { get; set; }
        public int CompanyID {  get; set; }
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; }=string.Empty;
        public Roles Role {  get; set; }
        public Status Status { get; set; }

        public OCMP Company { get; set; } = new();

        public List<OUMN> UserMenus { get; set; }=new();
    }
}
