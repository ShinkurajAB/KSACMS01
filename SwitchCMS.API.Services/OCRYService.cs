using SwitchCMS.API.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services
{
    public class OCRYService: IOCRYService
    {
        private readonly IOCRYRepository ocryRepository;
        public OCRYService(IOCRYRepository ocryRepository)
        {
            this.ocryRepository = ocryRepository;
        }

        public async Task<List<OCRY>> GetAllCountries()
        {
            List<OCRY> countryList = new List<OCRY>();
            countryList = await ocryRepository.GetAllCountries();
            return countryList;
        }
    }
}
