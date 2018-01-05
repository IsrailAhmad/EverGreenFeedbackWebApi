using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class ExtraRepository : IExtraRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExtraModel> GetAllExtraCharges(int storeid)
        {
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                var result = context.extrachargemasters.Where(x=>x.StoreId == storeid).OrderBy(s => s.ExtraId);

                var data = result.Select(s => new ExtraModel()
                {                    
                 ExtraId =s.ExtraId,
                 ExtraName =s.ExtraName   
                }).ToList();
                return data;
            }
        }
    }
}