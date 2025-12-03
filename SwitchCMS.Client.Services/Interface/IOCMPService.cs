using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Client.Services.Interface
{
    public interface IOCMPService
    {
        Task<List<OCMP>> GetAllCompany(string token);
        Task<bool> SignUpCompany(OCMP company);
        Task<CompanyPagination> GetCompanyByPagination(CompanyPagination pagination, string token);
        Task<bool> UpdateCompany(OCMP company,string token);
        Task<bool> UpdateCompanyStatus(int companyId, string companyStatus,string token);
        Task<bool> DeleteCompany(int companyId,string token);
    }
}
