using Dapper;
using Microsoft.EntityFrameworkCore;
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
    public class OATCRepository : IOATCRepository
    {
        private readonly IDapperContext dbContext;

        public OATCRepository(IDapperContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateDocument(OATC Model)
        {
            string SqlQuery = @"INSERT INTO [dbo].[OATC]
                               ([EntityName]
                               ,[StartDate]
                               ,[ExpiryDate]
                               ,[DocStatus]
                               ,[DocumentType]
                               ,[FileName]
                               ,[CompanyID])
                                VALUES
                               (@EntityName 
                               ,@StartDate 
                               ,@ExpiryDate 
                               ,@DocStatus 
                               ,@DocumentType 
                               ,@FileName 
                               ,@CompanyID )";
            DynamicParameters parameters=new DynamicParameters();
            parameters.Add("EntityName", Model.EntityName);
            parameters.Add("StartDate", Model.StartDate);
            parameters.Add("ExpiryDate", Model.ExpiryDate);
            parameters.Add("DocStatus", Model.DocStatus.ToString());
            parameters.Add("DocumentType", Model.documentType.ToString());
            parameters.Add("FileName", Model.FileName);
            parameters.Add("CompanyID", Model.CompanyID);

            int Success=await dbContext.ExecuteAsync(SqlQuery, parameters);
            return Success > 0;

            
        }

      

        public async Task<List<OATC>> GetDocumentByPageIndex(DocumentPagination Pagination)
        {
            int offset = Pagination.RowCount * Pagination.PageIndex;
            string SqlQuery = @"select * from OATC 
                                where CompanyID=@CompanyID and EntityName like @EntityName
                                order by ID desc
                                OFFSET @offset ROWS FETCH NEXT @RowCount ROWS ONLY";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("EntityName", "%"+Pagination.FilterSearch+"%");
            parameters.Add("CompanyID", Pagination.CompanyID);
            parameters.Add("offset", offset);
            parameters.Add("RowCount", Pagination.RowCount);

            IEnumerable<OATC> DocumentList= await dbContext.QueryAsync<OATC>(SqlQuery, parameters);
            return DocumentList.ToList();

             
        }

        public async Task<int> GetDocumentCount(DocumentPagination Pagination)
        {
            string SqlQuery = @"select Count(ID) from OATC 
                                where CompanyID=@CompanyID and EntityName like @EntityName";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CompanyID", Pagination.CompanyID);
            parameters.Add("EntityName", "%" + Pagination.FilterSearch + "%");
            int TotalCount = await dbContext.QuerySingleAsync<int>(SqlQuery, parameters);
            return TotalCount;
             
        }

        public async Task<OATC> GetDocumentByID(int ID)
        {
            string SqlQuery = "Select * from OATC where ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", ID);

            OATC Data = await dbContext.QueryFirstOrDefaultAsync<OATC>(SqlQuery, parameters);
            return Data;    

            
        }

        public async Task<bool> DeleteDocumentByID(int ID)
        {
            string SqlQuery = "delete from OATC where ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", ID);
            int Success=await dbContext.ExecuteAsync(SqlQuery, parameters);
            return Success>0;
            
        }

        public async Task<bool> UpdateDocument(OATC Model)
        {
            string SqlQuery = @"UPDATE [dbo].[OATC]
                               SET [EntityName] = @EntityName
                              ,[StartDate] = @StartDate 
                              ,[ExpiryDate] = @ExpiryDate 
                              ,[DocStatus] = @DocStatus 
                              ,[DocumentType] = @DocumentType 
                              ,[FileName] = @FileName 
                              ,[CompanyID] = @CompanyID 
                               WHERE ID=@ID";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("EntityName", Model.EntityName);
            parameters.Add("StartDate", Model.StartDate);
            parameters.Add("ExpiryDate", Model.ExpiryDate);
            parameters.Add("DocStatus", Model.DocStatus.ToString());
            parameters.Add("DocumentType", Model.documentType.ToString());
            parameters.Add("FileName", Model.FileName);
            parameters.Add("CompanyID", Model.CompanyID);
            parameters.Add("ID", Model.ID);

            int Success = await dbContext.ExecuteAsync(SqlQuery, parameters);
            return Success > 0;
             
        }
    }
}
