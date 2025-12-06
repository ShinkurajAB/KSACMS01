using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class HEM2Repository : IHEM2Repository
    {

        private readonly IDapperContext dbContext;
        public HEM2Repository(IDapperContext _dbContext) 
        { 
            this.dbContext = _dbContext;
        }

        public async Task<bool> CreateDirectNotification(HEM2 Model)
        {
            string SqlQuery = @"INSERT INTO [dbo].[HEM2]
                               ([EmployeeID]
                               ,[EmployeeName]
                               ,[JobNumber]
                               ,[Management]
                               ,[StartDate]
                               ,[CompanyID]
                               ,[CreatedDate])
                               VALUES
                               (@EmployeeID 
                               ,@EmployeeName 
                               ,@JobNumber 
                               ,@Management 
                               ,@StartDate 
                               ,@CompanyID 
                               ,@CreatedDate )";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("EmployeeID", Model.EmployeeID);
            parameters.Add("EmployeeName", Model.EmployeeName);
            parameters.Add("JobNumber", Model.JobNumber);
            parameters.Add("Management", Model.Management);
            parameters.Add("StartDate", Model.StartDate);
            parameters.Add("CompanyID", Model.CompanyID);
            parameters.Add("CreatedDate", Model.CreatedDate);
            int Success = await dbContext.ExecuteAsync(SqlQuery, parameters);

            return Success > 0;


             
        }

        public async Task<bool> UpdateDirectNotification(HEM2 Model)
        {
            string SqlQuery = @"UPDATE [dbo].[HEM2]
                               SET [EmployeeID] = @EmployeeID 
                                  ,[EmployeeName] = @EmployeeName 
                                  ,[JobNumber] = @JobNumber 
                                  ,[Management] = @Management 
                                  ,[StartDate] = @StartDate 
                                  ,[CompanyID] = @CompanyID                                     
                             WHERE  ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("EmployeeID", Model.EmployeeID);
            parameters.Add("EmployeeName", Model.EmployeeName);
            parameters.Add("JobNumber", Model.JobNumber);
            parameters.Add("Management", Model.Management);
            parameters.Add("StartDate", Model.StartDate);
            parameters.Add("CompanyID", Model.CompanyID);
            parameters.Add("ID", Model.ID);


            int Success = await dbContext.ExecuteAsync(SqlQuery, parameters);

            return Success > 0;
        }

        public async Task<List<HEM2>> GetDirectNotificationByPageIndex(DirectNotificationPagination Pagination)
        {
            int offset = Pagination.RowCount * Pagination.PageIndex;
            string SqlQuery = @"select HEM2.ID [DirectID] ,HEM2.*, OHEM.ID [EmpID] , OHEM.*, OCMP.ID [CmpID],OCMP.* from HEM2 inner join
                                OHEM on HEM2.EmployeeID=OHEM.ID inner join
                                OCMP on OCMP.ID=HEM2.CompanyID
                                where HEM2.CompanyID=@CompanyID
                                and (HEM2.EmployeeName like @EmployeeName)
                                ORDER BY HEM2.ID
                                OFFSET @offset ROWS FETCH NEXT @RowCount ROWS ONLY";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CompanyID", Pagination.CompanyID);
            parameters.Add("EmployeeName", "%" + Pagination.FilterSearch + "%");
            parameters.Add("offset", offset);
            parameters.Add("RowCount", Pagination.RowCount);
            Dictionary<int, HEM2> empDic = new();
            IEnumerable<HEM2> DirectHandoverList= await dbContext.QueryAsync<HEM2,OHEM,OCMP,HEM2>(SqlQuery,
                (hem2, ohem, ocmp) =>
                {

                    if(empDic.ContainsKey(hem2.ID))
                    {
                        if (ocmp != null)
                            hem2.Company = ocmp;
                        if (ohem != null)
                            hem2.Employee = ohem;

                        empDic[hem2.ID] = hem2;
                    }
                    else
                    {
                        if (ocmp != null)
                            hem2.Company = ocmp;
                        if(ohem != null)
                            hem2.Employee= ohem;
                        empDic.Add(hem2.ID, hem2);
                    }
                    return hem2;
                }
               , "DirectID,EmpID,CmpID ", parameters);

            return empDic.Values.ToList();             
        }

        public async Task<int> GetDirectNotificationCount(DirectNotificationPagination Pagination)
        {
            string SqlQuery = @"select Count(HEM2.ID) from HEM2 inner join
                                OHEM on HEM2.EmployeeID=OHEM.ID inner join
                                OCMP on OCMP.ID=HEM2.CompanyID
                                where HEM2.CompanyID=@CompanyID
                                and (HEM2.EmployeeName like @EmployeeName)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("filterName", "%" + Pagination.FilterSearch + "%");
            parameters.Add("CompanyID", Pagination.CompanyID);
            int TotalCount = await dbContext.QuerySingleAsync<int>(SqlQuery, parameters);
            return TotalCount;
            throw new NotImplementedException();
        }

        public async Task<HEM2> GetDirectNotificationByID(int ID)
        {
            string SqlQuery = @"select HEM2.ID [DirectID] ,HEM2.*, OHEM.ID [EmpID] , OHEM.*, OCMP.ID [CmpID],OCMP.* from HEM2 inner join
                                OHEM on HEM2.EmployeeID=OHEM.ID inner join
                                OCMP on OCMP.ID=HEM2.CompanyID
                                where HEM2.CompanyID=@CompanyID
                                and HEM2.ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", ID);
            Dictionary<int, HEM2> empDic = new();
            IEnumerable<HEM2> DirectHandoverList = await dbContext.QueryAsync<HEM2, OHEM, OCMP, HEM2>(SqlQuery,
               (hem2, ohem, ocmp) =>
               {

                   if (empDic.ContainsKey(hem2.ID))
                   {
                       if (ocmp != null)
                           hem2.Company = ocmp;
                       if (ohem != null)
                           hem2.Employee = ohem;

                       empDic[hem2.ID] = hem2;
                   }
                   else
                   {
                       if (ocmp != null)
                           hem2.Company = ocmp;
                       if (ohem != null)
                           hem2.Employee = ohem;
                       empDic.Add(hem2.ID, hem2);
                   }
                   return hem2;
               }
              , "DirectID,EmpID,CmpID ", parameters);


            return empDic.Values.FirstOrDefault()!;
        }

        public async Task<bool> DeleteDirectNotification(int ID)
        {
            string SqlQuery = "delete from HEM2 where ID=@ID";
            DynamicParameters parameters = new DynamicParameters(); 
            parameters.Add("ID", ID);
            int Success = await dbContext.ExecuteAsync(SqlQuery, parameters);

            return Success > 0;
             
        }
        
     
    }
}
