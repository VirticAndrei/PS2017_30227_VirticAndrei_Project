using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FoodDelivery.Controller;
using Data.Model;

namespace FoodDelivery.View
{
    public partial class AdminView : Form, IAdminView
    {
        private AdminController _adminController;
        private IStartView _startView;

        public AdminView(IStartView startView)
        {
            _startView = startView;
            InitializeComponent();
            categoryBox.SelectedIndex = 0;
        }

        public void SetController(AdminController controller)
        {
            _adminController = controller;
        }

        public void UpdateView()
        {
            this.dataGridView1.Invalidate();
            this.dataGridView2.Invalidate();
        }

        public void SetClientsInGrid(IList<Client> clients)
        {
            this.bindingSource1.DataSource = clients;
        }

        public void AddNewClient(Client client, Boolean error, string errorMsg)
        {
            if (error == false)
            {
                this.bindingSource1.Add(client);
                errorClient.Text = errorMsg;
            }
            else
                errorClient.Text = errorMsg;
        }

        public void DeleteClient(Client client, Boolean error, string errorMsg)
        {
            if (error == false)
            {
                this.bindingSource1.Remove(client);
                errorClient.Text = errorMsg;
            }
            else
                errorClient.Text = errorMsg;
        }

        public void EditClient(Boolean error, string errorMsg)
        {
            if (error == false)
            {
                errorClient.Text = errorMsg;
            }
            else
                errorClient.Text = errorMsg;
        }

        public void FindClient(Client client, Boolean error, string errorMsg)
        {
            if (error == false)
            {
                this.bindingSource1.Clear();
                this.bindingSource1.Add(client);
                errorClient.Text = errorMsg;
            }
            else
                errorClient.Text = errorMsg;
        }

        public Client GetSelectedClientFromGrid()
        {
            errorClient.Text = "";
            if (dataGridView1.SelectedRows.Count > 0)
                return (Client)this.dataGridView1.SelectedRows[0].DataBoundItem;
            else
                errorClient.Text = "Select a row from table!";
            return null;
        }

        public void SetProductsInGrid(IList<Product> products)
        {
            this.bindingSource2.DataSource = products;
        }

        public void AddNewProduct(Product product, Boolean error, string errorMsg)
        {
            if (error == false)
            {
                this.bindingSource2.Add(product);
                errorProduct.Text = errorMsg;
            }
            else
                errorProduct.Text = errorMsg;
        }

        public void DeleteProduct(Product product, Boolean error, string errorMsg)
        {
            if (error == false)
            {
                this.bindingSource2.Remove(product);
                errorProduct.Text = errorMsg;
            }
            else
                errorProduct.Text = errorMsg;
        }

        public void EditProduct(Boolean error, string errorMsg)
        {
            if (error == false)
            {
                errorProduct.Text = errorMsg;
            }
            else
                errorProduct.Text = errorMsg;
        }

        public void FindProduct(Product product, Boolean error, string errorMsg)
        {
            if (error == false)
            {
                this.bindingSource2.Clear();
                this.bindingSource2.Add(product);
                errorProduct.Text = errorMsg;
            }
            else
                errorProduct.Text = errorMsg;
        }

        public Product GetSelectedProductFromGrid()
        {
            errorProduct.Text = "";
            if (dataGridView2.SelectedRows.Count > 0)
                return (Product)this.dataGridView2.SelectedRows[0].DataBoundItem;
            else
                errorProduct.Text = "Select a row from table!";
            return null;
        }

        private void AdminView_FormClosing(object sender, FormClosingEventArgs e)
        {
            _startView.Logout();
            _startView.Open();
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addUserBtn_Click(object sender, EventArgs e)
        {
            errorClient.Text = "";
            _adminController.AddNewClient(fnameTxt.Text, lnameTxt.Text, passTxt.Text, emailTxt.Text, phoneTxt.Text);
        }

        private void editUserBtn_Click(object sender, EventArgs e)
        {
            errorClient.Text = "";
            _adminController.EditClient(fnameTxt.Text, lnameTxt.Text, passTxt.Text, emailTxt.Text, phoneTxt.Text);
        }

        private void delUserBtn_Click(object sender, EventArgs e)
        {
            errorClient.Text = "";
            _adminController.DeleteClient();
        }

        private void findUserBtn_Click(object sender, EventArgs e)
        {
            errorClient.Text = "";
            _adminController.FindClient(findUserTxt.Text);
        }

        private void findAllUsersBtn_Click(object sender, EventArgs e)
        {
            errorClient.Text = "";
            _adminController.SetClients();
        }

        private void addProdBtn_Click(object sender, EventArgs e)
        {
            errorProduct.Text = "";
            _adminController.AddNewProduct(nameTxt.Text, priceTxt.Text, descriptionTxt.Text, categoryBox.SelectedItem.ToString(), gramsTxt.Text);
        }

        private void editProdBtn_Click(object sender, EventArgs e)
        {
            errorProduct.Text = "";
            _adminController.EditProduct(nameTxt.Text, priceTxt.Text, descriptionTxt.Text, categoryBox.SelectedItem.ToString(), gramsTxt.Text);
        }

        private void delProdBtn_Click(object sender, EventArgs e)
        {
            errorProduct.Text = "";
            _adminController.DeleteProduct();
        }

        private void findProdBtn_Click(object sender, EventArgs e)
        {
            errorProduct.Text = "";
            _adminController.FindProduct(findProdTxt.Text);
        }

        private void findAllProdBtn_Click(object sender, EventArgs e)
        {
            errorProduct.Text = "";
            _adminController.SetProducts();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];
                Client client = (Client)selectedRow.DataBoundItem;
                passTxt.Text = client.Password;
                fnameTxt.Text = client.FirstName;
                lnameTxt.Text = client.LastName;
                emailTxt.Text = client.Email;
                phoneTxt.Text = client.Phone;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView2.Rows[rowIndex];
                Product product = (Product)selectedRow.DataBoundItem;
                nameTxt.Text = product.Name;
                priceTxt.Text = ""+product.Price;
                descriptionTxt.Text = product.Description;
                int index = categoryBox.Items.IndexOf(product.Category);
                categoryBox.SelectedIndex = index; ;
                gramsTxt.Text = ""+product.Grams;
            }
        }


    }
}
