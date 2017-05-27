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
    public partial class AccountView : Form, IAccountView
    {
        private AccountController _accountController;
        private IStartView _startView;

        public AccountView(IStartView startView)
        {
            _startView = startView;
            InitializeComponent();
            stateBox.SelectedIndex = 0;
            monthBox.SelectedIndex = 0;
            yearBox.SelectedIndex = 0;
        }

        public IStartView getStartView()
        {
            return _startView;
        }

        public void SetController(AccountController controller)
        {
            _accountController = controller;
        }

        public void CloseView()
        {
            this.Dispose();
        }

        public void SetAccountDetails(Client client)
        {
            fnameTxt.Text = client.FirstName;
            lnameTxt.Text = client.LastName;
            emailTxt.Text = client.Email;
            phoneTxt.Text = client.Phone;
        }

        public void SetAddressDetails(Address address)
        {
            line1Txt.Text = address.Line1;
            line2Txt.Text = address.Line2;
            line3Txt.Text = address.Line3;
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
            securityTxt.Text = ""+payment.Security_code;
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

        public void SaveAccountDetails(string errorMsg)
        {
            errorAccount.Text = errorMsg;
            newPassTxt.Text = "";
            confirmPassTxt.Text = "";
        }

        public void SaveAddressDetails(string errorMsg)
        {
            errorAddress.Text = errorMsg;
        }
        public void SavePaymentsDetails(string errorMsg)
        {
            errorPayment.Text = errorMsg;
        }

        public void SetOrder(IList<Cart> cart, IList<Product> products)
        {
            flowOrder.Controls.Clear();
            double total = 0;
            if (cart != null)
            {
                if (cart.Count > 0)
                {
                    placeOrderBtn.Visible = true;
                    label21.Visible = true;
                    label22.Visible = true;
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
                    label21.Visible = false;
                    label22.Visible = false;
                    label20.Visible = false;
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
            price.Location = new System.Drawing.Point(68, 30);
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
                _accountController.RemoveItem(cart, product);
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
                _accountController.Minus(i, cart, product);
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
                _accountController.Plus(i, cart, product);
            };
            item.Controls.Add(plus);
            flowOrder.Controls.Add(item);
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
            _startView.Open();
        }

        private void AccountView_FormClosing(object sender, FormClosingEventArgs e)
        {
            _startView.Open();
            this.Dispose();
        }

        private void paymentDetailBtn_Click(object sender, EventArgs e)
        {
            errorPayment.Text = "";
            string exp_date = monthBox.SelectedItem.ToString() + yearBox.SelectedItem.ToString();
            _accountController.SavePaymentDetails(cardNumberTxt.Text, holderNameTxt.Text, exp_date, securityTxt.Text);
        }

        private void addressDetailBtn_Click(object sender, EventArgs e)
        {
            errorAddress.Text = "";
            _accountController.SaveAdressDetails(line1Txt.Text, line2Txt.Text, line3Txt.Text, cityTxt.Text, postCodeTxt.Text, stateBox.SelectedItem.ToString());
        }

        private void accountDetailBtn_Click(object sender, EventArgs e)
        {
            errorAccount.Text = "";
            _accountController.SaveAccountDetails(fnameTxt.Text, lnameTxt.Text, emailTxt.Text, phoneTxt.Text);
        }

        private void placeOrderBtn_Click(object sender, EventArgs e)
        {
            _accountController.PlaceOrder();
        }

        private void orderHistBtn_Click(object sender, EventArgs e)
        {
            _accountController.OrderHistory();
        }

        private void changePassBtn_Click(object sender, EventArgs e)
        {
            _accountController.ChangePassword(newPassTxt.Text, confirmPassTxt.Text);
        }

        private void menuBtn_Click(object sender, EventArgs e)
        {
            _accountController.OpenMenu();
        }

    }
}
