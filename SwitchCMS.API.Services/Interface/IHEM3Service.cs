using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services.Interface
{
    public interface IHEM3Service
    {
        Task<ModificationStatus> InsertAbsentee(HEM3 modal);
        Task<ModificationStatus> UpdateAbsentee(HEM3 modal);
        Task<EmployeeAbsencePagination> GetAbsenteesByCompanyId(EmployeeAbsencePagination pagination);
        Task<bool> DeleteAbsentee(int id);
        
        Task<HEM3> GetAbsenteeById(int id);
    }
}
