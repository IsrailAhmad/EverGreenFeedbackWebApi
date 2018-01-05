using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class PrintReceiptModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNo { get; set; }
        public string CustomerEmailId { get; set; }
        public DateTime EventDate { get; set; }
        public string EventTime { get; set; }
        public string OrderNumber { get; set; }
        public int StoreId { get; set; }
        public int OrderId { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal AdvancePrice { get; set; }
        public decimal NetPrice { get; set; }
        public string Products { get; set; }
    }
}