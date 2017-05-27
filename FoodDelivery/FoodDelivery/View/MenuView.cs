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
    public partial class MenuView : Form, IMenuView
    {
        private MenuController _menuController;
        private IStartView _startView;

        public MenuView(IStartView startView)
        {
            _startView = startView;
            InitializeComponent();
        }

        public void SetController(MenuController controller)
        {
            this._menuController = controller;
        }

        public void SetMenu(IList<Product> products, string category)
        {
            panel4.Controls.Clear();
            Label categoryLbl = new Label();
            categoryLbl.Name = "categoryLbl";
            categoryLbl.ForeColor = Color.DarkBlue;
            categoryLbl.Location = new System.Drawing.Point(17, 6);
            categoryLbl.Font = new System.Drawing.Font("Monotype Corsiva", 26, FontStyle.Bold);
            categoryLbl.Size = new System.Drawing.Size(200,43);
            categoryLbl.Text = category;
            categoryLbl.Visible = true;
            panel4.Controls.Add(categoryLbl);
            if(products!=null)
            {
                int size = products.Count / 3;
                int rest = products.Count % 3;
                int index = 0;
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        AddProduct(products[index],i, j);
                        index++;
                    }
                }
                for (int j = 0; j < rest; j++)
                {
                    AddProduct(products[index], size, j);
                    index++;
                }
            }
        }

        public void SetOrder(Boolean log,IList<Cart> cart, IList<Product> products)
        {
            if (log == true)
            {
                
                flowOrder.Controls.Clear();
                double total = 0;
                if (cart != null)
                {
                    if (cart.Count > 0)
                    {
                        placeOrderBtn.Visible = true;
                        label18.Visible = true;
                        label19.Visible = true;
                        label20.Visible = true;
                        groupBox1.Visible = true;
                        for (int i = 0; i < cart.Count; i++)
                        {
                            AddProdToOrder(i, products[i], cart[i]);
                            total += cart[i].Total;
                        }
                    }
                    else
                    {
                        placeOrderBtn.Visible = false;
                        label18.Visible = false;
                        label19.Visible = false;
                        label20.Visible = false;
                    }
                }
                label19.Text = ""+ total;
            }
            else
            {
                groupBox1.Visible = false;
            }
        }

        public void AddProdToOrder(int i,Product product, Cart cart)
        {
            Panel item = new Panel();
            item.Name = "item" + i;
            item.Size = new System.Drawing.Size(300, 60);
            Label name = new Label();
            name.Name = "productName" + i;
            name.ForeColor = Color.White;
            name.Location = new System.Drawing.Point(2,0);
            name.Size = new System.Drawing.Size(280, 28);
            name.Font = new System.Drawing.Font("Monotype Corsiva", 18, FontStyle.Bold);
            name.Text = product.Name;
            name.Visible = true;
            item.Controls.Add(name);

            Label price = new Label();
            price.Name = "productPrice" + i;
            price.ForeColor = Color.White;
            price.Location = new System.Drawing.Point(68, 30);
            price.Size = new System.Drawing.Size(53, 22);
            price.Font = new System.Drawing.Font("Monotype Corsiva", 14, FontStyle.Bold);
            price.Text = product.Price+" lei";
            price.Visible = true;
            item.Controls.Add(price);

            Label quantity = new Label();
            quantity.Name = "productQuantity" + i;
            quantity.ForeColor = Color.White;
            quantity.Location = new System.Drawing.Point(177, 30);
            quantity.Size = new System.Drawing.Size(19, 22);
            quantity.Font = new System.Drawing.Font("Monotype Corsiva", 14, FontStyle.Bold);
            quantity.Text = ""+cart.Quantity;
            quantity.Visible = true;
            item.Controls.Add(quantity);

            Button delete = new Button();
            delete.Name = "delProd" + i;
            delete.BackColor = Color.Firebrick;
            delete.ForeColor = Color.White;
            delete.Location = new System.Drawing.Point(282, 0);
            delete.Size = new System.Drawing.Size(15, 25);
            delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12, FontStyle.Bold);
            delete.Text = "X";
            delete.UseVisualStyleBackColor = false;
            delete.FlatStyle = FlatStyle.Flat;
            delete.FlatAppearance.BorderSize = 0;
            delete.Visible = true;
            delete.Click += (sender, args) =>
            {
                _menuController.RemoveItem(cart,product);
            };
            item.Controls.Add(delete);

            Button minus = new Button();
            minus.Name = "minusBtn" + i;
            minus.BackColor = Color.Firebrick;
            minus.ForeColor = Color.White;
            minus.Location = new System.Drawing.Point(155, 28);
            minus.Size = new System.Drawing.Size(15, 25);
            minus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12, FontStyle.Bold);
            minus.Text = "-";
            minus.UseVisualStyleBackColor = false;
            minus.FlatStyle = FlatStyle.Flat;
            minus.FlatAppearance.BorderSize = 0;
            minus.Visible = true;
            minus.Click += (sender, args) =>
            {
                _menuController.Minus(i,cart,product);
            };
            item.Controls.Add(minus);

            Button plus = new Button();
            plus.Name = "plusBtn" + i;
            plus.BackColor = Color.Firebrick;
            plus.ForeColor = Color.White;
            plus.Location = new System.Drawing.Point(202, 28);
            plus.Size = new System.Drawing.Size(16, 25);
            plus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12, FontStyle.Bold);
            plus.Text = "+";
            plus.UseVisualStyleBackColor = false;
            plus.FlatStyle = FlatStyle.Flat;
            plus.FlatAppearance.BorderSize = 0;
            plus.Visible = true;
            plus.Click += (sender, args) =>
            {
                _menuController.Plus(i, cart,product);
            };
            item.Controls.Add(plus);
            flowOrder.Controls.Add(item);
        }

        public void AddProduct(Product product,int i, int j)
        {
            GroupBox groupBox = new GroupBox();
            groupBox.Name = "groupBox" + i + j;
            groupBox.Location = new System.Drawing.Point(25 + (265*j), 65 + (360*i));
            groupBox.Size = new System.Drawing.Size(230, 325);
            groupBox.Text = "";
            groupBox.Visible = true;
            TextBox name = new TextBox();
            name.Name = "productName" + i + j;
            name.Multiline = true;
            name.BackColor = SystemColors.Control;
            name.BorderStyle = BorderStyle.None;
            name.ForeColor = Color.Red;
            name.Location = new System.Drawing.Point(14, 16);
            name.Size = new System.Drawing.Size(196, 76);
            name.Font = new System.Drawing.Font("Monotype Corsiva", 22, FontStyle.Bold);
            name.Text = product.Name;
            name.Visible = true;
            name.ReadOnly = true;
            groupBox.Controls.Add(name);

            TextBox price = new TextBox();
            price.Name = "productPrice" + i + j;
            price.Multiline = true;
            price.BackColor = SystemColors.Control;
            price.BorderStyle = BorderStyle.None;
            price.ForeColor = Color.Red;
            price.Location = new System.Drawing.Point(14, 98);
            price.Size = new System.Drawing.Size(196, 53);
            price.Font = new System.Drawing.Font("Monotype Corsiva", 16, FontStyle.Bold);
            price.Text = "Price: " + product.Price + " lei" + Environment.NewLine + product.Grams + " gr";
            price.Visible = true;
            price.ReadOnly = true;
            groupBox.Controls.Add(price);

            TextBox description = new TextBox();
            description.Name = "productDescript" + i + j;
            description.Multiline = true;
            description.BackColor = SystemColors.Control;
            description.BorderStyle = BorderStyle.None;
            description.ForeColor = Color.Red;
            description.Location = new System.Drawing.Point(14, 155);
            description.Size = new System.Drawing.Size(196, 130);
            description.Font = new System.Drawing.Font("Monotype Corsiva", 16, FontStyle.Italic);
            description.Text = "Description: " + product.Description;
            description.Visible = true;
            description.ReadOnly = true;
            groupBox.Controls.Add(description);

            Button add = new Button();
            add.Name = "addProd" + i + j;
            add.BackColor = Color.Red;
            add.ForeColor = Color.Snow;
            add.Location = new System.Drawing.Point(14, 291);
            add.Size = new System.Drawing.Size(196, 28);
            add.Font = new System.Drawing.Font("Modern No. 20", 12, FontStyle.Bold);
            add.Text = "Add to order";
            add.Visible = true;
            add.Click += (sender, args) =>
            {
                _menuController.AddToOrder(product);
            };
            groupBox.Controls.Add(add);
            panel4.Controls.Add(groupBox);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            _menuController.SetMenu("Aperitive");
        }

        private void label4_Click(object sender, EventArgs e)
        {
            _menuController.SetMenu("Bauturi");
        }

        private void label5_Click(object sender, EventArgs e)
        {
            _menuController.SetMenu("Pizza");
        }

        private void label6_Click(object sender, EventArgs e)
        {
            _menuController.SetMenu("Supe");
        }

        private void label7_Click(object sender, EventArgs e)
        {
            _menuController.SetMenu("Salate");
        }

        private void label8_Click(object sender, EventArgs e)
        {
            _menuController.SetMenu("Paste");
        }

        private void label9_Click(object sender, EventArgs e)
        {
            _menuController.SetMenu("Fel principal");
        }

        private void label10_Click(object sender, EventArgs e)
        {
            _menuController.SetMenu("Burger");
        }

        private void label11_Click(object sender, EventArgs e)
        {
            _menuController.SetMenu("Peste");
        }

        private void label12_Click(object sender, EventArgs e)
        {
            _menuController.SetMenu("Garnituri");
        }

        private void label13_Click(object sender, EventArgs e)
        {
            _menuController.SetMenu("Desert");
        }

        private void label14_Click(object sender, EventArgs e)
        {
            _menuController.SetMenu("Sosuri");
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.DarkBlue;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.White;
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.DarkBlue;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.White;
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            label5.ForeColor = Color.DarkBlue;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.ForeColor = Color.White;
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.ForeColor = Color.DarkBlue;
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            label6.ForeColor = Color.White;
        }

        private void label7_MouseEnter(object sender, EventArgs e)
        {
            label7.ForeColor = Color.DarkBlue;
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            label7.ForeColor = Color.White;
        }

        private void label8_MouseEnter(object sender, EventArgs e)
        {
            label8.ForeColor = Color.DarkBlue;
        }

        private void label8_MouseLeave(object sender, EventArgs e)
        {
            label8.ForeColor = Color.White;
        }

        private void label9_MouseEnter(object sender, EventArgs e)
        {
            label9.ForeColor = Color.DarkBlue;
        }

        private void label9_MouseLeave(object sender, EventArgs e)
        {
            label9.ForeColor = Color.White;
        }

        private void label10_MouseEnter(object sender, EventArgs e)
        {
            label10.ForeColor = Color.DarkBlue;
        }

        private void label10_MouseLeave(object sender, EventArgs e)
        {
            label10.ForeColor = Color.White;
        }

        private void label11_MouseEnter(object sender, EventArgs e)
        {
            label11.ForeColor = Color.DarkBlue;
        }

        private void label11_MouseLeave(object sender, EventArgs e)
        {
            label11.ForeColor = Color.White;
        }

        private void label12_MouseEnter(object sender, EventArgs e)
        {
            label12.ForeColor = Color.DarkBlue;
        }

        private void label12_MouseLeave(object sender, EventArgs e)
        {
            label12.ForeColor = Color.White;
        }

        private void label13_MouseEnter(object sender, EventArgs e)
        {
            label13.ForeColor = Color.DarkBlue;
        }

        private void label13_MouseLeave(object sender, EventArgs e)
        {
            label13.ForeColor = Color.White;
        }

        private void label14_MouseEnter(object sender, EventArgs e)
        {
            label14.ForeColor = Color.DarkBlue;
        }

        private void label14_MouseLeave(object sender, EventArgs e)
        {
            label14.ForeColor = Color.White;
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            _startView.Open();
        }

        private void MenuView_FormClosing(object sender, FormClosingEventArgs e)
        {
            _startView.Open();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            _menuController.SetMenu("Menu");
        }

        private void placeOrderBtn_Click(object sender, EventArgs e)
        {
            _menuController.PlaceOrder();
        }
    }
}
