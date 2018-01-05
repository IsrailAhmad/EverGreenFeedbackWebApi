using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public UserModel UserLogin(string username,string password,int role,int storeid)
        {
            UserModel data = new UserModel();
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                var result = context.usermasters.Where(x => x.UserName == username && x.Password == password && x.Role == role && x.StoreId == storeid).FirstOrDefault();
                if (result.LoginId > 0)
                {
                    data.LoginId = result.LoginId;
                    data.UserName = result.UserName;
                    data.EmailId = result.EmailId;
                    data.Role = (int)result.Role;
                }
                return data;
            }
        }


    }
}