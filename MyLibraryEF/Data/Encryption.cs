using System;
using System.Security.Cryptography;
using System.Text;

namespace MyLibraryEF.Data
{
    internal class Encryption
    {
        internal string HashPassword(string password)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            byte[] encryptedBytes = sha1.ComputeHash(passwordBytes);
            var hashedPassword = Convert.ToBase64String(encryptedBytes);

            return hashedPassword;
        }
    }
}
