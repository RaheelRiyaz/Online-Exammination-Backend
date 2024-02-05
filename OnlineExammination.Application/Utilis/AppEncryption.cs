using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExammination.Application.Utilis
{
    public class AppEncryption
    {
        public static string GenerateSalt()
        {
            var randomNumbers= RandomNumberGenerator.GetBytes(20);
            return Convert.ToBase64String(randomNumbers);
        }


        public static string GenerateHashPassword(string salt,string password)
        {
            var passwordWithSalt = string.Concat(salt,password);
            var sha = SHA256.Create();
            var hashedpassword = sha.ComputeHash(Encoding.UTF8.GetBytes(passwordWithSalt));
            if (string.IsNullOrEmpty(salt)) return passwordWithSalt;
            return Convert.ToBase64String(hashedpassword);
        }

        public static bool ComparePassword(string userPassword,string salt,string password)
        {
            var pas= GenerateHashPassword(salt, password);
            return userPassword == pas;
        }
    }
}
