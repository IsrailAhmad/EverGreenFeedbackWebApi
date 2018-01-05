using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class CustomerBookedEventModel
    {
        public DateTime EventDate { get; set; }
        public int StoreId { get; set; }
        public string Time { get; set; }
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
    }
}