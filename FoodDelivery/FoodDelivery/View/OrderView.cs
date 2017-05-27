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
    public partial class OrderView : Form, IOrderView
    {
        private OrderController _orderController;

        public OrderView()
        {
            InitializeComponent();
            stateBox.SelectedIndex = 0;
            monthBox.SelectedIndex = 0;
            yearBox.SelectedIndex = 0;
        }

        public void SetController(OrderController controller)
        {
            _orderController = controller;
        }

        public void SetAccountDetails(Client client)
        {
            fnameTxt.Text = client.FirstName;
            lnameTxt.Text = client.LastName;
            phoneTxt.Text = client.Phone;
        }

        public void SetAddressDetails(Address address)
        {
            addressTxt.Text = address.Line1 + Environment.NewLine + address.Line2 + Environment.NewLine + address.Line3;
            cityTxt.Text = address.City;
            postCodeTxt.Text = address.Postcode;
            if (address.State.Length > 3)
            {
                int index = stateBox.Items.IndexOf(address.State);
                stateBox.SelectedIndex = index;
            }
        }

        public void SetPaymentDetails(Payment payment)
        {
            cardNumberTxt.Text = payment.Card_number;
            holderNameTxt.Text = payment.Holder_name;
            securityTxt.Text = "" + payment.Security_code;
            if (payment.Exp_date.Length == 4)
            {
                string month = payment.Exp_date.Substring(0, 2);
                string year = payment.Exp_date.Substring(2, 2);
                int index = monthBox.Items.IndexOf(month);
                monthBox.SelectedIndex = index;
                index = yearBox.Items.IndexOf(year);
                yearBox.SelectedIndex = index;
            }
        }

        public void SetOrder(IList<Cart> cart, IList<Product> products)
        {
            flowOrder.Controls.Clear();
            double total = 0;
            if (cart != null)
            {
                for (int i = 0; i < cart.Count; i++)
                {
                    AddProdToOrder(i, products[i], cart[i]);
                    total += cart[i].Total;
                }
            }
            label21.Text = "" + total;
        }

        public void AddProdToOrder(int i, Product product, Cart cart)
        {
            Panel item = new Panel();
            item.Name = "item" + i;
            item.Size = new System.Drawing.Size(300, 60);
            Label name = new Label();
            name.Name = "productName" + i;
            name.ForeColor = Color.White;
            name.Location = new System.Drawing.Point(2, 0);
            name.Size = new System.Drawing.Size(280, 28);
            name.Font = new System.Drawing.Font("Monotype Corsiva", 18, FontStyle.Bold);
            name.Text = product.Name;
            name.Visible = true;
            item.Controls.Add(name);

            Label price = new Label();
            price.Name = "productPrice" + i;
            price.ForeColor = Color.White;
            price.Location = new System.Drawing.Point(244, 30);
            price.Size = new System.Drawing.Size(53, 22);
            price.Font = new System.Drawing.Font("Monotype Corsiva", 14, FontStyle.Bold);
            price.Text = product.Price + " lei";
            price.Visible = true;
            item.Controls.Add(price);

            Label quantity = new Label();
            quantity.Name = "productQuantity" + i;
            quantity.ForeColor = Color.White;
            quantity.Location = new System.Drawing.Point(177, 30);
            quantity.Size = new System.Drawing.Size(19, 22);
            quantity.Font = new System.Drawing.Font("Monotype Corsiva", 14, FontStyle.Bold);
            quantity.Text = "" + cart.Quantity;
            quantity.Visible = true;
            item.Controls.Add(quantity);

            flowOrder.Controls.Add(item);
        }

        public void FinishOrder(Boolean error, string errorMsg)
        {
            if (error == false)
            {
                if (MessageBox.Show("Order was done, Thank you!", "Order", MessageBoxButtons.OK, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    this.Dispose();
                }
            }
            else
            {
                errorOrder.Text = errorMsg;
            }
        }

        private void finishOrderBtn_Click(object sender, EventArgs e)
        {
            List<string> fields = new List<string>();
            fields.Add(fnameTxt.Text);
            fields.Add(lnameTxt.Text);
            fields.Add(addressTxt.Text);
            fields.Add(cityTxt.Text);
            fields.Add(postCodeTxt.Text);
            fields.Add(stateBox.SelectedItem.ToString());
            fields.Add(phoneTxt.Text);
            fields.Add(cardNumberTxt.Text);
            fields.Add(holderNameTxt.Text);
            string exp_date = monthBox.SelectedItem.ToString() + yearBox.SelectedItem.ToString();
            fields.Add(exp_date);
            fields.Add(securityTxt.Text);
            fields.Add(label21.Text);
            _orderController.FinishOrder(fields);
        }
    }
}
