using SwitchCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Client.Services.Interface
{
    public interface IOMENService
    {
        Task<List<OMEN>> GetAllMenu(string token);
        Task<List<OMEN>> GetMenuByUserID(int UserID, string token);
    }
}
