using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository.Interface
{
    public interface IHEM2Repository
    {
        Task<bool> CreateDirectNotification(HEM2 Model);
        Task<bool> UpdateDirectNotification(HEM2 Model);
        Task<List<HEM2>> GetDirectNotificationByPageIndex(DirectNotificationPagination Pagination);
        Task<int> GetDirectNotificationCount(DirectNotificationPagination Pagination);
        Task<HEM2> GetDirectNotificationByID(int ID);
        Task<bool> DeleteDirectNotification(int ID);

    }
}
