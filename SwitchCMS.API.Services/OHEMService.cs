using SwitchCMS.API.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using SwitchCMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services
{
    public class OHEMService:IOHEMService
    {
        private readonly IOHEMRepository employeeRepository;
        private readonly IOCRYRepository countryRepository;
        public OHEMService(IOHEMRepository employeeRepository, IOCRYRepository countryRepository)
        {
            this.employeeRepository = employeeRepository;
            this.countryRepository = countryRepository;
        }

        public async Task<bool> DeleteEmployee(int empId)
        {
            bool isDelete = await employeeRepository!.DeleteEmployee(empId);
            return isDelete;
        }

        public async Task<List<OHEM>> EmployeeBulkUpload(List<OHEM> employeeList)
        {
            List<OHEM> result = new List<OHEM>();
            if (employeeList!=null)
            {
                foreach (var item in employeeList)
                {
                    OCRY country = await countryRepository.GetCountryByCountryCode(item.Nationality);
                    var empIqamaList = await employeeRepository.GetEmployeesByIqamaId(item.IqamaID);
                    var empEmailList = await employeeRepository.GetEmployeesByEmail(item.Email);
                    
                    if(string.IsNullOrEmpty(item.Name))
                    {
                        item.BulkUploadStatus = "Name is required";
                        result.Add(item);
                    }
                    else if (string.IsNullOrEmpty(item.JobTitle))
                    {
                        item.BulkUploadStatus = "Job Title is required";
                        result.Add(item);
                    }
                    else if (country == null)
                    {
                        item.BulkUploadStatus = "Invalid Nationality";
                        result.Add(item);
                    }
                    else if (empIqamaList.Count > 0)
                    {
                        item.BulkUploadStatus = "Duplicate IqamaID";
                        result.Add(item);

                    }
                    else if (empEmailList.Count > 0)
                    {
                        item.BulkUploadStatus = "Duplicate Email";
                        result.Add(item);
                    }
                    else
                    {
                        bool isInserted = await employeeRepository.InsertEmployee(item);
                        if (isInserted)
                        {
                            item.BulkUploadStatus = "Success";
                            result.Add(item);
                        }
                        else
                        {
                            item.BulkUploadStatus = "Failed to insert";
                            result.Add(item);
                        }

                    }
                }
            }
            return result;
        }

        public async Task<EmployeePagination> GetEmployeeByPagination(EmployeePagination pagination)
        {
            if (pagination.PageIndex > 0)
                pagination.PageIndex--;
            pagination.EmployeeList = await employeeRepository.GetAllEmployeesByCompanyId(pagination);
            pagination.TotalCount = await employeeRepository.GetTotalEmployeeCount();
            pagination.PageIndex++;

            if (pagination.TotalCount == pagination.RowCount)
                pagination.TotalPage = 1;
            else
                pagination.TotalPage = (int)Math.Ceiling(((decimal)pagination.TotalCount / (decimal)pagination.RowCount));
            return pagination;
        }

        public async Task<bool> InsertEmployee(OHEM modal)
        {
            bool isSuccess = await employeeRepository.InsertEmployee(modal);
            return isSuccess;
        }

        public async Task<bool> UpdateEmployee(OHEM modal)
        {
            bool isSuccess = await employeeRepository.UpdateEmployee(modal);
            return isSuccess;
        }
    }
}
