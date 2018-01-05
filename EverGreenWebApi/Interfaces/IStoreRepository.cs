using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EverGreenWebApi.Models;

namespace EverGreenWebApi.Interfaces
{
    public interface IStoreRepository: IDisposable
    {
        IEnumerable<StoreModel> GetAllStoreList();
        //StoreModel GetStoreDetailsById(int storeid);
        //StoreModel GetFavouriteStoreByUser(int loginid);
        //StoreModel AddUpdateFavouriteStoreByUser(int loginid,int storeid);
        //ResponseStatus RemoveFavouriteStoreByUser(int loginid);
    }
}