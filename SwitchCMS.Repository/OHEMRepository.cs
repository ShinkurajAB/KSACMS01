using Dapper;
using Microsoft.Identity.Client;
using SwitchCMS.DataBaseContext.Dapper;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using SwitchCMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository
{
    public class OHEMRepository:IOHEMRepository
    {
        private readonly IDapperContext DbContext;
        public OHEMRepository(IDapperContext _DbContext)
        {
            DbContext = _DbContext;
        }

        public async Task<bool> DeleteEmployee(int empId)
        {
            string Query = "Delete from OHEM where ID= @ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", empId);
            var i = await DbContext.ExecuteAsync(Query, parameters);
            return i > 0;
        }
       
        public async Task<List<OHEM>> GetAllEmployeesByCompanyId(EmployeePagination pagination)
        {
            int offset = pagination.RowCount * pagination.PageIndex;
            string SqlQuery = @"SELECT h.*, c.*
                                FROM OHEM h
                                INNER JOIN OCMP c ON h.CompanyID = c.ID
                                WHERE h.CompanyID = @CompanyID
                                ORDER BY h.ID OFFSET @offset ROWS FETCH NEXT @RowCount ROWS ONLY";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CompanyID", pagination.CompanyId);
            parameters.Add("offset", offset);
            parameters.Add("RowCount", pagination.RowCount);
            Dictionary<int, OHEM> empDic = new();
            IEnumerable<OHEM> employees = await DbContext.QueryAsync<OHEM, OCMP, OHEM>(
                                                     SqlQuery,
                                                    (ohem, ocmp) =>
                                                    {
                                                        if (empDic.ContainsKey(ohem.ID))
                                                        {
                                                            if (ocmp != null)
                                                            {
                                                                ohem.Company = ocmp;
                                                                empDic[ohem.ID] = ohem;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (ocmp != null)
                                                                ohem.Company = ocmp;

                                                            empDic.Add(ohem.ID, ohem);
                                                        }

                                                        return ohem;
                                                    },
                                                    "ID", parameters
                                                    );

            return empDic.Values.ToList();
        }

        public async Task<List<OHEM>> GetAllEmployessByCompany(int companyId)
        {
            string SqlQuery = "select * from OHEM WHERE CompanyID=@CompanyID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CompanyID", companyId);
            var Data = await DbContext.QueryAsync<OHEM>(SqlQuery, parameters);
            return Data.ToList();
        }

        public async Task<List<OHEM>> GetEmployeesByEmail(string email)
        {
            string SqlQuery = "select * from OHEM WHERE Email=@Email";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Email", email);
            var Data = await DbContext.QueryAsync<OHEM>(SqlQuery,parameters);
            return Data.ToList();
        }

        public async Task<List<OHEM>> GetEmployeesByIqamaId(string iqamaId)
        {
            string SqlQuery = "select * from OHEM WHERE IqamaID=@IqamaID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("IqamaID", iqamaId);
            var Data = await DbContext.QueryAsync<OHEM>(SqlQuery, parameters);
            return Data.ToList();
        }

        public async Task<int> GetTotalEmployeeCount(int companyId)
        {
            string SqlQuery = @"SELECT Count(ID) FROM OHEM Where CompanyID=@CompanyID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CompanyID", companyId);
            int TotalCount = await DbContext.QuerySingleAsync<int>(SqlQuery, parameters);
            return TotalCount;
        }

        public async Task<bool> InsertEmployee(OHEM modal)
        {
           
            string sqlQuery = @"INSERT INTO [dbo].[OHEM]
                                            ([Name]
                                            ,[JobTitle]
                                            ,[ProjectName]
                                            ,[Email]
                                            ,[PassportNumber]
                                            ,[DOB]
                                            ,[MobileNumber]
                                            ,[Gender]
                                            ,[Nationality]
                                            ,[IqamaID]
                                            ,[EmployeeStatus]
                                            ,[ContractType]
                                            ,[Sponsor]
                                            ,[NationalUnifiedNumber]
                                            ,[JoiningDate]
                                            ,[BankAccount]
                                            ,[ContractExpiry]
                                            ,[ContractStatus]
                                            ,[CompanyID]
                                            ,[IqamaExpiry]
                                            ,[InsuranceCompanyName]
                                            ,[InsuranceStartDate]
                                            ,[InsuranceExpiryDate]
                                            ,[InsuranceStatus]
                                            ,[CreatedBy])
                                    VALUES
                                            (@Name
                                            ,@JobTitle
                                            ,@ProjectName
                                           ,@Email
                                           ,@PassportNumber
                                           ,@DOB
                                           ,@MobileNumber
                                           ,@Gender
                                           ,@Nationality
                                           ,@IqamaID
                                           ,@EmployeeStatus
                                           ,@ContractType
                                           ,@Sponsor
                                           ,@NationalUnifiedNumber
                                           ,@JoiningDate
                                           ,@BankAccount
                                           ,@ContractExpiry
                                           ,@ContractStatus
                                           ,@CompanyID
                                           ,@IqamaExpiry
                                           ,@InsuranceCompanyName
                                           ,@InsuranceStartDate
                                           ,@InsuranceExpiryDate
                                           ,@InsuranceStatus
                                           ,@CreatedBy)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Name",modal.Name);
            parameters.Add("JobTitle", modal.JobTitle);
            parameters.Add("ProjectName", modal.ProjectName);
            parameters.Add("Email", modal.Email);
            parameters.Add("PassportNumber", modal.PassportNumber);
            parameters.Add("DOB", modal.DOB);
            parameters.Add("MobileNumber", modal.MobileNumber);
            parameters.Add("Gender", modal.Gender.ToString());
            parameters.Add("Nationality", modal.Nationality);
            parameters.Add("IqamaID", modal.IqamaID);
            parameters.Add("EmployeeStatus", modal.EmployeeStatus.ToString());
            parameters.Add("ContractType", modal.ContractType.ToString());
            parameters.Add("Sponsor", modal.Sponsor);
            parameters.Add("NationalUnifiedNumber", modal.NationalUnifiedNumber);
            parameters.Add("JoiningDate", modal.JoiningDate);
            parameters.Add("BankAccount", modal.BankAccount);
            parameters.Add("ContractExpiry", modal.ContractExpiry);
            parameters.Add("ContractStatus", modal.ContractStatus.ToString());
            parameters.Add("CompanyID", modal.CompanyID);
            parameters.Add("IqamaExpiry", modal.IqamaExpiry);
            parameters.Add("InsuranceCompanyName", modal.InsuranceCompanyName);
            parameters.Add("InsuranceStartDate", modal.InsuranceStartDate);
            parameters.Add("InsuranceExpiryDate", modal.InsuranceExpiryDate);
            parameters.Add("InsuranceStatus", modal.InsuranceStatus.ToString());
            parameters.Add("CreatedBy", modal.CreatedBy);
            int Success = await DbContext.ExecuteAsync(sqlQuery, parameters);
            return Success > 0;

        }

        public async Task<bool> UpdateEmployee(OHEM modal)
        {
            try
            {

                string sqlQuery = @"UPDATE OHEM
                                SET Name=@Name
                                   ,JobTitle=@JobTitle
                                  ,ProjectName = @ProjectName
                                  ,Email = @Email
                                  ,PassportNumber= @PassportNumber
                                  ,DOB = @DOB
                                  ,MobileNumber = @MobileNumber
                                  ,Gender = @Gender
                                  ,Nationality = @Nationality
                                  ,IqamaID = @IqamaID
                                  ,EmployeeStatus = @EmployeeStatus
                                  ,ContractType = @ContractType
                                  ,Sponsor = @Sponsor
                                  ,NationalUnifiedNumber = @NationalUnifiedNumber
                                  ,JoiningDate = @JoiningDate
                                  ,BankAccount = @BankAccount
                                  ,ContractExpiry = @ContractExpiry
                                  ,ContractStatus = @ContractStatus
                                  ,CompanyID = @CompanyID
                                  ,IqamaExpiry = @IqamaExpiry
                                  ,InsuranceCompanyName = @InsuranceCompanyName
                                  ,InsuranceStartDate = @InsuranceStartDate
                                  ,InsuranceExpiryDate = @InsuranceExpiryDate
                                  ,InsuranceStatus = @InsuranceStatus
                                  ,CreatedBy = @CreatedBy
                                   WHERE ID=@ID";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Name", modal.Name);
                parameters.Add("JobTitle", modal.JobTitle);
                parameters.Add("ProjectName", modal.ProjectName);
                parameters.Add("Email", modal.Email);
                parameters.Add("PassportNumber", modal.PassportNumber);
                parameters.Add("DOB", modal.DOB);
                parameters.Add("MobileNumber", modal.MobileNumber);
                parameters.Add("Gender", modal.Gender.ToString());
                parameters.Add("Nationality", modal.Nationality);
                parameters.Add("IqamaID", modal.IqamaID);
                parameters.Add("EmployeeStatus", modal.EmployeeStatus.ToString());
                parameters.Add("ContractType", modal.ContractType.ToString());
                parameters.Add("Sponsor", modal.Sponsor);
                parameters.Add("NationalUnifiedNumber", modal.NationalUnifiedNumber);
                parameters.Add("JoiningDate", modal.JoiningDate);
                parameters.Add("BankAccount", modal.BankAccount);
                parameters.Add("ContractExpiry", modal.ContractExpiry);
                parameters.Add("ContractStatus", modal.ContractStatus.ToString());
                parameters.Add("CompanyID", modal.CompanyID);
                parameters.Add("IqamaExpiry", modal.IqamaExpiry);
                parameters.Add("InsuranceCompanyName", modal.InsuranceCompanyName);
                parameters.Add("InsuranceStartDate", modal.InsuranceStartDate);
                parameters.Add("InsuranceExpiryDate", modal.InsuranceExpiryDate);
                parameters.Add("InsuranceStatus", modal.InsuranceStatus.ToString());
                parameters.Add("CreatedBy", modal.CreatedBy);
                parameters.Add("ID", modal.ID);
                int Success = await DbContext.ExecuteAsync(sqlQuery, parameters);
                return Success > 0;
            }
            catch(Exception ex)
            {
                               throw;
            }
        }

        public async Task<bool> UpdateEmployeeStatus(int empId, bool status)
        {
            string sqlQuery = @"UPDATE OHEM
                                SET EmployeeStatus=@EmployeeStatus
                                   WHERE ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("EmployeeStatus", status);
            parameters.Add("ID", empId);
            int Success = await DbContext.ExecuteAsync(sqlQuery, parameters);
            return Success > 0;
        }
    }
}
