using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Client.Services.Interface
{
    public interface IHEM3Service
    {
        Task<ModificationStatus> InsertAbsentee(HEM3 modal,string token);
        Task<ModificationStatus> UpdateAbsentee(HEM3 modal,string token);
        Task<EmployeeAbsencePagination> GetAbsenteesByCompanyId(EmployeeAbsencePagination pagination,string token);
        Task<bool> DeleteAbsentee(int id,string token);

        Task<HEM3> GetAbsenteeById(int id, string token);
    }
}
