using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class CustomerEventRepository : ICustomerEventRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public CustomerEventModel AddEventCustomer(CustomerEventModel model)
        {

            CustomerEventModel eventdata = new CustomerEventModel();
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                int customerid;
                customereventmaster u = new customereventmaster();
                u.Gathering = model.Gathering;
                u.Time = model.Time;
                u.Comment = model.Comment;
                u.StoreId = model.StoreId;
                u.EventDate = model.EventDate;
                //u.CustomerId = model.CustomerId;
                customermaster c = new customermaster();
                c.CustomerName = model.Name;
                c.CustomerEmailId = model.Email;
                c.PhoneNumber = model.Phone;
                c.StoreId = model.StoreId;
                //var cust = context.customermasters.Where(x=> x.PhoneNumber == model.Phone && x.StoreId == model.StoreId).FirstOrDefault();
                //if (cust != null)
                //{
                //    u.CustomerId = cust.CustomerId;              
                //    eventdata.CustomerId = cust.CustomerId;
                //    eventdata.Name = cust.CustomerName;
                //    eventdata.Phone = cust.PhoneNumber;
                //    eventdata.Email = cust.CustomerEmailId;                  

                //    var eventresult = context.customereventmasters.Where(x => x.StoreId == cust.StoreId && x.CustomerId == cust.CustomerId).FirstOrDefault();
                //    if (eventresult != null)
                //    {
                //        context.Entry(eventresult).State = System.Data.Entity.EntityState.Modified;
                //        context.SaveChanges();
                //        eventdata.CustomerId = (int)eventresult.CustomerId;
                //        eventdata.StoreId = (int)eventresult.StoreId;                        
                //        eventdata.Gathering = (int)eventresult.Gathering;
                //        eventdata.Comment = eventresult.Comment;
                //        eventdata.EventDate = (DateTime)eventresult.EventDate;
                //        eventdata.Time = eventresult.Time;
                //    }
                //    else
                //    {
                //        context.customereventmasters.Add(u);
                //    }
                //    var result = context.SaveChanges();                   
                //    if (result > 0)
                //    {
                //        var data = context.customereventmasters.Where(x => x.StoreId == u.StoreId && x.CustomerId == cust.CustomerId).FirstOrDefault();
                //        eventdata.CustomerId = (int)data.CustomerId;
                //        eventdata.StoreId = (int)data.StoreId;                        
                //        eventdata.Gathering =(int) data.Gathering;
                //        eventdata.Comment = data.Comment;
                //        eventdata.EventDate = (DateTime)data.EventDate;
                //        eventdata.Time = data.Time;

                //    }
                //}
                //else
                //{
                context.customermasters.Add(c);
                //}
                var resultcust = context.SaveChanges();
                var id = c.CustomerId;
                if (resultcust > 0)
                {
                    var cus = context.customermasters.Where(cs=>cs.CustomerId == id).SingleOrDefault();

                    eventdata.CustomerId = cus.CustomerId;
                    eventdata.Name = cus.CustomerName;
                    eventdata.Phone = cus.PhoneNumber;
                    eventdata.Email = cus.CustomerEmailId;
                    eventdata.StoreId = (int)cus.StoreId;


                    //var eventresult = context.customereventmasters.Where(x => x.StoreId == model.StoreId && x.CustomerId == model.CustomerId).FirstOrDefault();
                    //if (eventresult != null)
                    //{
                    //    context.Entry(eventresult).State = System.Data.Entity.EntityState.Modified;
                    //    context.SaveChanges();

                    //    eventdata.Gathering = (int)eventresult.Gathering;
                    //    eventdata.Comment = eventresult.Comment;
                    //    eventdata.StoreId = (int)eventresult.StoreId;
                    //    eventdata.EventDate = (DateTime)eventresult.EventDate;
                    //    eventdata.Time = eventresult.Time;
                    //}
                    //else
                    //{
                    u.CustomerId = cus.CustomerId;
                    context.customereventmasters.Add(u);
                    //}
                    var result = context.SaveChanges();
                    if (result > 0)
                    {
                        var data = context.customereventmasters.Where(x => x.StoreId == model.StoreId && x.CustomerId == cus.CustomerId).FirstOrDefault();

                        eventdata.Gathering = (int)data.Gathering;
                        eventdata.StoreId = (int)data.StoreId;
                        eventdata.Comment = data.Comment;
                        eventdata.EventDate = (DateTime)data.EventDate;
                        eventdata.Time = data.Time;
                    }
                }
            }
            return eventdata;
        }

        //public EventModel CustomerSeletedEvent(CustomerEventModel model)
        //{
        //    using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
        //    {
        //        EventModel eventdata = new EventModel();
        //        var data = context.customereventmasters.Where(x => x.CustomerId == model.CustomerId && x.StoreId == model.StoreId);
        //        //customereventmaster c = new customereventmaster();
        //        //c.CustomerEventId = model.EventId;
        //        if (data != null)
        //        {
        //            foreach (var item in data)
        //            {
        //                item.EventId = model.EventId;
        //            }

        //            try
        //            {
        //                var result = context.SaveChanges();
        //                if (result > 0)
        //                {
        //                    var temp = (from x in context.customereventmasters
        //                                join e in context.eventmasters on x.CustomerEventId equals e.EventId
        //                                where x.CustomerId == model.CustomerId && x.StoreId == model.StoreId && e.EventId == model.EventId
        //                                select e).SingleOrDefault();

        //                    if (temp != null)
        //                    {
        //                        eventdata.EventId = temp.EventId;
        //                        eventdata.EventName = temp.EventName;
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {

        //            }
        //        }
        //        return eventdata;
        //    }
        //}
    }
}