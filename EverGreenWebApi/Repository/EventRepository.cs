using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class EventRepository : IEventRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventModel> GetAllEventList(int storeid)
        {
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                var result = context.eventmasters.Where(x => x.StoreId == storeid).OrderBy(s => s.EventName);

                var data = result.Select(s => new EventModel()
                {
                    EventId = s.EventId,
                    EventName = s.EventName,
                    EventPrice = (decimal)s.EventPrice,
                    StoreId = (int)s.StoreId
                }).ToList();
                return data;
            }
        }
        public IEnumerable<CustomerBookedEventModel> GetAllBookedEventsbyDate(int storeid, DateTime df)
        {
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                int year = df.Year;
                int month = df.Month;
                int day = df.Day;

                //DateTime startDate = new DateTime(df.Year, df.Month, df.Day);
                //DateTime endDate = new DateTime(df.Year, df.Month, df.Day);
                var data = (from o in context.customerordermasters
                            join e in context.customereventmasters on o.CustomerId equals e.CustomerId into J1
                            from j2 in J1.DefaultIfEmpty()
                            join c in context.customermasters on o.CustomerId equals c.CustomerId into j3
                            from j4 in j3.DefaultIfEmpty()
                            where j2.StoreId == storeid
                        && j2.EventDate.Value.Year == df.Year
                        && j2.EventDate.Value.Month == df.Month
                        && j2.EventDate.Value.Day == df.Day
                            select new CustomerBookedEventModel()
                            {
                                EventDate = (DateTime)j2.EventDate,
                                Time = j2.Time,
                                OrderId = o.OrderId,
                                OrderNumber = o.OrderNumber,
                                CustomerName = j4.CustomerName,
                                PhoneNumber = j4.PhoneNumber,
                                StoreId = (int)j2.StoreId,
                            }).ToList();
                return data;
            }
        }

        public IEnumerable<CustomerBookedEventModel> AllBookedEventsOnADate(DateTime df)
        {
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                int year = df.Year;
                int month = df.Month;
                int day = df.Day;

                //DateTime startDate = new DateTime(df.Year, df.Month, df.Day);
                //DateTime endDate = new DateTime(df.Year, df.Month, df.Day);
                var data = (from o in context.customerordermasters
                            join e in context.customereventmasters on o.CustomerId equals e.CustomerId into J1
                            from j2 in J1.DefaultIfEmpty()
                            join c in context.customermasters on o.CustomerId equals c.CustomerId into j3
                            from j4 in j3.DefaultIfEmpty()
                            where j2.EventDate.Value.Year == df.Year
                        && j2.EventDate.Value.Month == df.Month
                        && j2.EventDate.Value.Day == df.Day
                            select new CustomerBookedEventModel()
                            {
                                EventDate = (DateTime)j2.EventDate,
                                Time = j2.Time,
                                OrderId = o.OrderId,
                                OrderNumber = o.OrderNumber,
                                CustomerName = j4.CustomerName,
                                PhoneNumber = j4.PhoneNumber,
                                StoreId = (int)j2.StoreId,
                            }).ToList();
                return data;
            }
        }
        //public IEnumerable<SmsReceiverModel> AllSmsReceiverList()
        //{
        //    IEnumerable<SmsReceiverModel> list;
        //    using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
        //    {
        //        //return list = context.smsreceivermasters.Where(a => a.IsActive == "Y").Select(s => new SmsReceiverModel()
        //        //{
        //        //    ReceiverId =s.ReceiverId,
        //        //    ReceiverName = s.Name,
        //        //    PhoneNumber = s.PhoneNumber
        //        //}).ToList();
        //    }
        //}
        public void SendSMS(string phonenumber, string customername, string ordernumber, string eventdate, string eventtime)
        {
            string _user = HttpUtility.UrlEncode("shamsweet"); // API user name to send SMS
            string _pass = HttpUtility.UrlEncode("12345");     // API password to send SMS
            string _route = HttpUtility.UrlEncode("transactional");
            string _senderid = HttpUtility.UrlEncode("WISHHH");
            string _recipient = HttpUtility.UrlEncode(phonenumber);  // who will receive message
            string _messageText = HttpUtility.UrlEncode("Dear " + customername + "\nYour Order has been generated: " + Convert.ToString(ordernumber) + "\nYour Scheduled Event on: " + eventdate + "\nTime: " + eventtime + "\nThanks & Regards\nEverGreen"); // text message

            // Creating URL to send sms
            string _createURL = "http://www.smsnmedia.com/api/push?user=" + _user + "&pwd=" + _pass + "&route=" + _route + "&sender=" + _senderid + "&mobileno=91" + _recipient + "&text=" + _messageText;

            HttpWebRequest _createRequest = (HttpWebRequest)WebRequest.Create(_createURL);
            // getting response of sms
            HttpWebResponse myResp = (HttpWebResponse)_createRequest.GetResponse();
            System.IO.StreamReader _responseStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = _responseStreamReader.ReadToEnd();
            _responseStreamReader.Close();
            myResp.Close();
        }

        //public ResponseStatus AutoSendSms(DateTime date)
        //{
        //    ResponseStatus response = new ResponseStatus();
        //    try
        //    {
        //        IEnumerable<CustomerBookedEventModel> eventList = AllBookedEventsOnADate(date);
        //        IEnumerable<SmsReceiverModel> receiverList = AllSmsReceiverList();
        //        foreach (var e in eventList)
        //        {
        //            foreach (var r in receiverList)
        //            {
        //                if (r.ReceiverId == 1)
        //                {
        //                    if (e.PhoneNumber == "9555740041")
        //                    {
        //                        SendSMS(r.PhoneNumber, e.CustomerName, e.OrderNumber, Convert.ToDateTime(e.EventDate.ToString("dd-MM-yyyy")).ToString(), e.Time);
        //                    }

        //                }

        //            }
        //        }
        //        response.isSuccess = true;
        //        response.serverResponseTime = DateTime.Now;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.isSuccess = true;
        //        response.serverResponseTime = DateTime.Now;
        //    }
        //    return response;
        //}
    }
}
