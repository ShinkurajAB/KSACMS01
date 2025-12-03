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
    public class OATCService : IOATCService
    {

        private readonly IOATCRepository attachmentRepository;

        public OATCService(IOATCRepository _attachmentRepository)
        {
            this.attachmentRepository = _attachmentRepository;
        }

        public async Task<ModificationStatus> CreateDocument(OATC Model)
        {
            bool Success = await attachmentRepository.CreateDocument(Model);
            if (Success)
            {
                return new ModificationStatus { Success = Success, Message = "Document Created Successfully" };
            }
            else
            {
                return new ModificationStatus { Success = Success, Message = "Document Creation failed" };
            }
             
        }

       
        public async Task<DocumentPagination> GetDocumentPagination(DocumentPagination Pagination)
        {
            if (Pagination.PageIndex > 0)
                Pagination.PageIndex--;
            Pagination.AttachmentDetails = await attachmentRepository.GetDocumentByPageIndex(Pagination);
            Pagination.TotalCount = await attachmentRepository.GetDocumentCount(Pagination);
            Pagination.PageIndex++;

            if (Pagination.TotalCount == Pagination.RowCount)
                Pagination.TotalPage = 1;
            else
                Pagination.TotalPage = (int)Math.Ceiling(((decimal)Pagination.TotalCount / (decimal)Pagination.RowCount));
            return Pagination;
        }

        public async Task<OATC> GetDocumentByID(int ID)
        {
            var data = await attachmentRepository.GetDocumentByID(ID);
            return data;            
        }

        public async Task<ModificationStatus> DeleteDocumentByID(int ID)
        {
            bool Success= await attachmentRepository.DeleteDocumentByID(ID);
            if (Success)
            {
                ModificationStatus modification = new ModificationStatus();
                modification.Success = Success;
                modification.Message = "Deleted Successfully";
                return modification;
            }
            return new ModificationStatus { Success= false, Message="Faild to Delete" };
        }

        public async Task<ModificationStatus> UpdateDocument(OATC Model)
        {

            bool Success = await attachmentRepository.UpdateDocument(Model);
            if (Success)
            {
                return new ModificationStatus { Success = Success, Message = "Document Updated Successfully" };
            }
            else
            {
                return new ModificationStatus { Success = Success, Message = "Document Updation failed" };
            }
        }
    }
}
