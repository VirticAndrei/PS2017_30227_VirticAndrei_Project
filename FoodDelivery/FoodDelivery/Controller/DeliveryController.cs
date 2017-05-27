using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Model;
using FoodDelivery.View;
using System.Windows.Forms;

namespace FoodDelivery.Controller
{
    public class DeliveryController
    {
        private IDeliveryView _deliveryView;
        private ClientOnServer _client;
        private List<Order> ordersList;

        public DeliveryController(IDeliveryView deliveryView, ClientOnServer client)
        {
            deliveryView.SetController(this);
            _deliveryView = deliveryView;
            _client = client;
            ordersList = new List<Order>();
            SetOrders();
        }

        public void SetOrders()
        {
            _client.SendOrderData("findBy", "0", "0", "0", "", "Delivered","","","");
            ordersList = _client.getOrders();
            _deliveryView.SetOrdersInList(ordersList);
        }

        public void SetProductList(Order order)
        {
            List<Product> productsList = new List<Product>();
            List<Product> allProducts = new List<Product>();
            _client.SendProductData("findAll", "0", "", "0", "", "", "0");
            allProducts = _client.getProducts();
            List<Order_item> order_items = new List<Order_item>();
            _client.SendOrder_itemData("find","0", "0", ""+order.Id, "0", "0");
            order_items = _client.getOrder_items();
            if (order_items != null)
            {
                foreach (Order_item item in order_items)
                {
                    foreach (Product prod in allProducts)
                    {
                        if (item.Product_id == prod.Id)
                        {
                            productsList.Add(prod);
                        }
                    }
                }
                _deliveryView.SetOrderList(order_items, productsList, order);
            }
        }

        public void OrderStatus(string status)
        {
            Order order = _deliveryView.GetSelectedOrderFromGrid();
            if (order != null)
            {
                _client.SendOrderData("update", ""+order.Id, ""+order.Client_id, ""+order.Total, order.Order_date, status, order.Name, order.Address, order.Phone);
                switch (status)
                {
                    case "Processing":
                        _client.Notify(""+order.Client_id, "Your order is in process!");
                        break;
                    case "Go to way":
                        _client.Notify("" + order.Client_id, "Your order is on the way!");
                        break;
                    case "Delivered":
                        _client.Notify("" + order.Client_id, "Your order was delivered!");
                        break;
                }
                
                SetOrders();
            }
        }
    }
}
