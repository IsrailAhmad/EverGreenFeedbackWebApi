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
    public class EventController : ApiController
    {
        static readonly IEventRepository _repository = new EventRepository();

        [HttpPost]
        public HttpResponseMessage GetAllEventList(EventModel model)
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                var data = _repository.GetAllEventList(model.StoreId);
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
        public HttpResponseMessage GetAllBookedEventsbyDate(CustomerBookedEventModel model)
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                if (model.EventDate != null && model.StoreId > 0)
                {

                    var data = _repository.GetAllBookedEventsbyDate(model.StoreId, model.EventDate);
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

        //[HttpGet]
        //public HttpResponseMessage AutoSmsOnADay(DateTime date)
        //{
        //    ResponseStatus response = new ResponseStatus();
        //    try
        //    {

        //        var data = _repository.AutoSendSms(date);
        //        if (data.isSuccess == true)
        //        {
        //            response.isSuccess = true;
        //            response.serverResponseTime = System.DateTime.Now;
        //            return Request.CreateResponse(HttpStatusCode.OK, new { response });
        //        }
        //        else
        //        {
        //            response.isSuccess = false;
        //            response.serverResponseTime = System.DateTime.Now;
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, new { response });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
        //    }
        //}
    }
}
