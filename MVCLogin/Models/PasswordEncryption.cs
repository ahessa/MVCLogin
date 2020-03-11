using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Text;

namespace MVCLogin.Models
{
    public class PasswordEncryption
    {
        public static string TextToEncrypt(string hashPass)
        {
            return Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(hashPass)));
        }
    }
}