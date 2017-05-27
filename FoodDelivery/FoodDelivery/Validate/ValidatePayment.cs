using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Model;
using System.Text.RegularExpressions;

namespace FoodDelivery.Validate
{
    public class ValidatePayment
    {
        private Payment _payment;
        private Boolean error;
        private string errorMsg;

        public ValidatePayment(Payment payment)
        {
            _payment = payment;
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
        }

        public void validate_card()
        {
            Regex regex = new Regex("^[0-9]+$");
            if (regex.IsMatch(_payment.Card_number) == false || _payment.Card_number.Length != 16)
            {
                error = true;
                errorMsg = "Incorrect card number!";
            }
        }

        public void validate_holder()
        {
            Regex regex = new Regex("^[A-Za-z ]+$");
            if (regex.IsMatch(_payment.Holder_name) == false || _payment.Holder_name.Length < 7)
            {
                error = true;
                errorMsg = "Incorrect holder name!";
            }
        }

        public void validate_exp_date()
        {
            DateTime now = DateTime.Now;
            int month = Int32.Parse(_payment.Exp_date.Substring(0,2));
            int year = Int32.Parse(_payment.Exp_date.Substring(2,2));
            Regex regex = new Regex("^[0-9]+$");
            if (regex.IsMatch(_payment.Exp_date) == false || _payment.Exp_date.Length != 4)
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
            string code = "" + _payment.Security_code;
            Regex regex = new Regex("^[0-9]+$");
            if (regex.IsMatch(code) == false || code.Length != 3)
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
