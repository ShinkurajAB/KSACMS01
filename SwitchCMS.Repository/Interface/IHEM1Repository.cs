using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository.Interface
{
    public interface IHEM1Repository
    {
        Task<bool> InsertResignation(HEM1 modal);
        Task<bool> UpdateResignation(HEM1 modal);
        Task<List<HEM1>> GetResignationsByCompanyId(EmployeeResignationPagination pagination);
        Task<bool> DeleteResignation(int id);
        Task<int> GetTotalResignationCount(int companyId);
        Task<HEM1> GetResignationById(int id);
    }
}
