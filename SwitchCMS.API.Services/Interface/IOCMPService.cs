using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services.Interface
{
    public interface IOCMPService
    {
        Task<List<OCMP>> GetAllCompany();
        Task<ModificationStatus> SignUpCompany(OCMP company);
        Task<CompanyPagination> GetCompanyByPagination(CompanyPagination pagination);
        Task<bool> UpdateCompany(OCMP company);
        Task<bool> UpdateCompanyStatus(int companyId, string companyStatus);
        Task<bool> DeleteCompany(int companyId);
        Task<bool> UpdateInActiveCustomerbyExpireDate(DateTime Date);
        Task<OCMP> GetCustomerByCustID(int ID);
    }
}
