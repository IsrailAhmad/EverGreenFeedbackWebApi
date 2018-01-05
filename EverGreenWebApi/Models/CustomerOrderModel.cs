using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class CustomerOrderModel
    {
        public string customerName { get; set; }
        public string CustomerPhoneNo { get; set; }
        public string CustomerEmailId { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string StorePhonNumber { get; set; }
        public string StoreAddress { get; set; }
        public decimal EventFee { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal Discount { get; set; }
        public decimal CustomerPrice { get; set; }
        public decimal Total { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal NetPrice { get; set; }
        public decimal AdvancePrice { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public decimal MenuPrice { get; set; }
        public string OrderNumber { get; set; }
        public DateTime EventDate { get; set; }
        public string EventTime { get; set; }
        public int Gathering { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<CustomerCategoryModel> Category { get; set; }
        public IEnumerable<ExtraChargesModel> Extra { get; set; }
        public string ExtraNames { get; set; }
        public string ProductNames { get; set; }
        public string CustomerExtraProducts { get; set; }
        
    }
}