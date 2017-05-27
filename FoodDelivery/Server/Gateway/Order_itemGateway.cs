using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Data.Model;
using System.Data;

namespace Server.Gateway
{
    public class Order_itemGateway
    {
        static String connectionString = "Server=localhost;Database=food_delivery;Uid=root;Pwd=v2c47";
        static MySqlConnection conn = new MySqlConnection(connectionString);

        public void create(Order_item order_item)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO order_product(product_id,order_id,quantity,price)VALUES(@product_id,@order_id,@quantity,@price)";
                command.Parameters.AddWithValue("@product_id", order_item.Product_id);
                command.Parameters.AddWithValue("@order_id", order_item.Order_id);
                command.Parameters.AddWithValue("@quantity", order_item.Quantity);
                command.Parameters.AddWithValue("@price", order_item.Price);
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

        public void delete(Order_item order_item)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "DELETE FROM order_product WHERE idorder_product = @id";
                command.Parameters.AddWithValue("@id", order_item.Id);
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

        public void update(Order_item order_item)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE order_product SET product_id=@product_id, order_id=@order_id, quantity=@quantity,  price=@price WHERE idorder_product = @id";
                command.Parameters.AddWithValue("@id", order_item.Id);
                command.Parameters.AddWithValue("@product_id", order_item.Product_id);
                command.Parameters.AddWithValue("@order_id", order_item.Order_id);
                command.Parameters.AddWithValue("@quantity", order_item.Quantity);
                command.Parameters.AddWithValue("@price", order_item.Price);
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

        public List<Order_item> findBy(String id)
        {
            List<Order_item> order_items = new List<Order_item>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM order_product WHERE order_id=@id";
                command.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Order_item order_item = new Order_item();
                    order_item.Id = Int32.Parse(reader[0].ToString());
                    order_item.Product_id = Int32.Parse(reader[1].ToString());
                    order_item.Order_id = Int32.Parse(reader[2].ToString());
                    order_item.Quantity = Int32.Parse(reader[3].ToString());
                    order_item.Price = Double.Parse(reader[4].ToString());
                    order_items.Add(order_item);
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
            if (order_items.Count <= 0)
                return null;
            return order_items;
        }
    }
}
