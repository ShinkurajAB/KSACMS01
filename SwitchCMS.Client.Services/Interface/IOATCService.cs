using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Client.Services.Interface
{
    public interface IOATCService
    {
        Task<ModificationStatus> CreateDocument(OATC Model, string token);
        Task<DocumentPagination> GetDocumentByPageIndex(DocumentPagination Pagination, string token);
        Task<OATC> GetDocumentByID(int ID, string token);

        Task<ModificationStatus> DeleteDocumentByID(int ID, string token);

        Task<ModificationStatus> UpdateDocumentByID(OATC Model, string token);
    }
}
