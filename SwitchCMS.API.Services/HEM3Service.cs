using SwitchCMS.API.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using SwitchCMS.Repository;
using SwitchCMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services
{
    public class HEM3Service:IHEM3Service
    {
        private readonly  IHEM3Repository hem3Repository;
        public HEM3Service(IHEM3Repository hem3Repository)
        {
            this.hem3Repository = hem3Repository;
        }

        public async Task<bool> DeleteAbsentee(int id)
        {
           return await hem3Repository.DeleteAbsentee(id);
        }

        public async Task<HEM3> GetAbsenteeById(int id)
        {
            return await hem3Repository.GetAbsenteeById(id);
        }

        public async Task<EmployeeAbsencePagination> GetAbsenteesByCompanyId(EmployeeAbsencePagination pagination)
        {
            if (pagination.PageIndex > 0)
                pagination.PageIndex--;
            pagination.EmployeeAbsenceList = await hem3Repository.GetAbsenteesByCompanyId(pagination);
            pagination.TotalCount = await hem3Repository.GetTotalAbsenteeCount(pagination.CompanyId);
            pagination.PageIndex++;

            if (pagination.TotalCount == pagination.RowCount)
                pagination.TotalPage = 1;
            else
                pagination.TotalPage = (int)Math.Ceiling(((decimal)pagination.TotalCount / (decimal)pagination.RowCount));
            return pagination;
        }

        
        public async Task<ModificationStatus> InsertAbsentee(HEM3 modal)
        {
            bool Success = await hem3Repository.InsertAbsentee(modal);
            if (Success)
            {              
                return new ModificationStatus { Success = Success, Message = "Absence Data Inserted Successfully" };
            }
            else
            {
                return new ModificationStatus { Success = Success, Message = "Absence Data Insertion Failed" };
            }
        }

        public async Task<ModificationStatus> UpdateAbsentee(HEM3 modal)
        {
            bool Success = await hem3Repository.UpdateAbsentee(modal);
            if (Success)
            {
                return new ModificationStatus { Success = Success, Message = "Absence Data Updated Successfully" };
            }
            else
            {
                return new ModificationStatus { Success = Success, Message = "Absence Data Updated Failed" };
            }
        }
    }
}
