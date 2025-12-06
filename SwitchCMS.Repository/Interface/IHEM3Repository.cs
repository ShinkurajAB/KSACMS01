using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository.Interface
{
    public interface IHEM3Repository
    {
        Task<bool> InsertAbsentee(HEM3 modal);
        Task<bool> UpdateAbsentee(HEM3 modal);
        Task<List<HEM3>> GetAbsenteesByCompanyId(EmployeeAbsencePagination pagination);
        Task<bool> DeleteAbsentee(int id);
        Task<int> GetTotalAbsenteeCount(int companyId);
        Task<HEM3> GetAbsenteeById(int id);
    }
}
