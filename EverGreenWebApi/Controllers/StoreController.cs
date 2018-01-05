using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using EverGreenWebApi.Repository;

namespace EverGreenWebApi.Controllers
{
    public class StoreController : ApiController
    {
        static readonly IStoreRepository _repository = new StoreRepository();

        [HttpPost]
        public HttpResponseMessage GetAllStoreList()
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                var data = _repository.GetAllStoreList();
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

        //[HttpPost]
        //public HttpResponseMessage GetStoreDetailsById(StoreModel store)
        //{
        //    var data = new StoreModel();
        //    var response = new ResponseStatus();
        //    try
        //    {
        //        if (store.StoreId > 0)
        //        {
        //            var result = _repository.GetStoreDetailsById(store.StoreId);
        //            if (result != null)
        //            {
        //                data.StoreId = result.StoreId;
        //                data.StoreName = result.StoreName;
        //                data.StorePhoneNumber = result.StorePhoneNumber;
        //                data.StoreEmailId = result.StoreEmailId;
        //                data.StoreAddress = result.StoreAddress;
        //                data.LocalityId = result.LocalityId;
        //                data.StorePicturesUrl = result.StorePicturesUrl;
        //                response.serverResponseTime = System.DateTime.Now;
        //                response.isSuccess = true;
        //                return Request.CreateResponse(HttpStatusCode.OK, new { data, response });
        //            }
        //            else
        //            {
        //                response.serverResponseTime = System.DateTime.Now;
        //                response.isSuccess = false;
        //                return Request.CreateResponse(HttpStatusCode.BadRequest, new { response });
        //            }
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
        //    }
        //}

        //[HttpPost]
        //public HttpResponseMessage GetFavouriteStoreByUser(UserModel user)
        //{
        //    var data = new StoreModel();
        //    var response = new ResponseStatus();
        //    try
        //    {
        //        if (user.LoginID > 0)
        //        {
        //            var result = _repository.GetFavouriteStoreByUser(user.LoginID);
        //            if (result != null)
        //            {
        //                data.StoreId = result.StoreId;
        //                data.StoreName = result.StoreName;
        //                data.StorePhoneNumber = result.StorePhoneNumber;
        //                data.StoreEmailId = result.StoreEmailId;
        //                data.StoreAddress = result.StoreAddress;
        //                data.LocalityId = result.LocalityId;
        //                data.FavouriteStore = result.FavouriteStore;
        //                data.StorePicturesUrl = result.StorePicturesUrl;
        //                response.serverResponseTime = System.DateTime.Now;
        //                response.isSuccess = true;
        //                return Request.CreateResponse(HttpStatusCode.OK, new { data, response });
        //            }
        //            else
        //            {
        //                response.serverResponseTime = System.DateTime.Now;
        //                response.isSuccess = false;
        //                return Request.CreateResponse(HttpStatusCode.BadRequest, new { response });
        //            }
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
        //    }
        //}

        //[HttpPost]
        //public HttpResponseMessage AddUpdateFavouriteStoreByUser(UserModel user)
        //{
        //    var data = new StoreModel();
        //    var response = new ResponseStatus();
        //    try
        //    {
        //        if (user.LoginID > 0 && user.FavouriteStoreId > 0)
        //        {
        //            var result = _repository.AddUpdateFavouriteStoreByUser(user.LoginID,user.FavouriteStoreId);
        //            if (result.StoreId > 0)
        //            {
        //                data.StoreId = result.StoreId;
        //                data.StoreName = result.StoreName;
        //                data.StorePhoneNumber = result.StorePhoneNumber;
        //                data.StoreEmailId = result.StoreEmailId;
        //                data.StoreAddress = result.StoreAddress;
        //                data.LocalityId = result.LocalityId;
        //                data.FavouriteStore = result.FavouriteStore;
        //                data.StorePicturesUrl = result.StorePicturesUrl;
        //                response.serverResponseTime = System.DateTime.Now;
        //                response.isSuccess = true;
        //                return Request.CreateResponse(HttpStatusCode.OK, new { data, response });
        //            }
        //            else
        //            {
        //                response.serverResponseTime = System.DateTime.Now;
        //                response.isSuccess = false;
        //                return Request.CreateResponse(HttpStatusCode.BadRequest, new { response });
        //            }
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
        //    }
        //}


        //[HttpPost]
        //public HttpResponseMessage RemoveFavouriteStoreByUser(UserModel user)
        //{

        //    var response = new ResponseStatus();
        //    try
        //    {
        //        if (user.LoginID > 0)
        //        {
        //            var result = _repository.RemoveFavouriteStoreByUser(user.LoginID);
        //            if (result != null)
        //            {                       
        //                response.serverResponseTime = System.DateTime.Now;
        //                response.isSuccess = true;
        //                return Request.CreateResponse(HttpStatusCode.OK, new { response });
        //            }
        //            else
        //            {
        //                response.serverResponseTime = System.DateTime.Now;
        //                response.isSuccess = false;
        //                return Request.CreateResponse(HttpStatusCode.BadRequest, new { response });
        //            }
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
        //    }
        //}
    }
}
