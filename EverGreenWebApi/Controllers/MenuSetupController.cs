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
    // GET: MenuSetup
    public class MenuSetupController : ApiController
    {
        // GET: MenuSetup
        static readonly IMenuSetupRepository _repository = new MenuSetupRepository();

        [HttpGet]
        public HttpResponseMessage SetupMenu(Int32 MenuId, Int32 CategoryId, Int32 ProductId)
        {
            MenuSetupModel model = new MenuSetupModel();
            model.MenuId = MenuId;
            model.CategoryId = CategoryId;
            model.ProductId = ProductId;
            ResponseStatus response = new ResponseStatus();
            try
            {
                var data = _repository.SetupMenu(model);
                if (data != null)
                {
                    //response.isSuccess = true;
                    //response.serverResponseTime = System.DateTime.Now;
                    return Request.CreateResponse(HttpStatusCode.OK, new { data });
                }
                else
                {
                    //response.isSuccess = false;
                    //response.serverResponseTime = System.DateTime.Now;
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { data });
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
            }
        }

        [HttpGet]
        public HttpResponseMessage RemoveSetupMenu(int Id)
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                var data = _repository.RemoveSetupMenu(Id);
                if (data != null)
                {
                    //response.isSuccess = true;
                    //response.serverResponseTime = System.DateTime.Now;
                    return Request.CreateResponse(HttpStatusCode.OK, new { data });
                }
                else
                {
                    //response.isSuccess = false;
                    //response.serverResponseTime = System.DateTime.Now;
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { data });
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetAllMenuList(int menuid)
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                var data = _repository.GetAllMenuList(menuid);
                if (data != null)
                {
                    //response.isSuccess = true;
                    //response.serverResponseTime = System.DateTime.Now;
                    return Request.CreateResponse(HttpStatusCode.OK, new { data });
                }
                else
                {
                    //response.isSuccess = false;
                    //response.serverResponseTime = System.DateTime.Now;
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { data });
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
            }
        }
    }
}
