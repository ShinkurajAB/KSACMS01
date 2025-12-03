using SwitchCMS.API.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Model.UI;
using SwitchCMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SwitchCMS.API.Services
{
    public class OUSRService : IOUSRService
    {
        private readonly IOUSRRepository ousrRepository;
        private readonly IHashPasswordService hashPasswordService;
        private readonly IOUMNRepository userMenuRepository;

        public OUSRService(IOUSRRepository ousrRepository, IHashPasswordService _hashPasswordService,
            IOUMNRepository _userMenuRepository)
        {
            this.ousrRepository = ousrRepository;
            hashPasswordService= _hashPasswordService;
            userMenuRepository= _userMenuRepository;
        }

      
        public async Task<OUSR> GetUserByUserName(string userName)
        {
            var user= await ousrRepository.GetUserbyUserName(userName);
            return user;
        }

        public async Task<OUSR> GetUserByUserID(int userID)
        {
            var user=await ousrRepository.GetUserbyUserID(userID);
            return user;
        }

        public async Task<UsersPagination> GetUserByPagination(UsersPagination Pagination)
        {
            if (Pagination.PageIndex > 0)
                Pagination.PageIndex--;
            Pagination.UserList = await ousrRepository.GetUserByPageIndex(Pagination);
            Pagination.TotalCount = await ousrRepository.GetTotalUserCount(Pagination.SelectedCompany.ID, Pagination.UsersName!);
            Pagination.PageIndex++;


            if (Pagination.TotalCount == Pagination.RowCount)
                Pagination.TotalPage = 1;
            else
                Pagination.TotalPage = (int)Math.Ceiling(((decimal)Pagination.TotalCount / (decimal)Pagination.RowCount));
            return Pagination;

            
        }

        public async Task<ModificationStatus> CreeateUpdateUser(OUSR user)
        {
            ModificationStatus Data = new ModificationStatus();

            // Password Hashing
            OUSR ExistUser = new OUSR();
            if (user.ID > 0)
            {
                ExistUser = await GetUserByUserID(user.ID);
                if (!string.IsNullOrEmpty(ExistUser.UserName))
                {
                    if (ExistUser.Password != user.Password)
                    {
                        user.Password = hashPasswordService.HashPassword(user.Password);
                    }
                }

            }
            else
            {
                user.Password = hashPasswordService.HashPassword(user.Password);
            }



            // if Company have Company admin then restrict user
            if (user.Role == Utility.Roles.CompanyAdmin)
            {
                var companyAdminUsers = await ousrRepository.CompanyAdminUserExistorNot(user.CompanyID, user.Role);
                if (companyAdminUsers.Count > 0) 
                {
                   if(user.ID>0)
                    {
                        bool exist= companyAdminUsers.Any(x=> x.ID == user.ID);
                        if (!exist)
                        {
                            return new ModificationStatus { Success = false, Message= "Company Admin Created as Same Company" };
                        }
                    }
                    else
                    {
                        return new ModificationStatus { Success = false, Message = "Company Admin Created as Same Company" };
                    }

                }
                
            }
           


            // If USer Exist or not
            var Exist = await ousrRepository.GetUserbyUserName(user.UserName);

            if (Exist != null) 
            {
                // if same ID can update user else already User Exist
                if(user.ID== Exist.ID)
                {
                    Data = await ousrRepository.CreeateUpdateUser(user);
                }
                else
                {
                    return new ModificationStatus { Success = false, Message = "This User Already Exist!!" };
                }

            }
            else
            {
                Data = await ousrRepository.CreeateUpdateUser(user);
            }




            // if Create or update is success then Create Submenus
            if (Data != null)
            {
                if (Data.Success)
                {
                    if (user.UserMenus.Count > 0)
                    {
                        user.UserMenus.ForEach(x => x.UserID = Data.ID);

                        await userMenuRepository.DeleteSubMenuByUserID(Data.ID);
                        await userMenuRepository.CreateUserMenus(user.UserMenus);
                    }
                }
            }




           


            return Data;

         
            
           
        }

        public async Task<ModificationStatus> DeleteUserByUserID(int UserID)
        {
            await userMenuRepository.DeleteSubMenuByUserID(UserID);
            var Data=await ousrRepository.DeleteUser(UserID);
            return Data;
            
        }
    }
}
