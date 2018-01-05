using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Interfaces
{
    public interface IUserRepository:IDisposable
    {
        UserModel UserLogin(string username, string password, int role, int storeid);
    }
}