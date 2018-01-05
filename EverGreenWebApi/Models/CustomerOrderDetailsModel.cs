using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class CustomerOrderDetailsModel
    {
        public IEnumerable<CustomerOrderModel> OrderDetails { get; set; }
    }
}