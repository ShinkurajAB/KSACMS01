using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository.Interface
{
    public interface IVHL1Repository
    {
        Task<bool> CreateHandover(VHL1 Model);
        Task<bool> UpdateHandover(VHL1 Model);
        Task<List<VHL1>> GetHandoverByPageIndex(HandoverPagination Pagination);
        Task<int> GetHandoverCount(HandoverPagination Pagination);

        Task<VHL1> GetHandoverByID(int ID);

        Task<bool> DeleteHandover(int ID);
    }
}
