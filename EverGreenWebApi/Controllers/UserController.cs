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
    public class UserController : ApiController
    {
        static readonly IUserRepository _repository = new UserRepository();

        [HttpPost]
        public HttpResponseMessage Login(UserModel user)
        {
            ResponseStatus response = new ResponseStatus();
            UserModel data = new UserModel();
            try
            {
                if (user.UserName != null && user.Password != null && user.Role > 0 && user.StoreId > 0)
                {
                    var result = _repository.UserLogin(user.UserName,user.Password,user.Role,user.StoreId);
                    if (result != null)
                    {
                        data.LoginId = result.LoginId;
                        data.UserName = result.UserName;
                        data.EmailId = result.EmailId;
                        data.Role = result.Role;
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
