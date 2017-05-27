using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace FoodDelivery.Controller
{
    public class Encrypt
    {
        String password;
        public Encrypt(String password)
        {
            this.password = encrypt_pass(password);
        }

        private static String encrypt_pass(String value)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                return String.Concat(hash
                  .ComputeHash(Encoding.UTF8.GetBytes(value))
                  .Select(item => item.ToString("x2")));
            }
        }

        public String getEncriptedPass()
        {
            return password;
        }
    }
}
