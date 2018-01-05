using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Interfaces
{
    public interface ICustomerRepository:IDisposable
    {
        CustomerModel AddCustomer(string phonenumber, int storeid, string customername);
    }
}