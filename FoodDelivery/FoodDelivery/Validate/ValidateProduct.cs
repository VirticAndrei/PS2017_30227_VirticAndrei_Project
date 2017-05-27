using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Data.Model;

namespace FoodDelivery.Validate
{
    public class ValidateProduct
    {
        private Product _product;
        private Boolean error;
        private string errorMsg;

        public ValidateProduct(Product product)
        {
            _product = product;
            error = false;
            errorMsg = "";
        }

        public void validate()
        {
            error = false;
            validate_grams();
            validate_description();
            validate_price();
            validate_name();
        }

        public void validate_name()
        {
            Regex regex = new Regex("^[A-Za-z. ]+$");
            if (regex.IsMatch(_product.Name) == false)
            {
                error = true;
                errorMsg = "Incorrect name!";
            }
        }

        public void validate_price()
        {
            string price = ""+_product.Price;
            Regex regex = new Regex("^[0-9.]+$");
            if (regex.IsMatch(price) == false)
            {
                error = true;
                errorMsg = "Incorrect price!";
            }
        }

        public void validate_description()
        {
            if (_product.Description.Length < 10)
            {
                error = true;
                errorMsg = "Incorrect description!";
            }
        }

        public void validate_grams()
        {
            string grams = ""+_product.Grams;
            Regex regex = new Regex("^[0-9.]+$");
            if (regex.IsMatch(grams) == false)
            {
                error = true;
                errorMsg = "Incorrect grams!";
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
