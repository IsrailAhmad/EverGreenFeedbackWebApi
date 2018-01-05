using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public FeedbackModel AddFeedback(int customerid, int questionid, int anwserid)
        {
            FeedbackModel feedback = new FeedbackModel();
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                feedbackmaster u = new feedbackmaster();
                u.CustomerId = customerid;
                u.QuestionId = questionid;
                u.AnswerId = anwserid;
                var cust = context.customermasters.Where(x => x.CustomerId == customerid).FirstOrDefault();
                if (cust.CustomerId > 0)
                {
                    var data = context.feedbackmasters.Where(x => x.CustomerId == u.CustomerId && x.QuestionId == u.QuestionId).FirstOrDefault();
                    if (data != null)
                    {
                        context.feedbackmasters.Where(x => x.CustomerId == u.CustomerId && x.QuestionId == u.QuestionId).ToList().ForEach(x => x.AnswerId = u.AnswerId);
                        //context.Entry(u).State = System.Data.Entity.EntityState.Modified;
                        //context.SaveChanges();
                        feedback.FeedbackId = data.FeedbackId;
                        feedback.CustomerId = (int)data.CustomerId;
                        feedback.QuestionId = (int)data.QuestionId;
                        feedback.AnswerId = (int)data.AnswerId;
                        feedback.CreatedOn = (DateTime)data.CreatedOn;
                    }
                    else
                    {
                        context.feedbackmasters.Add(u);
                    }
                    var result = context.SaveChanges();
                    if (result > 0)
                    {
                        var custdata = context.feedbackmasters.Where(x => x.CustomerId == u.CustomerId && x.QuestionId == u.QuestionId && x.AnswerId == anwserid).FirstOrDefault();
                        if (custdata != null)
                        {
                            feedback.FeedbackId = custdata.FeedbackId;
                            feedback.CustomerId = (int)custdata.CustomerId;
                            feedback.QuestionId = (int)custdata.QuestionId;
                            feedback.AnswerId = (int)custdata.AnswerId;
                            feedback.CreatedOn = (DateTime)custdata.CreatedOn;
                        }
                    }
                }
                return feedback;
            }
        }

        public FeedbackModel GetCustomerFeedback(int customerid, int storeid)
        {
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                var cust = (from x in context.customermasters
                            join s in context.feedbackmasters on x.CustomerId equals s.CustomerId
                            join ccc in context.remarksmasters on x.CustomerId equals ccc.CustomerId into j1
                            from j2 in j1.DefaultIfEmpty()
                            where x.StoreId == storeid && x.CustomerId == customerid
                            orderby x.CustomerName descending
                            select new FeedbackModel()
                            {
                                CustomerId = x.CustomerId,
                                CustomerName =x.CustomerName,
                                CustomerRemarks = j2.Remarks
                            }).First();

                var feedbackdata = (from z in context.customermasters
                                    join r in context.feedbackmasters on z.CustomerId equals r.CustomerId
                                    where r.CustomerId == cust.CustomerId
                                    orderby z.CustomerName descending
                                    select new FeedbackDetailsModel()
                                    {
                                        AnswerId = (int)r.AnswerId,
                                        QuestionId = (int)r.QuestionId
                                    }).ToList();
                cust.FeedbackData = feedbackdata;
                return cust;
            }
        }

        public FeedbackSummaryModel GetFeedbackSummary(int storeid)
        {
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                var data = (from c in context.customermasters
                            where c.StoreId == storeid
                            select new FeedbackSummaryModel()
                            {
                                CustomerName = c.CustomerName,
                                StoreId = (int)c.StoreId
                            }).First();


                //var questiondata = (from q in context.questionmasters
                //                    orderby q.PositionId ascending
                //                    select new FeedbackQuestionModel()
                //                    {
                //                        PositionId = (int)q.PositionId,
                //                        QuestionName = q.Question,
                //AnswerData = (from aa in context.answermasters
                //              join fff in context.feedbackmasters on aa.AnswerId equals fff.AnswerId into j1
                //              from j2 in j1.DefaultIfEmpty()
                //              join cc in context.customermasters on j2.CustomerId equals cc.CustomerId into j3
                //              from j4 in j3.DefaultIfEmpty()
                //              where j4.StoreId == storeid
                //              //&& j2.PositionId == 1
                //              group j2 by j2.AnswerId into grouped
                //              select new FeeddbackAnswerModel() { AnswerId = (int)grouped.Key, Answer = grouped.Count(t => t.AnswerId != null) })
                var AnswerData = (from aa in context.answermasters
                                  join fff in context.feedbackmasters on aa.AnswerId equals fff.AnswerId into j1
                                  from j2 in j1.DefaultIfEmpty()
                                  join cc in context.customermasters on j2.CustomerId equals cc.CustomerId into j3
                                  from j4 in j3.DefaultIfEmpty()
                                  join qq in context.questionmasters on j2.QuestionId equals qq.id into j5
                                  from j6 in j5.DefaultIfEmpty()
                                  where j4.StoreId == storeid
                                  group j2 by new { j2.QuestionId, j2.AnswerId } into g
                                  select new FeeddbackAnswerModel
                                  {
                                      QuestionId = (int)g.Key.QuestionId,
                                      QusetionName = (from q in context.questionmasters where q.PositionId == (int)g.Key.QuestionId select q.Question).FirstOrDefault(),
                                      AnswerId = (int)g.Key.AnswerId,
                                      Answer = g.Count()
                                  }).OrderBy(s => s.QuestionId).ToList();

                // }).ToList();
                data.AnswerData = AnswerData;
                return data;
            }
        }
    }
}