using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services.Interface
{
    public interface IHEM4Service
    {
        Task<ModificationStatus> InsertOfferLetter(HEM4 modal);
        Task<ModificationStatus> UpdateOfferLetter(HEM4 modal);
        Task<OfferLetterPagination> GetOfferLetterByCompanyId(OfferLetterPagination pagination);
        Task<bool> DeleteOfferLetter(int id);       
        Task<HEM4> GetOfferLetterById(int id);
    }
}
