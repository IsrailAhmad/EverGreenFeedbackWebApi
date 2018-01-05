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
    public class AdminController : ApiController
    {
        static readonly IAdminRepository _repository = new AdminRepository();

        [HttpPost]
        public HttpResponseMessage GetAllCustomerList(AdminModel model)
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                if (model.StoreId > 0 && model.CreatedOn != null)
                {
                    var data = _repository.GetAllCustomerList(model.StoreId, model.CreatedOn);
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
    }
}
