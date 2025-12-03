using Dapper;
using SwitchCMS.DataBaseContext.Dapper;
using SwitchCMS.Model;
using SwitchCMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Repository
{
    public class OMENRepository : IOMENRepository
    {
        private readonly IDapperContext DbContext;

        public OMENRepository(IDapperContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<List<OMEN>> GetAllMenus()
        {
            string SqlQuery = @"select OMEN.ID [MenuID], OMEN.*, OSMEN.ID [SubMenuID], OSMEN.* from OMEN  
                                inner join OSMEN on OSMEN.MenuID=OMEN.ID
                                order by OMEN.ID";

            Dictionary<int, OMEN> Dicitem = new Dictionary<int, OMEN>();
            IEnumerable<OMEN> Menus=await DbContext.QueryAsync<OMEN,OSMEN, OMEN>(SqlQuery, (menu, submenu) => {
                if (Dicitem.ContainsKey(menu.ID))
                {
                    Dicitem[menu.ID].SubMenu.Add(submenu);
                }
                else
                {
                    menu.SubMenu.Add(submenu);
                    Dicitem.Add(menu.ID, menu);
                }

                return menu;
            }, "MenuID, SubMenuID", null);
            return Dicitem.Values.ToList();

          
        }

        public async Task<List<OMEN>> GetMenuByUserIDMenus(int UserID)
        {
            string SqlQuery = @"select OMEN.ID [MenuID], OMEN.*, OSMEN.ID [SubMenuID], OSMEN.* from OMEN inner join
                                OUMN on OUMN.MenuID=OMEN.ID inner join
                                OSMEN on OSMEN.MenuID=OMEN.ID
                                where OUMN.UserID=@UserID
                                order by OMEN.ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("UserID", UserID);
            Dictionary<int, OMEN> Dicitem = new Dictionary<int, OMEN>();


            IEnumerable<OMEN> Menus = await DbContext.QueryAsync<OMEN, OSMEN, OMEN>(SqlQuery, (menu, submenu) => {
                if (Dicitem.ContainsKey(menu.ID))
                {
                    Dicitem[menu.ID].SubMenu.Add(submenu);
                }
                else
                {
                    menu.SubMenu.Add(submenu);
                    Dicitem.Add(menu.ID, menu);
                }

                return menu;
            }, "MenuID, SubMenuID", parameters);
            return Dicitem.Values.ToList();
            
        }
    }
}
