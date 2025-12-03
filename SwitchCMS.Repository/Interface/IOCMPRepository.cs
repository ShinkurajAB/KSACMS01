using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository.Interface
{
    public interface IOCMPRepository 
    {

        Task<List<OCMP>> GetAllCompanys();
        Task<bool> SignUpCompany(OCMP company);
        Task<List<OCMP>> GetCompanyByPageIndex(CompanyPagination companyPagination);
        Task<int> GetTotalCompanyCount(CompanyPagination Pagination);
        Task<bool> UpdateCompany(OCMP company);
        Task<bool> UpdateCompanyStatus(int companyId, string companyStatus);
        Task<bool> DeleteCompany(int companyId);

        Task<bool> UpdateInActiveCustomerbyExpireDate(DateTime Date);
        Task<OCMP> GetCustomerByCustID(int ID);
        

    }
}
