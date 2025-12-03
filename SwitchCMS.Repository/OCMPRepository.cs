using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SwitchCMS.DataBaseContext;
using SwitchCMS.DataBaseContext.Dapper;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using SwitchCMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SwitchCMS.Repository
{
    public class OCMPRepository : IOCMPRepository
    {
        private readonly IDapperContext DbContext;
        public OCMPRepository(IDapperContext _DbContext)
        {
            DbContext= _DbContext;
        }

        public async Task<bool> DeleteCompany(int companyId)
        {
            string Query = "Delete from OCMP where ID= @ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", companyId);
            var i = await DbContext.ExecuteAsync(Query, parameters);
            return i > 0;
        }

        public async Task<List<OCMP>> GetAllCompanys()
        {
            string SqlQuery = "select ID, Name from OCMP order by ID";           

            var CompanyList =await DbContext.QueryAsync<OCMP>(SqlQuery) ;
            return CompanyList.ToList();
            
        }


        public async Task<List<OCMP>> GetCompanyByPageIndex(CompanyPagination Pagination)
        {
            int offset = Pagination.RowCount * Pagination.PageIndex;
            string sqlQuery = @"SELECT c.*, cr.Code [CCode], cr.*
                                FROM OCMP c
                                LEFT JOIN OCRY cr ON c.CountryCode = cr.Code
                                where
                                (@CountryCode IS NULL OR @CountryCode = '' OR c.CountryCode = @CountryCode)                                
                                AND (@CompanyName IS NULL OR @CompanyName = '' OR c.Name LIKE '%' + @CompanyName + '%')
                                ORDER BY c.ID
                                OFFSET @offset ROWS FETCH NEXT @RowCount ROWS ONLY";
            DynamicParameters parameters= new DynamicParameters();
            parameters.Add("offset", offset);
            parameters.Add("RowCount", Pagination.RowCount);
            parameters.Add("CountryCode", Pagination.CountryCode);
            parameters.Add("CompanyName", Pagination.CompanyName);
            Dictionary<int, OCMP> companyDic = new();

            IEnumerable<OCMP> companies = await DbContext.QueryAsync<OCMP, OCRY, OCMP>(
                sqlQuery,
                (ocmp, ocry) =>
                {
                    if (companyDic.ContainsKey(ocmp.ID))
                    {
                        if (ocry != null)
                        {
                            ocmp.Country = ocry;
                            companyDic[ocmp.ID] = ocmp;
                        }
                    }
                    else
                    {
                        if (ocry != null)
                            ocmp.Country = ocry;

                        companyDic.Add(ocmp.ID, ocmp);
                    }

                    return ocmp;
                }, "ID,CCode", parameters
                
            );

            return companyDic.Values.ToList();
        }

       

        public async Task<int> GetTotalCompanyCount(CompanyPagination Pagination)
        {
            string SqlQuery = @"SELECT Count(ID) FROM OCMP c
                                LEFT JOIN OCRY cr ON c.CountryCode = cr.Code where
                                (@CountryCode IS NULL OR @CountryCode = '' OR c.CountryCode = @CountryCode)                                
                                AND (@CompanyName IS NULL OR @CompanyName = '' OR c.Name LIKE '%' + @CompanyName + '%')"; 
            DynamicParameters parameters=new DynamicParameters();
            parameters.Add("CountryCode", Pagination.CountryCode);
            parameters.Add("CompanyName", Pagination.CompanyName);
            int TotalCount = await DbContext.QuerySingleAsync<int>(SqlQuery,parameters);
            return TotalCount;
        }

        public async Task<bool> SignUpCompany(OCMP company)
        {
            try
            {
                string SqlQuery = @"INSERT INTO [dbo].[OCMP]
                                   ([Name]
                                   ,[Address]
                                   ,[Email]
                                   ,[PhoneNumber]
                                   ,[Status]
                                   ,[CountryCode],[ValidationDate])
                             VALUES
                                   (@Name
                                   ,@Address 
                                   ,@Email 
                                   ,@PhoneNumber 
                                   ,@Status 
                                   ,@CountryCode,@ValidationDate)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Name", company.Name);
                parameters.Add("Address", company.Address);
                parameters.Add("Email", company.Email);
                parameters.Add("PhoneNumber", company.PhoneNumber);
                parameters.Add("Status", company.Status.ToString());
                parameters.Add("CountryCode", company.CountryCode);
                parameters.Add("ValidationDate", company.ValidationDate);
                int Success = await DbContext.ExecuteAsync(SqlQuery, parameters);

                return Success>0;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateCompany(OCMP company)
        {
            try
            {
                string sqlQuery = "Update OCMP SET Name=@Name,Address=@Address,Email=@Email,PhoneNumber=@PhoneNumber,Status=@Status,CountryCode=@CountryCode,ValidationDate=@ValidationDate where ID=@ID";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Name", company.Name);
                parameters.Add("Address", company.Address);
                parameters.Add("Email", company.Email);
                parameters.Add("PhoneNumber", company.PhoneNumber);
                parameters.Add("Status", company.Status.ToString());
                parameters.Add("CountryCode", company.CountryCode);
                parameters.Add("ValidationDate", company.ValidationDate);
                parameters.Add("ID",company.ID);

                int Success = await DbContext.ExecuteAsync(sqlQuery, parameters);
                return Success > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateCompanyStatus(int companyId, string companyStatus)
        {
            try
            {
                string sqlQuery = "Update OCMP Set Status=@Status where ID=@ID";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Status", companyStatus);
                parameters.Add("ID", companyId);

                int Success = await DbContext.ExecuteAsync(sqlQuery, parameters);
                return Success > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }


        public async Task<bool> UpdateInActiveCustomerbyExpireDate(DateTime Date)
        {
            try
            {
                string SqlQuery = "update OCMP set Status='InActive' where ValidationDate<@ValidationDate and Status='Active'";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ValidationDate", Date.ToString("yyyy-MM-dd"));
                int Success = await DbContext.ExecuteAsync(SqlQuery, parameters);
                return Success > 0;
            }
            catch
            {
                return false;
            }
             
        }

        public async Task<OCMP> GetCustomerByCustID(int ID)
        {
            string sqlQuery = @"select * from OCMP where ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", ID);   

            var Data= await DbContext.QueryFirstOrDefaultAsync<OCMP>(sqlQuery, parameters);
            return Data;
             
        }
    }
}
