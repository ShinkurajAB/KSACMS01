using SwitchCMS.API.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services
{
    public class OACAService:IOACAService
    {
        private readonly IOACARepository accessRepository;

        public OACAService(IOACARepository accessRepository)
        {
            this.accessRepository = accessRepository;
        }

        public async Task<bool> DeleteAccessAccountManager(int accountId)
        {
            bool isDelete = await accessRepository.DeleteAccessAccountManager(accountId);
            return isDelete;
        }

        public async Task<List<OACA>> GetAccessAccountManagersByCompanyId(int companyId)
        {
            List<OACA> accessList = await accessRepository.GetAccessAccountManagersByCompanyId(companyId);
            return accessList;
        }

        public async Task<List<OACA>> GetAllAccessAccountManagers()
        {
            List<OACA> accessList=await accessRepository.GetAllAccessAccountManagers();
            return accessList;
        }

        public async Task<bool> InsertAccessAccountManager(OACA modal)
        {
            bool isSuccess=await accessRepository.InsertAccessAccountManager(modal);
            return isSuccess;
        }

        public async Task<bool> UpdateAccessAccountManager(OACA modal)
        {
            bool isSuccess = await accessRepository.UpdateAccessAccountManager(modal);
            return isSuccess;
        }
    }
}
