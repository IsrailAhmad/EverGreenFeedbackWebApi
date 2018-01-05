using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class ChargeModel
    {
        public int ChargeId { get; set; }
        public decimal DecorationCharge { get; set; }
        public decimal Discount { get; set; }
        public decimal EventFee { get; set; }
        public int StoreId { get; set; }
        public int MenuId { get; set; }
        public IEnumerable<MenuModel> MenuData { get; set; }
    }
}