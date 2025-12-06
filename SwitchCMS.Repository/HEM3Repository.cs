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
    public class HEM3Repository:IHEM3Repository
    {
        private readonly IDapperContext DbContext;
        public HEM3Repository(IDapperContext _DbContext)
        {
            DbContext = _DbContext;
        }

        public async Task<bool> DeleteAbsentee(int id)
        {
            string Query = "Delete from HEM3 where ID= @ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", id);
            var i = await DbContext.ExecuteAsync(Query, parameters);
            return i > 0;
        }

        public async Task<HEM3> GetAbsenteeById(int id)
        {
            string SqlQuery = @"select * from HEM3 where ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", id);
            var Data = await DbContext.QueryFirstOrDefaultAsync<HEM3>(SqlQuery, parameters);
            return Data;
        }

        public async Task<List<HEM3>> GetAbsenteesByCompanyId(EmployeeAbsencePagination pagination)
        {
            int offset = pagination.RowCount * pagination.PageIndex;
            string SqlQuery = @"SELECT h.*, c.*, e.*
                                FROM HEM3 h
                                INNER JOIN OCMP c ON h.CompanyID = c.ID
                                INNER JOIN OHEM e ON h.EmployeeID = e.ID
                                WHERE h.CompanyID = @CompanyID
                                ORDER BY h.ID OFFSET @offset ROWS FETCH NEXT @RowCount ROWS ONLY";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CompanyID", pagination.CompanyId);
            parameters.Add("offset", offset);
            parameters.Add("RowCount", pagination.RowCount);
            Dictionary<int, HEM3> empDic = new();
            IEnumerable<HEM3> employees = await DbContext.QueryAsync<HEM3, OCMP, OHEM, HEM3>(
                                                     SqlQuery,
                                                    (hem3, ocmp, ohem) =>
                                                    {
                                                        if (empDic.ContainsKey(hem3.ID))
                                                        {
                                                            if (ocmp != null)
                                                            {
                                                                hem3.Company = ocmp;

                                                            }
                                                            if (ohem != null)
                                                            {
                                                                hem3.Employee = ohem;
                                                            }
                                                            empDic[hem3.ID] = hem3;
                                                        }
                                                        else
                                                        {
                                                            if (ocmp != null)
                                                                hem3.Company = ocmp;
                                                            if (ohem != null)
                                                                hem3.Employee = ohem;

                                                            empDic.Add(hem3.ID, hem3);
                                                        }

                                                        return hem3;
                                                    },
                                                    "ID", parameters
                                                    );

            return empDic.Values.ToList();
        }

        public async Task<int> GetTotalAbsenteeCount(int companyId)
        {
            string SqlQuery = @"SELECT Count(ID) FROM HEM3 Where CompanyID=@CompanyId";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CompanyId", companyId);
            int TotalCount = await DbContext.QuerySingleAsync<int>(SqlQuery, parameters);
            return TotalCount;
        }

        public async Task<bool> InsertAbsentee(HEM3 modal)
        {
            string sqlQuery = @"INSERT INTO HEM3 (EmployeeID, EmployeeName, AbsenceDate, Notes,CompanyID, CreatedDate)
                                VALUES (@EmployeeID, @EmployeeName, @AbsenceDate, @Notes, @CompanyID, GETDATE())";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("EmployeeID", modal.EmployeeID);
            parameters.Add("EmployeeName", modal.EmployeeName);
            parameters.Add("AbsenceDate", modal.AbsenceDate!.Value.Date);
            parameters.Add("Notes", modal.Notes);
            parameters.Add("CompanyID", modal.CompanyID);
            var i = await DbContext.ExecuteAsync(sqlQuery, parameters);
            return i > 0;
        }

        public async Task<bool> UpdateAbsentee(HEM3 modal)
        {
            string sqlQuery = @"UPDATE HEM3 
                                SET EmployeeID = @EmployeeID, 
                                    EmployeeName = @EmployeeName, 
                                    AbsenceDate = @AbsenceDate, 
                                    Notes = @Notes,                                      
                                    CompanyID = @CompanyID
                                WHERE ID = @ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", modal.ID);
            parameters.Add("EmployeeID", modal.EmployeeID);
            parameters.Add("EmployeeName", modal.EmployeeName);
            parameters.Add("AbsenceDate", modal.AbsenceDate!.Value.Date);
            parameters.Add("Notes", modal.Notes);
            parameters.Add("CompanyID", modal.CompanyID);
            var i = await DbContext.ExecuteAsync(sqlQuery, parameters);
            return i > 0;
        }
    }
}
