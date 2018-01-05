using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class FeedbackModel
    {
        public int FeedbackId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public int QuestionId { get; set; }
        public int PositionId { get; set; }
        public int AnswerId { get; set; }
        public string CustomerRemarks { get; set; }
        public DateTime CreatedOn { get; set; }
        public IEnumerable<FeedbackDetailsModel> FeedbackData { get; set; }
    }
}