using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Model;
using FoodDelivery.View;
using FoodDelivery.Validate;

namespace FoodDelivery.Controller
{
    public class OrderController
    {
        private IOrderView _orderView;
        private ClientOnServer _client;
        private List<Product> productsList;
        private List<Cart> cartList;
        private int id, address_id, payment_id;

        public OrderController(IOrderView orderView, ClientOnServer client, int id, List<Product> productsList, List<Cart> cartList)
        {
            orderView.SetController(this);
            _orderView = orderView;
            _client = client;
            this.id = id;
            this.cartList = cartList;
            this.productsList = productsList;
            SetDetails();
            SetAddress();
            SetPayment();
            SetOrder();
        }

        public void SetDetails()
        {
            _client.SendClientData("findById", "" + id, "", "", "", "", "", "", "1", "1");
            List<Client> clients = _client.getClients();
            Client client;
            if (clients.Count == 0)
                client = null;
            else
                client = clients[0];
            if (client != null)
            {
                address_id = client.Address_id;
                payment_id = client.Payment_id;
                _orderView.SetAccountDetails(client);
            }
        }

        public void SetAddress()
        {
            _client.SendAddressData("find", "" + address_id, "", "", "", "", "", "");
            List<Address> addresses = _client.getAddresses();
            Address address;
            if (addresses.Count == 0)
                address = null;
            else
                address = addresses[0];
            if (address != null)
            {
                _orderView.SetAddressDetails(address);
            }
        }

        public void SetPayment()
        {
            _client.SendPaymentData("find", "" + payment_id, "", "", "", "0");
            List<Payment> payments = _client.getPayments();
            Payment payment;
            if (payments.Count == 0)
                payment = null;
            else
                payment = payments[0];
            if (payment != null)
            {
                _orderView.SetPaymentDetails(payment);
            }
        }

        public void SetOrder()
        {
            _orderView.SetOrder(cartList, productsList);
        }

        public void FinishOrder(List<string> fields)
        {
            ValidateOrderDetails orderV = new ValidateOrderDetails(fields);
            orderV.validate();
            if (orderV.isValid() == true)
            {
                List<Order> orders = new List<Order>();
                _client.SendOrderData("findAll", "0", "0", "0", "", "","","","");
                orders = _client.getOrders();
                int last = orders.Count;
                int idOrder = 1;
                if(last > 0)
                    idOrder= orders.ElementAt(last - 1).Id + 1;
                string total = fields[11];
                string name = fields[0]+" "+fields[1];
                string address = fields[2] + Environment.NewLine + fields[3] + Environment.NewLine + fields[4] + Environment.NewLine +fields[5];
                string phone = fields[6];
                DateTime now = DateTime.Now;
                Order order = new Order { Id = idOrder, Total = Double.Parse(total), Order_date = now.ToString(), Order_status = "Placed", Name = name, Address = address, Phone = phone};
                _client.SendOrderData("create", ""+idOrder, ""+id, total, order.Order_date, "Placed",name,address,phone);
                for(int i = 0; i<cartList.Count; i++)
                {
                    _client.SendOrder_itemData("create", "0", "" + productsList[i].Id, ""+idOrder, ""+cartList[i].Quantity, ""+productsList[i].Price);
                    _client.SendCartData("delete", "" + cartList[i].Id, "" + cartList[i].Client_id, "" + cartList[i].Product_id, "" + cartList[i].Quantity, "" + cartList[i].Total);
                }
                cartList.Clear();
                productsList.Clear();
                _orderView.FinishOrder(false, "Order complete!");
                _client.Notify("delivery","New order!");
            }
            else
            {
                _orderView.FinishOrder(true, orderV.getErrorMsg());
            }
        }

    }
}
