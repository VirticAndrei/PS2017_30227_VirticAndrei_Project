using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodDelivery.Controller;
using Data.Model;

namespace FoodDelivery.View
{
    public interface IOrderHistoryView
    {
        void SetController(OrderHistoryController controller);
        void SetOrderList(IList<Order_item> order_items, IList<Product> products, string total);
        void SetOrdersInList(IList<Order> orders);
        
    }
}
