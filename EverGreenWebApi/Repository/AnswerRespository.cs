using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class AnswerRespository : IAnswerRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AnswerModel> GetAllAnswerList()
        {
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                var result = context.answermasters.OrderBy(s => s.Answer);

                var data = result.Select(s => new AnswerModel()
                {
                    AnswerId = s.AnswerId,                  
                    Answer =s.Answer
                }).ToList();
                return data;
            }
        }
    }
}