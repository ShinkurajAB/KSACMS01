using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services.Interface
{
    public interface IVHL1Service
    {
        Task<ModificationStatus> CreateHandover(VHL1 Model);        
        Task<ModificationStatus> UpdateHandover(VHL1 Model);
        Task<HandoverPagination> GetVehiclePagination(HandoverPagination Pagination);
        Task<VHL1> GetHandoverByID(int HandoverID);
        Task<ModificationStatus> DeleteHandover(int HandoverID);
    }
}
