using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class CustomerEventModel
    {       
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone{ get; set; }
        public string EventName { get; set; }
        public int Gathering { get; set; }
        public DateTime EventDate { get; set; }
        public string Time { get; set; }
        public string Comment { get; set; }
        public int StoreId { get; set; }
    }
}