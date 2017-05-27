using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodDelivery.Controller;
using Data.Model;

namespace FoodDelivery.View
{
    public interface IAccountView
    {
        void SetController(AccountController controller);
        void CloseView();

        void SetAccountDetails(Client client);
        void SetAddressDetails(Address address);
        void SetPaymentDetails(Payment payment);
        void SetOrder(IList<Cart> cart, IList<Product> products);

        void SaveAccountDetails(string errorMsg);
        void SaveAddressDetails(string errorMsg);
        void SavePaymentsDetails(string errorMsg);

        IStartView getStartView();
    }
}
