using SwitchCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository.Interface
{
    public interface IOUMNRepository
    {
        Task<int> CreateUserMenus(List<OUMN> UserMenu);
        Task<int> DeleteSubMenuByUserID(int UserID);
    }
}
