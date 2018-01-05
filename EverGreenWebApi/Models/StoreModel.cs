using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class StoreModel
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string StorePhoneNumber { get; set; }
        public string StoreEmailId { get; set; }
        public string StoreAddress { get; set; }
        //public int LocalityId { get; set; }
        //public bool FavouriteStore { get; set; }
        public string StorePicturesUrl { get; set; }
        //public int LoginId { get; set; }
    }
}