using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class SmsReceiverModel
    {
        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string PhoneNumber { get; set; }
        public char IsActive { get; set; }
    }
}