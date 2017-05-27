using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodDelivery.Controller;
using Data.Model;

namespace FoodDelivery.View
{
    public interface IOrderView
    {
        void SetController(OrderController controller);
        void SetAccountDetails(Client client);
        void SetAddressDetails(Address address);
        void SetPaymentDetails(Payment payment);
        void SetOrder(IList<Cart> cart, IList<Product> products);
        void FinishOrder(Boolean error, string errorMsg);
    }
}
