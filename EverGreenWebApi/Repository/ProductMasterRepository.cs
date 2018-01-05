using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class ProductMasterRepository: IProductMasterRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductMasterModel> GetAllProductList()
        {
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                //string path = "http://103.233.79.234/Data/EverGreen_Android/ProductPictures/";

                var result = context.productmasters.OrderBy(p => p.ProductName);
                var data = result.Select(p => new ProductMasterModel()
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    //CategoryId = (int)p.CategoryId,
                    //UnitPrice = (decimal)p.UnitPrice,
                    //GST = (decimal)p.GST,
                    //Discount = (decimal)p.Discount,
                    //TaxType = p.TaxType,
                    //UOM = p.UOM,
                    //HSN = p.HSN,
                    //SweetsReset = p.SweetsReset,
                    //ProductDetails = p.ProductDetails,
                    //Lock = p.Lock,
                    //ProductPicturesUrl = path + p.ProductId + "ProductPictures.jpg",
                }).ToList();
                return data;
            }
        }
    }
}