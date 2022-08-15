using System.Security.Cryptography;
using System.Text;

namespace FinancialSnapshot.Common.Cryptography
{
    public static class CryptographyProcessor
    {
        public static string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            var randomNumber = RandomNumberGenerator.Create();
            byte[] buff = new byte[size];
            randomNumber.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string GenerateHash(string password, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password + salt);
            var sHA256ManagedString = SHA256.Create();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public static bool AreEqual(string password, string hashedpassword, string salt)
        {
            string newHashedPin = GenerateHash(password, salt);
            return newHashedPin.Equals(hashedpassword);
        }
    }
}
