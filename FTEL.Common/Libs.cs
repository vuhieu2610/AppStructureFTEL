using FTEL.Common.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace FTEL.Common
{
    public class Libs
    {
        public static void WriteLog(string type, string msg)
        {
            try
            {

                LogUtil.Info(type + ": " + msg);

                //var pathDate = DateTime.Now;
                //var fileName = "/" + pathDate.ToString("yyyyMMdd") + ".txt";

                //var fs = new FileStream(HttpRuntime.AppDomainAppPath + ConfigUtil.LogDirectory + fileName, FileMode.OpenOrCreate, FileAccess.Write);
                //var sw = new StreamWriter(fs);
                //sw.BaseStream.Seek(0, SeekOrigin.End);
                //sw.WriteLine("-------------------------");
                //sw.WriteLine(DateTime.Now + " " + type + " - " + msg);
                //sw.Flush();
                //sw.Close();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static string CleanUrl(string value)
        {
            var result = "";
            if (string.IsNullOrEmpty(value)) return result;
            // replace hyphens to spaces, remove all leading and trailing whitespace
            value = value.Replace("-", " ").Trim().ToLower();
            // replace multiple whitespace to one hyphen
            value = Regex.Replace(value, @"[\s]+", "-");
            // replace umlauts and eszett with their equivalent
            value = value.Replace("ß", "ss");
            value = value.Replace("ä", "ae");
            value = value.Replace("ö", "oe");
            value = value.Replace("ü", "ue");
            // removes diacritic marks (often called accent marks) from characters
            value = RemoveDiacritics(value);
            // remove all left unwanted chars (white list)
            value = Regex.Replace(value, @"[^a-z0-9\s-]", String.Empty);
            result = value;
            return result;
        }
        public static string RemoveDiacritics(string value)
        {
            var normalized = value.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var c in normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
            {
                sb.Append(c);
            }
            var nonunicode = Encoding.GetEncoding(850);
            var unicode = Encoding.Unicode;
            var nonunicodeBytes = Encoding.Convert(unicode, nonunicode, unicode.GetBytes(sb.ToString()));
            var nonunicodeChars = new char[nonunicode.GetCharCount(nonunicodeBytes, 0, nonunicodeBytes.Length)];
            nonunicode.GetChars(nonunicodeBytes, 0, nonunicodeBytes.Length, nonunicodeChars, 0);
            return new string(nonunicodeChars);
        }

        public static dynamic DeserializeObject(string jsonData)
        {
            var jData = JsonConvert.DeserializeObject(jsonData);
            return jData;
        }
        public static T Deserialize<T>(string jsonData)
        {
            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
                //,
                //DateFormatHandling = DateFormatHandling.IsoDateFormat
            };
            var jData = JsonConvert.DeserializeObject<T>(jsonData, microsoftDateFormatSettings);
            //var jData = new JavaScriptSerializer().Deserialize<T>(jsonData);
            return jData;
        }
        public static T DeserializeObject<T>(string jsonData)
        {
            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
                //,
                //DateFormatHandling = DateFormatHandling.IsoDateFormat
            };
            var jData = JsonConvert.DeserializeObject<T>(jsonData, microsoftDateFormatSettings);
            return jData;
        }

        /// <summary>
        /// Hàm convert object sang JSON string
        /// </summary>
        /// <author>TuanVN4</author>
        /// <unit>FTEL - ISC - HN</unit>
        /// <param name="objData">Đối tượng cần chuyển đổi</param>
        /// <returns>string => chuỗi JSON convert từ object truyền vào</returns>
        public static dynamic DeserializeToDynamicObject(string jsonData)
        {
            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
                //,
                //DateFormatHandling = DateFormatHandling.IsoDateFormat
            };
            var jData = JObject.Parse(jsonData);
            return jData;
        }

        /// <summary>
        /// Hàm convert object sang JSON string
        /// </summary>
        /// <author>TuanVN4</author>
        /// <unit>FTEL - ISC - HN</unit>
        /// <param name="objData">Đối tượng cần chuyển đổi</param>
        /// <returns>string => chuỗi JSON convert từ object truyền vào</returns>
        public static string SerializeObject(object objData)
        {
            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                //DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            };
            return JsonConvert.SerializeObject(objData, microsoftDateFormatSettings);
        }

        /// <summary>
        /// Hàm convert object sang JSON string - Ngày giờ tự config
        /// </summary>
        /// <author>TuanVN4</author>
        /// <unit>FTEL - ISC - HN</unit>
        /// <param name="objData">Đối tượng cần chuyển đổi</param>
        /// <returns>string => chuỗi JSON convert từ object truyền vào</returns>
        public static string SerializeObject(object objData, string dateTimeFormat)
        {
            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                //DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateFormatString = (string.IsNullOrEmpty(dateTimeFormat)) ? "yyyy-MM-dd HH:mm:ss" : dateTimeFormat
            };
            return JsonConvert.SerializeObject(objData, microsoftDateFormatSettings);
        }

        public static string KillSqlInjection(string chuoiKiemTra)
        {
            var chuoiDauVao = chuoiKiemTra;
            if (!string.IsNullOrEmpty(chuoiDauVao))
            {
                string[] chuoiDauRa = { "--", ";--", ";", "/*", "*/", "@@", "char", "nchar", "varchar", "nvarchar", "alter", "begin",
                                    "cast", "create", "cursor", "declare", "delete", "drop", "end", "exec", "execute", "fetch", "insert",
                                    "kill", "select", "sys", "sysobjects", "syscolumns", "table", "update" };
                for (var i = 0; i <= chuoiDauRa.Length - 1; i++)
                {
                    chuoiDauVao = chuoiDauVao.ToLower().Replace(chuoiDauRa[i], "");
                }
            }
            return chuoiDauVao;
        }

        public static string GetIpAddress()
        {
            var context = HttpContext.Current;
            var ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ipAddress))
            {
                var addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public static string GetMd5(string text)
        {
            // step 1, calculate MD5 hash from input
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(text);
            var hash = md5.ComputeHash(inputBytes);
            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString().ToLower();

        }

        /// <summary>
        /// Random string ngẫu nhiên
        /// </summary>
        /// <param name="length">Độ dài cần random</param>
        /// <returns>String random</returns>
        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNPQRSTUVWXYZ123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Random string ngẫu nhiên
        /// </summary>
        /// <param name="length">Độ dài cần random</param>
        /// <returns>String random</returns>
        public static string RandomStringNumber(int length)
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int RandomNumber(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }
    }
}
