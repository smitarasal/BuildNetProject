using QSI.Services.Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QSI.Services
{
   public  class OrderService :IOrderService
    {

        public string GetOrderTotal(string OrderID)
        {
            string orderTotal = string.Empty;

            try
            {
                XDocument doc = XDocument.Load("F:\\Orders.xml");

                orderTotal =
                    (from result in doc.Descendants("DocumentElement")
                    .Descendants("Orders")
                     where result.Element("OrderID").Value == OrderID.ToString()
                     select result.Element("OrderTotal").Value)
                    .FirstOrDefault<string>();

            }
            catch (Exception ex)
            {
                throw new FaultException<string>
                     (ex.Message);
            }
            return orderTotal;
        }



        public OrderContract GetOrderDetails(string OrderID)
        {
            OrderContract order = new OrderContract();

            try
            {
                XDocument doc = XDocument.Load("F:\\Orders.xml");

                IEnumerable<XElement> orders =
                         (from result in doc.Descendants("DocumentElement")
                             .Descendants("Orders")
                          where result.Element("OrderID").Value == OrderID.ToString()
                          select result);

                order.OrderID = orders.ElementAt(0).Element("OrderID").Value;
                order.OrderDate = orders.ElementAt(0).Element("OrderDate").Value;
                order.ShippedDate = orders.ElementAt(0).Element("ShippedDate").Value;
                order.ShipCountry = orders.ElementAt(0).Element("ShipCountry").Value;
                order.OrderTotal = orders.ElementAt(0).Element("OrderTotal").Value;
            }
            catch (Exception ex)
            {
                throw new FaultException<string>
                     (ex.Message);
            }
            return order;
        }

        public List<OrderContract> GetAllOrderDetails()
        {
            List<OrderContract> orderList = new List<OrderContract>();

            try
            {
                XDocument doc = XDocument.Load("F:\\Orders.xml");

                IEnumerable<XElement> orders =
                         (from result in doc.Descendants("DocumentElement")
                             .Descendants("Orders")
                          select result);


                foreach (var order in orders)
                {
                    OrderContract data = new OrderContract();

                    data.OrderID = order.Element("OrderID").Value;
                    data.OrderDate = order.Element("OrderDate").Value;
                    data.ShippedDate = order.Element("ShippedDate").Value;
                    data.ShipCountry = order.Element("ShipCountry").Value;
                    data.OrderTotal = order.Element("OrderTotal").Value;

                    orderList.Add(data);
                }

            }
            catch (Exception ex)
            {
                throw new FaultException<string>
                     (ex.Message);
            }
            return orderList;
        }

        public bool PlaceOrder(OrderContract order)
        {
            try
            {
                XDocument doc = XDocument.Load("F:\\Orders.xml");

                IEnumerable<XElement> orders =
                        (from result in doc.Descendants("DocumentElement")
                            .Descendants("Orders")
                         where result.Element("OrderID").Value == order.OrderID.ToString()
                         select result);

                if (orders.Count() > 0)
                {
                    orders.ElementAt(0).Element("OrderDate").Value = order.OrderDate;
                    orders.ElementAt(0).Element("ShippedDate").Value = order.ShippedDate;
                    orders.ElementAt(0).Element("ShipCountry").Value = order.ShipCountry;
                    orders.ElementAt(0).Element("OrderTotal").Value = order.OrderTotal;
                }
                else
                {
                    doc.Element("DocumentElement").Add(
                            new XElement("Orders",
                            new XElement("OrderID", order.OrderID),
                            new XElement("OrderDate", order.OrderDate),
                            new XElement("ShippedDate", order.ShippedDate),
                            new XElement("ShipCountry", order.ShipCountry),
                            new XElement("OrderTotal", order.OrderTotal)));

                }
                doc.Save("F:\\Orders.xml");

            }
            catch (Exception ex)
            {
                throw new FaultException<string>
                     (ex.Message);
            }
            return true;
        }


        public OrderContract UpdateOrder(string OrderID)
        {
            OrderContract order = new OrderContract();

            try
            {
                //XDocument doc = XDocument.Load("F:\\Orders.xml");

                //IEnumerable<XElement> orders =
                //         (from result in doc.Descendants("DocumentElement")
                //             .Descendants("Orders")
                //          where result.Element("OrderID").Value == OrderID.ToString()
                //          select result);


                //orders.ElementAt(0).Element("ShipCountry").Value = ShipCountry;
                //doc.Save("F:\\Orders.xml");

                order = GetOrderDetails(OrderID);

            }
            catch (Exception ex)
            {
                throw new FaultException<string>
                     (ex.Message);
            }
            return order;

        }
    }
}
