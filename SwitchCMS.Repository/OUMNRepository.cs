using Dapper;
using SwitchCMS.DataBaseContext.Dapper;
using SwitchCMS.Model;
using SwitchCMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository
{
    public class OUMNRepository : IOUMNRepository
    {
        private readonly IDapperContext DbContext;

        public OUMNRepository(IDapperContext _dbContext)
        {
            DbContext = _dbContext;
        }

        public async Task<int> CreateUserMenus(List<OUMN> UserMenu)
        {
            string SqlQuery = @"insert into OUMN(UserID, MenuID)
                                values(@UserID, @MenuID)";
            List<DynamicParameters> parameterList= new List<DynamicParameters>();
            foreach (OUMN o in UserMenu) 
            { 
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("UserID", o.UserID);
                parameters.Add("MenuID", o.MenuID);
                parameterList.Add(parameters);
            }

            int Success=await DbContext.ExecuteBulkAsync<int>(SqlQuery, parameterList); 
            return Success;
        }

        public async Task<int> DeleteSubMenuByUserID(int UserID)
        {
            try
            {
                string SqlQuery = "delete from OUMN where UserID=@UserID";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("UserID", UserID);
                int Success = await DbContext.ExecuteAsync(SqlQuery, parameters);
                return Success;
            }
            catch (Exception ex) 
            {
                return 0;
            }
             
        }
    }
}
