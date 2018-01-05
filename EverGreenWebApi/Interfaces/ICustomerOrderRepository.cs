using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Interfaces
{
    public interface ICustomerOrderRepository:IDisposable
    {
        CustomerOrderModel OrderPlaced(CustomerOrderModel model);
        CustomerOrderModel GetAllEventOrderByOrderNumber(string OderNumber);
        IEnumerable<CustomerOrderModel> GetAllEventOrderList(int storeid);
        ResponseStatus EventOrderDelete(string strOrderNumber, int customerid, int storeid);
        CustomerOrderModel EventOrderUpdate(CustomerOrderModel model);
        CustomerOrderModel PrintReceipt(PrintReceiptModel model);
    }
}