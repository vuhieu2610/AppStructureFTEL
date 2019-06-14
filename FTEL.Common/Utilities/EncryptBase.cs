using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FTEL.Common.Utilities
{
    public sealed class EncryptBase
    {
        public static readonly string KeyEncrypt = "!@$v@22cdf|";
        public static readonly string PassKey = "tguyen@123!";
        public static readonly string TimeToken = DateTime.Now.ToString("ddMMyyyy");
        public static string GenerateTokenCode(string userId)
        {
            var tokenCode = Md5Get(userId + PassKey + TimeToken);
            return tokenCode;
        }
        public static string GenerateUniqueToken()
        {
            var time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            var key = Guid.NewGuid().ToByteArray();
            var token = Convert.ToBase64String(time.Concat(key).ToArray());
            return token;
        }
        public static string DecodeUniqueToken(string token)
        {
            var data = Convert.FromBase64String(token);
            var when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            if (when < DateTime.UtcNow.AddHours(-24))
            {
                // too old
            }
            return when.ToString();
        }
        //mã hóa và giải mã dữ liệu dùng thuật toán TripleDES mã hóa kiểu mã hóa MD5
        public static string Encrypt(string key, string toEncrypt)
        {
            try
            {
                var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
                var hashmd5 = new MD5CryptoServiceProvider();
                var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                var tdes =
                new TripleDESCryptoServiceProvider { Key = keyArray, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
                var cTransform = tdes.CreateEncryptor();
                var resultArray = cTransform.TransformFinalBlock(
                    toEncryptArray, 0, toEncryptArray.Length);
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch
            {
                return string.Empty;
            }
        }
        public static string Decrypt(string key, string toDecrypt)
        {
            try
            {
                var toEncryptArray = Convert.FromBase64String(toDecrypt);
                var hashmd5 = new MD5CryptoServiceProvider();
                var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                var tdes = new TripleDESCryptoServiceProvider
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                var cTransform = tdes.CreateDecryptor();
                var resultArray = cTransform.TransformFinalBlock(
                toEncryptArray, 0, toEncryptArray.Length);
                return Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return string.Empty;
            }
        }
        public static string Md5Get(string text)
        {
            // step 1, calculate MD5 hash from input
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(text);
            var hash = md5.ComputeHash(inputBytes);
            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            foreach (var t in hash)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString().ToLower();
        }
        public static string EncryptId(string key, string toEncrypt)
        {
            string returnstring;
            try
            {
                var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
                var hashmd5 = new MD5CryptoServiceProvider();
                var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                var tdes =
                new TripleDESCryptoServiceProvider { Key = keyArray, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
                var cTransform = tdes.CreateEncryptor();
                var resultArray = cTransform.TransformFinalBlock(
                    toEncryptArray, 0, toEncryptArray.Length);
                returnstring = Convert.ToBase64String(resultArray, 0, resultArray.Length);
                //URL Encryption Avoid Reserved Characters
                returnstring = returnstring.Replace("/", "-2F-");
                returnstring = returnstring.Replace("!", "-21-");
                returnstring = returnstring.Replace("#", "-23-");
                returnstring = returnstring.Replace("$", "-24-");
                returnstring = returnstring.Replace("&", "-26-");
                returnstring = returnstring.Replace("'", "-27-");
                returnstring = returnstring.Replace("(", "-28-");
                returnstring = returnstring.Replace(")", "-29-");
                returnstring = returnstring.Replace("*", "-2A-");
                returnstring = returnstring.Replace("+", "-2B-");
                returnstring = returnstring.Replace(",", "-2C-");
                returnstring = returnstring.Replace(":", "-3A-");
                returnstring = returnstring.Replace(";", "-3B-");
                returnstring = returnstring.Replace("=", "-3D-");
                returnstring = returnstring.Replace("?", "-3F-");
                returnstring = returnstring.Replace("@", "-40-");
                returnstring = returnstring.Replace("[", "-5B-");
                returnstring = returnstring.Replace("]", "-5D-");
            }
            catch
            {
                returnstring = "";
            }
            return returnstring;
        }

        public static string DecryptId(string key, string stringToDecrypt)
        {
            try
            {
                //URL Decrytion Avoid Reserved Characters
                stringToDecrypt = stringToDecrypt.Replace("-2F-", "/");
                stringToDecrypt = stringToDecrypt.Replace("-21-", "!");
                stringToDecrypt = stringToDecrypt.Replace("-23-", "#");
                stringToDecrypt = stringToDecrypt.Replace("-24-", "$");
                stringToDecrypt = stringToDecrypt.Replace("-26-", "&");
                stringToDecrypt = stringToDecrypt.Replace("-27-", "'");
                stringToDecrypt = stringToDecrypt.Replace("-28-", "(")
                ;
                stringToDecrypt = stringToDecrypt.Replace("-29-", ")");
                stringToDecrypt = stringToDecrypt.Replace("-2A-", "*");
                stringToDecrypt = stringToDecrypt.Replace("-2B-", "+");
                stringToDecrypt = stringToDecrypt.Replace("-2C-", ",");
                stringToDecrypt = stringToDecrypt.Replace("-3A-", ":");
                stringToDecrypt = stringToDecrypt.Replace("-3B-", ";");
                stringToDecrypt = stringToDecrypt.Replace("-3D-", "=");
                stringToDecrypt = stringToDecrypt.Replace("-3F-", "?");
                stringToDecrypt = stringToDecrypt.Replace("-40-", "@");
                stringToDecrypt = stringToDecrypt.Replace("-5B-", "[");
                stringToDecrypt = stringToDecrypt.Replace("-5D-", "]");

                var toEncryptArray = Convert.FromBase64String(stringToDecrypt);
                var hashmd5 = new MD5CryptoServiceProvider();
                var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                var tdes = new TripleDESCryptoServiceProvider
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                var cTransform = tdes.CreateDecryptor();
                var resultArray = cTransform.TransformFinalBlock(
                toEncryptArray, 0, toEncryptArray.Length);
                return Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
