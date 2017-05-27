using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Data.Model;
using System.Data;

namespace Server.Gateway
{
    public class ClientGateway
    {
        static String connectionString = "Server=localhost;Database=food_delivery;Uid=root;Pwd=v2c47";
        static MySqlConnection conn = new MySqlConnection(connectionString);

        public void create(Client client)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO client(idclient,username,password,first_name,last_name,email,phone,address_id,payment_id)VALUES(@idclient,@username,@pass,@fname,@lname,@email,@phone,@address_id,@payment_id)";
                command.Parameters.AddWithValue("@idclient", client.Id);
                command.Parameters.AddWithValue("@username", client.UserName);
                command.Parameters.AddWithValue("@pass", client.Password);
                command.Parameters.AddWithValue("@fname", client.FirstName);
                command.Parameters.AddWithValue("@lname", client.LastName);
                command.Parameters.AddWithValue("@email", client.Email);
                command.Parameters.AddWithValue("@phone", client.Phone);
                command.Parameters.AddWithValue("@address_id", client.Address_id);
                command.Parameters.AddWithValue("@payment_id", client.Payment_id);
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

        public void delete(Client client)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "DELETE FROM client WHERE idclient = @id";
                command.Parameters.AddWithValue("@id", client.Id);
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

        public void update(Client client)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE client SET username=@username, password=@pass, first_name=@fname, last_name=@lname, email=@email, phone=@phone, address_id=@address_id, payment_id=@payment_id WHERE idclient = @id";
                command.Parameters.AddWithValue("@id", client.Id);
                command.Parameters.AddWithValue("@username", client.UserName);
                command.Parameters.AddWithValue("@pass", client.Password);
                command.Parameters.AddWithValue("@fname", client.FirstName);
                command.Parameters.AddWithValue("@lname", client.LastName);
                command.Parameters.AddWithValue("@email", client.Email);
                command.Parameters.AddWithValue("@phone", client.Phone);
                command.Parameters.AddWithValue("@address_id", client.Address_id);
                command.Parameters.AddWithValue("@payment_id", client.Payment_id);
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

        public List<Client> findAll()
        {
            List<Client> clients = new List<Client>();
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM client";
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Client client = new Client();
                    client.Id = Int32.Parse(reader[0].ToString());
                    client.UserName = reader[1].ToString();
                    client.Password = reader[2].ToString();
                    client.FirstName = reader[3].ToString();
                    client.LastName = reader[4].ToString();
                    client.Email = reader[5].ToString();
                    client.Phone = reader[6].ToString();
                    client.Address_id = Int32.Parse(reader[7].ToString());
                    client.Payment_id = Int32.Parse(reader[8].ToString());
                    clients.Add(client);
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
            return clients;

        }

        public Client findBy(String username)
        {
            Client client = new Client();
            client.UserName = null;
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM client WHERE username=@username";
                command.Parameters.AddWithValue("@username", username);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    client.Id = Int32.Parse(reader[0].ToString());
                    client.UserName = reader[1].ToString();
                    client.Password = reader[2].ToString();
                    client.FirstName = reader[3].ToString();
                    client.LastName = reader[4].ToString();
                    client.Email = reader[5].ToString();
                    client.Phone = reader[6].ToString();
                    client.Address_id = Int32.Parse(reader[7].ToString());
                    client.Payment_id = Int32.Parse(reader[8].ToString());
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
            if (client.UserName == null)
                return null;
            return client;
        }

        public Client findById(String id)
        {
            Client client = new Client();
            client.UserName = null;
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM client WHERE idclient=@id";
                command.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    client.Id = Int32.Parse(reader[0].ToString());
                    client.UserName = reader[1].ToString();
                    client.Password = reader[2].ToString();
                    client.FirstName = reader[3].ToString();
                    client.LastName = reader[4].ToString();
                    client.Email = reader[5].ToString();
                    client.Phone = reader[6].ToString();
                    client.Address_id = Int32.Parse(reader[7].ToString());
                    client.Payment_id = Int32.Parse(reader[8].ToString());
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
            if (client.UserName == null)
                return null;
            return client;
        }
    }
}
