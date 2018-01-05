using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Interfaces
{
    public interface IMenuRepository:IDisposable
    {
        IEnumerable<MenuModel> GetAllMenuList(int storeid);
        IEnumerable<MenuModel> GetAllMenuList();
    }
}