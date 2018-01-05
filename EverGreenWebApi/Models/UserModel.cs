using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class UserModel
    {
        public int LoginId { get; set; }
        public string UserName{ get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
        public int Role { get; set; }
        public int StoreId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}