using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Data.Model;
using System.Data;

namespace Server.Gateway
{
    public class OrderGateway
    {
        static String connectionString = "Server=localhost;Database=food_delivery;Uid=root;Pwd=v2c47";
        static MySqlConnection conn = new MySqlConnection(connectionString);

        public void create(Order order)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO orders(idorder,client_id,total,order_date,order_status,name,address,phone)VALUES(@id,@client_id,@total,@order_date,@order_status,@name,@address,@phone)";
                command.Parameters.AddWithValue("@id", order.Id);
                command.Parameters.AddWithValue("@client_id", order.Client_id);
                command.Parameters.AddWithValue("@total", order.Total);
                command.Parameters.AddWithValue("@order_date", order.Order_date);
                command.Parameters.AddWithValue("@order_status", order.Order_status);
                command.Parameters.AddWithValue("@name", order.Name);
                command.Parameters.AddWithValue("@address", order.Address);
                command.Parameters.AddWithValue("@phone", order.Phone);
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

        public void delete(Order order)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "DELETE FROM orders WHERE idorder = @id";
                command.Parameters.AddWithValue("@id", order.Id);
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

        public void update(Order order)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE orders SET client_id=@client_id, total=@total, order_date=@order_date, order_status=@order_status, name=@name, address=@address, phone=@phone WHERE idorder = @id";
                command.Parameters.AddWithValue("@id", order.Id);
                command.Parameters.AddWithValue("@client_id", order.Client_id);
                command.Parameters.AddWithValue("@total", order.Total);
                command.Parameters.AddWithValue("@order_date", order.Order_date);
                command.Parameters.AddWithValue("@order_status", order.Order_status);
                command.Parameters.AddWithValue("@name", order.Name);
                command.Parameters.AddWithValue("@address", order.Address);
                command.Parameters.AddWithValue("@phone", order.Phone);
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

        public List<Order> findAll()
        {
            List<Order> orders = new List<Order>();
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM orders";
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new Order();
                    order.Id = Int32.Parse(reader[0].ToString());
                    order.Client_id=  Int32.Parse(reader[1].ToString());
                    order.Total = Double.Parse(reader[2].ToString());
                    order.Order_date = reader[3].ToString();
                    order.Order_status = reader[4].ToString();
                    order.Name = reader[5].ToString();
                    order.Address = reader[6].ToString();
                    order.Phone = reader[7].ToString();
                    orders.Add(order);
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
            return orders;

        }

        public List<Order> findBy(String id)
        {
            List<Order> orders = new List<Order>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM orders WHERE client_id=@id";
                command.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new Order();
                    order.Id = Int32.Parse(reader[0].ToString());
                    order.Client_id = Int32.Parse(reader[1].ToString());
                    order.Total = Double.Parse(reader[2].ToString());
                    order.Order_date = reader[3].ToString();
                    order.Order_status = reader[4].ToString();
                    order.Name = reader[5].ToString();
                    order.Address = reader[6].ToString();
                    order.Phone = reader[7].ToString();
                    orders.Add(order);
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
            if (orders.Count <= 0)
                return null;
            return orders;
        }

        public List<Order> findByStatus(String status)
        {
            List<Order> orders = new List<Order>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM orders WHERE order_status!=@order_status";
                command.Parameters.AddWithValue("@order_status", status);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new Order();
                    order.Id = Int32.Parse(reader[0].ToString());
                    order.Client_id = Int32.Parse(reader[1].ToString());
                    order.Total = Double.Parse(reader[2].ToString());
                    order.Order_date = reader[3].ToString();
                    order.Order_status = reader[4].ToString();
                    order.Name = reader[5].ToString();
                    order.Address = reader[6].ToString();
                    order.Phone = reader[7].ToString();
                    orders.Add(order);
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
            if (orders.Count <= 0)
                return null;
            return orders;
        }
    }
}
