using Dapper;
using SwitchCMS.DataBaseContext.Dapper;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using SwitchCMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository
{
    public class OADVRepository : IOADVRepository
    {

        private readonly IDapperContext DbContext;

        public OADVRepository(IDapperContext _dbContext)
        {
            DbContext = _dbContext;
        }

        public async Task<int> CreateAdvatisement(OADV AdvtiseModal)
        {
            string SqlQuery = @"INSERT INTO [OADV]
                               ([StartDate]
                               ,[EndDate]
                               ,[ImageStatus]
                               ,[ImagePath])
                               VALUES
                               ( @StartDate
                               ,@EndDate 
                               ,@ImageStatus 
                               ,@ImagePath );
                                select SCOPE_IDENTITY();";
           DynamicParameters parameters = new DynamicParameters();
            parameters.Add("StartDate", AdvtiseModal.StartDate);
            parameters.Add("EndDate", AdvtiseModal.EndDate);
            parameters.Add("ImageStatus", AdvtiseModal.ImageStatus.ToString());
            parameters.Add("ImagePath", AdvtiseModal.ImagePath);
            int SucessID=await DbContext.QuerySingleAsync<int>(SqlQuery, parameters);
            return SucessID;

        }

        public async Task<bool> DeleteeAdvatisementModal(int ID)
        {
            string SqlQuery = "delete from OADV where ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", ID);
            int success = await DbContext.ExecuteAsync(SqlQuery, parameters);

            return success > 0;
        }

       

        public async Task<List<OADV>> GetAllActiveAdvatisementByCompanyID(int CompanyID)
        {
            string SqlQuery = @"select OADV.ID [AdvID], OADV.*, ADV1.ID [CusID], ADV1.* from OADV inner join
                                ADV1 on OADV.ID=ADV1.OADVID
                                where OADV.Status='Active' and ADV1.CustomerID=@CustomerID and GetDate() between StartDate and EndDate";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CustomerID", CompanyID);
            Dictionary<int, OADV> Advtise=new Dictionary<int, OADV>();

            IEnumerable<OADV> data = await DbContext.QueryAsync<OADV, ADV1, OADV>(SqlQuery,
                (adv, adv1) => 
                {
                    if(Advtise.ContainsKey(adv.ID))
                    {
                        if (adv1 != null)
                        {
                            adv.CustList.Add(adv1);
                            Advtise.Add(adv.ID, adv);
                        }
                        else
                        {
                            if(adv1!=null)
                            {
                                Advtise[adv.ID].CustList.Add(adv1);
                            }

                        }
                    }
                    return adv;
                }, "AdvID, CusID",parameters);

            return Advtise.Values.ToList();
             
        }

        public async Task<List<OADV>> GetAllAdvatisement()
        {
            string SqlQuery = @"select * from OADV";

            IEnumerable<OADV> AdvtiseList = await DbContext.QueryAsync<OADV>(SqlQuery);
            return AdvtiseList.ToList();
            
        }

        public async Task<bool> UpdateAdvatisement(OADV AdvtisemrntModal)
        {
            string SqlQuery = @"UPDATE  [OADV]
                               SET StartDate = @StartDate
                                  ,EndDate = @EndDate 
                                  ,ImageStatus = @ImageStatus
                                  ,ImagePath = @ImagePath
                             WHERE ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("StartDate", AdvtisemrntModal.StartDate);
            parameters.Add("EndDate", AdvtisemrntModal.EndDate);
            parameters.Add("ImageStatus", AdvtisemrntModal.ImageStatus.ToString());
            parameters.Add("ImagePath", AdvtisemrntModal.ImagePath.ToString());
            parameters.Add("ID", AdvtisemrntModal.ID);
            int Success=await DbContext.ExecuteAsync(SqlQuery,parameters);
            return Success > 0;

        }

        public async  Task<OADV> GetAdvatisementByID(int ID)
        {
            string SqlQuery = @"select OADV.ID [AdvID], OADV.*, ADV1.ID [CusID], ADV1.* from OADV left join
                                ADV1 on OADV.ID=ADV1.OADVID
                                where  OADV.ID=@ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ID", ID);
            Dictionary<int, OADV> Advtise = new Dictionary<int, OADV>();

            IEnumerable<OADV> data = await DbContext.QueryAsync<OADV, ADV1, OADV>(SqlQuery,
                (adv, adv1) =>
                {
                    if (!Advtise.ContainsKey(adv.ID))
                    {
                        if (adv1 != null)
                        {
                            adv.CustList.Add(adv1);                            
                        }
                        Advtise.Add(adv.ID, adv);
                    }
                    else
                    {
                        if (adv1 != null)
                        {
                            Advtise[adv.ID].CustList.Add(adv1);
                        }

                    }
                    return adv;
                }, "AdvID, CusID", parameters);

            return Advtise.Values.FirstOrDefault()!;
            throw new NotImplementedException();
        }

        public async Task<List<OADV>> GetAdvatisementPagination(AdvatisementPagination Pagination)
        {
            int offset = Pagination.RowCount * Pagination.PageIndex;
            string SqlQuery = @"select distinct OADV.ID from OADV left join
                                ADV1 on ADV1.OADVID=OADV.ID left join
                                OCMP on OCMP.ID=ADV1.CustomerID
                                where 
                                (@CompanyName IS NULL OR @CompanyName = '' OR OCMP.Name LIKE '%' + @CompanyName + '%')
                                ORDER BY OADV.ID
                                OFFSET @offset ROWS FETCH NEXT @RowCount ROWS ONLY";

            DynamicParameters parameters=new DynamicParameters();
            parameters.Add("CompanyName", Pagination.CompanyName);
            parameters.Add("offset", offset);
            parameters.Add("RowCount", Pagination.RowCount);

            IEnumerable<OADV> IDCollection = await DbContext.QueryAsync<OADV>(SqlQuery, parameters);

            List<int> Ids=IDCollection.Select(x=> x.ID).ToList();

            SqlQuery = "select * from OADV where ID in @Ids";
            parameters.Add("Ids", Ids);

            var Data = await DbContext.QueryAsync<OADV>(SqlQuery, parameters);
            return Data.ToList();            
        }

        public async Task<int> GetTotalAdvatisementCount(AdvatisementPagination Pagination)
        {
            string SqlQuery = @"select  Count(distinct OADV.ID) from OADV left join
                                ADV1 on ADV1.OADVID=OADV.ID left join
                                OCMP on OCMP.ID=ADV1.CustomerID
                                where 
                                (@CompanyName IS NULL OR @CompanyName = '' OR OCMP.Name LIKE '%' + @CompanyName + '%')";
            DynamicParameters parameters=new DynamicParameters();
            parameters.Add("CompanyName", Pagination.CompanyName);

            int TotalCount = await DbContext.QuerySingleAsync<int>(SqlQuery, parameters);
            return TotalCount;

             
        }

        public async Task<List<OADV>> GetAdvatisementByCustomerIDandIsAll(int CustID, bool IsAll)
        {
            string SqlQuery = string.Empty;
            if(IsAll)
            {
                SqlQuery = @"select distinct OADV.*
                                    from oadv left join adv1 on oadv.id=adv1.OADVID left join ocmp on adv1.CustomerID=ocmp.ID
                                    where GETDATE()>=StartDate and GETDATE()<=EndDate and 
                                    ImageStatus='Active'";
            }
            else
            {
                SqlQuery = @"select OADV.*
                                    from oadv left join adv1 on oadv.id=adv1.OADVID left join ocmp on adv1.CustomerID=ocmp.ID
                                    where GETDATE()>=StartDate and GETDATE()<=EndDate and 
                                    isnull(CustomerID,0)=0 and ImageStatus='Active'
                                    union all
                                    select OADV.*
                                    from oadv left join adv1 on oadv.id=adv1.OADVID left join ocmp on adv1.CustomerID=ocmp.ID
                                    where GETDATE()>=StartDate and GETDATE()<=EndDate and CustomerID=@CustomerID and ImageStatus='Active'";
            }
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CustomerID", CustID);

            IEnumerable<OADV> AdvatisementList = await DbContext.QueryAsync<OADV>(SqlQuery, parameters);
            return AdvatisementList.ToList();           
            
        }
    }
}
