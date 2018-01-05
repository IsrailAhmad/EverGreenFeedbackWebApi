using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using EverGreenWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace EverGreenWebApi.Controllers
{
    public class CustomerOrderController : ApiController
    {
        static readonly ICustomerOrderRepository _repository = new CustomerOrderRepository();

        [HttpPost]
        public HttpResponseMessage OrderPlaced(CustomerOrderModel model)
        {
            ResponseStatus response = new ResponseStatus();
            //OrderModel data = new OrderModel();
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _repository.OrderPlaced(model);
                    if (data.OrderId > 0)
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
        public HttpResponseMessage GetAllEventOrderList(CustomerOrderModel model)
        {
            ResponseStatus response = new ResponseStatus();
            //OrderModel data = new OrderModel();
            try
            {
                if (model.StoreId > 0)
                {
                    var data = _repository.GetAllEventOrderList(model.StoreId);
                    if (data != null)
                    {
                        //data.OrderId = result.OrderId;
                        //data.OrderNumber = result.OrderNumber;
                        //data.StoreId = (int)result.StoreId;
                        //data.ProductId = (int)result.ProductId;
                        //data.AddressId = (int)result.AddressId;
                        //data.Quantity = (double)result.Quantity;
                        //data.LoginId = (int)result.LoginId;
                        //data.OrderTime = result.OrderTime;
                        //data.TotalPrice = (double)result.TotalPrice;
                        //data.OrderStatus = result.OrderStatus;
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
        public HttpResponseMessage GetAllEventOrderByOrderNumber(CustomerOrderModel model)
        {
            ResponseStatus response = new ResponseStatus();
            //OrderModel data = new OrderModel();
            try
            {
                if (model.OrderNumber != null)
                {
                    var data = _repository.GetAllEventOrderByOrderNumber(model.OrderNumber);
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

        [HttpPost]
        public HttpResponseMessage EventOrderDelete(CustomerOrderModel model)
        {
            try
            {
                if (model.OrderNumber.ToString() != null || model.OrderNumber != "")
                {
                    var result = _repository.EventOrderDelete(model.OrderNumber, model.CustomerId, model.StoreId);
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
        public HttpResponseMessage EventOrderUpdate(CustomerOrderModel model)
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _repository.EventOrderUpdate(model);
                    if (data.OrderId > 0)
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
        public HttpResponseMessage PrintReceipt(PrintReceiptModel model)
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _repository.PrintReceipt(model);
                    if (data.OrderId > 0)
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
