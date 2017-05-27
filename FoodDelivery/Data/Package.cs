using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Model;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace Data
{
    [Serializable]
    public class Package
    {
        public List<string> Gdata;
        public string senderId;
        public string command;
        public PackageType packageType;
        public List<Client> clients = new List<Client>();
        public List<Product> products = new List<Product>();
        public List<Order> orders = new List<Order>();
        public List<Address> addresses = new List<Address>();
        public List<Order_item> order_items = new List<Order_item>();
        public List<Payment> payments = new List<Payment>();
        public List<Cart> carts = new List<Cart>();

        public Package(PackageType type, string senderId)
        {
            Gdata = new List<string>();
            this.packageType = type;
            this.senderId = senderId;

        }

        public Package(byte[] packetBytes)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(packetBytes);

            Package p = (Package)bf.Deserialize(ms);
            this.Gdata = p.Gdata;
            this.senderId = p.senderId;
            this.command = p.command;
            this.packageType = p.packageType;
            this.clients = p.clients;
            this.products = p.products;
            this.orders = p.orders;
            this.addresses = p.addresses;
            this.order_items = p.order_items;
            this.payments = p.payments;
            this.carts = p.carts;
        }

        public byte[] ToBytes()
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

            bf.Serialize(ms, this);

            byte[] bytes = ms.ToArray();
            ms.Close();

            return bytes;
        }

        public enum PackageType
        {
            Registration, LogData, Client, Product, Order, Order_item, Address, Payment, Cart, Notify
        }
    }
}
