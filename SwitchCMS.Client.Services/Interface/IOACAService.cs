using SwitchCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Client.Services.Interface
{
    public interface IOACAService
    {
        Task<bool> InsertAccessAccountManager(OACA modal,string token);
        Task<bool> UpdateAccessAccountManager(OACA modal,string token);
        Task<bool> DeleteAccessAccountManager(int accountId,string token);
        Task<List<OACA>> GetAllAccessAccountManagers(string token);
        Task<List<OACA>> GetAccessAccountManagersByCompanyId(int companyId, string token);
    }
}
