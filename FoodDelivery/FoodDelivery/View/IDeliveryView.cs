using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodDelivery.Controller;
using Data.Model;

namespace FoodDelivery.View
{
    public interface IDeliveryView
    {
        void SetController(DeliveryController controller);
        void UpdateView();
        void SetOrderList(IList<Order_item> order_items, IList<Product> products, Order order);
        void SetOrdersInList(IList<Order> orders);
        Order GetSelectedOrderFromGrid();
    }
}
