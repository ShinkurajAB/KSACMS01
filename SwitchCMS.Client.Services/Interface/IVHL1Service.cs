using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Client.Services.Interface
{
    public interface IVHL1Service
    {
        Task<ModificationStatus> CreateHandover(VHL1 Model, string token);
        Task<HandoverPagination> GetHandoverByPageIndex(HandoverPagination Pagination, string token);
        Task<VHL1> GetHandoverByHandoverID(int HandoverID, string token);
        Task<ModificationStatus> UpdateHandover(VHL1 Model, string token);
        Task<ModificationStatus> DeleteHandover(int HandoverID, string token);
    }
}
