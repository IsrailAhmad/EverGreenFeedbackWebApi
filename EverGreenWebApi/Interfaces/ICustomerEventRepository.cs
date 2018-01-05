using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Interfaces
{
    public interface ICustomerEventRepository:IDisposable
    {
        CustomerEventModel AddEventCustomer(CustomerEventModel model);
        //EventModel CustomerSeletedEvent(CustomerEventModel model);
    }
}