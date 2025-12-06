using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Client.Services.Interface
{
    public interface IHEM4Service
    {
        Task<ModificationStatus> InsertOfferLetter(HEM4 modal,string token);
        Task<ModificationStatus> UpdateOfferLetter(HEM4 modal,string token);
        Task<OfferLetterPagination> GetOfferLetterByCompanyId(OfferLetterPagination pagination,string token);
        Task<bool> DeleteOfferLetter(int id,string token);
        Task<HEM4> GetOfferLetterById(int id, string token);
    }
}
