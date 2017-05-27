using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodDelivery.View;
using System.Windows.Forms;
using Data.Model;
using FoodDelivery.Validate;

namespace FoodDelivery.Controller
{
    public class StartController
    {
        private IStartView _startView;
        private Boolean loged;
        private ClientOnServer _client;
        private int id;

        public StartController(IStartView startView, ClientOnServer client)
        {
            startView.SetController(this);
            _startView = startView;
            _client = client;
            loged = false;

        }

        public void Disconnect()
        {
            _client.Disconnect();
        }

        public void OpenAccountView()
        {
            if (loged == true)
                OpenAccount("login");
            else
                MessageBox.Show("Log in or Sign in first!", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public void Login(string username, string password)
        {
            Boolean error = false;
            Encrypt en = new Encrypt(password);
            String pass = en.getEncriptedPass();
            if (username == "Admin" || username == "Delivery")
            {
                if (username == "Admin")
                {
                    _client.SendLogData(username);
                    if (pass == "41b5f3ba46fb37a22189d21891c1597586c4775274f9c236fea0f33a4a3af186")
                    {
                        _startView.ValidateLog("Correct!");
                        AdminView admin = new AdminView(_startView);
                        AdminController adminController = new AdminController(admin, _client);
                        _startView.CloseView();
                        admin.ShowDialog();
                    }
                    else
                        _startView.ValidateLog("Incorrect admin password!");
                }
                if (username == "Delivery") 
                {
                    _client.SendLogData(username);
                    if (pass == "41b5f3ba46fb37a22189d21891c1597586c4775274f9c236fea0f33a4a3af186")
                    {
                        _startView.ValidateLog("Correct!");
                        DeliveryView delivery = new DeliveryView(_startView);
                        DeliveryController deliveryController = new DeliveryController(delivery, _client);
                        _startView.CloseView();
                        delivery.ShowDialog();
                    }
                    else
                        _startView.ValidateLog("Incorrect delivery password!");
                }
            }
            else
            {
                _client.SendLogData(username);
                List<Client> clientList = _client.getClients();
                Client client = null;
                if (clientList.Count != 0)
                    client = clientList[0];
                if (client == null)
                {
                    client = new Client();
                    client.UserName = "";
                    client.Password = "";
                }
                if (client.Password != pass)
                {
                    _startView.ValidateLog("Incorrect password!");
                    error = true;
                }
                if (client.UserName != username)
                {
                    _startView.ValidateLog("Incorrect username!");
                    error = true;
                }
                if (error == false)
                {
                    this.id = client.Id;
                    _startView.ValidateLog("Correct!");
                    OpenAccount("login");
                    loged = true;
                }
            }
        }

        public void Logout()
        {
            loged = false;
            _startView.Logout();
        }

        public void Signin(string firstName, string lastName, string password, string confirmPass, string email)
        {
            _client.SendClientData("findAll", "0", "","","","","","","1","1");
            List<Client> clientsList = _client.getClients();
            int last = clientsList.Count;
            Client newClient = new Client();
            newClient.Id = clientsList.ElementAt(last - 1).Id + 1;
            newClient.UserName = firstName + "." + lastName;
            newClient.Password = password;
            newClient.FirstName = firstName;
            newClient.LastName = lastName;
            newClient.Email = email;
            newClient.Phone = "0000000000";
            ValidateClient clientV = new ValidateClient(newClient);
            clientV.validate();
            if ((clientV.isValid() == true) && (password == confirmPass))
            {
                newClient.Phone = "";
                _client.SendAddressData("findAll", "0" , "", "", "", "", "", "");
                List<Address> addressList = _client.getAddresses();
                last = addressList.Count;
                int idaddress = addressList.ElementAt(last - 1).Id + 1;
                _client.SendAddressData("create", "" + idaddress, "", "", "", "", "", "");
                addressList.Add(new Address { Id = idaddress, Line1 = "", Line2 = "", Line3 = "", City = "", Postcode = "", State = "" });
                newClient.Address_id = idaddress;
                _client.SendPaymentData("findAll", "0" , "", "", "", "0");
                List<Payment> paymentsList = _client.getPayments();
                last = paymentsList.Count;
                int idpayment = paymentsList.ElementAt(last - 1).Id + 1;
                _client.SendPaymentData("create", "" + idpayment, "", "", "", "0");
                paymentsList.Add(new Payment { Id = idpayment, Card_number = "", Holder_name = "", Exp_date = "", Security_code = 0 });
                newClient.Payment_id = idpayment;
                _client.SendClientData("find", "0", newClient.UserName, "", "", "", "", "", "" + idaddress, "" + idpayment);
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
                    this._startView.ValidateSign("Correct!");
                    _client.SendClientData("create", "" + newClient.Id, newClient.UserName, newClient.Password, newClient.FirstName, newClient.LastName, newClient.Email, newClient.Phone, "" + newClient.Address_id, "" + newClient.Payment_id);
                    this.id = newClient.Id;
                    OpenAccount("login");
                    loged = true;
                }
                else
                {
                    this._startView.ValidateSign("Client already exist!");
                }
            }
            else
            {
                this._startView.ValidateSign(clientV.getErrorMsg());
            }
        }

        public void OpenAccount(string type)
        {
            AccountView account = new AccountView(_startView);
            AccountController accountController = new AccountController(account,type, id,_client);
            _startView.CloseView();
            account.ShowDialog();
        }

        public void OpenMeniu(string category)
        {
            MenuView menu = new MenuView(_startView);
            MenuController menuController = new MenuController(menu,category,loged,id,_client);
            _startView.CloseView();
            menu.ShowDialog();
        }
    }
}
