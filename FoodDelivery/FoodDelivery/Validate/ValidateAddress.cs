using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Model;
using System.Text.RegularExpressions;

namespace FoodDelivery.Validate
{
    public class ValidateAddress
    {
        private Address _address;
        private Boolean error;
        private string errorMsg;

        public ValidateAddress(Address address)
        {
            _address = address;
            error = false;
            errorMsg = "";
        }

        public void validate()
        {
            error = false;
            validate_state();
            validate_city();
            validate_line1();
        }

        public void validate_line1()
        {
            Regex regex = new Regex("^[A-Za-z0-9,. -]+$");
            if (regex.IsMatch(_address.Line1) == false)
            {
                error = true;
                errorMsg = "Incorrect line 1!";
            }
        }

        public void validate_city()
        {
            Regex regex = new Regex("^[A-Za-z -]+$");
            if (regex.IsMatch(_address.City) == false || _address.City.Length < 4)
            {
                error = true;
                errorMsg = "Incorrect city!";
            }
        }

        public void validate_state()
        {
            Regex regex = new Regex("^[A-Za-z -]+$");
            if (regex.IsMatch(_address.State) == false || _address.State.Length < 4)
            {
                error = true;
                errorMsg = "Incorrect state!";
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
