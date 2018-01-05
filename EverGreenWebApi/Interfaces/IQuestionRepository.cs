using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Interfaces
{
    public interface IQuestionRepository:IDisposable
    {
        IEnumerable<QuestionModel> GetAllQuestionList();

        QuestionModel AddQuestion(QuestionModel model);
        ResponseStatus DeleteQuestionByID(int id);
        QuestionModel QuestionDisableEnable(QuestionModel model);

        QuestionModel QuestionUpdate(QuestionModel model);

        IEnumerable<QuestionModel> ArrangeQuestion(int id,int questionid);


    }
}