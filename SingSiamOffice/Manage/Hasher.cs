using System.Security.Cryptography;
using System.Text;

namespace SingSiamOffice.Manage
{
    public class Hasher
    {
        public string generateSalt(int maxSize = 10)
        {
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        public string hashPassword(string inputPassword, string salt)
        {
            byte[] buffer = Encoding.UTF8.GetBytes($"{inputPassword}{salt}");
            byte[] key = Encoding.UTF8.GetBytes(salt);
            HMACSHA256 hmac = new HMACSHA256(key);
            string hash = Convert.ToBase64String(hmac.ComputeHash(buffer)).Replace("-", "");
            return hash;
        }
    }
}
