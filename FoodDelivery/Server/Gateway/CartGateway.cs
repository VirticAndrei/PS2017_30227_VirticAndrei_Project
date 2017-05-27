using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Data.Model;
using System.Data;

namespace Server.Gateway
{
    public class CartGateway
    {
        static String connectionString = "Server=localhost;Database=food_delivery;Uid=root;Pwd=v2c47";
        static MySqlConnection conn = new MySqlConnection(connectionString);

        public void create(Cart cart)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO cart(client_id,product_id,quantity,total)VALUES(@client_id,@product_id,@quantity,@total)";
                command.Parameters.AddWithValue("@client_id", cart.Client_id);
                command.Parameters.AddWithValue("@product_id", cart.Product_id);
                command.Parameters.AddWithValue("@quantity", cart.Quantity);
                command.Parameters.AddWithValue("@total", cart.Total);
                command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void delete(Cart cart)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "DELETE FROM cart WHERE client_id = @client_id AND product_id=@product_id";
                command.Parameters.AddWithValue("@client_id", cart.Client_id);
                command.Parameters.AddWithValue("@product_id", cart.Product_id);
                command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void update(Cart cart)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE cart SET quantity=@quantity, total=@total WHERE client_id = @client_id AND product_id=@product_id";
                command.Parameters.AddWithValue("@client_id", cart.Client_id);
                command.Parameters.AddWithValue("@product_id", cart.Product_id);
                command.Parameters.AddWithValue("@quantity", cart.Quantity);
                command.Parameters.AddWithValue("@total", cart.Total);
                command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public List<Cart> findBy(String idclient)
        {
            List<Cart> carts = new List<Cart>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM cart WHERE client_id=@client_id";
                command.Parameters.AddWithValue("@client_id", idclient);
                MySqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    Cart cart = new Cart();
                    cart.Id = Int32.Parse(reader[0].ToString());
                    cart.Client_id = Int32.Parse(reader[1].ToString());
                    cart.Product_id = Int32.Parse(reader[2].ToString());
                    cart.Quantity = Int32.Parse(reader[3].ToString());
                    cart.Total = Double.Parse(reader[4].ToString());
                    carts.Add(cart);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            if (carts.Count <= 0)
                return null;
            return carts;
        }

        
    }
}
