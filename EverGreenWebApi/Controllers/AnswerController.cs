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
    public class AnswerController : ApiController
    {
        static readonly IAnswerRepository _repository = new AnswerRespository();

        [HttpPost]
        public HttpResponseMessage GetAllFeedbackAnswerList()
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                var data = _repository.GetAllAnswerList();
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
    }
}
