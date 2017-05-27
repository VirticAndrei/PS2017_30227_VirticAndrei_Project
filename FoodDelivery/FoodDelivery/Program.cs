using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FoodDelivery.Controller;


namespace FoodDelivery
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            StartView startView = new StartView();
            ClientOnServer client = new ClientOnServer("192.168.56.1", 9999);
            StartController startController = new StartController(startView, client);
            Application.Run(startView);
        }
    }
}
