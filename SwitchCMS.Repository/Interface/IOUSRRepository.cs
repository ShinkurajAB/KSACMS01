using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using SwitchCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository.Interface
{
    public interface IOUSRRepository
    {
        Task<OUSR> GetUserbyUserName(string userName);
        Task<OUSR> GetUserbyUserID(int UserID);
        Task<List<OUSR>> GetUserByPageIndex(UsersPagination Pagination);
        Task<int> GetTotalUserCount(int CompanyID, string UsersName);
        Task<bool> UserNameExist(string UserName);
        Task<ModificationStatus> CreeateUpdateUser(OUSR user);
        Task<List<OUSR>> CompanyAdminUserExistorNot(int CompanyID, Roles role);
        Task<ModificationStatus> DeleteUser(int UserID);
    }
}
