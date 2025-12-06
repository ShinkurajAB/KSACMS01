using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository.Interface
{
    public interface IHEM4Repository
    {
        Task<bool> InsertOfferLetter(HEM4 modal);
        Task<bool> UpdateOfferLetter(HEM4 modal);
        Task<List<HEM4>> GetOfferLetterByCompanyId(OfferLetterPagination pagination);
        Task<bool> DeleteOfferLetter(int id);
        Task<int> GetTotalOfferLetterCount(int companyId);
        Task<HEM4> GetOfferLetterById(int id);
    }
}
