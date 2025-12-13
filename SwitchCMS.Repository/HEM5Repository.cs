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
    public class HEM5Repository:IHEM5Repository
    {
        private readonly IDapperContext dbContext;
        public HEM5Repository(IDapperContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task<bool> CreateGeneralWarning(HEM5 Model)
        {
            string SqlQuery = @"INSERT INTO HEM5 (EmployeeID, CompanyID, EmployeeName, Violation, AdditionalDetails, CreatedDate)
                                VALUES (@EmployeeID, @CompanyID, @EmployeeName, @Violation, @AdditionalDetails, GETDATE())";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@EmployeeID", Model.EmployeeID);
            parameters.Add("@CompanyID", Model.CompanyID);
            parameters.Add("@EmployeeName", Model.EmployeeName);
            parameters.Add("@Violation", Model.Violation);
            parameters.Add("@AdditionalDetails", Model.AdditionalDetails);
            var result = await dbContext.ExecuteAsync(SqlQuery, parameters);
            return result > 0;
        }

        public async Task<bool> DeleteGeneralWarning(int ID)
        {
            string SqlQuery = "delete from HEM5 where ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", ID);
            int Success = await dbContext.ExecuteAsync(SqlQuery, parameters);
            return Success > 0;
        }

        public async Task<HEM5> GetGeneralWarningByID(int ID)
        {
            string SqlQuery = @"select HEM5.ID [WarnID] ,HEM5.*, OHEM.ID [EmpID] , OHEM.*, OCMP.ID [CmpID],OCMP.* from HEM5 inner join
                                OHEM on HEM5.EmployeeID=OHEM.ID inner join
                                OCMP on OCMP.ID=HEM5.CompanyID
                                where  HEM5.ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", ID);
            Dictionary<int, HEM5> empDic = new();
            IEnumerable<HEM5> WarningList = await dbContext.QueryAsync<HEM5, OHEM, OCMP, HEM5>(SqlQuery,
               (hem5, ohem, ocmp) =>
               {

                   if (empDic.ContainsKey(hem5.ID))
                   {
                       if (ocmp != null)
                           hem5.Company = ocmp;
                       if (ohem != null)
                           hem5.Employee = ohem;

                       empDic[hem5.ID] = hem5;
                   }
                   else
                   {
                       if (ocmp != null)
                           hem5.Company = ocmp;
                       if (ohem != null)
                           hem5.Employee = ohem;
                       empDic.Add(hem5.ID, hem5);
                   }
                   return hem5;
               }
              , "WarnID,EmpID,CmpID ", parameters);


            return empDic.Values.FirstOrDefault()!;
        }

        public async Task<List<HEM5>> GetGeneralWarningByPageIndex(GeneralWarningPagination Pagination)
        {
            int offset = Pagination.RowCount * Pagination.PageIndex;
            string SqlQuery = @"select HEM5.ID [WarnID] ,HEM5.*, OHEM.ID [EmpID] , OHEM.*, OCMP.ID [CmpID],OCMP.* from HEM5 inner join
                                OHEM on HEM5.EmployeeID=OHEM.ID inner join
                                OCMP on OCMP.ID=HEM5.CompanyID
                                where HEM5.CompanyID=@CompanyID                                
                                ORDER BY HEM5.ID
                                OFFSET @offset ROWS FETCH NEXT @RowCount ROWS ONLY";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CompanyID", Pagination.CompanyID);          
            parameters.Add("offset", offset);
            parameters.Add("RowCount", Pagination.RowCount);
            Dictionary<int, HEM5> empDic = new();
            IEnumerable<HEM5>GeneralWarningList = await dbContext.QueryAsync<HEM5, OHEM, OCMP, HEM5>(SqlQuery,
                (hem5, ohem, ocmp) =>
                {

                    if (empDic.ContainsKey(hem5.ID))
                    {
                        if (ocmp != null)
                            hem5.Company = ocmp;
                        if (ohem != null)
                            hem5.Employee = ohem;

                        empDic[hem5.ID] = hem5;
                    }
                    else
                    {
                        if (ocmp != null)
                            hem5.Company = ocmp;
                        if (ohem != null)
                            hem5.Employee = ohem;
                        empDic.Add(hem5.ID, hem5);
                    }
                    return hem5;
                }
               , "WarnID,EmpID,CmpID ", parameters);

            return empDic.Values.ToList();
        }

        public async Task<int> GetGeneralWarningCount(GeneralWarningPagination Pagination)
        {
            string SqlQuery = @"select Count(HEM5.ID) from HEM5 inner join
                                OHEM on HEM5.EmployeeID=OHEM.ID inner join
                                OCMP on OCMP.ID=HEM5.CompanyID
                                where HEM5.CompanyID=@CompanyID";
            DynamicParameters parameters = new DynamicParameters();           
            parameters.Add("CompanyID", Pagination.CompanyID);
            int TotalCount = await dbContext.QuerySingleAsync<int>(SqlQuery, parameters);
            return TotalCount;
            
        }

        public async Task<bool> UpdateGeneralWarning(HEM5 Model)
        {
            string sqlQuery = @"UPDATE HEM5 SET 
                                EmployeeID=@EmployeeID,
                                CompanyID=@CompanyID,
                                EmployeeName=@EmployeeName,
                                Violation=@Violation,
                                AdditionalDetails=@AdditionalDetails
                                WHERE ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", Model.ID);
            parameters.Add("EmployeeID", Model.EmployeeID);
            parameters.Add("CompanyID", Model.CompanyID);
            parameters.Add("EmployeeName", Model.EmployeeName);
            parameters.Add("Violation", Model.Violation);
            parameters.Add("AdditionalDetails", Model.AdditionalDetails);
            var result = await dbContext.ExecuteAsync(sqlQuery, parameters);
            return result > 0;
        }
    }
}
