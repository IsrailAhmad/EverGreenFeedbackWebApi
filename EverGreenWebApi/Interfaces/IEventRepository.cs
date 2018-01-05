using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Interfaces
{
    public interface IEventRepository:IDisposable
    {
        IEnumerable<EventModel> GetAllEventList(int storeid);
        IEnumerable<CustomerBookedEventModel> GetAllBookedEventsbyDate(int storeid,DateTime streventdate);
        //ResponseStatus AutoSendSms(DateTime date);
    }
}