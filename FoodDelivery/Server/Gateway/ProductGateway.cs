using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Data.Model;
using System.Data;

namespace Server.Gateway
{
    public class ProductGateway
    {
        static String connectionString = "Server=localhost;Database=food_delivery;Uid=root;Pwd=v2c47";
        static MySqlConnection conn = new MySqlConnection(connectionString);

        public void create(Product product)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO product(name,price,description,category,grams)VALUES(@name,@price,@description,@category,@grams)";
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@description", product.Description);
                command.Parameters.AddWithValue("@category", product.Category);
                command.Parameters.AddWithValue("@grams", product.Grams);
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

        public void delete(Product product)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "DELETE FROM product WHERE idproduct = @id";
                command.Parameters.AddWithValue("@id", product.Id);
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

        public void update(Product product)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE product SET name=@name, price=@price, description=@description, category=@category, grams=@grams WHERE idproduct = @id";
                command.Parameters.AddWithValue("@id", product.Id);
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@description", product.Description);
                command.Parameters.AddWithValue("@category", product.Category);
                command.Parameters.AddWithValue("@grams", product.Grams);
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

        public List<Product> findAll()
        {
            List<Product> products = new List<Product>();
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM product";
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product();
                    product.Id = Int32.Parse(reader[0].ToString());
                    product.Name = reader[1].ToString();
                    product.Price = Double.Parse(reader[2].ToString());
                    product.Description= reader[3].ToString();
                    product.Category = reader[4].ToString();
                    product.Grams = Double.Parse(reader[5].ToString());
                    products.Add(product);
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
            return products;

        }

        public Product findBy(String name)
        {
            Product product = new Product();
            product.Name = null;
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM product WHERE name=@name";
                command.Parameters.AddWithValue("@name", name);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    product.Id = Int32.Parse(reader[0].ToString());
                    product.Name = reader[1].ToString();
                    product.Price = Double.Parse(reader[2].ToString());
                    product.Description = reader[3].ToString();
                    product.Category = reader[4].ToString();
                    product.Grams = Double.Parse(reader[5].ToString());
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
            if (product.Name == null)
                return null;
            return product;
        }

        public List<Product> findByCategory(String category)
        {
            List<Product> products = new List<Product>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM product WHERE category=@category";
                command.Parameters.AddWithValue("@category", category);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product();
                    product.Id = Int32.Parse(reader[0].ToString());
                    product.Name = reader[1].ToString();
                    product.Price = Double.Parse(reader[2].ToString());
                    product.Description = reader[3].ToString();
                    product.Category = reader[4].ToString();
                    product.Grams = Double.Parse(reader[5].ToString());
                    products.Add(product);
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
            if (products.Count <= 0)
                return null;
            return products;
        }
    }
}
