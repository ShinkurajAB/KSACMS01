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
    public class HEM5Service: IHEM5Service
    {
        private readonly IHEM5Repository hEM5Repository;

        public HEM5Service(IHEM5Repository _hEM5Repository)
        {
            this.hEM5Repository = _hEM5Repository;
        }

        public async Task<ModificationStatus> CreateGeneralWarning(HEM5 Model)
        {
            bool Success = await hEM5Repository.CreateGeneralWarning(Model);
            if (Success)
            {
                return new ModificationStatus { Success = Success, Message = "General Warning Created Successfully" };
            }
            else
            {
                return new ModificationStatus { Success = Success, Message = "General Warning Creation failed" };
            }
        }

        public async Task<ModificationStatus> DeleteGeneralWarning(int ID)
        {
            bool Success = await hEM5Repository.DeleteGeneralWarning(ID);
            if (Success)
            {
                return new ModificationStatus { Success = Success, Message = "General Warning Delete Successfully" };
            }
            else
            {
                return new ModificationStatus { Success = Success, Message = "General Warning Deletion failed" };
            }
        }

        public async Task<HEM5> GetGeneralWarningByID(int ID)
        {
            var data = await hEM5Repository.GetGeneralWarningByID(ID);
            return data;
        }

        public async Task<GeneralWarningPagination> GetGeneralWarningPagination(GeneralWarningPagination Pagination)
        {
            if (Pagination.PageIndex > 0)
                Pagination.PageIndex--;
            Pagination.GeneralWarningList = await hEM5Repository.GetGeneralWarningByPageIndex(Pagination);
            Pagination.TotalCount = await hEM5Repository.GetGeneralWarningCount(Pagination);
            Pagination.PageIndex++;

            if (Pagination.TotalCount == Pagination.RowCount)
                Pagination.TotalPage = 1;
            else
                Pagination.TotalPage = (int)Math.Ceiling(((decimal)Pagination.TotalCount / (decimal)Pagination.RowCount));
            return Pagination;
        }

        public async Task<ModificationStatus> UpdateGeneralWarning(HEM5 Model)
        {
            bool Success = await hEM5Repository.UpdateGeneralWarning(Model);
            if (Success)
            {
                return new ModificationStatus { Success = Success, Message = "General Warning update Successfully" };
            }
            else
            {
                return new ModificationStatus { Success = Success, Message = "General Warning updation failed" };
            }
        }
    }
}
