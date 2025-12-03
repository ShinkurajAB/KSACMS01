using SwitchCMS.API.Services.Interface;
using SwitchCMS.Model;
using SwitchCMS.Repository.Interface;
using SwitchCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.API.Services
{
    public class OMENService : IOMENService
    {

        private readonly IOMENRepository MenuRepository;

        public OMENService(IOMENRepository _MenuRepository)
        {
            MenuRepository= _MenuRepository;
        }

        public async Task<List<OMEN>> GetAllMenus(string UserID, string Role)
        {
            if(Role == Roles.SuperAdmin.ToString())
            {
                var data = await MenuRepository.GetAllMenus();
                return data;

            }
            else
            {
                var data= await MenuRepository.GetMenuByUserIDMenus(Convert.ToInt32(UserID));
                return data;
            }
           

           
             
        }

        public async Task<List<OMEN>> GetMenuUserID(int UserID)
        {
            var data = await MenuRepository.GetMenuByUserIDMenus(UserID);
            return data;
        }
    }
}
