using Dapper;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SwitchCMS.DataBaseContext.Dapper;
using SwitchCMS.Model;
using SwitchCMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository
{
    public class OACARepository:IOACARepository
    {
        private readonly IDapperContext DbContext;
        public OACARepository(IDapperContext _DbContext)
        {
            DbContext = _DbContext;
        }

        public async Task<bool> DeleteAccessAccountManager(int accountId)
        {
            string Query = "Delete from OACA where ID= @ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", accountId);
            var i = await DbContext.ExecuteAsync(Query, parameters);
            return i > 0;
        }

        public async Task<List<OACA>> GetAccessAccountManagersByCompanyId(int companyId)
        {
            string SqlQuery = @"SELECT a.*, c.*
                                FROM OACA a
                                INNER JOIN OCMP c ON a.CompanyID = c.ID
                                WHERE a.CompanyID = @CompanyID
                                ORDER BY a.ID;";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CompanyID", companyId);
            Dictionary<int, OACA> accessAccountDic = new();
            IEnumerable<OACA> accessAccounts = await DbContext.QueryAsync<OACA, OCMP, OACA>(
                                                     SqlQuery,
                                                    (oaca, ocmp) =>
                                                    {
                                                        if (accessAccountDic.ContainsKey(oaca.ID))
                                                        {
                                                            if (ocmp != null)
                                                            {
                                                                oaca.Company = ocmp;
                                                                accessAccountDic[oaca.ID] = oaca;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (ocmp != null)
                                                                oaca.Company = ocmp;

                                                               accessAccountDic.Add(oaca.ID, oaca);
                                                        }

                                                        return oaca;
                                                    },
                                                    "ID", parameters
                                                    );

            return accessAccountDic.Values.ToList();
        }

        public async Task<List<OACA>> GetAllAccessAccountManagers()
        {
            string SqlQuery = "select * from OACA order by ID";
            DynamicParameters parameters = new DynamicParameters();
            Dictionary<int, OACA> accessAccountDic = new();
            IEnumerable<OACA> accessAccounts = await DbContext.QueryAsync<OACA, OCMP, OACA>(
                                                     SqlQuery,
                                                    (oaca, ocmp) =>
                                                    {
                                                        if (accessAccountDic.ContainsKey(oaca.ID))
                                                        {
                                                            if (ocmp != null)
                                                            {
                                                                oaca.Company = ocmp;
                                                                accessAccountDic[oaca.ID] = oaca;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (ocmp != null)
                                                                oaca.Company = ocmp;

                                                            accessAccountDic.Add(oaca.ID, oaca);
                                                        }

                                                        return oaca;
                                                    },
                                                    "ID", parameters
                                                    );

            return accessAccountDic.Values.ToList();
        }

        public async Task<bool> InsertAccessAccountManager(OACA modal)
        {
            string sqlQuery = @"INSERT INTO [dbo].[OACA]
                                       ([CompanyID]
                                       ,[Platform]
                                       ,[URL]
                                       ,[UserName]
                                       ,[Password]
                                       ,[Remark]
                                       ,[CreatedBy]
                                       ,[LastUpdatedDate])
                                VALUES
                                        (@CompanyID,
                                         @Platform,
                                         @URL,
                                         @UserName,
                                         @Password,
                                         @Remark,
                                         @CreatedBy,
                                         GETDATE())";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CompanyID",modal.CompanyID);
            parameters.Add("Platform", modal.Platform);
            parameters.Add("URL", modal.URL);
            parameters.Add("UserName", modal.UserName);
            parameters.Add("Password", modal.Password);
            parameters.Add("Remark", modal.Remark);
            parameters.Add("CreatedBy", modal.CreatedBy);
            int Success = await DbContext.ExecuteAsync(sqlQuery, parameters);
            return Success > 0;
        }

        public async Task<bool> UpdateAccessAccountManager(OACA modal)
        {
            string sqlQuery = @"UPDATE OACA SET CompanyID=@CompanyID,Platform=@Platform,URL=@URL,UserName=@UserName,
                                Password=@Password,Remark=@Remark,CreatedBy=@CreatedBy,LastUpdatedDate=GETDATE() where ID=@ID ";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CompanyID", modal.CompanyID);
            parameters.Add("Platform", modal.Platform);
            parameters.Add("URL", modal.URL);
            parameters.Add("UserName", modal.UserName);
            parameters.Add("Password", modal.Password);
            parameters.Add("Remark", modal.Remark);
            parameters.Add("CreatedBy", modal.CreatedBy);
            parameters.Add("ID", modal.ID);
            int Success = await DbContext.ExecuteAsync(sqlQuery, parameters);
            return Success > 0;
        }
    }
}
