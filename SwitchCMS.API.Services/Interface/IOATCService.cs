using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services.Interface
{
    public interface IOATCService
    {
        Task<ModificationStatus> CreateDocument(OATC Model);
        Task<DocumentPagination> GetDocumentPagination(DocumentPagination Pagination);
        Task<OATC> GetDocumentByID(int ID);
        Task<ModificationStatus> DeleteDocumentByID(int ID);

        Task<ModificationStatus> UpdateDocument(OATC Model);
    }
}
