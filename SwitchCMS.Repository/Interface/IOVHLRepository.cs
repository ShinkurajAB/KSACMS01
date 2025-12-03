using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository.Interface
{
    public interface IOVHLRepository
    {
        Task<bool> CreateVehicle(OVHL Model);
        Task<bool> BulkCreateVehicle(List<OVHL> List);
        Task<bool> UpdateVehicle(OVHL Model);

        Task<List<OVHL>> GetVehicleByPageIndex(VehiclePagination Pagination);
        Task<int> GetVehicleCount(VehiclePagination Pagination);
        Task<OVHL> GetVehicleByID(int VehicleID);
        Task<bool> DeleteVehicle(int VehicleID);
    }
}
