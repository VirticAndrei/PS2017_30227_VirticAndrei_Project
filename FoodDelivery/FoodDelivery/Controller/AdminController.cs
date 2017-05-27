using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodDelivery.View;
using Data.Model;
using FoodDelivery.Validate;

namespace FoodDelivery.Controller
{
    public class AdminController
    {
        private IAdminView _adminView;
        private ClientOnServer _client;
        private List<Client> clientsList;
        private List<Product> productsList;
        private List<Address> addressList;
        private List<Payment> paymentsList;

        public AdminController(IAdminView adminView, ClientOnServer client)
        {
            adminView.SetController(this);
            _adminView = adminView;
            _client = client;
            SetClients();
            SetProducts();
        }

        public void SetClients()
        {
            clientsList = new List<Client>();
            _client.SendClientData("findAll", "0", "", "", "", "", "","","1","1");
            clientsList = _client.getClients();
            addressList = new List<Address>();
            _client.SendAddressData("findAll", "0", "", "", "", "", "","");
            addressList = _client.getAddresses();
            paymentsList = new List<Payment>();
            _client.SendPaymentData("findAll", "0", "", "", "", "1");
            paymentsList = _client.getPayments();
            _adminView.SetClientsInGrid(clientsList);
        }


        public void AddNewClient(string firstName, string lastName, string password, string email, string phone)
        {
            int last = clientsList.Count;
            Client newClient = new Client();
            newClient.Id = clientsList.ElementAt(last - 1).Id + 1;
            newClient.UserName = firstName+"."+lastName;
            newClient.Password = password;
            newClient.FirstName = firstName;
            newClient.LastName = lastName;
            newClient.Email = email;
            newClient.Phone =phone;
            ValidateClient clientV = new ValidateClient(newClient);
            clientV.validate();
            if (clientV.isValid() == true)
            {
                last = addressList.Count;
                int idaddress = addressList.ElementAt(last - 1).Id + 1;
                _client.SendAddressData("create", "" + idaddress, "", "", "", "", "", "");
                addressList.Add(new Address {Id=idaddress,Line1="",Line2="",Line3="",City="",Postcode="",State=""});
                newClient.Address_id = idaddress;
                last = paymentsList.Count;
                int idpayment = paymentsList.ElementAt(last - 1).Id + 1;
                _client.SendPaymentData("create", "" + idpayment, "", "", "", "0");
                paymentsList.Add(new Payment {Id=idpayment,Card_number="",Holder_name="",Exp_date="",Security_code=0});
                newClient.Payment_id = idpayment;
                _client.SendClientData("find", "0", newClient.UserName, "", "", "", "","",""+idaddress,""+idpayment);
                List<Client> clients = _client.getClients();
                Client client;
                if (clients.Count == 0)
                    client = null;
                else
                    client = clients[0];
                if (client == null)
                {
                    Encrypt e = new Encrypt(password);
                    newClient.Password = e.getEncriptedPass();
                    this._adminView.AddNewClient(newClient, false, "Insert Complete!");
                    _client.SendClientData("create", "" + newClient.Id, newClient.UserName, newClient.Password, newClient.FirstName, newClient.LastName, newClient.Email,newClient.Phone,""+newClient.Address_id,""+newClient.Payment_id);
                    _adminView.UpdateView();
                }
                else
                {
                    this._adminView.AddNewClient(newClient, true, "Client already exist!");
                }
            }
            else
            {
                this._adminView.AddNewClient(newClient, true, clientV.getErrorMsg());
            }
        }

        public void EditClient(string firstName, string lastName, string password, string email, string phone)
        {
            Client client = _adminView.GetSelectedClientFromGrid();
            if (client != null)
            {
                client.UserName = firstName+"."+lastName;
                client.Password = password;
                client.FirstName = firstName;
                client.LastName = lastName;
                client.Email = email;
                client.Phone = phone;
                ValidateClient clientV = new ValidateClient(client);
                clientV.validate();
                if (clientV.isValid() == true)
                {
                    Encrypt e = new Encrypt(password);
                    client.Password = e.getEncriptedPass();
                    _client.SendClientData("update", "" + client.Id, client.UserName, client.Password, client.FirstName, client.LastName, client.Email, client.Phone, "" + client.Address_id, "" + client.Payment_id);
                    this._adminView.EditClient(false, "Edit complete");
                    _adminView.UpdateView();
                }
                else
                {
                    this._adminView.EditClient(true, clientV.getErrorMsg());
                }
            }
        }

        public void DeleteClient()
        {
            Client client = _adminView.GetSelectedClientFromGrid();
            if (client != null)
            {
                _client.SendClientData("delete", "" + client.Id, client.UserName, client.Password, client.FirstName, client.LastName, client.Email, client.Phone, ""+client.Address_id, ""+client.Payment_id);
                _client.SendAddressData("delete", "" + client.Address_id, "", "", "", "", "", "");
                _client.SendPaymentData("delete", "" + client.Payment_id, "", "", "", "0");
                this._adminView.DeleteClient(client, false, "Delete complete!");
                _adminView.UpdateView();
            }
        }

        public void FindClient(string username)
        {
            _client.SendClientData("find", "0", username, "", "", "", "","","1","1");
            List<Client> clients = _client.getClients();
            Client client;
            if (clients.Count == 0)
                client = null;
            else
                client = clients[0];
            if (client != null)
            {
                _adminView.FindClient(client, false, "Client found!");
            }
            else
                _adminView.FindClient(client, true, "Client not found!");
        }

        public void SetProducts()
        {
            productsList = new List<Product>();
            _client.SendProductData("findAll", "0", "", "0.0", "", "", "0.0");
            productsList = _client.getProducts();
            _adminView.SetProductsInGrid(productsList);
        }


        public void AddNewProduct(string name, string price, string description, string category, string grams)
        {
            int last = productsList.Count;
            string id = "" + productsList.ElementAt(last - 1).Id + 1;
            Product newProd = new Product();
            newProd.Id = productsList.ElementAt(last - 1).Id + 1;
            newProd.Name = name;
            newProd.Price = Double.Parse(price);
            newProd.Description = description;
            newProd.Category = category;
            newProd.Grams = Double.Parse(grams);
            ValidateProduct productV = new ValidateProduct(newProd);
            productV.validate();
            if (productV.isValid() == true)
            {
                _client.SendProductData("find", "0", newProd.Name, "0.0", "", "", "0.0");
                List<Product> products = _client.getProducts();
                Product product;
                if (products.Count == 0)
                    product = null;
                else
                    product = products[0];
                if (product == null)
                {
                    this._adminView.AddNewProduct(newProd, false, "Insert Complete!");
                    _client.SendProductData("create", "" + newProd.Id, newProd.Name, ""+newProd.Price, newProd.Description, newProd.Category, ""+newProd.Grams);
                    _adminView.UpdateView();
                }
                else
                {
                    this._adminView.AddNewProduct(newProd, true, "Product already exist!");
                }
            }
            else
            {
                this._adminView.AddNewProduct(newProd, true, productV.getErrorMsg());
            }
        }

        public void EditProduct(string name, string price, string description, string category, string grams)
        {
            Product product = _adminView.GetSelectedProductFromGrid();
            if (product != null)
            {
                product.Name = name;
                product.Price = Double.Parse(price);
                product.Description = description;
                product.Category = category;
                product.Grams = Double.Parse(grams); ;
                ValidateProduct productV = new ValidateProduct(product);
                productV.validate();
                if (productV.isValid() == true)
                {
                    _client.SendProductData("update", "" + product.Id, product.Name, ""+product.Price, product.Description, product.Category, ""+product.Grams);
                    this._adminView.EditProduct(false, "Edit complete");
                    _adminView.UpdateView();
                }
                else
                {
                    this._adminView.EditProduct(true, productV.getErrorMsg());
                }
            }
        }

        public void DeleteProduct()
        {
            Product product = _adminView.GetSelectedProductFromGrid();
            if (product != null)
            {
                _client.SendProductData("delete", "" + product.Id, product.Name, "" + product.Price, product.Description, product.Category, "" + product.Grams);
                this._adminView.DeleteProduct(product, false, "Delete complete!");
                _adminView.UpdateView();
            }
        }

        public void FindProduct(string name)
        {
            _client.SendProductData("find", "0", name, "0.0", "", "", "0.0");
            List<Product> products = _client.getProducts();
            Product product;
            if (products.Count == 0)
                product = null;
            else
                product = products[0];
            if (product != null)
            {
                _adminView.FindProduct(product, false, "Product found!");
            }
            else
                _adminView.FindProduct(product, true, "Product not found!");
        }
    }
}
