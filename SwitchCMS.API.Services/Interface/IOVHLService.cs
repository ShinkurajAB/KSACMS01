using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services.Interface
{
    public interface IOVHLService
    {
        Task<ModificationStatus> CreateVehicle(OVHL Model);
        Task<ModificationStatus> BulkCreateVehicle(List<OVHL> Model);
        Task<ModificationStatus> UpdateVehicle(OVHL Model);
        Task<VehiclePagination> GetVehiclePagination(VehiclePagination Pagination);
        Task<OVHL> GetVehicleByID(int VehicleID);
        Task<ModificationStatus> DeleteVehicle(int VehicleID);
    }
}
