using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Data.Model;
using FoodDelivery.Controller;

namespace FoodDelivery.View
{
    public partial class DeliveryView : Form,IDeliveryView
    {
        private DeliveryController _deliveryController;
        private IStartView _startView;
        public DeliveryView(IStartView _startView)
        {
            this._startView = _startView;
            InitializeComponent();
        }

        public void SetController(DeliveryController controller)
        {
            this._deliveryController = controller;
        }

        public void UpdateView()
        {
            dataGridView1.Invalidate();
        }

        public void SetOrdersInList(IList<Order> orders)
        {
            this.bindingSource1.DataSource = orders;
        }

        public void SetOrderList(IList<Order_item> order_items, IList<Product> products, Order order)
        {
            flowOrder.Controls.Clear();
            if (order_items != null)
            {
                for (int i = 0; i < order_items.Count; i++)
                {
                    AddProdToOrder(i, order_items[i], products[i]);

                }
            }
            label21.Text = "" + order.Total;
            nameLbl.Text = order.Name;
            addressLbl.Text = order.Address;
            phoneLbl.Text = order.Phone;
        }

        public void AddProdToOrder(int i, Order_item order_item, Product product)
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
            quantity.Text = "" + order_item.Quantity;
            quantity.Visible = true;
            item.Controls.Add(quantity);

            flowOrder.Controls.Add(item);
        }

        public Order GetSelectedOrderFromGrid()
        {
            errorOrder.Text = "";
            if (dataGridView1.SelectedRows.Count > 0)
                return (Order)this.dataGridView1.SelectedRows[0].DataBoundItem;
            else
                errorOrder.Text = "Select a row from table!";
            return null;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];
                Order order = (Order)selectedRow.DataBoundItem;
                _deliveryController.SetProductList(order);
            }
        }

        private void DeliveryView_FormClosing(object sender, FormClosingEventArgs e)
        {
            _startView.Logout();
            _startView.Open();
        }

        private void processingBtn_Click(object sender, EventArgs e)
        {
            _deliveryController.OrderStatus("Processing");
        }

        private void onWayBtn_Click(object sender, EventArgs e)
        {
            _deliveryController.OrderStatus("Go to way");
        }

        private void deliveredBtn_Click(object sender, EventArgs e)
        {
            _deliveryController.OrderStatus("Delivered");
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            _deliveryController.SetOrders();
        }
    }
}
