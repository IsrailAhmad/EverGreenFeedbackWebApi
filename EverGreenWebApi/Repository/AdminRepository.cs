using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class AdminRepository : IAdminRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerModel> GetAllCustomerList(int storeid, DateTime df)
        {
            CustomerModel cust = new CustomerModel();
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                DateTime startDate = new DateTime(df.Year, df.Month, df.Day, 0, 0, 0);
                DateTime endDate = new DateTime(df.Year, df.Month, df.Day, 23, 59, 59);               

                var result = (from a in context.adminmasters
                              join c in context.customermasters on a.AdminId equals c.AdminId
                              where a.StoreId == storeid && (c.CreatedOn >= startDate && c.CreatedOn < endDate)
                              select c).ToList();
                
                var data = result.Select(s => new CustomerModel()
                {
                    CustomerId = s.CustomerId,
                    CustomerName = s.CustomerName,
                    PhoneNumber = s.PhoneNumber,
                    Role = (int)s.Role,
                    CustomerEmailId = s.CustomerEmailId,
                    StoreId = (int)s.StoreId,
                    CreatedOn = (DateTime)s.CreatedOn
                }).ToList();
                return data;
            }
        }


    }
}

