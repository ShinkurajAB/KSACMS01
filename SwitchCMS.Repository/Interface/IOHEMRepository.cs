using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository.Interface
{
    public interface IOHEMRepository
    {
        Task<bool> InsertEmployee(OHEM modal);
        Task<bool> UpdateEmployee(OHEM modal);
        Task<bool> DeleteEmployee(int empId);        
        Task<List<OHEM>> GetAllEmployeesByCompanyId(EmployeePagination pagination);
        Task<int> GetTotalEmployeeCount();
        Task<List<OHEM>> GetEmployeesByIqamaId(string iqamaId);
        Task<List<OHEM>> GetEmployeesByEmail(string email);
    }
}
