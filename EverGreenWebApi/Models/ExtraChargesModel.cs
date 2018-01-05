using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class ExtraChargesModel
    {
        public int ExtraId { get; set; }
        public string ExtraName { get; set; }
        public decimal ExtraCharges { get; set; }
    }
}