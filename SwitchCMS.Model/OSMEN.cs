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
    /// SubMenu Model
    /// </summary>
    public class OSMEN
    {
        
        public int ID {  get; set; }
        public int MenuID {  get; set; }
        public string Name {  get; set; }=string.Empty;
        public string Link { get; set; } = string.Empty;
        public OMEN Menu { get; set; } = new();
    }
}
