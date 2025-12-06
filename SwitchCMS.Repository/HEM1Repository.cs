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
    public class HEM1Repository:IHEM1Repository
    {
        private readonly IDapperContext DbContext;
        public HEM1Repository(IDapperContext _DbContext)
        {
            DbContext = _DbContext;
        }

        public async Task<bool> DeleteResignation(int id)
        {
            string Query = "Delete from HEM1 where ID= @ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", id);
            var i = await DbContext.ExecuteAsync(Query, parameters);
            return i > 0;
        }

        public async Task<HEM1> GetResignationById(int id)
        {
            string SqlQuery = @"select * from HEM1 where ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", id);
            var Data = await DbContext.QueryFirstOrDefaultAsync<HEM1>(SqlQuery, parameters);
            return Data;
        }

        public async Task<List<HEM1>> GetResignationsByCompanyId(EmployeeResignationPagination pagination)
        {
            int offset = pagination.RowCount * pagination.PageIndex;
            string SqlQuery = @"SELECT h.*, c.*, e.*
                                FROM HEM1 h
                                INNER JOIN OCMP c ON h.CompanyId = c.ID
                                INNER JOIN OHEM e ON h.EmployeeId = e.ID
                                WHERE h.CompanyId = @CompanyID
                                ORDER BY h.ID OFFSET @offset ROWS FETCH NEXT @RowCount ROWS ONLY";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CompanyID", pagination.CompanyId);
            parameters.Add("offset", offset);
            parameters.Add("RowCount", pagination.RowCount);
            Dictionary<int, HEM1> empDic = new();
            IEnumerable<HEM1> employees = await DbContext.QueryAsync<HEM1, OCMP, OHEM, HEM1>(
                                                     SqlQuery,
                                                    (hem1, ocmp,ohem) =>
                                                    {
                                                        if (empDic.ContainsKey(hem1.ID))
                                                        {
                                                            if (ocmp != null)
                                                            {
                                                                hem1.Company = ocmp;
                                                               
                                                            }
                                                            if( ohem != null)
                                                            {
                                                                hem1.Employee = ohem;
                                                            }
                                                            empDic[hem1.ID] = hem1;
                                                        }
                                                        else
                                                        {
                                                            if (ocmp != null)
                                                                hem1.Company = ocmp;
                                                            if(ohem != null)
                                                                hem1.Employee = ohem;

                                                            empDic.Add(hem1.ID, hem1);
                                                        }

                                                        return hem1;
                                                    },
                                                    "ID", parameters
                                                    );

            return empDic.Values.ToList();
        }

        public async Task<int> GetTotalResignationCount(int companyId)
        {
            string SqlQuery = @"SELECT Count(ID) FROM HEM1 Where CompanyId=@CompanyId";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CompanyId", companyId);
            int TotalCount = await DbContext.QuerySingleAsync<int>(SqlQuery, parameters);
            return TotalCount;
        }

        public async Task<bool> InsertResignation(HEM1 modal)
        {
            string sqlQuery = @"INSERT INTO HEM1 (EmployeeId, CompanyId, SubmissionDate, EmployeeName,LastWorkingDay, CreatedDate)
                                VALUES (@EmployeeId, @CompanyId, @SubmissionDate, @EmployeeName, @LastWorkingDay, GETDATE())";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("EmployeeId", modal.EmployeeId);
            parameters.Add("CompanyId", modal.CompanyId);
            parameters.Add("SubmissionDate", modal.SubmissionDate!.Value.Date);
            parameters.Add("EmployeeName", modal.EmployeeName);            
            parameters.Add("LastWorkingDay", modal.LastWorkingDay!.Value.Date);
            var i = await DbContext.ExecuteAsync(sqlQuery, parameters);
            return i > 0;
        }

        public async Task<bool> UpdateResignation(HEM1 modal)
        {
            string sqlQuery = @"UPDATE HEM1 
                                SET EmployeeId = @EmployeeId, 
                                    CompanyId = @CompanyId, 
                                    SubmissionDate = @SubmissionDate, 
                                    EmployeeName = @EmployeeName,                                      
                                    LastWorkingDay = @LastWorkingDay
                                WHERE ID = @ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", modal.ID);
            parameters.Add("EmployeeId", modal.EmployeeId);
            parameters.Add("CompanyId", modal.CompanyId);
            parameters.Add("SubmissionDate", modal.SubmissionDate);
            parameters.Add("EmployeeName", modal.EmployeeName);          
            parameters.Add("LastWorkingDay", modal.LastWorkingDay);
            var i = await DbContext.ExecuteAsync(sqlQuery, parameters);
            return i > 0;
        }
    }
}
