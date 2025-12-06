using SwitchCMS.API.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services
{
    public class HEM1Service: IHEM1Service
    {
        private readonly Repository.Interface.IHEM1Repository _hem1Repository;
        private readonly Repository.Interface.IOHEMRepository _ohemRepository;
        public HEM1Service(Repository.Interface.IHEM1Repository hem1Repository, Repository.Interface.IOHEMRepository _ohemRepository)
        {
            _hem1Repository = hem1Repository;
            this._ohemRepository = _ohemRepository;
        }
        public async Task<bool> DeleteResignation(int id)
        {
            bool success= await _hem1Repository.DeleteResignation(id);
            if( success)
            {
                var resignation = await _hem1Repository.GetResignationById(id);
                if(resignation is not null)
                {
                    await _ohemRepository.UpdateEmployeeStatus(resignation.EmployeeId, true);
                }
            }
            return success;
        }

        public async Task<HEM1> GetResignationById(int id)
        {
            return await _hem1Repository.GetResignationById(id);
        }

        public async Task<EmployeeResignationPagination> GetResignationsByCompanyId(EmployeeResignationPagination pagination)
        {
            if (pagination.PageIndex > 0)
                pagination.PageIndex--;
            pagination.EmployeeResignationList = await _hem1Repository.GetResignationsByCompanyId(pagination);
            pagination.TotalCount = await _hem1Repository.GetTotalResignationCount(pagination.CompanyId);
            pagination.PageIndex++;

            if (pagination.TotalCount == pagination.RowCount)
                pagination.TotalPage = 1;
            else
                pagination.TotalPage = (int)Math.Ceiling(((decimal)pagination.TotalCount / (decimal)pagination.RowCount));
            return pagination;
        }
        public async Task<ModificationStatus> InsertResignation(HEM1 modal)
        {
           bool Success = await _hem1Repository.InsertResignation(modal);
            if (Success)
            {
                bool isSuccess=await _ohemRepository.UpdateEmployeeStatus(modal.EmployeeId, false);
                return new ModificationStatus { Success = Success, Message = "Resignation Data Inserted Successfully" };
            }
            else
            {
                return new ModificationStatus { Success = Success, Message = "Resignation Data Insertion Failed" };
            }
        }
        public async Task<ModificationStatus> UpdateResignation(HEM1 modal)
        {
            bool Success= await _hem1Repository.UpdateResignation(modal);
            if (Success)
            {
                return new ModificationStatus { Success = Success, Message = "Resignation Data Updated Successfully" };
            }
            else
            {
                return new ModificationStatus { Success = Success, Message = "Resignation Data Updation Failed" };
            }
        }
    }
}
