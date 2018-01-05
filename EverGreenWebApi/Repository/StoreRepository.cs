using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;

namespace EverGreenWebApi.Repository
{
    public class StoreRepository : IStoreRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        string path = "http://103.233.79.234/Data/EverGreenFeedback_Android/StorePictures/";

        public IEnumerable<StoreModel> GetAllStoreList()
        {
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                var result = context.storemasters.OrderBy(s => s.StoreName);

                var data = result.Select(s => new StoreModel()
                {
                    StoreId = s.StoreId,
                    StoreName = s.StoreName,
                    StoreEmailId = s.StoreEmailId,
                    StorePhoneNumber = s.StorePhoneNumber,
                    StoreAddress = s.StoreAddress,
                    //LocalityId = (int)s.LocalityId,
                    StorePicturesUrl = path + s.StoreId + "StorePictures.jpg",
                    //FavouriteStore = favouriteStore == s.StoreId ? true : false,
                }).ToList();
                return data;
            }
        }

        //public StoreModel GetStoreDetailsById(int storeid)
        //{
        //    StoreModel store = new StoreModel();
        //    using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
        //    {
        //        var result = context.storemasters.Where(s => s.StoreId == storeid).FirstOrDefault();
        //        if (result.StoreId > 0)
        //        {
        //            store.StoreId = result.StoreId;
        //            store.StoreName = result.StoreName;
        //            store.StoreEmailId = result.StoreEmailId;
        //            store.StorePhoneNumber = result.StorePhoneNumber;
        //            store.StoreAddress = result.StoreAddress;
        //            store.LocalityId = (int)result.LocalityId;
        //            store.StorePicturesUrl = path + result.StoreId + "StorePictures.jpg";
        //        }
        //    }
        //    return store;
        //}

        //public StoreModel GetFavouriteStoreByUser(int loginid)
        //{
        //    StoreModel store = new StoreModel();
        //    using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
        //    {
        //        var result = (from u in context.registrationmasters
        //                      join s in context.storemasters on u.FavouriteStoreId equals s.StoreId
        //                      where u.LoginID == (long)loginid
        //                      select s).FirstOrDefault();

        //        if (result.StoreId > 0)
        //        {
        //            store.StoreId = result.StoreId;
        //            store.StoreName = result.StoreName;
        //            store.StoreEmailId = result.StoreEmailId;
        //            store.StorePhoneNumber = result.StorePhoneNumber;
        //            store.StoreAddress = result.StoreAddress;
        //            store.LocalityId = (int)result.LocalityId;
        //            store.FavouriteStore = true;
        //            store.StorePicturesUrl = path + result.StoreId + "StorePictures.jpg";
        //        }
        //    }
        //    return store;
        //}

        //public StoreModel AddUpdateFavouriteStoreByUser(int loginid, int storeid)
        //{
        //    StoreModel store = new StoreModel();
        //    using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
        //    {

        //        if (loginid > 0 && storeid > 0)
        //        {
        //            var data = context.registrationmasters.Where(w => w.LoginID.Equals(loginid));
        //            foreach (var item in data)
        //            {
        //                item.FavouriteStoreId = storeid;
        //            }
        //            try
        //            {
        //                context.SaveChanges();
        //                var result = (from u in context.registrationmasters
        //                              join s in context.storemasters on u.FavouriteStoreId equals s.StoreId
        //                              where u.LoginID == (long)loginid && u.FavouriteStoreId == storeid
        //                              select s).FirstOrDefault();

        //                if (result.StoreId > 0)
        //                {
        //                    store.StoreId = result.StoreId;
        //                    store.StoreName = result.StoreName;
        //                    store.StoreEmailId = result.StoreEmailId;
        //                    store.StorePhoneNumber = result.StorePhoneNumber;
        //                    store.StoreAddress = result.StoreAddress;
        //                    store.LocalityId = (int)result.LocalityId;
        //                    store.FavouriteStore = true;
        //                    store.StorePicturesUrl = path + result.StoreId + "StorePictures.jpg";
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                //Handle ex
        //            }
        //        }
        //    }
        //    return store;
        //}

        //public ResponseStatus RemoveFavouriteStoreByUser(int loginid)
        //{
        //    ResponseStatus response = new ResponseStatus();
        //    using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
        //    {

        //        if (loginid > 0)
        //        {
        //            var data = context.registrationmasters.FirstOrDefault(w => w.LoginID == loginid);
        //            if (data != null)
        //            {
        //                data.FavouriteStoreId = 0;
        //                context.SaveChanges();
        //                response.isSuccess = true;
        //                response.serverResponseTime = System.DateTime.Now;
        //            }
        //            else
        //            {
        //                response.isSuccess = false;
        //                response.serverResponseTime = System.DateTime.Now;
        //            }
        //        }
        //    }
        //    return response;
        //}
    }
}