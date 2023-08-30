using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WOM_EYE.Models.Menu;

namespace WOM_EYE.Interfaces.Menu
{
    public interface IMenuProvider
    {
        List<MenuModel> ddlMenu(string userId);
    }
}
