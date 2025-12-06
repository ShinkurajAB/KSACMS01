using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Client.Services.Interface
{
    public interface IOHEMService
    {
        Task<bool> InsertEmployee(OHEM modal,string token);
        Task<bool> UpdateEmployee(OHEM modal,string token);
        Task<bool> DeleteEmployee(int empId,string token);
        Task<EmployeePagination> GetEmployeeByPagination(EmployeePagination pagination, string token);
        Task<List<OHEM>> EmployeeBulkUpload(List<OHEM> employeeList,string token);
        Task<List<OHEM>> GetAllEmployessByCompany(int companyId, string token);
    }
}
