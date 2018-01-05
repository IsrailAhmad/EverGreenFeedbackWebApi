using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryModel> GetAllCategoryList()
        {

            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                // string path = "http://103.233.79.234/Data/EverGreen_Android/CategoryPictures/";

                var result = context.categorymasters.OrderBy(c => c.CategoryName);
                var data = result.Select(c => new CategoryModel()
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    //CategoryDescription = c.CategoryDescription,
                    //StoreId = (int)c.StoreId,
                    //CategoryPictures = path + c.CategoryId + "CategoryPictures.jpg",
                }).ToList();
                return data;
            }

        }

        public IEnumerable<CategoryModel> GetAllCategoryList(int menuid)
        {
            CategoryModel cust = new CategoryModel();
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                //var category = (from g in context.categorymasters
                //                where g.StoreId == storeid
                //                orderby g.CategoryId
                //                select new CategoryModel()
                //                {
                //                    CategoryId = g.CategoryId,
                //                    CategoryName = g.CategoryName,
                //                    // MenuId =x.MenuId,
                //                    ProductData = (from s in context.categorymasters
                //                                   join r in context.productmasters on s.CategoryId equals r.CategoryId
                //                                   where s.StoreId == storeid && s.CategoryId == g.CategoryId
                //                                   orderby r.ProductId
                //                                   select new ProductModel()
                //                                   {
                //                                       ProductId = r.ProductId,
                //                                       ProductName = r.ProductName,
                //                                       FoodType = r.FoodType,
                //                                       Price = (decimal)r.Price,
                //                                   }).ToList(),
                //                }).ToList();

                var category = (from ms in context.menusetupmasters
                                join c in context.categorymasters on ms.categoryid equals c.CategoryId into j1
                                from j2 in j1.DefaultIfEmpty()
                                where ms.menuid == menuid
                                orderby j2.CategoryName descending
                                select new CategoryModel()
                                {
                                    CategoryId = j2.CategoryId,
                                    CategoryName = j2.CategoryName,
                                    ProductData = (from msm in context.menusetupmasters
                                                   join p in context.productmasters on msm.Productid equals p.ProductId into j3
                                                   from j4 in j3.DefaultIfEmpty()
                                                   where msm.menuid == menuid && msm.categoryid == j2.CategoryId
                                                   orderby j4.ProductName ascending
                                                   select new ProductModel()
                                                   {
                                                       ProductId = j4.ProductId,
                                                       ProductName = j4.ProductName,
                                                       FoodType = j4.FoodType,
                                                       Price = (decimal)j4.Price,
                                                   }).ToList()
                                }).ToList();

                return category.GroupBy(x => x.CategoryId)
                            .Select(g => g.First())
                            .ToList();
            }
        }
    }
}