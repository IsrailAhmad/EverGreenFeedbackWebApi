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
    public class CustomerController : ApiController
    {
        static readonly ICustomerRepository _repository = new CustomerRepository();

        [HttpPost]
        public HttpResponseMessage AddCustomer(CustomerModel model)
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                if (model.PhoneNumber !="" && model.StoreId > 0)
                {
                    var data = _repository.AddCustomer(model.PhoneNumber, model.StoreId,model.CustomerName);
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
    }
}
