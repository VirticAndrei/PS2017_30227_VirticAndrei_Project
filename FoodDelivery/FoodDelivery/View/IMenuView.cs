using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodDelivery.Controller;
using Data.Model;

namespace FoodDelivery.View
{
    public interface IMenuView
    {
        void SetController(MenuController controller);
        void SetMenu(IList<Product> products, string category);
        void SetOrder(Boolean log,IList<Cart> cart, IList<Product> products);
    }
}
