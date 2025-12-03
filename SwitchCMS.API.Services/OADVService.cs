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
    public class OADVService : IOADVService
    {
        private readonly IOADVRepository advRepository;
        private readonly IADV1Repository aDV1Repository;
        private readonly IOCMPService ocmpService;
        public OADVService(IOADVRepository _advRepository, 
            IADV1Repository _aDV1Repository,
            IOCMPService _oCMPService
            )
        {
            advRepository = _advRepository;
            this.aDV1Repository = _aDV1Repository;
            ocmpService= _oCMPService;
        }

        public async Task<ModificationStatus> CreateAdvatisement(OADV Model)
        {
            try
            {
                int CreatedID = await advRepository.CreateAdvatisement(Model);
                if (Model.CustList != null)
                {
                    if (Model.CustList.Count > 0)
                    {
                        Model.CustList.ForEach(x => x.OADVID = CreatedID);

                        foreach (var cust in Model.CustList)
                        {
                            await aDV1Repository.InsertAdvCust(cust);
                        }
                    }
                }
                return new ModificationStatus { Message = "Successfully Created", Success = true };
            }
            catch(Exception ex)
            {
                return new ModificationStatus { Message = ex.Message, Success = false };
            }
        }

        public async Task<List<OADV>> GetAdvatisementByCustomerID(int CustID)
        {
           List<OADV> advatiseList=await advRepository.GetAllActiveAdvatisementByCompanyID(CustID);
            return advatiseList;
        }
        
        public async Task<List<OADV>> GetAllAdvatisement()
        {
            List<OADV> advatiseList = await advRepository.GetAllAdvatisement();
            return advatiseList;
        }

        public async Task<ModificationStatus> updateAdvatisement(OADV Model)
        {
            try
            {
                bool success = await advRepository.UpdateAdvatisement(Model);
                if (success)
                {
                    // delete exist Dataa
                    await aDV1Repository.deleteAdvbyCustID(Model.ID);
                    if (Model.CustList!= null)
                    {
                        
                        if (Model.CustList.Count>0)
                        {
                            
                           


                            // insert Data
                            foreach (var cust in Model.CustList)
                            {
                                await aDV1Repository.InsertAdvCust(cust);
                            }
                        }
                    }
                }
                return  new ModificationStatus { Message = "successfully Updated", Success = true }; ;
            }
            catch (Exception ex) 
            {
                return new ModificationStatus { Message = ex.Message, Success = false };
            }
        }
        public async Task<OADV> GetAdvatisementByID(int ID)
        {
            var Data= await advRepository.GetAdvatisementByID(ID);
            return Data;             
        }

        public async Task<AdvatisementPagination> GetAdvatisementPagination(AdvatisementPagination Pagination)
        {
            if (Pagination.PageIndex > 0)
                Pagination.PageIndex--;
            Pagination.AdvatisementList = await advRepository.GetAdvatisementPagination(Pagination);
            Pagination.TotalCount = await advRepository.GetTotalAdvatisementCount(Pagination);
            Pagination.PageIndex++;

            if (Pagination.TotalCount == Pagination.RowCount)
                Pagination.TotalPage = 1;
            else
                Pagination.TotalPage = (int)Math.Ceiling(((decimal)Pagination.TotalCount / (decimal)Pagination.RowCount));
            return Pagination;
             
        }

        public async Task<ModificationStatus> DeleteAdvatisement(int ID)
        {
            bool Success= await aDV1Repository.deleteAdvbyCustID(ID);
            Success= await advRepository.DeleteeAdvatisementModal(ID);
            return new ModificationStatus { Message = "Successfully Deleted", Success = true }; ;
            
        }

        public async Task<List<OADV>> GetAdvatisementByCustID(int CustID)
        {

            var Customer=await ocmpService.GetCustomerByCustID(CustID);
            List<OADV> AdList = await advRepository.GetAdvatisementByCustomerIDandIsAll(Customer.ID, Customer.IsAll);
            return AdList;           

             
        }
    }
}
