using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Model;
using System.Text.RegularExpressions;

namespace FoodDelivery.Validate
{
    public class ValidateClient
    {
        private Client _client;
        private Boolean error;
        private string errorMsg;

        public ValidateClient(Client client)
        {
            _client = client;
            error = false;
            errorMsg = "";
        }

        public void validate()
        {
            error = false;
            validate_phone();
            validate_email();
            validate_lastName();
            validate_firstName();
            validate_password();
            validate_username();
        }

        public void validate_username()
        {
            Regex regex = new Regex("^[A-Za-z.]+$");
            if (regex.IsMatch(_client.UserName) == false)
            {
                error = true;
                errorMsg = "Incorrect username!";
            }
            string user = "" + _client.FirstName + "." + _client.LastName;
            if (_client.UserName.CompareTo(user) != 0)
            {
                error = true;
                errorMsg = "Incorrect username!";
            }
        }

        public void validate_password()
        {
            Regex regex = new Regex("^[A-Za-z0-9]+$");
            if (regex.IsMatch(_client.Password) == false || _client.Password.Length < 5)
            {
                error = true;
                errorMsg = "Incorrect password!";
            }
        }

        public void validate_firstName()
        {
            Regex regex = new Regex("^[A-Za-z]+$");
            if (regex.IsMatch(_client.FirstName) == false || _client.FirstName.Length < 3)
            {
                error = true;
                errorMsg = "Incorrect first name!";
            }
        }

        public void validate_lastName()
        {
            Regex regex = new Regex("^[A-Za-z]+$");
            if (regex.IsMatch(_client.LastName) == false || _client.LastName.Length < 3)
            {
                error = true;
                errorMsg = "Incorrect last name!";
            }
        }

        public void validate_email()
        {
            Regex regex = new Regex("^[A-Za-z0-9._@-]+$");
            if (regex.IsMatch(_client.Email) == false || _client.Email.Length < 10)
            {
                error = true;
                errorMsg = "Incorrect email!";
            }
        }

        public void validate_phone()
        {
            Regex regex = new Regex("^[0-9]+$");
            if (regex.IsMatch(_client.Phone) == false || _client.Phone.Length != 10)
            {
                error = true;
                errorMsg = "Incorrect phone!";
            }
        }

        public Boolean isValid()
        {
            if (error == false)
            {
                return true;
            }
            return false;
        }

        public String getErrorMsg()
        {
            return errorMsg;
        } 
    }
}
