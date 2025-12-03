using SwitchCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository.Interface
{
    public interface IOCRYRepository
    {
        Task<List<OCRY>> GetAllCountries();
        Task<OCRY> GetCountryByCountryCode(string countryCode);
    }
}
