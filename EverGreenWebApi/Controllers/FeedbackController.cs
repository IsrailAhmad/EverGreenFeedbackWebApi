using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using EverGreenWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EverGreenWebApi.Controllers
{
    public class FeedbackController : ApiController
    {
        static readonly IFeedbackRepository _repository = new FeedbackRepository();

        [HttpPost]
        public HttpResponseMessage AddFeedback(FeedbackModel model)
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                if (model.CustomerId > 0 && model.QuestionId > 0 && model.AnswerId > 0)
                {
                    var data = _repository.AddFeedback(model.CustomerId, model.QuestionId, model.AnswerId);
                    if (data != null)
                    {
                        response.Message = "Awesome we appericiate your feedback";
                        response.isSuccess = true;
                        response.serverResponseTime = System.DateTime.Now;
                        return Request.CreateResponse(HttpStatusCode.OK, new { data, response });
                    }
                    else
                    {
                        response.isSuccess = false;
                        response.serverResponseTime = System.DateTime.Now;
                        return Request.CreateResponse(HttpStatusCode.BadRequest, new { response });
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetCustomerFeedback(CustomerModel model)
        {
            ResponseStatus response = new ResponseStatus();
            //OrderModel data = new OrderModel();
            try
            {
                if (model.StoreId > 0 && model.CustomerId > 0)
                {
                    var data = _repository.GetCustomerFeedback(model.CustomerId, model.StoreId);
                    if (data.CustomerId > 0)
                    {
                        response.serverResponseTime = System.DateTime.Now;
                        response.isSuccess = true;
                        return Request.CreateResponse(HttpStatusCode.OK, new { data, response });
                    }
                    else
                    {
                        response.serverResponseTime = System.DateTime.Now;
                        response.isSuccess = false;
                        return Request.CreateResponse(HttpStatusCode.BadRequest, new { response });
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetFeedbackSummary(CustomerModel model)
        {
            ResponseStatus response = new ResponseStatus();
            //OrderModel data = new OrderModel();
            try
            {
                if (model.StoreId > 0)
                {
                    var data = _repository.GetFeedbackSummary(model.StoreId);
                    if (data != null)
                    {
                        response.serverResponseTime = System.DateTime.Now;
                        response.isSuccess = true;
                        return Request.CreateResponse(HttpStatusCode.OK, new { data, response });
                    }
                    else
                    {
                        response.serverResponseTime = System.DateTime.Now;
                        response.isSuccess = false;
                        return Request.CreateResponse(HttpStatusCode.BadRequest, new { response });
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
            }
        }

    }
}
