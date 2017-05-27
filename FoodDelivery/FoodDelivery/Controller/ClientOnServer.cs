using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using Data.Model;
using System.Net;
using Data;
using System.Windows.Forms;
using FoodDelivery.View;

namespace FoodDelivery.Controller
{
    public class ClientOnServer
    {
        private static string ipAdr;
        private static string id;
        private static int portNumber;
        private static bool response;
        private static Socket master;
        private static Thread t;
        private static List<Client> clients = new List<Client>();
        private static List<Product> products = new List<Product>();
        private static List<Order> orders = new List<Order>();
        private static List<Address> addresses = new List<Address>();
        private static List<Order_item> order_items = new List<Order_item>();
        private static List<Payment> payments = new List<Payment>();
        private static List<Cart> carts = new List<Cart>();

        public ClientOnServer(string ip, int port)
        {
            ipAdr = ip;
            portNumber = port;
            response = false;
            Connect();
        }

        public static void Connect()
        {
            master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(ipAdr), portNumber);
            try
            {
                master.Connect(ip);
            }
            catch
            {

            }

            t = new Thread(Data_IN);
            t.Start();
        }

        public void Disconnect()
        {
            t.Abort();
        }

        public static void Data_IN()
        {
            byte[] buffer;
            int readBytes;

            for (; ; )
            {
                try
                {
                    buffer = new byte[master.SendBufferSize];
                    readBytes = master.Receive(buffer);

                    if (readBytes > 0)
                    {
                        DataManager(new Package(buffer));
                    }
                }
                catch (SocketException ex)
                {
                    
                    Environment.Exit(ex.ErrorCode);
                }
            }
        }

        public static void DataManager(Package p)
        {
            switch (p.packageType)
            {
                case Package.PackageType.Registration:
                    id = p.Gdata[0];
                    IamConnected();
                    break;
                case Package.PackageType.LogData:
                    clients = p.clients;
                    response = true;
                    break;
                case Package.PackageType.Client:
                    clients = p.clients;
                    response = true;
                    break;
                case Package.PackageType.Product:
                    products = p.products;
                    response = true;
                    break;
                case Package.PackageType.Order:
                    orders = p.orders;
                    response = true;
                    break;
                case Package.PackageType.Order_item:
                    order_items = p.order_items;
                    response = true;
                    break;
                case Package.PackageType.Address:
                    addresses = p.addresses;
                    response = true;
                    break;
                case Package.PackageType.Payment:
                    payments = p.payments;
                    response = true;
                    break;
                case Package.PackageType.Cart:
                    carts = p.carts;
                    response = true;
                    break;
                case Package.PackageType.Notify:
                    MessageBox.Show(p.Gdata[0], "Notify", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
            }
        }

        public void SendLogData(string username)
        {
            Package p = new Package(Package.PackageType.LogData, id);
            p.Gdata.Add(username);
            master.Send(p.ToBytes());
            wait();
        }

        public void SendClientData(string command, string id_client, string username, string password, string fname, string lname, string email, string phone, string address_id, string payment_id)
        {
            Package p = new Package(Package.PackageType.Client, id);
            p.command = command;
            p.Gdata.Add(id_client);
            p.Gdata.Add(username);
            p.Gdata.Add(password);
            p.Gdata.Add(fname);
            p.Gdata.Add(lname);
            p.Gdata.Add(email);
            p.Gdata.Add(phone);
            p.Gdata.Add(address_id);
            p.Gdata.Add(payment_id);
            master.Send(p.ToBytes());
            wait();
        }

        public void SendProductData(string command, string id_product, string name, string price, string description, string category, string grams)
        {
            Package p = new Package(Package.PackageType.Product, id);
            p.command = command;
            p.Gdata.Add(id_product);
            p.Gdata.Add(name);
            p.Gdata.Add(price);
            p.Gdata.Add(description);
            p.Gdata.Add(category);
            p.Gdata.Add(grams);
            master.Send(p.ToBytes());
            wait();
        }

        public void SendOrderData(string command, string id_order, string client_id, string total, string order_date, string order_status, string name, string address, string phone)
        {
            Package p = new Package(Package.PackageType.Order, id);
            p.command = command;
            p.Gdata.Add(id_order);
            p.Gdata.Add(client_id);
            p.Gdata.Add(total);
            p.Gdata.Add(order_date);
            p.Gdata.Add(order_status);
            p.Gdata.Add(name);
            p.Gdata.Add(address);
            p.Gdata.Add(phone);
            master.Send(p.ToBytes());
            wait();
        }

        public void SendAddressData(string command, string id_address, string line1, string line2, string line3, string city, string postcode, string state)
        {
            Package p = new Package(Package.PackageType.Address, id);
            p.command = command;
            p.Gdata.Add(id_address);
            p.Gdata.Add(line1);
            p.Gdata.Add(line2);
            p.Gdata.Add(line3);
            p.Gdata.Add(city);
            p.Gdata.Add(postcode);
            p.Gdata.Add(state);
            master.Send(p.ToBytes());
            wait();
        }

        public void SendPaymentData(string command, string id_payment, string card_number, string holder_name, string exp_date, string security_code)
        {
            Package p = new Package(Package.PackageType.Payment, id);
            p.command = command;
            p.Gdata.Add(id_payment);
            p.Gdata.Add(card_number);
            p.Gdata.Add(holder_name);
            p.Gdata.Add(exp_date);
            p.Gdata.Add(security_code);
            master.Send(p.ToBytes());
            wait();
        }

        public void SendOrder_itemData(string command, string id_orderItem, string product_id, string order_id, string quantity, string price)
        {
            Package p = new Package(Package.PackageType.Order_item, id);
            p.command = command;
            p.Gdata.Add(id_orderItem);
            p.Gdata.Add(product_id);
            p.Gdata.Add(order_id);
            p.Gdata.Add(quantity);
            p.Gdata.Add(price);
            master.Send(p.ToBytes());
            wait();
        }

        public void SendCartData(string command, string id_cart, string client_id, string product_id, string quantity, string total)
        {
            Package p = new Package(Package.PackageType.Cart, id);
            p.command = command;
            p.Gdata.Add(id_cart);
            p.Gdata.Add(client_id);
            p.Gdata.Add(product_id);
            p.Gdata.Add(quantity);
            p.Gdata.Add(total);
            master.Send(p.ToBytes());
            wait();
        }

        public void Notify(string idClient, string Msg)
        {
            Package p = new Package(Package.PackageType.Notify, id);
            p.Gdata.Add(idClient);
            p.Gdata.Add(Msg);
            master.Send(p.ToBytes());
        }

        public static void IamConnected()
        {
            Package p = new Package(Package.PackageType.Registration, id);
            master.Send(p.ToBytes());
        }

        public void wait()
        {
            while (response == false)
            {
            }
            response = false;
        }

        public List<Client> getClients()
        {
            return clients;
        }

        public List<Product> getProducts()
        {
            return products;
        }

        public List<Order> getOrders()
        {
            return orders;
        }

        public List<Payment> getPayments()
        {
            return payments;
        }

        public List<Address> getAddresses()
        {
            return addresses;
        }

        public List<Order_item> getOrder_items()
        {
            return order_items;
        }

        public List<Cart> getCarts()
        {
            return carts;
        }
    }
}
