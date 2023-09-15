using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Brag.SharedServices.Helpers
{
    public class GenerateCodeNumbers
    {
        public static string GetRandomNumber(int length)
        {
            var numbers = Guid.NewGuid().ToString();
            numbers = Regex.Replace(numbers, "[^0-9]", "");
            numbers = numbers[..length];
            return numbers;
        }
        public static string GetPaymentReference(int length, string Prefix)
        {
            var numbers = Guid.NewGuid().ToString();
            numbers = Regex.Replace(numbers, "[^0-9]", "");
            numbers = numbers[..length];
            var yearvalue=DateTime.Now.Year;
            return Prefix + yearvalue + numbers;
        }
        public static string GetReferenceNubers(int length, string Prefix)
        {
            var numbers = "0123456789".ToCharArray();
            using var rngCryptoServiceProvider = new System.Security.Cryptography.RSACryptoServiceProvider();
            var randomBytes = new byte[4 * length];
            rngCryptoServiceProvider.EncryptValue(randomBytes);
            var token = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int index = BitConverter.ToUInt16(randomBytes, i * 4) % numbers.Length;
                token.Append(numbers[index]);
            }
            return Prefix + token.ToString();
        }
        public static string GetNubers(int length)
        {
            var numbers = "0123456789".ToCharArray();
            using var rngCryptoServiceProvider = new System.Security.Cryptography.RSACryptoServiceProvider();
            var randomBytes = new byte[4 * length];
            rngCryptoServiceProvider.EncryptValue(randomBytes);
            var token = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int index = BitConverter.ToUInt16(randomBytes, i * 4) % numbers.Length;
                token.Append(numbers[index]);
            }
            return token.ToString();
        }

        public static string GenerateKey(int keylength)
        {
            DateTime dt = DateTime.Now;
            var sDt = dt.ToString();
            sDt = sDt.Replace("/", "#");
            sDt = sDt.Replace(":", "@");
            sDt = sDt.Replace(" ", "");
            var numbers = Guid.NewGuid().ToString();
            numbers = numbers[..keylength];
            var res = "OID-" + sDt.Trim() + numbers;
            return res;
        }
    }
}
