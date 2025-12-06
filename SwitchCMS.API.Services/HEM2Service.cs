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
    public class HEM2Service : IHEM2Service
    {
        private readonly IHEM2Repository hEM2Repository;

        public HEM2Service(IHEM2Repository _hEM2Repository)
        {
            this.hEM2Repository = _hEM2Repository;
        }

        public async Task<ModificationStatus> CreateDirectNotification(HEM2 Model)
        {
            bool Success = await hEM2Repository.CreateDirectNotification(Model);
            if (Success)
            {
                return new ModificationStatus { Success = Success, Message = "Direct Notification Created Successfully" };
            }
            else
            {
                return new ModificationStatus { Success = Success, Message = "Direct Notification Creation failed" };
            }
        }

        public async Task<ModificationStatus> UpdateDirectNotification(HEM2 Model)
        {
            bool Success = await hEM2Repository.UpdateDirectNotification(Model);
            if (Success)
            {
                return new ModificationStatus { Success = Success, Message = "Direct Notification update Successfully" };
            }
            else
            {
                return new ModificationStatus { Success = Success, Message = "Direct Notification updation failed" };
            }
        }

        public async Task<DirectNotificationPagination> GetDirectNotificationPagination(DirectNotificationPagination Pagination)
        {
            if (Pagination.PageIndex > 0)
                Pagination.PageIndex--;
            Pagination.DirectNotificationList = await hEM2Repository.GetDirectNotificationByPageIndex(Pagination);
            Pagination.TotalCount = await hEM2Repository.GetDirectNotificationCount(Pagination);
            Pagination.PageIndex++;

            if (Pagination.TotalCount == Pagination.RowCount)
                Pagination.TotalPage = 1;
            else
                Pagination.TotalPage = (int)Math.Ceiling(((decimal)Pagination.TotalCount / (decimal)Pagination.RowCount));
            return Pagination;
        }

        public async Task<HEM2> GetDirectNotificationByID(int ID)
        {
            var data = await hEM2Repository.GetDirectNotificationByID(ID);
            return data;
             
        }

        public async Task<ModificationStatus> DeleteDirectNotification(int ID)
        {
            bool Success = await hEM2Repository.DeleteDirectNotification(ID);
            if (Success)
            {
                return new ModificationStatus { Success = Success, Message = "Direct Notification Delete Successfully" };
            }
            else
            {
                return new ModificationStatus { Success = Success, Message = "Direct Notification Deletion failed" };
            }
        }

            
      
    }
}
