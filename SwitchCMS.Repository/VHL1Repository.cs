using Dapper;
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
    public class VHL1Repository : IVHL1Repository
    {
        private readonly IDapperContext DbContext;

        public VHL1Repository(IDapperContext _DbContext)
        {
            DbContext = _DbContext;
        }


        public async Task<bool> CreateHandover(VHL1 Model)
        {
            try
            {
                string SqlQuery = @"INSERT INTO [dbo].[VHL1]
                                   ([CreatedDate]
                                   ,[EmployeeName]
                                   ,[JobTitle]
                                   ,[Department]
                                   ,[EmploymentDate]
                                   ,[VehicleType]
                                   ,[VehicleModel]
                                   ,[PlateNumber]
                                   ,[ChasisNumber]
                                   ,[VehicleColor]
                                   ,[License]
                                   ,[IssueDate]
                                   ,[ExpiryDate]
                                   ,[InsurancePolicyNumber]
                                   ,[InsuranceDate]
                                   ,[Milage]
                                   ,[InteriorCondition]
                                   ,[ExteriorCondition]
                                   ,[Dameges]
                                   ,[TooolsAndSpareTire]
                                   ,[GeneralNotes]
                                   ,[FinanceDepartmentNotes]
                                   ,[HRDepartmentNotes]
                                   ,[CompanyID])
                                    VALUES
                                   (Getdate()
                                   ,@EmployeeName 
                                   ,@JobTitle
                                   ,@Department 
                                   ,@EmploymentDate 
                                   ,@VehicleType 
                                   ,@VehicleModel 
                                   ,@PlateNumber 
                                   ,@ChasisNumber 
                                   ,@VehicleColor 
                                   ,@License
                                   ,@IssueDate 
                                   ,@ExpiryDate 
                                   ,@InsurancePolicyNumber 
                                   ,@InsuranceDate 
                                   ,@Milage 
                                   ,@InteriorCondition 
                                   ,@ExteriorCondition 
                                   ,@Dameges 
                                   ,@TooolsAndSpareTire 
                                   ,@GeneralNotes 
                                   ,@FinanceDepartmentNotes 
                                   ,@HRDepartmentNotes 
                                   ,@CompanyID)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("EmployeeName", Model.EmployeeName);
                parameters.Add("JobTitle", Model.JobTitle);
                parameters.Add("Department", Model.Department);
                parameters.Add("EmploymentDate", Model.EmploymentDate);
                parameters.Add("VehicleType", Model.VehicleType);
                parameters.Add("VehicleModel", Model.VehicleModel);
                parameters.Add("PlateNumber", Model.PlateNumber);
                parameters.Add("ChasisNumber", Model.ChasisNumber);
                parameters.Add("VehicleColor", Model.VehicleColor);
                parameters.Add("License", Model.License);
                parameters.Add("IssueDate", Model.IssueDate);
                parameters.Add("ExpiryDate", Model.ExpiryDate);
                parameters.Add("InsurancePolicyNumber", Model.InsurancePolicyNumber);
                parameters.Add("InsuranceDate", Model.InsuranceDate);
                parameters.Add("Milage", Model.Milage);
                parameters.Add("InteriorCondition", Model.InteriorCondition);
                parameters.Add("ExteriorCondition", Model.ExteriorCondition);
                parameters.Add("Dameges", Model.Dameges);
                parameters.Add("TooolsAndSpareTire", Model.TooolsAndSpareTire);
                parameters.Add("GeneralNotes", Model.GeneralNotes);
                parameters.Add("FinanceDepartmentNotes", Model.FinanceDepartmentNotes);
                parameters.Add("HRDepartmentNotes", Model.HRDepartmentNotes);
                parameters.Add("CompanyID", Model.CompanyID);
                int Success = await DbContext.ExecuteAsync(SqlQuery, parameters);

                return Success > 0;

            }
            catch (Exception ex) 
            {
                throw ex;
            }
             
        }

        public async Task<bool> UpdateHandover(VHL1 Model)
        {
            string SqlQuery = @"UPDATE [dbo].[VHL1]
                               SET  
                                   [EmployeeName] = @EmployeeName 
                                  ,[JobTitle] = @JobTitle 
                                  ,[Department] = @Department 
                                  ,[EmploymentDate] = @EmploymentDate 
                                  ,[VehicleType] = @VehicleType 
                                  ,[VehicleModel] = @VehicleModel 
                                  ,[PlateNumber] = @PlateNumber 
                                  ,[ChasisNumber] = @ChasisNumber
                                  ,[VehicleColor] = @VehicleColor 
                                  ,[License] = @License
                                  ,[IssueDate] = @IssueDate 
                                  ,[ExpiryDate] = @ExpiryDate 
                                  ,[InsurancePolicyNumber] = @InsurancePolicyNumber 
                                  ,[InsuranceDate] = @InsuranceDate 
                                  ,[Milage] = @Milage 
                                  ,[InteriorCondition] = @InteriorCondition 
                                  ,[ExteriorCondition] = @ExteriorCondition 
                                  ,[Dameges] = @Dameges 
                                  ,[TooolsAndSpareTire] = @TooolsAndSpareTire 
                                  ,[GeneralNotes] = @GeneralNotes
                                  ,[FinanceDepartmentNotes] = @FinanceDepartmentNotes 
                                  ,[HRDepartmentNotes] = @HRDepartmentNotes 
                                  ,[CompanyID] = @CompanyID 
                             WHERE ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("EmployeeName", Model.EmployeeName);
            parameters.Add("JobTitle", Model.JobTitle);
            parameters.Add("Department", Model.Department);
            parameters.Add("EmploymentDate", Model.EmploymentDate);
            parameters.Add("VehicleType", Model.VehicleType);
            parameters.Add("VehicleModel", Model.VehicleModel);
            parameters.Add("PlateNumber", Model.PlateNumber);
            parameters.Add("ChasisNumber", Model.ChasisNumber);
            parameters.Add("VehicleColor", Model.VehicleColor);
            parameters.Add("License", Model.License);
            parameters.Add("IssueDate", Model.IssueDate);
            parameters.Add("ExpiryDate", Model.ExpiryDate);
            parameters.Add("InsurancePolicyNumber", Model.InsurancePolicyNumber);
            parameters.Add("InsuranceDate", Model.InsuranceDate);
            parameters.Add("Milage", Model.Milage);
            parameters.Add("InteriorCondition", Model.InteriorCondition);
            parameters.Add("ExteriorCondition", Model.ExteriorCondition);
            parameters.Add("Dameges", Model.Dameges);
            parameters.Add("TooolsAndSpareTire", Model.TooolsAndSpareTire);
            parameters.Add("GeneralNotes", Model.GeneralNotes);
            parameters.Add("FinanceDepartmentNotes", Model.FinanceDepartmentNotes);
            parameters.Add("HRDepartmentNotes", Model.HRDepartmentNotes);
            parameters.Add("CompanyID", Model.CompanyID);
            parameters.Add("ID", Model.ID);


            int Success = await DbContext.ExecuteAsync(SqlQuery, parameters);

            return Success > 0;
        }

        public async Task<List<VHL1>> GetHandoverByPageIndex(HandoverPagination Pagination)
        {
            int offset = Pagination.RowCount * Pagination.PageIndex;
            string SqlQuery = @"select * from VHL1
                                where CompanyID=@CompanyID
                                and (EmployeeName like @filterName or 
                                VehicleType like @filterName or VehicleModel like @filterName 
                                or PlateNumber like @filterName or ChasisNumber like @filterName)
                                ORDER BY VHL1.ID
                                OFFSET @offset ROWS FETCH NEXT @RowCount ROWS ONLY ";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("filterName", "%" + Pagination.FilterSearch + "%");
            parameters.Add("offset", offset);
            parameters.Add("RowCount", Pagination.RowCount);
            parameters.Add("CompanyID", Pagination.CompanyID);
            IEnumerable<VHL1> HandoverList = await DbContext.QueryAsync<VHL1>(SqlQuery, parameters);
            return HandoverList.ToList();
        }

        public async Task<int> GetHandoverCount(HandoverPagination Pagination)
        {
            string SqlQuery = @"select Count(ID) from VHL1
                                where CompanyID=@CompanyID
                                and (EmployeeName like @filterName or 
                                VehicleType like @filterName or VehicleModel like @filterName 
                                or PlateNumber like @filterName or ChasisNumber like @filterName)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("filterName", "%" + Pagination.FilterSearch + "%");
            parameters.Add("CompanyID", Pagination.CompanyID);
            int TotalCount = await DbContext.QuerySingleAsync<int>(SqlQuery, parameters);
            return TotalCount;
        }

        public async Task<VHL1> GetHandoverByID(int ID)
        {
            string SqlQuery = "select * from VHL1 where ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", ID);
            var Data = await DbContext.QueryFirstOrDefaultAsync<VHL1>(SqlQuery, parameters);
            return Data;
        }

        public async Task<bool> DeleteHandover(int ID)
        {
            string SqlQuery = "delete from VHL1 where ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", ID);
            int Success = await DbContext.ExecuteAsync(SqlQuery, parameters);
            return Success > 0;
        }
    }
}
