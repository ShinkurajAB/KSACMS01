using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository.Interface
{
    public interface IOATCRepository
    {
        Task<bool> CreateDocument(OATC Model);

        Task<List<OATC>> GetDocumentByPageIndex(DocumentPagination Pagination);
        Task<int> GetDocumentCount(DocumentPagination Pagination);

        Task<OATC> GetDocumentByID(int ID);
        Task<bool> DeleteDocumentByID(int ID);

        Task<bool> UpdateDocument(OATC Model);

    }
}
