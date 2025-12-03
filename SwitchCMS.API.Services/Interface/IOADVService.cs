using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services.Interface
{
    public interface IOADVService
    {
        Task<ModificationStatus> CreateAdvatisement(OADV Model);
        Task<ModificationStatus> updateAdvatisement(OADV Model);
        Task<List<OADV>> GetAllAdvatisement();
        Task<List<OADV>> GetAdvatisementByCustomerID(int CustID);
        Task<OADV> GetAdvatisementByID(int ID);
        Task<AdvatisementPagination> GetAdvatisementPagination(AdvatisementPagination Pagination);

        Task<ModificationStatus> DeleteAdvatisement(int ID);

        Task<List<OADV>> GetAdvatisementByCustID(int CustID);
    }
}
