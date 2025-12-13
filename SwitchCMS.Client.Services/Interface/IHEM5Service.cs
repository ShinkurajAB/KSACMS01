using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Client.Services.Interface
{
    public interface IHEM5Service
    {
        Task<ModificationStatus> CreateGeneralWarning(HEM5 Model,string token);
        Task<ModificationStatus> UpdateGeneralWarning(HEM5 Model,string token);
        Task<GeneralWarningPagination> GetGeneralWarningPagination(GeneralWarningPagination Pagination,string token);
        Task<HEM5> GetGeneralWarningByID(int ID,string token);
        Task<ModificationStatus> DeleteGeneralWarning(int ID, string token);
    }
}
