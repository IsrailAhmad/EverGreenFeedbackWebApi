using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Interfaces
{
    public interface IFeedbackRepository:IDisposable
    {
        FeedbackModel AddFeedback(int customerid, int questionid, int anwserid);
        FeedbackModel GetCustomerFeedback(int customerid,int storeid);
        FeedbackSummaryModel GetFeedbackSummary(int storeid);
    }
}