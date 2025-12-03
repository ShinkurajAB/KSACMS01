using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Client.Services.Interface
{
    public interface IOVHLService
    {
        Task<ModificationStatus> CreateVehicle(OVHL Model, string token);
        Task<VehiclePagination> GetVehicleByPageIndex(VehiclePagination Pagination, string token);
        Task<OVHL> GetVehicleByVehcileID(int VehcleID,  string token);
        Task<ModificationStatus> UpdateVehicle(OVHL Model, string token);
        Task<ModificationStatus> DeleteVehicle(int VehicleID, string token);
     }
}
