using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository.Interface
{
    public interface IOADVRepository
    {
        Task<int> CreateAdvatisement(OADV AdvtiseModal);
        Task<bool> UpdateAdvatisement(OADV AdvtisemrntModal);
        Task<bool> DeleteeAdvatisementModal(int ID);
        Task<List<OADV>> GetAllAdvatisement();
        Task<List<OADV>> GetAllActiveAdvatisementByCompanyID(int CompanyID);
        Task<OADV> GetAdvatisementByID(int ID);
        Task<List<OADV>> GetAdvatisementPagination(AdvatisementPagination Pagination);
        Task<int> GetTotalAdvatisementCount(AdvatisementPagination Pagination);

        Task<List<OADV>> GetAdvatisementByCustomerIDandIsAll(int CustID, bool IsAll);
    }
}
