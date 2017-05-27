using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using Data.Model;
using System.Net.Sockets;
using System.Threading;
using Server.Gateway;

namespace Server
{
    public class ClientData
    {
        public Socket clientSocket;
        public Thread clientThread;
        public string id;
        public string userId;
        public ClientGateway clientG = new ClientGateway();
        public ProductGateway prodG = new ProductGateway();
        public PaymentGateway paymentG = new PaymentGateway();
        public AddressGateway addressG = new AddressGateway();
        public CartGateway cartG = new CartGateway();
        public OrderGateway orderG = new OrderGateway();
        public Order_itemGateway order_itemG = new Order_itemGateway();

        public ClientData()
        {
            id = Guid.NewGuid().ToString();
            clientThread = new Thread(Server.Data_IN);
            clientThread.Start(clientSocket);
            SendRegistrationPackage();
        }

        public ClientData(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
            id = Guid.NewGuid().ToString();
            clientThread = new Thread(Server.Data_IN);
            clientThread.Start(clientSocket);
            SendRegistrationPackage();
        }

        public void SendRegistrationPackage()
        {
            Package p = new Package(Package.PackageType.Registration, "server");
            p.Gdata.Add(id);
            clientSocket.Send(p.ToBytes());
        }

        public void FindUser(Package p, string username)
        {
            if (username == "Admin" || username == "Delivery")
            {
                if (username == "Delivery")
                {
                    this.userId = "delivery";
                    clientSocket.Send(p.ToBytes());
                }
                if (username == "Admin")
                {
                    this.userId = "admin";
                    clientSocket.Send(p.ToBytes());
                }
            }
            else
            {
                Client client = clientG.findBy(username);
                if(client != null)
                    this.userId = "" + client.Id;
                p.clients.Add(client);
                clientSocket.Send(p.ToBytes());
            }

        }

        public void Notify(Package rp, Package p)
        {
            p.Gdata.Add(rp.Gdata[1]);
            clientSocket.Send(p.ToBytes());
        }

        public void Client(Package rp, Package p)
        {
            Client client = new Client();
            client.Id = Int32.Parse(rp.Gdata[0]);
            client.UserName = rp.Gdata[1];
            client.Password = rp.Gdata[2];
            client.FirstName = rp.Gdata[3];
            client.LastName = rp.Gdata[4];
            client.Email = rp.Gdata[5];
            client.Phone = rp.Gdata[6];
            client.Address_id = Int32.Parse(rp.Gdata[7]);
            client.Payment_id = Int32.Parse(rp.Gdata[8]);

            switch (rp.command)
            {
                case "create":
                    clientG.create(client);
                    Console.Write("\nClient " + rp.senderId + " create new client");
                    break;
                case "update":
                    clientG.update(client);
                    Console.Write("\nClient " + rp.senderId + " update client");
                    break;
                case "delete":
                    clientG.delete(client);
                    Console.Write("\nClient " + rp.senderId + " delete client");
                    break;
                case "findById":
                    p.clients.Add(clientG.findById(rp.Gdata[0]));
                    Console.Write("\nClient " + rp.senderId + " search client");
                    break;
                case "find":
                    p.clients.Add(clientG.findBy(rp.Gdata[1]));
                    Console.Write("\nClient " + rp.senderId + " search client");
                    break;
                case "findAll":
                    p.clients = clientG.findAll();
                    Console.Write("\nClient " + rp.senderId + " search all clients");
                    break;
            }

            clientSocket.Send(p.ToBytes());
        }

        public void Product(Package rp, Package p)
        {
            Product product = new Product();
            product.Id = Int32.Parse(rp.Gdata[0]);
            product.Name = rp.Gdata[1];
            product.Price = Double.Parse(rp.Gdata[2]);
            product.Description = rp.Gdata[3];
            product.Category = rp.Gdata[4];
            product.Grams = Double.Parse(rp.Gdata[5]);

            switch (rp.command)
            {
                case "create":
                    prodG.create(product);
                    Console.Write("\nClient " + rp.senderId + " create new product");
                    break;
                case "update":
                    prodG.update(product);
                    Console.Write("\nClient " + rp.senderId + " update product");
                    break;
                case "delete":
                    prodG.delete(product);
                    Console.Write("\nClient " + rp.senderId + " delete product");
                    break;
                case "find":
                    p.products.Add(prodG.findBy(rp.Gdata[1]));
                    Console.Write("\nClient " + rp.senderId + " search product by name");
                    break;
                case "findAll":
                    p.products = prodG.findAll();
                    Console.Write("\nClient " + rp.senderId + " search all products");
                    break;
                case "findBy":
                    p.products = prodG.findByCategory(rp.Gdata[4]);
                    Console.Write("\nClient " + rp.senderId + " search product by category");
                    break;
            }

            clientSocket.Send(p.ToBytes());
        }

        public void Payment(Package rp, Package p)
        {
            Payment payment = new Payment();
            payment.Id = Int32.Parse(rp.Gdata[0]);
            payment.Card_number = rp.Gdata[1];
            payment.Holder_name = rp.Gdata[2];
            payment.Exp_date = rp.Gdata[3];
            payment.Security_code = Int32.Parse(rp.Gdata[4]);

            switch (rp.command)
            {
                case "create":
                    paymentG.create(payment);
                    Console.Write("\nClient " + rp.senderId + " create new payment method");
                    break;
                case "update":
                    paymentG.update(payment);
                    Console.Write("\nClient " + rp.senderId + " update payment method");
                    break;
                case "delete":
                    paymentG.delete(payment);
                    Console.Write("\nClient " + rp.senderId + " delete payment");
                    break;
                case "find":
                    p.payments.Add(paymentG.findBy(rp.Gdata[0]));
                    Console.Write("\nClient " + rp.senderId + " search payment");
                    break;
                case "findAll":
                    p.payments = paymentG.findAll();
                    Console.Write("\nClient " + rp.senderId + " search all payments");
                    break;
            }

            clientSocket.Send(p.ToBytes());
        }

        public void Address(Package rp, Package p)
        {
            Address address = new Address();
            address.Id = Int32.Parse(rp.Gdata[0]);
            address.Line1 = rp.Gdata[1];
            address.Line2 = rp.Gdata[2];
            address.Line3 = rp.Gdata[3];
            address.City = rp.Gdata[4];
            address.Postcode = rp.Gdata[5];
            address.State = rp.Gdata[6];

            switch (rp.command)
            {
                case "create":
                    addressG.create(address);
                    Console.Write("\nClient " + rp.senderId + " create new address");
                    break;
                case "update":
                    addressG.update(address);
                    Console.Write("\nClient " + rp.senderId + " update address");
                    break;
                case "delete":
                    addressG.delete(address);
                    Console.Write("\nClient " + rp.senderId + " delete address");
                    break;
                case "find":
                    p.addresses.Add(addressG.findBy(rp.Gdata[0]));
                    Console.Write("\nClient " + rp.senderId + " search address");
                    break;
                case "findAll":
                    p.addresses = addressG.findAll();
                    Console.Write("\nClient " + rp.senderId + " search all addresses");
                    break;
            }

            clientSocket.Send(p.ToBytes());
        }

        public void Cart(Package rp, Package p)
        {
            Cart cart = new Cart();
            cart.Id = Int32.Parse(rp.Gdata[0]);
            cart.Client_id = Int32.Parse(rp.Gdata[1]);
            cart.Product_id = Int32.Parse(rp.Gdata[2]);
            cart.Quantity = Int32.Parse(rp.Gdata[3]);
            cart.Total = Double.Parse(rp.Gdata[4]);

            switch (rp.command)
            {
                case "create":
                    cartG.create(cart);
                    Console.Write("\nClient " + rp.senderId + " create new cart");
                    break;
                case "update":
                    cartG.update(cart);
                    Console.Write("\nClient " + rp.senderId + " update cart");
                    break;
                case "delete":
                    cartG.delete(cart);
                    Console.Write("\nClient " + rp.senderId + " delete cart");
                    break;
                case "find":
                    p.carts = cartG.findBy(rp.Gdata[1]);
                    Console.Write("\nClient " + rp.senderId + " search cart");
                    break;
            }

            clientSocket.Send(p.ToBytes());
        }

        public void Order(Package rp, Package p)
        {
            Order order = new Order();
            order.Id = Int32.Parse(rp.Gdata[0]);
            order.Client_id = Int32.Parse(rp.Gdata[1]);
            order.Total = Double.Parse(rp.Gdata[2]);
            order.Order_date = rp.Gdata[3];
            order.Order_status = rp.Gdata[4];
            order.Name = rp.Gdata[5];
            order.Address = rp.Gdata[6];
            order.Phone = rp.Gdata[7];
            switch (rp.command)
            {
                case "create":
                    orderG.create(order);
                    Console.Write("\nClient " + rp.senderId + " create new order");
                    break;
                case "update":
                    orderG.update(order);
                    Console.Write("\nClient " + rp.senderId + " update order");
                    break;
                case "delete":
                    orderG.delete(order);
                    Console.Write("\nClient " + rp.senderId + " delete order");
                    break;
                case "find":
                    p.orders = orderG.findBy(rp.Gdata[1]);
                    Console.Write("\nClient " + rp.senderId + " search order");
                    break;
                case "findBy":
                    p.orders = orderG.findByStatus(rp.Gdata[4]);
                    Console.Write("\nClient " + rp.senderId + " search order");
                    break;
                case "findAll":
                    p.orders = orderG.findAll();
                    Console.Write("\nClient " + rp.senderId + " search order");
                    break;
            }

            clientSocket.Send(p.ToBytes());
        }

        public void Order_item(Package rp, Package p)
        {
            Order_item order_item = new Order_item();
            order_item.Id = Int32.Parse(rp.Gdata[0]);
            order_item.Product_id = Int32.Parse(rp.Gdata[1]);
            order_item.Order_id = Int32.Parse(rp.Gdata[2]);
            order_item.Quantity = Int32.Parse(rp.Gdata[3]);
            order_item.Price = Double.Parse(rp.Gdata[4]);

            switch (rp.command)
            {
                case "create":
                    order_itemG.create(order_item);
                    Console.Write("\nClient " + rp.senderId + " create new order item");
                    break;
                case "update":
                    order_itemG.update(order_item);
                    Console.Write("\nClient " + rp.senderId + " update order item");
                    break;
                case "delete":
                    order_itemG.delete(order_item);
                    Console.Write("\nClient " + rp.senderId + " delete order item");
                    break;
                case "find":
                    p.order_items = order_itemG.findBy(rp.Gdata[2]);
                    Console.Write("\nClient " + rp.senderId + " search order item");
                    break;
            }

            clientSocket.Send(p.ToBytes());
        }
    }
}
