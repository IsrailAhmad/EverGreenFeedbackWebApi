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
    public class RemarksController : ApiController
    {
        static readonly IRemarksRepository _repository = new RemarksRepository();

        [HttpPost]
        public HttpResponseMessage AddRemarks(RemarksModel model)
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                if (model.CustomerId > 0 && model.StoreId > 0 && model.Remarks != null)
                {
                    var data = _repository.AddRemarks(model.CustomerId, model.StoreId, model.Remarks);
                    if (data != null)
                    {
                        //response.Message = "Awesome we appericiate your feedback";
                        response.isSuccess = true;
                        response.serverResponseTime = System.DateTime.Now;
                        return Request.CreateResponse(HttpStatusCode.OK, new {response });
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
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please Check Store Id !");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
            }
        }
    }
}
