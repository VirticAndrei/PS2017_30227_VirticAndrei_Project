using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Data.Model;
using System.Data;

namespace Server.Gateway
{
    public class PaymentGateway
    {
        static String connectionString = "Server=localhost;Database=food_delivery;Uid=root;Pwd=v2c47";
        static MySqlConnection conn = new MySqlConnection(connectionString);

        public void create(Payment payment)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "INSERT INTO payment(idpayment, card_number, holder_name, exp_date, security_code)VALUES(@id,@card,@holder,@exp_date,@security)";
                command.Parameters.AddWithValue("@id", payment.Id);
                command.Parameters.AddWithValue("@card", payment.Card_number);
                command.Parameters.AddWithValue("@holder", payment.Holder_name);
                command.Parameters.AddWithValue("@exp_date", payment.Exp_date);
                command.Parameters.AddWithValue("@security", payment.Security_code);
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

        public void delete(Payment payment)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "DELETE FROM payment WHERE idpayment = @id";
                command.Parameters.AddWithValue("@id", payment.Id);
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

        public void update(Payment payment)
        {
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE payment SET card_number=@card, holder_name=@holder, exp_date=@exp_date, security_code=@security WHERE idpayment = @id";
                command.Parameters.AddWithValue("@id", payment.Id);
                command.Parameters.AddWithValue("@card", payment.Card_number);
                command.Parameters.AddWithValue("@holder", payment.Holder_name);
                command.Parameters.AddWithValue("@exp_date", payment.Exp_date);
                command.Parameters.AddWithValue("@security", payment.Security_code);
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

        public List<Payment> findAll()
        {
            List<Payment> payments = new List<Payment>();
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM payment";
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Payment payment = new Payment();
                    payment.Id = Int32.Parse(reader[0].ToString());
                    payment.Card_number = reader[1].ToString();
                    payment.Holder_name= reader[2].ToString();
                    payment.Exp_date = reader[3].ToString();
                    payment.Security_code = Int32.Parse(reader[4].ToString());
                    payments.Add(payment);
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
            return payments;

        }

        public Payment findBy(String id)
        {
            Payment payment = new Payment();
            payment.Card_number = null;
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM payment WHERE idpayment=@id";
                command.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    payment.Id = Int32.Parse(reader[0].ToString());
                    payment.Card_number = reader[1].ToString();
                    payment.Holder_name = reader[2].ToString();
                    payment.Exp_date = reader[3].ToString();
                    payment.Security_code = Int32.Parse(reader[4].ToString());
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
            if (payment.Card_number == null)
                return null;
            return payment;
        }
    }
}
