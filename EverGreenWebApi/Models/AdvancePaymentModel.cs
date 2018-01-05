using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class AdvancePaymentModel
    {
        public int AdvanceId { get; set; }
        public int StoreId { get; set; }
        public int CustomerId { get; set; }
        public string OrderNumber { get; set; }
        public decimal AdvancePrice { get; set; }
    }
}