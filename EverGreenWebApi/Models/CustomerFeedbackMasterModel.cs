using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class CustomerFeedbackMasterModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public int AnswerId { get; set; }
        public string AnswerName { get; set; }
    }
}