using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodDelivery.Controller;
using Data.Model;

namespace FoodDelivery.View
{
    public interface IAdminView
    {
        void SetController(AdminController controller);
        void UpdateView();

        void SetClientsInGrid(IList<Client> clients);
        void AddNewClient(Client client, Boolean error, string errorMsg);
        void DeleteClient(Client client, Boolean error, string errorMsg);
        void EditClient(Boolean error, string errorMsg);
        void FindClient(Client client, Boolean error, string errorMsg);
        Client GetSelectedClientFromGrid();

        void SetProductsInGrid(IList<Product> products);
        void AddNewProduct(Product product, Boolean error, string errorMsg);
        void DeleteProduct(Product product, Boolean error, string errorMsg);
        void EditProduct(Boolean error, string errorMsg);
        void FindProduct(Product product, Boolean error, string errorMsg);
        Product GetSelectedProductFromGrid();
    }
}
