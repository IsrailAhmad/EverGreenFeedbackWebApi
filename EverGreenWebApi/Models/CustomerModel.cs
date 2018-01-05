using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string CustomerEmailId { get; set; }
        public int Role { get; set; }
        public int StoreId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}