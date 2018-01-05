using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class CustomerCategoryModel
    {
        public int CustomerCategoryId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string OrderNumber { get; set; }
        public IEnumerable<CustomerProductModel> Product { get; set; }
    }
}