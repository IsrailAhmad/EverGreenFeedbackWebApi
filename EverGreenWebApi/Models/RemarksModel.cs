using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class RemarksModel
    {
        public int RemarksId { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}