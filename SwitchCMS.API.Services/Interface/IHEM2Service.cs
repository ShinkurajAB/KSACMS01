using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services.Interface
{
    public interface IHEM2Service
    {
        Task<ModificationStatus> CreateDirectNotification(HEM2 Model);
        Task<ModificationStatus> UpdateDirectNotification(HEM2 Model);
        Task<DirectNotificationPagination> GetDirectNotificationPagination(DirectNotificationPagination Pagination);
        Task<HEM2> GetDirectNotificationByID(int ID);
        Task<ModificationStatus> DeleteDirectNotification(int ID);
    }
}
