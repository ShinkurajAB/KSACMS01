using Microsoft.EntityFrameworkCore;
using SwitchCMS.DataBaseContext.Dapper;
using SwitchCMS.DataBaseContext;
using SwitchCMS.Model;
using SwitchCMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace SwitchCMS.Repository
{
    public class OCRYRepository :  IOCRYRepository
    {
        private readonly IDapperContext DbContext;
        public OCRYRepository(IDapperContext _DbContext)
        {
            DbContext = _DbContext;
        }

        public async Task<List<OCRY>> GetAllCountries()
        {
            string SqlQuery = "select * from OCRY order by CountryName";
            var Data= await DbContext.QueryAsync<OCRY>(SqlQuery);
            return Data.ToList();

        }

        public async Task<OCRY> GetCountryByCountryCode(string countryCode)
        {
            try
            {
                string SqlQuery = "select * from OCRY where Code=@Code";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Code", countryCode);
                OCRY country = await DbContext.QueryFirstOrDefaultAsync<OCRY>(SqlQuery, parameters);
                return country;
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }
    }
}
