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
    public class OVHLService : IOVHLService
    {
        private readonly IOVHLRepository VehicleRepository;
        public OVHLService(IOVHLRepository _VehicleRepository)
        {
            this.VehicleRepository = _VehicleRepository;
        }


       
        public async Task<ModificationStatus> CreateVehicle(OVHL Model)
        {
            bool Success= await VehicleRepository.CreateVehicle(Model);
            if (Success) 
            {
                return new ModificationStatus { Success = Success, Message = "Vehicle Created Successfully" };
            }
            else
            {
                return new ModificationStatus { Success = Success, Message = "Vehicle Creation failed" };
            }
             
        }


        public async Task<ModificationStatus> BulkCreateVehicle(List<OVHL> Model)
        {
            bool Success = await VehicleRepository.BulkCreateVehicle(Model);
            if (Success)
            {
                return new ModificationStatus { Success = Success, Message = "Bulk Vehicle Created Successfully" };
            }
            else
            {
                return new ModificationStatus { Success = Success, Message = "Vehicle Creation failed" };
            }
        }


        public async Task<ModificationStatus> UpdateVehicle(OVHL Model)
        {
            bool Success = await VehicleRepository.UpdateVehicle(Model);
            if (Success)
            {
                return new ModificationStatus { Success = Success, Message = "Vehicle Updated Successfully" };
            }
            else
            {
                return new ModificationStatus { Success = Success, Message = "Vehicle updation failed" };
            }
        }

        public async Task<VehiclePagination> GetVehiclePagination(VehiclePagination Pagination)
        {
            if (Pagination.PageIndex > 0)
                Pagination.PageIndex--;
            Pagination.VehicleList = await VehicleRepository.GetVehicleByPageIndex(Pagination);
            Pagination.TotalCount = await VehicleRepository.GetVehicleCount(Pagination);
            Pagination.PageIndex++;

            if (Pagination.TotalCount == Pagination.RowCount)
                Pagination.TotalPage = 1;
            else
                Pagination.TotalPage = (int)Math.Ceiling(((decimal)Pagination.TotalCount / (decimal)Pagination.RowCount));
            return Pagination;
             
        }

        public async Task<OVHL> GetVehicleByID(int VehicleID)
        {
            var Data= await VehicleRepository.GetVehicleByID(VehicleID);
            return Data;
             
        }

        public async Task<ModificationStatus> DeleteVehicle(int VehicleID)
        {

            bool Success = await VehicleRepository.DeleteVehicle(VehicleID);
            if (Success)
            {
                return new ModificationStatus { Success = Success, Message = "Vehicle Deleted Successfully" };
            }
            else
            {
                return new ModificationStatus { Success = Success, Message = "Vehicle deletion failed" };
            }
        }
    }
}
