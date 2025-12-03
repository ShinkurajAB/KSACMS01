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
    public class ADV1Repository : IADV1Repository
    {

        private readonly IDapperContext dbContext;

        public ADV1Repository(IDapperContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> deleteAdvbyCustID(int AdvatisementID)
        {
            try
            {
                string SqlQuery = "delete from ADV1 where OADVID=@AdvatisementID";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("AdvatisementID", AdvatisementID);
                int Success = await dbContext.ExecuteAsync(SqlQuery, parameters);
                return Success > 0;
            }
            catch
            {
                return false;
            }
          
        }

        public async Task<bool> InsertAdvCust(ADV1 Modal)
        {
            string SqlQuery = @"INSERT INTO [dbo].[ADV1]
                               ([OADVID]
                               ,[CustomerID])
                                VALUES
                               (@OADVID 
                               ,@CustomerID )";
            DynamicParameters parameters=new DynamicParameters();
            parameters.Add("OADVID", Modal.OADVID);
            parameters.Add("CustomerID", Modal.CustomerID);
            int Success=await dbContext.ExecuteAsync(SqlQuery, parameters);
            return Success > 0;
             
        }
    }
}
