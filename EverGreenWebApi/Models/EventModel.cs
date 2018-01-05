using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class EventModel
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public decimal EventPrice { get; set; }
        public int StoreId { get; set; }
    }
}