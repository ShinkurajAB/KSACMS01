using SwitchCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Model
{
    /// <summary>
    /// Advatisement Modal
    /// </summary>
    public class OADV
    {
        public int ID {  get; set; }
        public DateTime? StartDate {  get; set; }=DateTime.Now;
        public DateTime? EndDate { get; set; } = DateTime.Now;
        public Status ImageStatus {  get; set; }       

        public string ImagePath { get; set; } =string.Empty;


        public List<ADV1> CustList { get; set; } = new();

    }
}
