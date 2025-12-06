using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services.Interface
{
    public interface IHEM1Service
    {
        Task<ModificationStatus> InsertResignation(HEM1 modal);
        Task<ModificationStatus> UpdateResignation(HEM1 modal);
        Task<EmployeeResignationPagination> GetResignationsByCompanyId(EmployeeResignationPagination pagination);
        Task<bool> DeleteResignation(int id);
        Task<HEM1> GetResignationById(int id);
    }
}
