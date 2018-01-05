using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class QuestionModel
    {
        public int id { get; set; }
        public int PositionId { get; set; }
        public string Question { get; set; }
        public int StoreId { get; set; }
        public string IsActive { get; set; }

    }
}