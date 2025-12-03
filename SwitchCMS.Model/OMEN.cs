using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwitchCMS.Model
{
    /// <summary>
    /// Menu Model
    /// </summary>
    public class OMEN
    {
        
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Icon {  get; set; }= string.Empty;


        // UI Support Selecter
        public string Selector {  get; set; } = string.Empty;

        public List<OSMEN> SubMenu {  get; set; }=new List<OSMEN>();
    }
}
