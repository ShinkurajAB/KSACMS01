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
    /// User Menu Model
    /// </summary>
    public class OUMN
    {       
       
        public int ID { get; set; }

        public int UserID {  get; set; }
        public int MenuID {  get; set; }

        public OUSR? User { get; set; }
        public OMEN Menu { get; set; } = new();

    }
}
