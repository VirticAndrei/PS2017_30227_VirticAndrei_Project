using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FoodDelivery.Validate
{
    public class ValidateOrderDetails
    {
        private Boolean error;
        private string errorMsg;
        private string firstName, lastName, address, city, phone, cardNumber, holderName, exp_date, security;

        public ValidateOrderDetails(List<string> fields)
        {
            firstName = fields[0];
            lastName = fields[1];
            address = fields[2];
            city = fields[3];
            phone = fields[6];
            cardNumber = fields[7];
            holderName = fields[8];
            exp_date = fields[9];
            security = fields[10];
            error = false;
            errorMsg = "";
        }

        public void validate()
        {
            error = false;
            validate_security();
            validate_exp_date();
            validate_holder();
            validate_card();
            validate_phone();
            validate_city();
            validate_address();
            validate_lastName();
            validate_firstName();
        }

        public void validate_firstName()
        {
            Regex regex = new Regex("^[A-Za-z]+$");
            if (regex.IsMatch(firstName) == false || firstName.Length < 3)
            {
                error = true;
                errorMsg = "Incorrect first name!";
            }
        }

        public void validate_lastName()
        {
            Regex regex = new Regex("^[A-Za-z]+$");
            if (regex.IsMatch(lastName) == false || lastName.Length < 3)
            {
                error = true;
                errorMsg = "Incorrect last name!";
            }
        }

        public void validate_address()
        {
            if (address.Length < 10)
            {
                error = true;
                errorMsg = "Incorrect address!";
            }
        }

        public void validate_city()
        {
            Regex regex = new Regex("^[A-Za-z -]+$");
            if (regex.IsMatch(city) == false || city.Length < 4)
            {
                error = true;
                errorMsg = "Incorrect city!";
            }
        }

        public void validate_phone()
        {
            Regex regex = new Regex("^[0-9]+$");
            if (regex.IsMatch(phone) == false || phone.Length != 10)
            {
                error = true;
                errorMsg = "Incorrect phone!";
            }
        }

        public void validate_card()
        {
            Regex regex = new Regex("^[0-9]+$");
            if (regex.IsMatch(cardNumber) == false || cardNumber.Length != 16)
            {
                error = true;
                errorMsg = "Incorrect card number!";
            }
        }

        public void validate_holder()
        {
            Regex regex = new Regex("^[A-Za-z ]+$");
            if (regex.IsMatch(holderName) == false || holderName.Length < 7)
            {
                error = true;
                errorMsg = "Incorrect holder name!";
            }
        }

        public void validate_exp_date()
        {
            DateTime now = DateTime.Now;
            int month = Int32.Parse(exp_date.Substring(0, 2));
            int year = Int32.Parse(exp_date.Substring(2, 2));
            Regex regex = new Regex("^[0-9]+$");
            if (regex.IsMatch(exp_date) == false || exp_date.Length != 4)
            {
                error = true;
                errorMsg = "Incorrect expire date!";
            }
            int nowYear = Int32.Parse(now.Year.ToString().Substring(2, 2));
            if (now.Month >= month && nowYear == year)
            {
                error = true;
                errorMsg = "Incorrect expire date!";
            }

        }

        public void validate_security()
        {
            Regex regex = new Regex("^[0-9]+$");
            if (regex.IsMatch(security) == false || security.Length != 3)
            {
                error = true;
                errorMsg = "Incorrect security code!";
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
