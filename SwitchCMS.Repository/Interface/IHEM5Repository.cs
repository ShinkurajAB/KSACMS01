using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository.Interface
{
    public interface IHEM5Repository
    {
        Task<bool> CreateGeneralWarning(HEM5 Model);
        Task<bool> UpdateGeneralWarning(HEM5 Model);
        Task<List<HEM5>> GetGeneralWarningByPageIndex(GeneralWarningPagination Pagination);
        Task<int> GetGeneralWarningCount(GeneralWarningPagination Pagination);
        Task<HEM5> GetGeneralWarningByID(int ID);
        Task<bool> DeleteGeneralWarning(int ID);
    }
}
