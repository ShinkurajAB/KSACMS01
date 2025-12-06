using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services.Interface
{
    public interface IOHEMService
    {
        Task<bool> InsertEmployee(OHEM modal);
        Task<bool> UpdateEmployee(OHEM modal);
        Task<bool> DeleteEmployee(int empId);
        Task<EmployeePagination> GetEmployeeByPagination(EmployeePagination pagination);
        Task<List<OHEM>> EmployeeBulkUpload(List<OHEM> employeeList);
        Task<List<OHEM>> GetAllEmployessByCompany(int companyId);
    }
}
