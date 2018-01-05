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
    public class QuestionController : ApiController
    {

        static readonly IQuestionRepository _repository = new QuestionRepository();

        [HttpPost]
        public HttpResponseMessage GetAllFeedbackQuestionList()
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                var data = _repository.GetAllQuestionList();
                if (data.Count() > 0)
                {
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
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddQuestion(QuestionModel model)
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                var data = _repository.AddQuestion(model);
                if (data.id > 0)
                {
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
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
            }
        }

        // question delete by id (lalit & deepak)..

        [HttpPost]
        public HttpResponseMessage DeleteQuestion(QuestionModel model)
        {
            try
            {
                if (model.id != 0)
                {
                    var result = _repository.DeleteQuestionByID(model.id);
                    if (result.isSuccess == true)
                    {

                        return Request.CreateResponse(HttpStatusCode.OK, new { result });
                    }
                    else
                    {

                        return Request.CreateResponse(HttpStatusCode.BadRequest, new { result });
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
        public HttpResponseMessage QuestionDisableEnable(QuestionModel model)
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                var data = _repository.QuestionDisableEnable(model);
                if (data.id > 0)
                {
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
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
            }
        }
        //14-10-17 by lalit & deepak
        [HttpPost]
        public HttpResponseMessage QuestionUpdate(QuestionModel model)
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                if (model != null)
                {

                    var data = _repository.QuestionUpdate(model);
                    if (data.id > 0)
                    {
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
                    response.isSuccess = false;
                    response.serverResponseTime = System.DateTime.Now;
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { response });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
            }
        }
        // arrange questions 
        [HttpPost]
        public HttpResponseMessage ArrangeQuestion(QuestionModel model)
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                var data = _repository.ArrangeQuestion(model.id,model.PositionId);
                if (data != null)
                {
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
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
            }
        }
    }
}
