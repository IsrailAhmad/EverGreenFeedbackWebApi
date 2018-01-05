using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class CustomerOrderRepository : ICustomerOrderRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        string strSequnceNumber;
        //public string GenerateOrderNumber()
        //{
        //    int _min = 100000;
        //    int _max = 999999;
        //    string strOrderNumber = string.Empty;
        //    Random _rdm = new Random();
        //    int rand = _rdm.Next(_min, _max);
        //    return strOrderNumber = "ODR" + rand;
        //}

        public string MakeIntoSequence(int i, int total_length, string prefix)
        {
            string output = i.ToString();
            int length_minus_prefix = total_length - prefix.Length;
            while (output.Length < length_minus_prefix)
                output = "0" + output;
            return prefix + output;
        }

        public CustomerOrderModel OrderPlaced(CustomerOrderModel model)
        {

            CustomerOrderModel data = new CustomerOrderModel();
            var maxseq = 0;
            //CustomerCategoryModel categorymodel = new CustomerCategoryModel();
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                var first = context.customerordermasters.Select(s => s.OrderId).FirstOrDefault();
                if (first == 0)
                {
                    strSequnceNumber = "ODR00001";
                }
                else
                {
                    maxseq = context.customerordermasters.Max(s => s.OrderId);
                }

                if (maxseq != null && maxseq > 0)
                {
                    strSequnceNumber = MakeIntoSequence(maxseq + 1, 8, "ODR");
                }
                else
                {
                    strSequnceNumber = "ODR00001";
                }
                customerordermaster o = new customerordermaster();
                string strOrderNumber = strSequnceNumber;
                decimal totalmenucharge = 0;
                decimal extracharges = 0;
                decimal discount = 0;
                decimal advanceprice = 0;
                decimal eventfee = 0;
                decimal total = 0;
                decimal grandtotal = 0;
                decimal netprice = 0;

                foreach (var item in model.Extra)
                {
                    extracharges = extracharges + item.ExtraCharges;
                }

                var menucharge = context.menumasters.Where(m => m.MenuId == model.MenuId).Select(s => s.MenuPrice).FirstOrDefault();
                var noofgathering = context.customereventmasters.Where(c => c.CustomerId == model.CustomerId).Select(s => s.Gathering).FirstOrDefault();
                if (model.CustomerPrice > 0)
                {
                    totalmenucharge = model.CustomerPrice * (int)noofgathering;
                }
                else
                {
                    totalmenucharge = (decimal)menucharge * (int)noofgathering;
                }
                var advancepaid = 0.0;
                //var advance = context.advancemasters.Where(x => x.OrderNumber == model.OrderNumber).Select(s => s.AdvancePrice).FirstOrDefault();
                //if (advance != null)
                //{
                //    advancepaid = (double)advance;
                //}
                //else
                //{
                //    advancepaid = 0.0;
                //}
                //var advance = context.advancemasters.Where(x => x.OrderNumber == model.OrderNumber).Select(s => s.AdvancePrice).ToList();

                //if (advance != null)
                //{
                //    foreach (var a in advance)
                //    {
                //        advancepaid += (double)a;
                //    }
                //}
                //else
                //{
                //    advancepaid = 0.0;
                //}               
                advanceprice = (decimal)advancepaid;
                eventfee = model.EventFee;
                total = extracharges + totalmenucharge + eventfee;
                if (model.DiscountPercent > 0)
                {
                    //discount = (model.DiscountPercent * total) / 100;
                    discount = (model.DiscountPercent / 100) * total;
                }
                else if (model.DiscountValue > 0)
                {
                    discount = model.DiscountValue;
                }
                else
                {
                    discount = 0;
                }
                grandtotal = total - discount;
                //netprice = grandtotal - (decimal)advancepaid;
                netprice = grandtotal;
                o.StoreId = model.StoreId;
                o.CustomerId = model.CustomerId;
                o.MenuId = model.MenuId;
                o.CustomerPrice = model.CustomerPrice;
                o.EventFee = eventfee;
                o.Discount = discount;
                //o.AdvancePrice = advanceprice;
                o.Total = total;
                o.GrandTotal = grandtotal;
                o.NetPrice = netprice;
                o.OrderNumber = strOrderNumber;
                o.DiscountPercent = model.DiscountPercent;
                o.DiscountValue = model.DiscountValue;
                context.customerordermasters.Add(o);

                foreach (var extra in model.Extra)
                {
                    customerextramaster e = new customerextramaster();
                    e.CustomerId = model.CustomerId;
                    e.StoreId = model.StoreId;
                    e.OrderNumber = strOrderNumber;
                    e.ExtraId = extra.ExtraId;
                    e.ExtraCharges = extra.ExtraCharges;
                    context.customerextramasters.Add(e);
                }

                foreach (var item in model.Category)
                {
                    customercategorymaster c = new customercategorymaster();
                    c.CategoryId = item.CategoryId;
                    c.OrderNumber = strOrderNumber;
                    context.customercategorymasters.Add(c);
                    foreach (var productitem in item.Product)
                    {
                        customerproductmaster p = new customerproductmaster();
                        p.ProductId = productitem.ProductId;
                        p.CategoryId = item.CategoryId;
                        p.OrderNumber = strOrderNumber;
                        context.customerproductmasters.Add(p);
                    }
                }

                if (model.CustomerExtraProducts != null && model.CustomerExtraProducts != "")
                {
                    string extraproduct = model.CustomerExtraProducts;
                    string s = model.CustomerExtraProducts;
                    string[] values = s.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        customerextraproductmaster p = new customerextraproductmaster();
                        values[i] = values[i].Trim();
                        p.ProductName = values[i];
                        p.CustomerId = model.CustomerId;
                        p.OrderNumber = strSequnceNumber;
                        context.customerextraproductmasters.Add(p);
                    }
                }

                var result = context.SaveChanges();

                string CustExtraProducts = "";
                var extraproductList = (from ep in context.customerextraproductmasters
                                        where ep.CustomerId == model.CustomerId && ep.OrderNumber == strSequnceNumber
                                        select new
                                        {
                                            ProductName = ep.ProductName
                                        }).ToList();
                foreach (var p in extraproductList)
                {
                    CustExtraProducts += p.ProductName + ",";
                }
                var order = (from x in context.customerordermasters
                             join c in context.customermasters on x.CustomerId equals c.CustomerId
                             join ev in context.customereventmasters on x.CustomerId equals ev.CustomerId into j1
                             from j2 in j1.DefaultIfEmpty()
                             join m in context.menumasters on x.MenuId equals m.MenuId into j3
                             from j4 in j3.DefaultIfEmpty()
                             where x.CustomerId == model.CustomerId && x.StoreId == model.StoreId
                             orderby x.OrderId descending
                             select new CustomerOrderModel()
                             {
                                 OrderId = x.OrderId,
                                 CustomerId = (int)x.CustomerId,
                                 customerName = c.CustomerName,
                                 CustomerPhoneNo = c.PhoneNumber,
                                 CustomerEmailId = c.CustomerEmailId,
                                 MenuId = (int)x.MenuId,
                                 MenuName = j4.MenuName,
                                 MenuPrice = (decimal)j4.MenuPrice,
                                 Gathering = (int)j2.Gathering,
                                 StoreId = (int)x.StoreId,
                                 OrderNumber = x.OrderNumber,
                                 CustomerPrice = (decimal)x.CustomerPrice,
                                 Discount = (decimal)x.Discount,
                                 AdvancePrice = (decimal)advancepaid,
                                 Total = (decimal)x.Total,
                                 GrandTotal = (decimal)x.GrandTotal,
                                 NetPrice = (decimal)x.NetPrice,
                                 DiscountPercent = (decimal)x.DiscountPercent,
                                 DiscountValue = (decimal)x.DiscountValue,
                                 EventDate = (DateTime)j2.EventDate,
                                 EventTime = j2.Time,
                                 EventFee = (decimal)x.EventFee,
                                 OrderDate = (DateTime)x.CreatedOn,
                                 CustomerExtraProducts = CustExtraProducts,
                                 Extra = (from e in context.customerextramasters
                                              //join c in context.customerordermasters on e.CustomerId equals c.CustomerId
                                          join ex in context.extrachargemasters on e.ExtraId equals ex.ExtraId into j1
                                          from j2 in j1.DefaultIfEmpty()
                                          where e.CustomerId == model.CustomerId && e.OrderNumber == strOrderNumber
                                          orderby e.ExtraId ascending
                                          select new ExtraChargesModel()
                                          {
                                              ExtraId = (int)e.ExtraId,
                                              ExtraName = j2.ExtraName,
                                              ExtraCharges = (decimal)e.ExtraCharges
                                          }).ToList(),
                                 Category = (from g in context.customercategorymasters
                                                 //join g in context.customerordermasters on c.OrderNumber equals g.OrderNumber
                                             where g.OrderNumber == strOrderNumber
                                             orderby g.CategoryId ascending
                                             select new CustomerCategoryModel()
                                             {
                                                 CategoryId = (int)g.CategoryId,
                                                 Product = (from p in context.customerproductmasters
                                                            join kt in context.productmasters on p.ProductId equals kt.ProductId into r1
                                                            from r2 in r1.DefaultIfEmpty()
                                                            where p.OrderNumber == strOrderNumber && p.CategoryId == g.CategoryId
                                                            orderby p.ProductId ascending
                                                            select new CustomerProductModel()
                                                            {
                                                                ProductId = (int)p.ProductId,
                                                                ProductName = r2.ProductName
                                                            }).ToList()

                                             }).ToList(),


                             }).First();

                //if (order.OrderId > 0)
                //{
                //    SendSMS(order.CustomerPhoneNo, order.customerName, order.OrderNumber, order.NetPrice, Convert.ToString(order.EventDate.ToString("dd-MM-yyyy")), order.EventTime);
                //    SendEmail(order.CustomerEmailId, order.CustomerPhoneNo, order.customerName, order.OrderNumber, order.NetPrice, Convert.ToString(order.EventDate.ToString("dd-MM-yyyy")), order.EventTime);
                //}
                return order;
            }
        }
        public CustomerOrderModel EventOrderUpdate(CustomerOrderModel model)
        {
            CustomerOrderModel data = new CustomerOrderModel();
            CustomerCategoryModel categorymodel = new CustomerCategoryModel();

            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                var o = context.customerordermasters.Find(model.OrderId);
                if (o != null)
                {
                    decimal totalmenucharge = 0;
                    decimal extracharges = 0;
                    decimal discount = 0;
                    decimal advanceprice = 0;
                    decimal eventfee = 0;
                    decimal total = 0;
                    decimal grandtotal = 0;
                    decimal netprice = 0;

                    foreach (var item in model.Extra)
                    {
                        extracharges = extracharges + item.ExtraCharges;
                    }

                    var menucharge = context.menumasters.Where(m => m.MenuId == model.MenuId).Select(s => s.MenuPrice).FirstOrDefault();
                    var noofgathering = context.customereventmasters.Where(c => c.CustomerId == model.CustomerId).Select(s => s.Gathering).FirstOrDefault();
                    if (model.CustomerPrice > 0)
                    {
                        totalmenucharge = model.CustomerPrice * (int)noofgathering;
                    }
                    else
                    {
                        totalmenucharge = (decimal)menucharge * (int)noofgathering;
                    }

                    var advancepaid = 0.0;
                    var advance = context.advancemasters.Where(x => x.OrderNumber == model.OrderNumber).Select(s => s.AdvancePrice).ToList();

                    if (advance != null)
                    {
                        foreach (var a in advance)
                        {
                            advancepaid += (double)a;
                        }
                    }
                    else
                    {
                        advancepaid = 0.0;
                    }
                    eventfee = model.EventFee;
                    advanceprice = (decimal)advancepaid;
                    total = extracharges + totalmenucharge + eventfee;
                    if (model.DiscountPercent > 0)
                    {
                        //discount = (model.DiscountPercent * total) / 100;
                        discount = (model.DiscountPercent / 100) * total;
                    }
                    else if (model.DiscountValue > 0)
                    {
                        discount = model.DiscountValue;
                    }
                    else
                    {
                        discount = 0;
                    }
                    grandtotal = total - discount;
                    netprice = grandtotal - advanceprice;
                    o.OrderId = model.OrderId;
                    o.StoreId = model.StoreId;
                    o.CustomerId = model.CustomerId;
                    o.MenuId = model.MenuId;
                    o.CustomerPrice = model.CustomerPrice;
                    o.EventFee = eventfee;
                    o.Discount = discount;
                    o.AdvancePrice = advanceprice;
                    o.Total = total;
                    o.GrandTotal = grandtotal;
                    o.NetPrice = netprice;
                    o.OrderNumber = model.OrderNumber;
                    o.DiscountPercent = model.DiscountPercent;
                    o.DiscountValue = model.DiscountValue;
                    context.SaveChanges();
                    context.customerproductmasters.RemoveRange(context.customerproductmasters.Where(d => d.OrderNumber == model.OrderNumber));
                    context.customercategorymasters.RemoveRange(context.customercategorymasters.Where(c => c.OrderNumber == model.OrderNumber));
                    context.customerextramasters.RemoveRange(context.customerextramasters.Where(e => e.OrderNumber == model.OrderNumber));
                    context.customerextraproductmasters.RemoveRange(context.customerextraproductmasters.Where(ep => ep.OrderNumber == model.OrderNumber));
                    context.SaveChanges();
                    foreach (var extra in model.Extra)
                    {
                        customerextramaster e = new customerextramaster();
                        e.CustomerId = model.CustomerId;
                        e.StoreId = model.StoreId;
                        e.OrderNumber = model.OrderNumber;
                        e.ExtraId = extra.ExtraId;
                        e.ExtraCharges = extra.ExtraCharges;
                        context.customerextramasters.Add(e);
                    }

                    foreach (var item in model.Category)
                    {
                        customercategorymaster c = new customercategorymaster();
                        c.CategoryId = item.CategoryId;
                        c.OrderNumber = model.OrderNumber;
                        context.customercategorymasters.Add(c);
                        foreach (var productitem in item.Product)
                        {
                            customerproductmaster p = new customerproductmaster();
                            p.ProductId = productitem.ProductId;
                            p.CategoryId = item.CategoryId;
                            p.OrderNumber = model.OrderNumber;
                            context.customerproductmasters.Add(p);
                        }
                    }
                    if (model.CustomerExtraProducts != null && model.CustomerExtraProducts != "")
                    {
                        string extraproduct = model.CustomerExtraProducts;
                        string s = model.CustomerExtraProducts;
                        string[] values = s.Split(',');
                        for (int i = 0; i < values.Length; i++)
                        {
                            customerextraproductmaster p = new customerextraproductmaster();
                            values[i] = values[i].Trim();
                            p.ProductName = values[i];
                            p.CustomerId = model.CustomerId;
                            p.OrderNumber = model.OrderNumber;
                            context.customerextraproductmasters.Add(p);
                        }
                    }
                    var result = context.SaveChanges();

                }
                string CustExtraProducts = "";
                var extraproductList = (from ep in context.customerextraproductmasters
                                        where ep.CustomerId == model.CustomerId && ep.OrderNumber == model.OrderNumber
                                        select new
                                        {
                                            ProductName = ep.ProductName
                                        }).ToList();
                foreach (var p in extraproductList)
                {
                    CustExtraProducts += p.ProductName + ",";
                }
                var order = (from x in context.customerordermasters
                             join c in context.customermasters on x.CustomerId equals c.CustomerId
                             join ev in context.customereventmasters on x.CustomerId equals ev.CustomerId into j1
                             from j2 in j1.DefaultIfEmpty()
                             join m in context.menumasters on x.MenuId equals m.MenuId into j3
                             from j4 in j3.DefaultIfEmpty()
                             where x.CustomerId == model.CustomerId && x.StoreId == model.StoreId && x.OrderId == model.OrderId
                             orderby x.OrderId descending
                             select new CustomerOrderModel()
                             {
                                 OrderId = x.OrderId,
                                 CustomerId = (int)x.CustomerId,
                                 customerName = c.CustomerName,
                                 CustomerPhoneNo = c.PhoneNumber,
                                 CustomerEmailId = c.CustomerEmailId,
                                 MenuId = (int)x.MenuId,
                                 MenuName = j4.MenuName,
                                 MenuPrice = (decimal)j4.MenuPrice,
                                 StoreId = (int)x.StoreId,
                                 OrderNumber = x.OrderNumber,
                                 CustomerPrice = (decimal)x.CustomerPrice,
                                 EventFee = (decimal)x.EventFee,
                                 Discount = (decimal)x.Discount,
                                 AdvancePrice = (decimal)x.AdvancePrice,
                                 Total = (decimal)x.Total,
                                 GrandTotal = (decimal)x.GrandTotal,
                                 DiscountPercent = (decimal)x.DiscountPercent,
                                 DiscountValue = (decimal)x.DiscountValue,
                                 NetPrice = (decimal)x.NetPrice,
                                 EventDate = (DateTime)j2.EventDate,
                                 EventTime = j2.Time,
                                 Gathering = (int)j2.Gathering,
                                 OrderDate = (DateTime)x.CreatedOn,
                                 CustomerExtraProducts = CustExtraProducts,
                                 Extra = (from e in context.customerextramasters
                                          join ex in context.extrachargemasters on e.ExtraId equals ex.ExtraId into j1
                                          from j2 in j1.DefaultIfEmpty()
                                          where e.OrderNumber == model.OrderNumber && e.CustomerId == model.CustomerId
                                          orderby e.ExtraId ascending
                                          select new ExtraChargesModel()
                                          {
                                              ExtraId = (int)e.ExtraId,
                                              ExtraName = j2.ExtraName,
                                              ExtraCharges = (decimal)e.ExtraCharges
                                          }).ToList(),
                                 Category = (from c in context.customercategorymasters
                                             where c.OrderNumber == model.OrderNumber
                                             orderby c.CategoryId ascending
                                             select new CustomerCategoryModel()
                                             {
                                                 CategoryId = (int)c.CategoryId,
                                                 Product = (from p in context.customerproductmasters
                                                            join kt in context.productmasters on p.ProductId equals kt.ProductId into r1
                                                            from r2 in r1.DefaultIfEmpty()
                                                            where p.OrderNumber == model.OrderNumber && p.CategoryId == c.CategoryId
                                                            orderby p.ProductId ascending
                                                            select new CustomerProductModel()
                                                            {
                                                                ProductId = (int)p.ProductId,
                                                                ProductName = r2.ProductName
                                                            }).ToList()

                                             }).ToList()
                             }).First();
                //var category =
                //order.Category = category;
                data = order;
            }
            return data;
        }

        public CustomerOrderModel GetAllEventOrderByOrderNumber(string OderNumber)
        {
            CustomerOrderModel order = new CustomerOrderModel();
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {

                var advancepaid = 0.0;
                var netprice = 0.0;
                var advance = context.advancemasters.Where(x => x.OrderNumber == OderNumber).Select(s => s.AdvancePrice).ToList();

                if (advance != null)
                {
                    foreach (var a in advance)
                    {
                        advancepaid += (double)a;
                    }
                }
                else
                {
                    advancepaid = 0.0;
                }
                var grandtotal = context.customerordermasters.Where(x => x.OrderNumber == OderNumber).Select(s => s.GrandTotal).FirstOrDefault();
                netprice = (double)((decimal)grandtotal - (decimal)advancepaid);

                var data = context.customerordermasters.Where(s => s.OrderNumber == OderNumber);
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        item.AdvancePrice = (decimal)advancepaid;
                        item.NetPrice = (decimal)netprice;
                    }
                    context.SaveChanges();
                }

                order = (from x in context.customerordermasters
                         join c in context.customermasters on x.CustomerId equals c.CustomerId
                         join px in context.menumasters on x.MenuId equals px.MenuId into l1
                         from l2 in l1.DefaultIfEmpty()
                         join ev in context.customereventmasters on x.CustomerId equals ev.CustomerId into j1
                         from j2 in j1.DefaultIfEmpty()
                         join ad in context.advancemasters on x.OrderNumber equals ad.OrderNumber into j3
                         from j4 in j3.DefaultIfEmpty()
                         where x.OrderNumber == OderNumber
                         orderby x.CreatedOn descending
                         select new CustomerOrderModel()
                         {
                             OrderId = x.OrderId,
                             CustomerId = (int)x.CustomerId,
                             customerName = c.CustomerName,
                             CustomerPhoneNo = c.PhoneNumber,
                             CustomerEmailId = c.CustomerEmailId,
                             MenuId = (int)x.MenuId,
                             MenuName = l2.MenuName,
                             MenuPrice = (decimal)l2.MenuPrice,
                             StoreId = (int)x.StoreId,
                             OrderNumber = x.OrderNumber,
                             CustomerPrice = (decimal)x.CustomerPrice,
                             EventFee = (decimal)x.EventFee,
                             Discount = (decimal)x.Discount,
                             AdvancePrice = j4.AdvancePrice != null ? (decimal)j4.AdvancePrice : (decimal)0.0,
                             Total = (decimal)x.Total,
                             GrandTotal = (decimal)x.GrandTotal,
                             NetPrice = (decimal)x.NetPrice,
                             DiscountPercent = (decimal)x.DiscountPercent,
                             DiscountValue = (decimal)x.DiscountValue,
                             EventDate = (DateTime)j2.EventDate,
                             EventTime = j2.Time,
                             Gathering = (int)j2.Gathering,
                             OrderDate = (DateTime)x.CreatedOn,
                             Extra = (from e in context.customerextramasters
                                          //join c in context.customerordermasters on e.CustomerId equals c.CustomerId
                                      join ex in context.extrachargemasters on e.ExtraId equals ex.ExtraId into j1
                                      from j2 in j1.DefaultIfEmpty()
                                      where e.CustomerId == x.CustomerId && e.OrderNumber == x.OrderNumber
                                      orderby e.ExtraId ascending
                                      select new ExtraChargesModel()
                                      {
                                          ExtraId = (int)e.ExtraId,
                                          ExtraName = j2.ExtraName,
                                          ExtraCharges = (decimal)e.ExtraCharges
                                      }).ToList(),
                             Category = (from c in context.customercategorymasters
                                             // join g in context.customerordermasters on c.OrderNumber equals g.OrderNumber
                                         where c.OrderNumber == x.OrderNumber
                                         orderby c.CategoryId ascending
                                         select new CustomerCategoryModel()
                                         {
                                             CategoryId = (int)c.CategoryId,
                                             Product = (from p in context.customerproductmasters
                                                        join kt in context.productmasters on p.ProductId equals kt.ProductId into
                                                        r1
                                                        from r2 in r1.DefaultIfEmpty()
                                                        where p.OrderNumber == c.OrderNumber && p.CategoryId == c.CategoryId
                                                        orderby p.ProductId ascending
                                                        select new CustomerProductModel()
                                                        {
                                                            ProductId = (int)p.ProductId,
                                                            ProductName = r2.ProductName
                                                        }).ToList()
                                         }).ToList()
                         }).FirstOrDefault();

                string CustExtraProducts = "";
                var extraproductList = (from ep in context.customerextraproductmasters
                                        where ep.CustomerId == order.CustomerId && ep.OrderNumber == order.OrderNumber
                                        select new
                                        {
                                            ProductName = ep.ProductName
                                        }).ToList();
                foreach (var p in extraproductList)
                {
                    CustExtraProducts += p.ProductName + ",";
                }
                order.CustomerExtraProducts = CustExtraProducts;
            }
            return order;
        }

        public IEnumerable<CustomerOrderModel> GetAllEventOrderList(int storeid)
        {
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                var order = (from x in context.customerordermasters
                             join c in context.customermasters on x.CustomerId equals c.CustomerId
                             join px in context.menumasters on x.MenuId equals px.MenuId into l1
                             from l2 in l1.DefaultIfEmpty()
                             join ev in context.customereventmasters on x.CustomerId equals ev.CustomerId into j1
                             from j2 in j1.DefaultIfEmpty()
                             join ad in context.advancemasters on x.OrderNumber equals ad.OrderNumber into j3
                             from j4 in j3.DefaultIfEmpty()
                             where x.StoreId == storeid
                             orderby x.CreatedOn descending
                             select new CustomerOrderModel()
                             {
                                 OrderId = x.OrderId,
                                 CustomerId = (int)x.CustomerId,
                                 customerName = c.CustomerName,
                                 CustomerPhoneNo = c.PhoneNumber,
                                 CustomerEmailId = c.CustomerEmailId,
                                 MenuId = (int)x.MenuId,
                                 MenuName = l2.MenuName,
                                 MenuPrice = (decimal)l2.MenuPrice,
                                 StoreId = (int)x.StoreId,
                                 OrderNumber = x.OrderNumber,
                                 CustomerPrice = (decimal)x.CustomerPrice,
                                 EventFee = (decimal)x.EventFee,
                                 Discount = (decimal)x.Discount,
                                 AdvancePrice = j4.AdvancePrice != null ? (decimal)j4.AdvancePrice : (decimal)0.0,
                                 Total = (decimal)x.Total,
                                 GrandTotal = (decimal)x.GrandTotal,
                                 DiscountPercent = (decimal)x.DiscountPercent,
                                 DiscountValue = (decimal)x.DiscountValue,
                                 NetPrice = (decimal)x.NetPrice,
                                 EventDate = (DateTime)j2.EventDate,
                                 EventTime = j2.Time,
                                 Gathering = (int)j2.Gathering,
                                 OrderDate = (DateTime)x.CreatedOn,
                                 Extra = (from e in context.customerextramasters
                                              //join c in context.customerordermasters on e.CustomerId equals c.CustomerId
                                          join ex in context.extrachargemasters on e.ExtraId equals ex.ExtraId into j1
                                          from j2 in j1.DefaultIfEmpty()
                                          where e.CustomerId == x.CustomerId && e.OrderNumber == x.OrderNumber
                                          orderby e.ExtraId ascending
                                          select new ExtraChargesModel()
                                          {
                                              ExtraId = (int)e.ExtraId,
                                              ExtraName = j2.ExtraName,
                                              ExtraCharges = (decimal)e.ExtraCharges
                                          }).ToList(),
                                 Category = (from c in context.customercategorymasters
                                                 // join g in context.customerordermasters on c.OrderNumber equals g.OrderNumber
                                             where c.OrderNumber == x.OrderNumber
                                             orderby c.CategoryId ascending
                                             select new CustomerCategoryModel()
                                             {
                                                 CategoryId = (int)c.CategoryId,
                                                 Product = (from p in context.customerproductmasters
                                                            join kt in context.productmasters on p.ProductId equals kt.ProductId into
                                                            r1
                                                            from r2 in r1.DefaultIfEmpty()
                                                            where p.OrderNumber == c.OrderNumber && p.CategoryId == c.CategoryId
                                                            orderby p.ProductId ascending
                                                            select new CustomerProductModel()
                                                            {
                                                                ProductId = (int)p.ProductId,
                                                                ProductName = r2.ProductName
                                                            }).ToList()
                                             }).ToList()
                             }).ToList();
                foreach (var o in order)
                {
                    string CustExtraProducts = "";
                    var extraproductList = (from ep in context.customerextraproductmasters
                                            where ep.CustomerId == o.CustomerId && ep.OrderNumber == o.OrderNumber
                                            select new
                                            {
                                                ProductName = ep.ProductName
                                            }).ToList();
                    foreach (var p in extraproductList)
                    {
                        CustExtraProducts += p.ProductName + ",";
                    }
                    o.CustomerExtraProducts = CustExtraProducts;
                }
                return order;
            }
        }
        public ResponseStatus EventOrderDelete(string strOrderNumber, int customerid, int storeid)
        {
            ResponseStatus response = new ResponseStatus();
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                customerordermaster order = new customerordermaster();
                var ordernumber = context.customerordermasters.Where(o => o.StoreId == storeid && o.CustomerId == customerid && o.OrderNumber == strOrderNumber).SingleOrDefault();
                if (ordernumber != null)
                {
                    context.customerproductmasters.RemoveRange(context.customerproductmasters.Where(d => d.OrderNumber == strOrderNumber));
                    context.customercategorymasters.RemoveRange(context.customercategorymasters.Where(c => c.OrderNumber == strOrderNumber));
                    context.customerextramasters.RemoveRange(context.customerextramasters.Where(e => e.OrderNumber == strOrderNumber));
                    context.customerordermasters.RemoveRange(context.customerordermasters.Where(o => o.CustomerId == customerid && o.OrderNumber == strOrderNumber));
                    context.SaveChanges();
                    response.isSuccess = true;
                    response.serverResponseTime = System.DateTime.Now;
                }
                else
                {
                    response.isSuccess = false;
                    response.serverResponseTime = System.DateTime.Now;
                }
            }
            return response;
        }
        public CustomerOrderModel PrintReceipt(PrintReceiptModel model)
        {
            CustomerOrderModel result = new CustomerOrderModel();
            List<ProductModel> extraproductList = new List<ProductModel>();
            List<CustomerCategoryModel> categorylist = new List<CustomerCategoryModel>();
            string productnames = "";
            string extranames = "";
            using (evergreenfeedback_androidEntities context = new evergreenfeedback_androidEntities())
            {
                var advancepaid = 0.0;
                var netprice = 0.0;
                var advance = context.advancemasters.Where(x => x.OrderNumber == model.OrderNumber).Select(s => s.AdvancePrice).ToList();

                if (advance.Count() > 0)
                {
                    foreach (var a in advance)
                    {
                        advancepaid += (double)a;
                    }
                }
                else
                {
                    advancepaid = 0.0;
                }
                var grandtotal = context.customerordermasters.Where(x => x.OrderNumber == model.OrderNumber).Select(s => s.GrandTotal).FirstOrDefault();
                netprice = (double)((decimal)grandtotal - (decimal)advancepaid);

                var data = context.customerordermasters.Where(s => s.OrderNumber == model.OrderNumber);
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        item.AdvancePrice = (decimal)advancepaid;
                        item.NetPrice = (decimal)netprice;
                    }
                    context.SaveChanges();
                }

                var order = (from x in context.customerordermasters
                             join c in context.customermasters on x.CustomerId equals c.CustomerId
                             join ev in context.customereventmasters on x.CustomerId equals ev.CustomerId into j1
                             from j2 in j1.DefaultIfEmpty()
                             join m in context.menumasters on x.MenuId equals m.MenuId into j3
                             from j4 in j3.DefaultIfEmpty()
                             where x.CustomerId == model.CustomerId && x.StoreId == model.StoreId && x.OrderId == model.OrderId
                             orderby x.OrderId descending
                             select new CustomerOrderModel()
                             {
                                 OrderId = x.OrderId,
                                 CustomerId = (int)x.CustomerId,
                                 customerName = c.CustomerName,
                                 CustomerPhoneNo = c.PhoneNumber,
                                 CustomerEmailId = c.CustomerEmailId,
                                 MenuId = (int)x.MenuId,
                                 MenuName = j4.MenuName,
                                 MenuPrice = (decimal)j4.MenuPrice,
                                 StoreId = (int)x.StoreId,
                                 OrderNumber = x.OrderNumber,
                                 CustomerPrice = (decimal)x.CustomerPrice,
                                 EventFee = (decimal)x.EventFee,
                                 Discount = (decimal)x.Discount,
                                 AdvancePrice = (decimal)x.AdvancePrice,
                                 Total = (decimal)x.Total,
                                 GrandTotal = (decimal)x.GrandTotal,
                                 NetPrice = (decimal)x.NetPrice,
                                 EventDate = (DateTime)j2.EventDate,
                                 EventTime = j2.Time,
                                 Gathering = (int)j2.Gathering,
                                 OrderDate = (DateTime)x.CreatedOn,
                                 Extra = (from e in context.customerextramasters
                                          join ex in context.extrachargemasters on e.ExtraId equals ex.ExtraId into j1
                                          from j2 in j1.DefaultIfEmpty()
                                          where e.CustomerId == model.CustomerId && e.OrderNumber == model.OrderNumber
                                          orderby e.ExtraId ascending
                                          select new ExtraChargesModel()
                                          {
                                              ExtraId = (int)e.ExtraId,
                                              ExtraName = j2.ExtraName,
                                              ExtraCharges = (decimal)e.ExtraCharges
                                          }).ToList(),
                                 Category = (from c in context.customercategorymasters
                                             join cn in context.categorymasters on c.CategoryId equals cn.CategoryId into j1
                                             from j2 in j1.DefaultIfEmpty()
                                             where c.OrderNumber == model.OrderNumber
                                             orderby c.CategoryId ascending
                                             select new CustomerCategoryModel()
                                             {
                                                 CategoryId = (int)c.CategoryId,
                                                 CategoryName = j2.CategoryName,
                                                 Product = (from p in context.customerproductmasters
                                                            join kt in context.productmasters on p.ProductId equals kt.ProductId into r1
                                                            from r2 in r1.DefaultIfEmpty()
                                                            where p.OrderNumber == model.OrderNumber && p.CategoryId == c.CategoryId
                                                            orderby p.ProductId ascending
                                                            select new CustomerProductModel()
                                                            {
                                                                ProductId = (int)p.ProductId,
                                                                ProductName = r2.ProductName
                                                            }).ToList()

                                             }).ToList()
                             }).First();
                foreach (var item in order.Extra)
                {
                    extranames += item.ExtraName + " ,";
                }
                foreach (var cat in order.Category)
                {
                    foreach (var pro in cat.Product)
                    {
                        productnames += pro.ProductName + " ,";
                    }
                }
                var storedetails = context.storemasters.Where(s => s.StoreId == order.StoreId)
                    .Select(s => new StoreModel()
                    {
                        StoreId = s.StoreId,
                        StoreName = s.StoreName,
                        StorePhoneNumber = s.StorePhoneNumber,
                        StoreAddress = s.StoreAddress
                    }).FirstOrDefault();
                //order.StoreName = context.storemasters.Where(s => s.StoreId == order.StoreId).Select(s => s.StoreName).FirstOrDefault();
                order.StoreName = storedetails.StoreName;
                order.StorePhonNumber = storedetails.StorePhoneNumber;
                order.StoreAddress = storedetails.StoreAddress;
                order.ExtraNames = extranames;
                order.ProductNames = productnames;
                string CustExtraProducts = "";
                extraproductList = (from ep in context.customerextraproductmasters
                                    where ep.CustomerId == model.CustomerId && ep.OrderNumber == model.OrderNumber
                                    select new ProductModel()
                                    {
                                        ProductName = ep.ProductName
                                    }).ToList();
                foreach (var p in extraproductList)
                {
                    CustExtraProducts += p.ProductName + ",";
                }
                order.CustomerExtraProducts = CustExtraProducts;
                categorylist = order.Category.ToList();

                result = order;
            }
            if (result.AdvancePrice > 0)
            {
                SendSMS(result);
                SendEmail(result, categorylist, extraproductList);
            }
            return result;
        }
        //public void SendSMS(string phonenumber, string customername, string ordernumber, decimal totalamount, string eventdate, string eventtime, decimal advancepaid, string storename)
        public void SendSMS(CustomerOrderModel order)
        {
            decimal minimumguarantee = 0;
            decimal menuprice = 0;
            if (order.CustomerPrice > 0)
            {
                menuprice = order.CustomerPrice;
                minimumguarantee = order.Gathering * order.CustomerPrice;
            }
            else
            {
                menuprice = order.MenuPrice;
                minimumguarantee = order.Gathering * order.MenuPrice;
            }

            string eventdate = Convert.ToString(order.EventDate.ToString("dd-MMM-yyyy"));
            string _user = HttpUtility.UrlEncode("shamsweet"); // API user name to send SMS
            string _pass = HttpUtility.UrlEncode("12345");     // API password to send SMS
            string _route = HttpUtility.UrlEncode("transactional");
            string _senderid = HttpUtility.UrlEncode("WISHHH");
            string _recipient = HttpUtility.UrlEncode(order.CustomerPhoneNo);  // who will receive message
            string _messageText = HttpUtility.UrlEncode("Dear " + order.customerName + ", Thanks for Booking Party with us!.\nYour Party is confirmed with ID: " + Convert.ToString(order.OrderNumber) + ".\nOn: " + eventdate + ",Time: " + order.EventTime + " at " + order.StoreName + ".\nNo of Pax – " + order.Gathering + " @ Rs. " + menuprice + " + Taxes\nAdvance Paid: Rs" + order.AdvancePrice + "\nThanks & Regards\n " + order.StoreName + "\n " + order.StorePhonNumber + ""); // text message

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
        public void SendEmail(CustomerOrderModel order, List<CustomerCategoryModel> categorylist, List<ProductModel> extraproductList)
        {
            string subject = "Your order: " + order.OrderNumber + " has been received";
            string body = PopulateBody(order, categorylist, extraproductList);
            //string body = "Dear " + order.customerName + ", Thank you for visiting us." +
            //              "<br>Your Party is confirmed with ID : " + Convert.ToString(order.OrderNumber) +
            //              "<br>On: " + eventdate + " " + order.EventTime + " at " + order.StoreName +
            //              "<br>No of Pax – " + order.Gathering + " @ Rs." + menuprice + " + Taxes" +
            //              "<br>Advance Amount: " + order.AdvancePrice +
            //              "<br>Minimum Guarantee: Rs." + minimumguarantee +
            //              "<br>Menu: " + catregory +
            //              "<br>Extra Item: " + order.ExtraNames +
            //              //"<br>Your Scheduled Event on: " + eventdate +
            //              //"<br>Time: " + order.EventTime +
            //              //"<br>Total Price: " + order.Total +
            //              //"<br>Discount: " + order.Discount +
            //              //"<br>GrandTotal: " + order.GrandTotal +                          
            //              //"<br>Net Amount: " + order.NetPrice +
            //              "<br>" +
            //              "<br>Looking forwards to serve you." +
            //              "<br>Thanks & Regards " +
            //              "<br> " + order.StoreName +
            //              "<br> " + order.StorePhonNumber;

            string FromMail = "parkballuchi77@pindballuchi.com";
            string emailTo = order.CustomerEmailId;
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.rediffmailpro.com");


            //LinkedResource LinkedImage = new LinkedResource(System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/Images/parkballuchi.png"));
            //LinkedImage.ContentId = "MyPic";
            ////Added the patch for Thunderbird as suggested by Jorge
            //LinkedImage.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);
            //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
            //  "You should see image next to this line. <img src=cid:MyPic>",
            //  null, "text/html");
            //htmlView.LinkedResources.Add(LinkedImage);
            //mail.AlternateViews.Add(htmlView);



            mail.From = new MailAddress(FromMail);
            mail.To.Add(emailTo);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("parkballuchi77@pindballuchi.com", "Aindia10");
            SmtpServer.EnableSsl = false;
            SmtpServer.Send(mail);
        }
        public string PopulateBody(CustomerOrderModel order, List<CustomerCategoryModel> categorylist, List<ProductModel> extraproductList)
        {
            string eventdate = Convert.ToString(order.EventDate.ToString("dd-MMM-yyyy"));
            decimal minimumguarantee = 0;
            decimal menuprice = 0;
            string category = "";
            string extranames = "";
            string extraproduct = "";
            if (order.CustomerPrice > 0)
            {
                menuprice = order.CustomerPrice;
                minimumguarantee = order.Gathering * order.CustomerPrice;
            }
            else
            {
                menuprice = order.MenuPrice;
                minimumguarantee = order.Gathering * order.MenuPrice;
            }

            if (categorylist.Count() > 0)
            {
                foreach (var cat in categorylist)
                {
                    category += "<tr>";
                    category += "<td width='20px'></td>";
                    category += "<td valign='top' width='280px' style='font-family: Helvetica, arial, sans-serif; font-size:15px; color: #212121;line-height:20px;'>";
                    category += cat.CategoryName + "<br/>";
                    foreach (var pro in cat.Product)
                    {
                        category += "<ul style='color:#666666;'>" + pro.ProductName + "</ul>";
                    }
                    category += "</td>";
                    category += "<td width='20px'></td>";
                    category += "</tr>";
                }
            }
            else
            {
                category = "";
            }

            if (categorylist.Count() > 0)
            {

                foreach (var item in order.Extra)
                {
                    extranames += "<tr height='40' style='border-bottom:1px solid #49c9b2;'>";
                    extranames += "<td width='20px'></td>";
                    extranames += "<td width='580px' style='font-family:Helvetica, arial, sans-serif; font-size:15px; color: #ffffff;line-height: 20px;'>";
                    extranames += item.ExtraName;
                    extranames += "<td width='580px' style='font-family:Helvetica, arial, sans-serif; font-size:15px; color: #212121;line-height: 20px;text-align:right;font-weight:700;'>";
                    extranames += item.ExtraCharges;
                    extranames += "</td>";
                    extranames += "</td>";
                    extranames += "<td width='20px'></td>";
                    extranames += "</tr>";
                }
            }
            else
            {
                extranames = "";
            }

            if (extraproductList.Count() > 0)
            {
                foreach (var i in extraproductList)
                {
                    extraproduct += "<tr height='40' style='border-bottom:1px solid #49c9b2;'>";
                    extraproduct += "<td width='20px'></td>";
                    extraproduct += "<td width='580px' style='font-family:Helvetica, arial, sans-serif; font-size:15px; color: #ffffff;line-height: 20px;'>";
                    extraproduct += i.ProductName;
                    extraproduct += "</td>";
                    extraproduct += "<td width='20px'></td>";
                    extraproduct += "</tr>";
                }

            }
            else
            {
                extraproduct = "";
            }
            string body = string.Empty;

            using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/Templates/index.html")))
            {
                body = reader.ReadToEnd();
            }


            body = body.Replace("{EventDate}", eventdate);
            body = body.Replace("{EventTime}", order.EventTime);
            body = body.Replace("{StoreName}", order.StoreName);
            body = body.Replace("{OrderNumber}", order.OrderNumber);
            body = body.Replace("{Gathering}", Convert.ToString(order.Gathering));
            body = body.Replace("{MenuPrice}", Convert.ToString(menuprice));
            body = body.Replace("{AdvanceAmount}", Convert.ToString(order.AdvancePrice));
            body = body.Replace("{MinimumGuarantee}", Convert.ToString(minimumguarantee));
            body = body.Replace("{NetPrice}", Convert.ToString(order.NetPrice));
            body = body.Replace("{Category}", category);
            body = body.Replace("{ExtraNames}", extranames);
            body = body.Replace("{ExtraProduct}", extraproduct);
            body = body.Replace("{StoreName}", order.StoreName);
            body = body.Replace("{StorePhonNumber}", order.StorePhonNumber);
            body = body.Replace("{StoreAddress}", order.StoreAddress);
            return body;
        }

    }
}