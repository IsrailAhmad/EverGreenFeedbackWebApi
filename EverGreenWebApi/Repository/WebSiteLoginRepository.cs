using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class WebSiteLoginRepository : IWebSiteLoginRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public UserLoginModel WebsiteLogin(string username, string password)
        {
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                var result = context.website_login.Where(u => u.UserName == username && u.Password == password && u.IsActive == "Y")
                    .Select(u => new UserLoginModel()
                {
                    UserID = u.UserId,
                    UserName = u.UserName,
                    FirstName = u.FisrtName,
                    LastName = u.LastName
                }).FirstOrDefault();
                return result;
            }
        }
    }
}