using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services.Interface
{
    public interface IOUSRService
    {
        Task<OUSR> GetUserByUserName(string userName);
        Task<OUSR> GetUserByUserID(int userID);
        Task<UsersPagination> GetUserByPagination(UsersPagination pagination);         
        Task<ModificationStatus> CreeateUpdateUser(OUSR user);

        Task<ModificationStatus> DeleteUserByUserID(int UserID);
    }
}
