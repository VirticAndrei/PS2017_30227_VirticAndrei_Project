using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Data.Model;
using System.Data;


namespace Server.Gateway
{
    public class AddressGateway
    {
        static String connectionString = "Server=localhost;Database=food_delivery;Uid=root;Pwd=v2c47";
        static MySqlConnection conn = new MySqlConnection(connectionString);

        public void create(Address address)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO address(idaddress,line1,line2,line3,city,postcode,state)VALUES(@id,@line1,@line2,@line3,@city,@postcode,@state)";
                command.Parameters.AddWithValue("@id", address.Id);
                command.Parameters.AddWithValue("@line1", address.Line1);
                command.Parameters.AddWithValue("@line2", address.Line2);
                command.Parameters.AddWithValue("@line3", address.Line3);
                command.Parameters.AddWithValue("@city", address.City);
                command.Parameters.AddWithValue("@postcode", address.Postcode);
                command.Parameters.AddWithValue("@state", address.State);
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

        public void delete(Address address)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "DELETE FROM address WHERE idaddress = @id";
                command.Parameters.AddWithValue("@id", address.Id);
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

        public void update(Address address)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE address SET line1=@line1, line2=@line2, line3=@line3, city=@city, postcode=@postcode, state=@state WHERE idaddress = @id";
                command.Parameters.AddWithValue("@id", address.Id);
                command.Parameters.AddWithValue("@line1", address.Line1);
                command.Parameters.AddWithValue("@line2", address.Line2);
                command.Parameters.AddWithValue("@line3", address.Line3);
                command.Parameters.AddWithValue("@city", address.City);
                command.Parameters.AddWithValue("@postcode", address.Postcode);
                command.Parameters.AddWithValue("@state", address.State);
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

        public List<Address> findAll()
        {
            List<Address> addresses = new List<Address>();
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM address";
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Address address = new Address();
                    address.Id = Int32.Parse(reader[0].ToString());
                    address.Line1 = reader[1].ToString();
                    address.Line2 = reader[2].ToString();
                    address.Line3 = reader[3].ToString();
                    address.City = reader[4].ToString();
                    address.Postcode = reader[5].ToString();
                    address.State = reader[6].ToString();
                    addresses.Add(address);
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
            return addresses;

        }

        public Address findBy(String id)
        {
            Address address = new Address();
            address.City = null;
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM address WHERE idaddress=@id";
                command.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    address.Id = Int32.Parse(reader[0].ToString());
                    address.Line1 = reader[1].ToString();
                    address.Line2 = reader[2].ToString();
                    address.Line3 = reader[3].ToString();
                    address.City = reader[4].ToString();
                    address.Postcode = reader[5].ToString();
                    address.State = reader[6].ToString();
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
            if (address.City == null)
                return null;
            return address;
        }
    }
}
