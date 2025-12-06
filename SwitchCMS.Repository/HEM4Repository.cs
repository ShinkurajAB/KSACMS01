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
    public class HEM4Repository:IHEM4Repository
    {
        private readonly IDapperContext DbContext;
        public HEM4Repository(IDapperContext _DbContext)
        {
            DbContext = _DbContext;
        }

        public async Task<bool> DeleteOfferLetter(int id)
        {
            string Query = "Delete from HEM4 where ID= @ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", id);
            var i = await DbContext.ExecuteAsync(Query, parameters);
            return i > 0;
        }

        public async Task<List<HEM4>> GetOfferLetterByCompanyId(OfferLetterPagination pagination)
        {
            int offset = pagination.RowCount * pagination.PageIndex;
            string SqlQuery = @"SELECT h.*, c.*
                                FROM HEM4 h
                                INNER JOIN OCMP c ON h.CompanyID = c.ID                                
                                WHERE h.CompanyID = @CompanyID
                                ORDER BY h.ID OFFSET @offset ROWS FETCH NEXT @RowCount ROWS ONLY";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CompanyID", pagination.CompanyId);
            parameters.Add("offset", offset);
            parameters.Add("RowCount", pagination.RowCount);
            Dictionary<int, HEM4> empDic = new();
            IEnumerable<HEM4> employees = await DbContext.QueryAsync<HEM4, OCMP, HEM4>(
                                                     SqlQuery,
                                                    (hem4, ocmp) =>
                                                    {
                                                        if (empDic.ContainsKey(hem4.ID))
                                                        {
                                                            if (ocmp != null)
                                                            {
                                                                hem4.Company = ocmp;

                                                            }
                                                           
                                                            empDic[hem4.ID] = hem4;
                                                        }
                                                        else
                                                        {
                                                            if (ocmp != null)
                                                                hem4.Company = ocmp;
                                                           

                                                            empDic.Add(hem4.ID, hem4);
                                                        }

                                                        return hem4;
                                                    },
                                                    "ID", parameters
                                                    );

            return empDic.Values.ToList();
        }

        public async Task<HEM4> GetOfferLetterById(int id)
        {
            string SqlQuery = @"select * from HEM4 where ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", id);
            var Data = await DbContext.QueryFirstOrDefaultAsync<HEM4>(SqlQuery, parameters);
            return Data;
        }

        public async Task<int> GetTotalOfferLetterCount(int companyId)
        {
            string SqlQuery = @"SELECT Count(ID) FROM HEM4 Where CompanyID=@CompanyId";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CompanyId", companyId);
            int TotalCount = await DbContext.QuerySingleAsync<int>(SqlQuery, parameters);
            return TotalCount;
        }

        public async Task<bool> InsertOfferLetter(HEM4 modal)
        {
            string sqlQuery = @"INSERT INTO HEM4 (CompanyID, Name, JobTitle, Department,WorkLocation,StartDate,TotalMonthlySalary,HousingAllowance,TransportationAllowance,OtherAllowance, CreatedDate)
                                VALUES (@CompanyID, @Name, @JobTitle, @Department, @WorkLocation,@StartDate,@TotalMonthlySalary,@HousingAllowance,@TransportationAllowance,@OtherAllowance, GETDATE())";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CompanyID", modal.CompanyID);
            parameters.Add("Name", modal.Name);
            parameters.Add("JobTitle", modal.JobTitle);
            parameters.Add("Department", modal.Department);
            parameters.Add("WorkLocation", modal.WorkLocation);
            parameters.Add("StartDate", modal.StartDate!.Value.Date);
            parameters.Add("TotalMonthlySalary", modal.TotalMonthlySalary);
            parameters.Add("HousingAllowance", modal.HousingAllowance);
            parameters.Add("TransportationAllowance", modal.TransportationAllowance);
            parameters.Add("OtherAllowance", modal.OtherAllowance);
            var i = await DbContext.ExecuteAsync(sqlQuery, parameters);
            return i > 0;
        }

        public async Task<bool> UpdateOfferLetter(HEM4 modal)
        {
            string sqlQuery = @"UPDATE HEM4 SET CompanyID=@CompanyID, Name=@Name, JobTitle=@JobTitle, Department=@Department,
                                               WorkLocation=@WorkLocation,StartDate=@StartDate,TotalMonthlySalary=@TotalMonthlySalary,
                                               HousingAllowance=@HousingAllowance,TransportationAllowance=@TransportationAllowance,OtherAllowance=@OtherAllowance WHERE ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CompanyID", modal.CompanyID);
            parameters.Add("Name", modal.Name);
            parameters.Add("JobTitle", modal.JobTitle);
            parameters.Add("Department", modal.Department);
            parameters.Add("WorkLocation", modal.WorkLocation);
            parameters.Add("StartDate", modal.StartDate!.Value.Date);
            parameters.Add("TotalMonthlySalary", modal.TotalMonthlySalary);
            parameters.Add("HousingAllowance", modal.HousingAllowance);
            parameters.Add("TransportationAllowance", modal.TransportationAllowance);
            parameters.Add("OtherAllowance", modal.OtherAllowance);
            parameters.Add("ID", modal.ID);
            var i = await DbContext.ExecuteAsync(sqlQuery, parameters);
            return i > 0;
        }
    }
}
