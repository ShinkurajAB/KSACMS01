using SwitchCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services.Interface
{
    public interface IOACAService
    {
        Task<bool> InsertAccessAccountManager(OACA modal);
        Task<bool> UpdateAccessAccountManager(OACA modal);
        Task<bool> DeleteAccessAccountManager(int accountId);
        Task<List<OACA>> GetAllAccessAccountManagers();
        Task<List<OACA>> GetAccessAccountManagersByCompanyId(int companyId);
    }
}
