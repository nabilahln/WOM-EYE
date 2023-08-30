using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WOM_EYE.Interfaces.Menu;
using WOM_EYE.Models.Menu;

namespace WOM_EYE.Providers.Menu
{
    public class MenuProvider : IMenuProvider
    {
        IDbConnection _dbConnection;
        MenuModel _menuModel;
        List<MenuModel> _listMenu;

        public MenuProvider(IDbConnection dbConnection, MenuModel menuModel, List<MenuModel> listMenu)
        {
            _dbConnection = dbConnection;
            _menuModel = menuModel;
            _listMenu = listMenu;
        }

        public List<MenuModel> ddlMenu(string userId)
        {
            string sp = "spWOMEYE_GetMenu";

            var resp = _dbConnection.ExecuteReader(sp, new { @userId = userId }, commandType: CommandType.StoredProcedure, commandTimeout: 30);
            while (resp.Read())
            {
                MenuModel menu = new MenuModel();
                menu.MENU_DESC = resp[0].ToString();
                menu.MENU_URL = resp[1].ToString();
                menu.IS_ACCESS = resp[2].ToString();
                _listMenu.Add(menu);
            }
            resp.Close();
            return _listMenu;
        }
    }
}
