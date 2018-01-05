using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class FeedbackQuestionModel
    {
        public int PositionId { get; set; }
        public string QuestionName { get; set; }
        public IEnumerable<FeeddbackAnswerModel> AnswerData { get; set; }
    }
}