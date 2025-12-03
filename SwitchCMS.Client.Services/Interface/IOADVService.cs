using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Client.Services.Interface
{
    public interface IOADVService
    {
        Task<ModificationStatus> CreateAdvatisement(OADV model, string token);
        Task<AdvatisementPagination> GetAdvatisementPagination(AdvatisementPagination Pagination,  string token);
        Task<OADV> GetAdvatisementByID(int ID,  string token);
        Task<ModificationStatus> UpdateAdvatisement(OADV model, string token);
        Task<ModificationStatus> DeleteAdvatisement(int ID, string token);
        Task<List<OADV>> GetAdvatisementByCustID(int CustID, string token);
    }
}
