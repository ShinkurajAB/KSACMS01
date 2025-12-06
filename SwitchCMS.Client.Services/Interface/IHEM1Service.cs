using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Client.Services.Interface
{
    public interface IHEM1Service
    {
        Task<ModificationStatus> InsertResignation(HEM1 modal,string token);
        Task<ModificationStatus> UpdateResignation(HEM1 modal,string token);
        Task<EmployeeResignationPagination> GetResignationsByCompanyId(EmployeeResignationPagination pagination,string token);
        Task<bool> DeleteResignation(int id,string token);
        Task<HEM1> GetResignationById(int id,string token);
    }
}
