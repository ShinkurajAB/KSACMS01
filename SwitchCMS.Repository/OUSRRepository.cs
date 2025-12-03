using Dapper;
using Microsoft.EntityFrameworkCore;
using SwitchCMS.DataBaseContext;
using SwitchCMS.DataBaseContext.Dapper;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using SwitchCMS.Repository.Interface;
using SwitchCMS.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository
{
    public class OUSRRepository :   IOUSRRepository
    {
        private readonly IDapperContext DbContext;
        public OUSRRepository(IDapperContext _DbContext)
        {
            DbContext = _DbContext;
        }

        public async Task<OUSR> GetUserbyUserName(string userName)
        {
            string SqlQuery = @"select OUSR.ID[UserID],OUSR.*, OCMP.ID [CmpID], OCMP.* from OUSR 
                                inner join OCMP on OUSR.CompanyID=OCMP.ID
                                where UserName=@UserName";
            DynamicParameters Parameter = new DynamicParameters();
            Parameter.Add("UserName", userName);
            var user = await DbContext.QueryAsync<OUSR, OCMP, OUSR>(SqlQuery, (USR, CMP) =>
            {
                if (CMP != null)
                    USR.Company = CMP;
                return USR;
            }, "UserID,CmpID", Parameter);

            return user.FirstOrDefault()!;

        }

        public async Task<OUSR> GetUserbyUserID(int UserID)
        {
            string SqlQuery = @"select OUSR.ID[UserID],OUSR.*, OCMP.ID [CmpID], OCMP.* from OUSR 
                                inner join OCMP on OUSR.CompanyID=OCMP.ID
                                where OUSR.ID=@ID";
            DynamicParameters Parameter = new DynamicParameters();
            Parameter.Add("ID", UserID);
            var user = await DbContext.QueryAsync<OUSR, OCMP, OUSR>(SqlQuery, (USR, CMP) =>
            {
                if (CMP != null)
                    USR.Company = CMP;
                return USR;
            } , "UserID,CmpID", Parameter);

            return user.FirstOrDefault()!;
        }

        public async Task<List<OUSR>> GetUserByPageIndex(UsersPagination Pagination)
        {
            int offset = Pagination.RowCount * Pagination.PageIndex;

            string SqlQuery = @"select U.ID [UserID],U.*,C.ID [CmpID], C.* from OUSR U 
                                inner join OCMP C on U.CompanyID=C.ID
                                where  UserName like @UserName ";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("UserName", "%" + Pagination.UsersName + "%");
            if (Pagination.SelectedCompany != null && Pagination.SelectedCompany.ID != 0)
            {                 
                SqlQuery += " and CompanyID=@CompanyID";
                parameters.Add("CompanyID", Pagination.SelectedCompany.ID);
            }

            SqlQuery += @" order by  U.ID                            
                        offset @offset  ROWS FETCH NEXT @RowCount row only";
            parameters.Add("offset", offset);
            parameters.Add("RowCount", Pagination.RowCount);

            Dictionary<int, OUSR> UserDic = new Dictionary<int, OUSR>();

            IEnumerable<OUSR> User = await DbContext.QueryAsync<OUSR, OCMP, OUSR>(SqlQuery, (ousr, ocmp) =>
            {
                if (UserDic.ContainsKey(ousr.ID))
                {
                    if (ocmp != null)
                    {
                        ousr.Company = ocmp;
                        UserDic[ousr.ID] = ousr;
                    }
                }
                else
                {
                    if (ocmp != null)
                        ousr.Company = ocmp;

                    UserDic.Add(ousr.ID, ousr);
                }
                return ousr;

            }, "UserID, CmpID", parameters);
             

            return UserDic.Values.ToList();


        }

        public async Task<int> GetTotalUserCount(int CompanyID, string UsersName)
        {
            string SqlQuery = @"SELECT Count(ID) 
                                        FROM OUSR
                                        WHERE UserName LIKE @UserName";
            DynamicParameters parameters=new DynamicParameters();
            parameters.Add("UserName", "%"+UsersName +"%");
            if(CompanyID > 0)
            {
                SqlQuery += " and CompanyID=@CompanyID";
                parameters.Add("CompanyID", CompanyID);
            }
            int TotalCount = await DbContext.QuerySingleAsync<int>(SqlQuery, parameters);
            return TotalCount;
        }

        public async Task<bool> UserNameExist(string UserName)
        {
            string SqlQuery = "select * from OUSR where UserName=@UserName";
            DynamicParameters Parameter = new DynamicParameters();
            Parameter.Add("UserName", UserName);
            var user = await DbContext.QueryFirstOrDefaultAsync<OUSR>(SqlQuery, Parameter);

            if(user == null) 
                return false;
            return true;
           
        }

        public async Task<ModificationStatus> CreeateUpdateUser(OUSR user)
        {
            try
            {
                string SqlQuery = string.Empty;
                ModificationStatus status = new ModificationStatus();
                DynamicParameters Parameter = new DynamicParameters();
                Parameter.Add("CompanyID", user.CompanyID);
                Parameter.Add("UserName", user.UserName);
                Parameter.Add("Password", user.Password);
                Parameter.Add("Role", user.Role.ToString());
                Parameter.Add("Name", user.Name);
                Parameter.Add("Status", user.Status.ToString());
                if (user.ID>0)
                {
                    SqlQuery = @"UPDATE [dbo].[OUSR]
                                   SET [CompanyID] = @CompanyID 
                                      ,[UserName] = @UserName 
                                      ,[Password] = @Password 
                                      ,[Role] = @Role 
                                      ,[Name] = @Name 
                                      ,[Status] = @Status 
                                 WHERE ID=@ID;
                                  SELECT @ID;";
                    Parameter.Add("ID", user.ID);
                    status.Message = "Updated Successfully";
                }
                else
                {
                   SqlQuery = @"INSERT INTO [dbo].[OUSR]
                                       ([CompanyID]
                                       ,[UserName]
                                       ,[Password]
                                       ,[Role]
                                       ,[Name]
                                       ,[Status])
                                 VALUES
                                       (@CompanyID
                                       ,@UserName 
                                       ,@Password 
                                       ,@Role 
                                       ,@Name 
                                       ,@Status );
                                        SELECT CAST(SCOPE_IDENTITY() AS INT);
                                        ";
                    status.Message = "Created Successfully";

                }

                int Success=await DbContext.ExecuteScalarAsync<int>(SqlQuery, Parameter);
                status.Success = Success > 0;
                status.ID = Success;
                return status;

            }
            catch (Exception ex)
            {
                return new ModificationStatus
                {
                    Success = false,
                    Message = ex.Message
                };
            }

        }

        public async Task<List<OUSR>> CompanyAdminUserExistorNot(int CompanyID, Roles role)
        {
            string SqlQuery = "select * from OUSR where CompanyID=@CompanyID and [Role]=@Role";
            DynamicParameters parameters=new DynamicParameters();
            parameters.Add("CompanyID", CompanyID);
            parameters.Add("Role", role.ToString());
            var user= await DbContext.QueryAsync<OUSR>(SqlQuery, parameters);

           
             return user.ToList();
            
        }

        public async Task<ModificationStatus> DeleteUser(int UserID)
        {
            ModificationStatus status = new ModificationStatus();
            string SqlQuery = "delete from OUSR where ID=@UserID";
            DynamicParameters parameters=new DynamicParameters();
            parameters.Add("UserID", UserID);
            int Success = await DbContext.ExecuteAsync(SqlQuery, parameters);

            status.Success = Success > 0;
            if(status.Success)
            {
                status.Message = "Deleted Successfully!";
            }
            return status;
            throw new NotImplementedException();
        }
    }
}
