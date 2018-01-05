using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class AdminModel
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string PhoneNumber { get; set; }
        public string AdminEmailId { get; set; }
        public int Role { get; set; }
        public int StoreId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}