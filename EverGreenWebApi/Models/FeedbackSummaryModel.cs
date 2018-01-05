using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class FeedbackSummaryModel
    {
        public int StoreId{ get; set; }
        public string CustomerName { get; set; }
        //public int PositionId { get; set; }
        //public IEnumerable<FeedbackQuestionModel> QuestionData{ get; set; }
        public IEnumerable<FeeddbackAnswerModel> AnswerData { get; set; }
    }
}