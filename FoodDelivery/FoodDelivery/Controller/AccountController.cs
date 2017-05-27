using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodDelivery.View;
using Data.Model;
using FoodDelivery.Validate;

namespace FoodDelivery.Controller
{
    public class AccountController
    {
        private IAccountView _accountView;
        private ClientOnServer _client;
        private List<Product> allProducts;
        private List<Product> productsList;
        private List<Cart> cartList;
        private string type;
        private int id, address_id, payment_id;

        public AccountController(IAccountView accountView, string type, int id, ClientOnServer client)
        {
            accountView.SetController(this);
            _accountView = accountView;
            _client = client;
            this.type = type;
            this.id = id;
            SetDetails();
            SetAddress();
            SetPayment();
            SetOrder();
        }

        public void SetDetails()
        {
            _client.SendClientData("findById", ""+id, "", "", "", "", "","","1","1");
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
                _accountView.SetAccountDetails(client);
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
                _accountView.SetAddressDetails(address);
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
                _accountView.SetPaymentDetails(payment);
            }
        }

        public void SetOrder()
        {
            allProducts = new List<Product>();
            _client.SendProductData("findAll", "0", "", "0", "", "", "0");
            allProducts = _client.getProducts();
            cartList = new List<Cart>();
            _client.SendCartData("find", "0", "" + id, "0", "0", "0");
            cartList = _client.getCarts();

            if (cartList != null)
            {
                productsList = new List<Product>();
                foreach (Cart cart in cartList)
                {
                    foreach (Product prod in allProducts)
                    {
                        if (cart.Product_id == prod.Id)
                        {
                            productsList.Add(prod);
                        }
                    }
                }
            }
            _accountView.SetOrder(cartList, productsList);
        }

        public void ChangePassword(string newPass, string confirmPass)
        {
            if (newPass == confirmPass)
            {
                if (newPass.Length > 4)
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
                        Encrypt e = new Encrypt(newPass);
                        client.Password = e.getEncriptedPass();
                        _client.SendClientData("update", "" + client.Id, client.UserName, client.Password, client.FirstName, client.LastName, client.Email, client.Phone, ""+client.Address_id,""+client.Payment_id);
                        this._accountView.SaveAccountDetails("Password changed!");
                    }
                }
                else
                {
                    _accountView.SaveAccountDetails("Password to short!");
                }
            }
            else
            {
                _accountView.SaveAccountDetails("Incorrect password!");
            }
        }

        public void SaveAccountDetails(string fname, string lname, string email, string phone)
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
                client.UserName = fname + "." + lname;
                client.FirstName = fname;
                client.LastName = lname;
                client.Email = email;
                client.Phone = phone;
                ValidateClient clientV = new ValidateClient(client);
                clientV.validate();
                if (clientV.isValid() == true)
                {
                    _client.SendClientData("update", "" + client.Id, client.UserName, client.Password, client.FirstName, client.LastName, client.Email, client.Phone, "" + client.Address_id, "" + client.Payment_id);
                    this._accountView.SaveAccountDetails("Edit complete");
                }
                else
                {
                    this._accountView.SaveAccountDetails(clientV.getErrorMsg());
                }
            }
        }

        public void SaveAdressDetails(string line1, string line2, string line3, string city, string postcode, string state)
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
                address.Line1 = line1;
                address.Line2 = line2;
                address.Line3 = line3;
                address.City = city;
                address.Postcode = postcode;
                address.State = state;
                ValidateAddress addressV = new ValidateAddress(address);
                addressV.validate();
                if (addressV.isValid() == true)
                {
                    _client.SendAddressData("update", "" + address.Id, address.Line1, address.Line2, address.Line3, address.City, address.Postcode,address.State);
                    this._accountView.SaveAddressDetails("Edit complete");
                }
                else
                {
                    this._accountView.SaveAddressDetails(addressV.getErrorMsg());
                }
            }
        }

        public void SavePaymentDetails(string card, string holder, string exp_date, string security)
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
                payment.Card_number = card;
                payment.Holder_name= holder;
                payment.Exp_date = exp_date;
                payment.Security_code = Int32.Parse(security);
                ValidatePayment paymentV = new ValidatePayment(payment);
                paymentV.validate();
                if (paymentV.isValid() == true)
                {
                    _client.SendPaymentData("update", "" + payment.Id, payment.Card_number, payment.Holder_name, payment.Exp_date, ""+payment.Security_code);
                    this._accountView.SavePaymentsDetails("Edit complete");
                }
                else
                {
                    this._accountView.SavePaymentsDetails(paymentV.getErrorMsg());
                }
            }
        }

        public void OpenMenu()
        {
            IStartView _startView = _accountView.getStartView();
            MenuView menu = new MenuView(_startView);
            MenuController menuController = new MenuController(menu, "Menu", true, id, _client);
            _accountView.CloseView();
            menu.ShowDialog();
        }

        public void RemoveItem(Cart cart, Product product)
        {
            _client.SendCartData("delete", "" + cart.Id, "" + cart.Client_id, "" + cart.Product_id, "" + cart.Quantity, "" + cart.Total);
            productsList.Remove(product);
            cartList.Remove(cart);
            _accountView.SetOrder(cartList, productsList);

        }

        public void Minus(int i, Cart cart, Product product)
        {
            if (cart.Quantity > 1)
            {
                cart.Quantity = cart.Quantity - 1;
                cart.Total = cart.Total - product.Price;
                _client.SendCartData("update", "0", "" + cart.Client_id, "" + cart.Product_id, "" + cart.Quantity, "" + cart.Total);
                cartList.ElementAt(i).Quantity = cart.Quantity;
                cartList.ElementAt(i).Total = cart.Total;
                _accountView.SetOrder(cartList, productsList);
            }
        }

        public void Plus(int i, Cart cart, Product product)
        {
            if (cart.Quantity < 9)
            {
                cart.Quantity = cart.Quantity + 1;
                cart.Total = cart.Total + product.Price;
                _client.SendCartData("update", "0", "" + cart.Client_id, "" + cart.Product_id, "" + cart.Quantity, "" + cart.Total);
                cartList.ElementAt(i).Quantity = cart.Quantity;
                cartList.ElementAt(i).Total = cart.Total;
                _accountView.SetOrder(cartList, productsList);
            }
        }

        public void PlaceOrder()
        {
            OrderView order = new OrderView();
            OrderController orderControl = new OrderController(order, _client, id, productsList, cartList);
            order.ShowDialog();
        }

        public void OrderHistory()
        {
            OrderHistoryView history = new OrderHistoryView();
            OrderHistoryController historyControl = new OrderHistoryController(history, _client, id);
            history.ShowDialog();
        }
    }
}
