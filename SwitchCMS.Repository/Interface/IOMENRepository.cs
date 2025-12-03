using SwitchCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository.Interface
{
    public interface IOMENRepository
    {
        Task<List<OMEN>> GetMenuByUserIDMenus(int UserID);
        Task<List<OMEN>> GetAllMenus();
    }
}
