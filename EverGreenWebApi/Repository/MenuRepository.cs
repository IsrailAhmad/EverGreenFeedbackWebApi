using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class MenuRepository : IMenuRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<MenuModel> GetAllMenuList(int storeid)
        {
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                var data = (from z in context.menumasters
                                //join r in context.orderdetails on z.OrderNumber equals r.OrderNumber
                                //join p in context.productmasters on r.ProductId equals p.ProductId into productDetails
                                //from tempc in productDetails.DefaultIfEmpty()
                                //where z.LoginId == loginid
                            orderby z.MenuName descending
                            select new MenuModel()
                            {
                                MenuId = z.MenuId,
                                MenuName = z.MenuName,
                                MenuPrice = (decimal)z.MenuPrice,
                                MenuImageUrl = "http://103.233.79.234/Data/StoreFeedback_Android/MenuImage/" + z.MenuId + ".jpeg"
                            }).ToList();
                return data; 
            }
        }

        public IEnumerable<MenuModel> GetAllMenuList()
        {
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                var data = (from z in context.menumasters
                                //join r in context.orderdetails on z.OrderNumber equals r.OrderNumber
                                //join p in context.productmasters on r.ProductId equals p.ProductId into productDetails
                                //from tempc in productDetails.DefaultIfEmpty()
                                //where z.LoginId == loginid
                            orderby z.MenuName descending
                            select new MenuModel()
                            {
                                MenuId = z.MenuId,
                                MenuName = z.MenuName,
                                MenuPrice = (decimal)z.MenuPrice,
                                MenuImageUrl = "http://103.233.79.234/Data/StoreFeedback_Android/MenuImage/" + z.MenuId + ".jpeg"
                            }).ToList();
                return data;
            }
        }
    }
}