using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public CustomerModel AddCustomer(string phonenumber, int storeid, string customername)
        {

            CustomerModel cust = new CustomerModel();
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                customermaster u = new customermaster();
                u.PhoneNumber = phonenumber;
                u.CustomerName = customername;
                u.StoreId = storeid;
                u.AdminId = 1;
                u.Role = 1;
                //var data = context.customermasters.Where(x => x.PhoneNumber == u.PhoneNumber && x.StoreId == u.StoreId).FirstOrDefault();
                //if (data != null)
                //{
                //    //context.customermasters.Where(p => p.PhoneNumber == u.PhoneNumber && p.StoreId == u.StoreId).ToList().ForEach(x => x.PhoneNumber = u.PhoneNumber);
                //    cust.CustomerId = Convert.ToInt32(data.CustomerId);
                //    //cust.CustomerName = custdata.CustomerName;
                //    //cust.PhoneNumber = custdata.PhoneNumber;
                //    //cust.Role = Convert.ToInt32(custdata.Role);
                //    //cust.CustomerEmailId = cust.CustomerEmailId;
                //    //user.ProfilePictureUrl = "http://103.233.79.234/Data/EverGreen_Android/EverGreenProfilePicture/" + user.LoginID + "ProfilePicture.jpg";
                //}
                //else
                //{
                //context.customermasters.Add(u);
                //}
                context.customermasters.Add(u);
                var result = context.SaveChanges();
                //int id = u.CustomerId;
                if (result > 0)
                {
                    //var custdata = context.customermasters.Where(x => x.CustomerId == u.CustomerId).FirstOrDefault();
                    //if (custdata != null)
                    //{
                    cust.CustomerId = Convert.ToInt32(u.CustomerId);
                    //cust.CustomerName = custdata.CustomerName;
                    //cust.PhoneNumber = custdata.PhoneNumber;
                    //cust.Role = Convert.ToInt32(custdata.Role);
                    //cust.CustomerEmailId = cust.CustomerEmailId;
                    //user.ProfilePictureUrl = "http://103.233.79.234/Data/EverGreen_Android/EverGreenProfilePicture/" + user.LoginID + "ProfilePicture.jpg";
                    //}
                }
            }
            return cust;
        }
    }
}