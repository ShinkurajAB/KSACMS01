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
    public class VHL1Service : IVHL1Service
    {

        private readonly IVHL1Repository HandoverRepository;
        public VHL1Service(IVHL1Repository _HandoverRepository)
        {
            this.HandoverRepository = _HandoverRepository;
        }


        public async  Task<ModificationStatus> CreateHandover(VHL1 Model)
        {
            bool Success = await HandoverRepository.CreateHandover(Model);
            if (Success)
            {
                return new ModificationStatus { Success = Success, Message = "Handover Created Successfully" };
            }
            else
            {
                return new ModificationStatus { Success = Success, Message = "Handover Creation failed" };
            }
        }

        public async Task<ModificationStatus> UpdateHandover(VHL1 Model)
        {
            bool Success = await HandoverRepository.UpdateHandover(Model);
            if (Success)
            {
                return new ModificationStatus { Success = Success, Message = "Vehicle Updated Successfully" };
            }
            else
            {
                return new ModificationStatus { Success = Success, Message = "Vehicle updation failed" };
            }
        }


        public async Task<HandoverPagination> GetVehiclePagination(HandoverPagination Pagination)
        {
            if (Pagination.PageIndex > 0)
                Pagination.PageIndex--;
            Pagination.HandoverList = await HandoverRepository.GetHandoverByPageIndex(Pagination);
            Pagination.TotalCount = await HandoverRepository.GetHandoverCount(Pagination);
            Pagination.PageIndex++;

            if (Pagination.TotalCount == Pagination.RowCount)
                Pagination.TotalPage = 1;
            else
                Pagination.TotalPage = (int)Math.Ceiling(((decimal)Pagination.TotalCount / (decimal)Pagination.RowCount));
            return Pagination;
        }

        public async Task<VHL1> GetHandoverByID(int HandoverID)
        {
            var Data = await HandoverRepository.GetHandoverByID(HandoverID);
            return Data;
        }

      

        public async Task<ModificationStatus> DeleteHandover(int HandoverID)
        {
            bool Success = await HandoverRepository.DeleteHandover(HandoverID);
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
