using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodDelivery.View;
using Data.Model;
using System.Windows.Forms;

namespace FoodDelivery.Controller
{
    public class MenuController
    {
        private IMenuView _menuView;
        private ClientOnServer _client;
        private Boolean loged;
        private List<Product> allProducts;
        private List<Product> productsList;
        private List<Cart> cartList;
        private int id;

        public MenuController(IMenuView menuView, string category, Boolean loged, int id, ClientOnServer client)
        {
            menuView.SetController(this);
            _menuView = menuView;
            _client = client;
            this.loged = loged;
            this.id = id;
            SetMenu(category);
            SetOrder();
        }

        public void SetMenu(string category)
        {
            List<Product> products = new List<Product>();
            if (category == "Menu")
            {
                _client.SendProductData("findAll", "0", "", "0", "", "", "0");
                products = _client.getProducts();
                _menuView.SetMenu(products, category);
            }
            else
            {
                _client.SendProductData("findBy", "0","", "0", "", category, "0");
                products = _client.getProducts();
                _menuView.SetMenu(products, category);
            }   
        }

        public void SetOrder()
        {
            if (loged == true)
            {
                allProducts = new List<Product>();
                _client.SendProductData("findAll", "0", "", "0", "", "", "0");
                allProducts = _client.getProducts();
                cartList = new List<Cart>();
                _client.SendCartData("find", "0", "" + id, "0", "0", "0");
                cartList = _client.getCarts();
                
                if (cartList != null)
                {
                    productsList = new List<Product>();
                    foreach (Cart cart in cartList)
                    {
                        foreach (Product prod in allProducts)
                        {
                            if (cart.Product_id == prod.Id)
                            {
                                productsList.Add(prod);
                            }
                        }
                    }
                }
                _menuView.SetOrder(true,cartList, productsList);
            }
            else
            {
                _menuView.SetOrder(false, cartList, productsList);
            }
        }

        public void AddToOrder(Product product)
        {
            if (loged == true)
            {
                Cart existCart = new Cart();
                existCart.Id = -1;
                int index = 0, i=0;
                if (cartList != null)
                {
                    foreach (Cart c in cartList)
                    {
                        if (c.Product_id == product.Id)
                        {
                            existCart = c;
                            index = i;
                        }
                        i++;
                    }
                }
                if (existCart.Id == -1)
                {
                    _client.SendCartData("create", "0", "" + id, "" + product.Id, "1", "" + product.Price);
                    Cart cart = new Cart { Id = 0, Client_id = id, Product_id = product.Id, Quantity = 1, Total = product.Price };
                    if (cartList != null)
                        cartList.Add(cart);
                    else
                    {
                        cartList = new List<Cart>();
                        cartList.Add(cart);
                    }
                    if (productsList != null)
                        productsList.Add(product);
                    else
                    {
                        productsList = new List<Product>();
                        productsList.Add(product);
                    }
                }
                else
                {
                    existCart.Quantity = existCart.Quantity + 1;
                    existCart.Total = existCart.Total + product.Price;
                    _client.SendCartData("update", "0", "" + id, "" + product.Id, ""+existCart.Quantity, "" + existCart.Total);
                    cartList.ElementAt(index).Quantity = existCart.Quantity;
                    cartList.ElementAt(index).Total = existCart.Total;
                }
                _menuView.SetOrder(true,cartList, productsList);
                //MessageBox.Show(product.Name + " added to order!", "Order", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Log in or Sign in first!", "Order", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        public void RemoveItem(Cart cart,Product product)
        {
            _client.SendCartData("delete", "" + cart.Id, "" + cart.Client_id, "" + cart.Product_id, "" + cart.Quantity, "" + cart.Total);
            productsList.Remove(product);
            cartList.Remove(cart);
            _menuView.SetOrder(true, cartList, productsList);

        }

        public void Minus(int i,Cart cart,Product product)
        {
            if (cart.Quantity > 1)
            {
                cart.Quantity = cart.Quantity - 1;
                cart.Total = cart.Total - product.Price;
                _client.SendCartData("update", "0", "" + cart.Client_id, "" + cart.Product_id, "" + cart.Quantity, "" + cart.Total);
                cartList.ElementAt(i).Quantity = cart.Quantity;
                cartList.ElementAt(i).Total = cart.Total;
                _menuView.SetOrder(true, cartList, productsList);
            }
        }

        public void Plus(int i, Cart cart, Product product)
        {
            if (cart.Quantity < 9)
            {
                cart.Quantity = cart.Quantity + 1;
                cart.Total = cart.Total + product.Price;
                _client.SendCartData("update", "0", "" + cart.Client_id, "" + cart.Product_id, "" + cart.Quantity, "" + cart.Total);
                cartList.ElementAt(i).Quantity = cart.Quantity;
                cartList.ElementAt(i).Total = cart.Total;
                _menuView.SetOrder(true, cartList, productsList);
            }
        }

        public void PlaceOrder()
        {
            OrderView order = new OrderView();
            OrderController orderControl = new OrderController(order, _client, id, productsList, cartList);
            order.ShowDialog();
        }

    }
}
