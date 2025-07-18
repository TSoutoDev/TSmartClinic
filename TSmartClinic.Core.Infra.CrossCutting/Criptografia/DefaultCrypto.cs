using System.Security.Cryptography;
using System.Text;

namespace TSmartClinic.Core.Infra.CrossCutting.Criptografia
{
    public class DefaultCrypto
    {
        public string Encrypt(string plainText, string key)
        {
            if (string.IsNullOrEmpty(plainText) || string.IsNullOrEmpty(key))
                return string.Empty;

            var keyBytes = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32));
            var iv = new byte[16]; // IV zerado (pode ser melhorado)

            using var aes = Aes.Create();
            aes.Key = keyBytes;
            aes.IV = iv;

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using var sw = new StreamWriter(cs);

            sw.Write(plainText);

            return Convert.ToBase64String(ms.ToArray());
        }

        public string Decrypt(string cipherText, string key)
        {
            if (string.IsNullOrEmpty(cipherText) || string.IsNullOrEmpty(key))
                return string.Empty;

            var keyBytes = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32));
            var iv = new byte[16]; // mesmo IV fixo

            using var aes = Aes.Create();
            aes.Key = keyBytes;
            aes.IV = iv;

            var cipherBytes = Convert.FromBase64String(cipherText);

            using var ms = new MemoryStream(cipherBytes);
            using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}
