using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Server
{
    public class Server
    {
        static Socket listenerSocket;
        static List<ClientData> _clients = new List<ClientData>();
        private static string ipAdr = "192.168.56.1";
        private static int portNumber = 9999;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.Write("Server starting on " + ipAdr + " and port " + portNumber);
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(ipAdr), portNumber);
            listenerSocket.Bind(ip);

            Thread listenThread = new Thread(ListenThread);
            listenThread.Start();

        }

        static void ListenThread()
        {
            for (; ; )
            {
                listenerSocket.Listen(0);
                _clients.Add(new ClientData(listenerSocket.Accept()));
            }
        }

        public static void Data_IN(object cSocket)
        {
            Socket clientSocket = (Socket)cSocket;

            byte[] buffer;
            int readBytes;
            try
            {
                for (; ; )
                {
                    buffer = new byte[clientSocket.SendBufferSize];
                    readBytes = clientSocket.Receive(buffer);

                    if (readBytes > 0)
                    {
                        Package package = new Package(buffer);
                        DataManager(package);
                    }
                }
            }
            catch (SocketException e)
            {
                Console.Write("\nClient disconnected!");
            }

        }

        public static void DataManager(Package p)
        {
            Package sendP;
            ClientData c;
            switch (p.packageType)
            {
                case Package.PackageType.Registration:
                    Console.Write("\nClient " + p.senderId + " connected!");
                    break;
                case Package.PackageType.LogData:
                    Console.Write("\nClient " + p.senderId + " request find Client");
                    sendP = new Package(Package.PackageType.LogData, "server");
                    c = getClient(p.senderId);
                    c.FindUser(sendP, p.Gdata[0]);
                    break;
                case Package.PackageType.Client:
                    sendP = new Package(Package.PackageType.Client, "server");
                    c = getClient(p.senderId);
                    c.Client(p, sendP);
                    break;
                case Package.PackageType.Product:
                    sendP = new Package(Package.PackageType.Product, "server");
                    c = getClient(p.senderId);
                    c.Product(p, sendP);
                    break;
                case Package.PackageType.Order_item:
                    sendP = new Package(Package.PackageType.Order_item, "server");
                    c = getClient(p.senderId);
                    c.Order_item(p, sendP);
                    break;
                case Package.PackageType.Address:
                    sendP = new Package(Package.PackageType.Address, "server");
                    c = getClient(p.senderId);
                    c.Address(p, sendP);
                    break;
                case Package.PackageType.Payment:
                    sendP = new Package(Package.PackageType.Payment, "server");
                    c = getClient(p.senderId);
                    c.Payment(p, sendP);
                    break;
                case Package.PackageType.Order:
                    sendP = new Package(Package.PackageType.Order, "server");
                    c = getClient(p.senderId);
                    c.Order(p, sendP);
                    break;
                case Package.PackageType.Cart:
                    sendP = new Package(Package.PackageType.Cart, "server");
                    c = getClient(p.senderId);
                    c.Cart(p, sendP);
                    break;
                case Package.PackageType.Notify:
                    sendP = new Package(Package.PackageType.Notify, "server");
                    string id = p.Gdata[0];
                    Console.Write("\nClient " + p.senderId + " notify " +id);
                    c = null;
                    foreach (ClientData client in _clients)
                    {
                        if (client.userId == id)
                        {
                            c = client;
                        }
                    } 
                    if(c!=null)
                        c.Notify(p, sendP);
                    break;
            }
        }

        public static ClientData getClient(string id)
        {
            ClientData client = null;
            foreach (ClientData c in _clients)
            {
                if (c.id == id)
                {
                    client = c;
                }
            }
            return client;
        }
        
    }
}
