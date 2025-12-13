using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services.Interface
{
    public interface IHEM5Service
    {
        Task<ModificationStatus> CreateGeneralWarning(HEM5 Model);
        Task<ModificationStatus> UpdateGeneralWarning(HEM5 Model);
        Task<GeneralWarningPagination> GetGeneralWarningPagination(GeneralWarningPagination Pagination);
        Task<HEM5> GetGeneralWarningByID(int ID);
        Task<ModificationStatus> DeleteGeneralWarning(int ID);
    }
}
