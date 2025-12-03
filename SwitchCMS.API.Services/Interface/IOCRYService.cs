using SwitchCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services.Interface
{
    public interface IOCRYService
    {
        Task<List<OCRY>> GetAllCountries();
    }
}
