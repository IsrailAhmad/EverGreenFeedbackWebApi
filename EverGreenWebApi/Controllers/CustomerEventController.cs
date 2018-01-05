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
    public class CustomerEventController : ApiController
    {
        static readonly ICustomerEventRepository _repository = new CustomerEventRepository();

        [HttpPost]
        public HttpResponseMessage AddEventCustomer(CustomerEventModel model)
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                if (model.Name != null && model.Email != null && model.Phone != null)
                {
                    var data = _repository.AddEventCustomer(model);
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


        //[HttpPost]
        //public HttpResponseMessage CustomerSelectedEvent(CustomerEventModel model)
        //{
        //    ResponseStatus response = new ResponseStatus();
        //    try
        //    {
        //        if (model.CustomerId > 0 && model.StoreId > 0 && model.EventId > 0)
        //        {
        //            var data = _repository.CustomerSeletedEvent(model);
        //            if (data != null)
        //            {
        //                response.isSuccess = true;
        //                response.serverResponseTime = System.DateTime.Now;
        //                return Request.CreateResponse(HttpStatusCode.OK, new { data, response });
        //            }
        //            else
        //            {
        //                response.isSuccess = false;
        //                response.serverResponseTime = System.DateTime.Now;
        //                return Request.CreateResponse(HttpStatusCode.BadRequest, new { response });
        //            }
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please Check Store Id !");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
        //    }
        //}


    }
}
