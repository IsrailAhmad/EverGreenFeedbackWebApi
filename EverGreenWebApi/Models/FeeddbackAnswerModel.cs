using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class FeeddbackAnswerModel
    {
        public int QuestionId { get; set; }
        public string QusetionName { get; set; }
        public int AnswerId { get; set; }        
        public int Answer { get; set; }
    }
}