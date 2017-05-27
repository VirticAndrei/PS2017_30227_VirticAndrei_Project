using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Model;
using FoodDelivery.View;

namespace FoodDelivery.Controller
{
    public class OrderHistoryController
    {
        private IOrderHistoryView _historyView;
        private ClientOnServer _client;
        private int id;

        public OrderHistoryController(IOrderHistoryView historyView, ClientOnServer client, int id)
        {
            historyView.SetController(this);
            _historyView = historyView;
            _client = client;
            this.id = id;
            SetOrders();
        }

        public void SetOrders()
        {
            List<Order> ordersList = new List<Order>();
            _client.SendOrderData("find", "0", "" + id, "0", "", "","","","");
            ordersList = _client.getOrders();
            _historyView.SetOrdersInList(ordersList);
        }

        public void SetProductList(int idOrder, string total)
        {
            List<Product> productsList = new List<Product>();
            List<Product> allProducts = new List<Product>();
            _client.SendProductData("findAll", "0", "", "0", "", "", "0");
            allProducts = _client.getProducts();
            List<Order_item> order_items = new List<Order_item>();
            _client.SendOrder_itemData("find","0", "0", ""+idOrder, "0", "0");
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
                _historyView.SetOrderList(order_items, productsList, total);
            }
        }

    }
}
