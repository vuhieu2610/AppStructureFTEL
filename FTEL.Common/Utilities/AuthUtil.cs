using FTEL.Common.Constant;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FTEL.Common.Utilities
{
    public class AuthUtil
    {
        /// <summary>
        /// Quản trị nhập hệ thống quản trị của FPT Jobs
        /// </summary>
        /// <param name="data">Thông tin đăng nhập(UserName và mật khẩu)</param>
        /// <returns>Token của quản trị</returns>
        public static string GetAdminToken(Dictionary<string, string> data)
        {
            string urlLoginApi = string.Empty;
            try
            {
                // Lấy url api đăng nhập quản trị
                urlLoginApi = HttpUtilities.UrlAPI(ConfigUtil.InsideUrlAPI, InsideApiMethods.LOGIN_API);

                // Gọi và lấy kết quả trả về của api đăng nhập hệ thống quản trị
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentTypes.Json));
                    HttpResponseMessage response = httpClient.PostAsync(urlLoginApi, new FormUrlEncodedContent(data)).Result;
                    HttpContext.Current.Response.StatusCode = (int)response.StatusCode;
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(string.Format("Data:{0}. Message:{1}", urlLoginApi, ex.Message), ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// Ứng viên đăng nhập hệ thống FPT Jobs
        /// </summary>
        /// <param name="data">Thông tin đăng nhập(Email hoặc Phone, mật khẩu)</param>
        /// <returns>Token of user</returns>
        public static string GetToken(Dictionary<string, string> data)
        {
            string urlLoginApi = string.Empty;
            try
            {
                urlLoginApi = HttpUtilities.UrlAPI(ConfigUtil.UrlAPI, APIMethods.LOGIN_TOKEN_API);
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentTypes.Json));
                    HttpResponseMessage response = httpClient.PostAsync(urlLoginApi, new FormUrlEncodedContent(data)).Result;
                    HttpContext.Current.Response.StatusCode = (int)response.StatusCode;
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(string.Format("Data:{0}. Message:{1}", urlLoginApi, ex.Message), ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// Ứng viên đăng nhập với Facebook, Gooogle, LinkedIn
        /// </summary>
        /// <param name="data">Thông tin đăng nhập</param>
        /// <returns>Token đăng nhập</returns>
        public static string GetExternalToken(string data)
        {
            string urlLoginApi = string.Empty;
            try
            {
                urlLoginApi = HttpUtilities.UrlAPI(ConfigUtil.UrlAPI, APIMethods.EXTERNAL_LOGIN_TOKEN_API);
                using (HttpClient httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(data, Encoding.UTF8, ContentTypes.Json);
                    HttpResponseMessage response = httpClient.PostAsync(urlLoginApi, content).Result;
                    HttpContext.Current.Response.StatusCode = (int)response.StatusCode;
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(string.Format("Data:{0}. Message:{1}", urlLoginApi, ex.Message), ex);
                return string.Empty;
            }
        }



        /// <summary>
        /// Xác thực token đăng nhập
        /// </summary>
        /// <param name="provider">Thông tin đăng nhâp(FB, GG, LN)</param>
        /// <param name="accessToken">External token</param>
        /// <returns>Thông tin xác thực</returns>
        public static ParsedExternalAccessToken VerifyExternalAccessToken(string provider, string accessToken, string email)
        {
            // Provider external login
            const string GG = "google";
            const string FB = "facebook";
            const string LN = "linkedin";

            // Link verify external login
            const string FB_LINK = "https://graph.facebook.com/me?fields=email,last_name,first_name&access_token={0}";
            const string GG_LINK = "https://www.googleapis.com/oauth2/v1/tokeninfo?id_token={0}";
            const string LN_LINK = "https://api.linkedin.com/v1/people/~:(id,emailAddress)?oauth_token={0}&format=json";

            ParsedExternalAccessToken parsedToken = null;
            try
            {
                // Get verify link
                var verifyTokenEndPoint = string.Empty;
                if (provider.ToLower().Equals(FB))
                {
                    verifyTokenEndPoint = string.Format(FB_LINK, accessToken);
                }
                else if (provider.ToLower().Equals(GG))
                {
                    verifyTokenEndPoint = string.Format(GG_LINK, accessToken);
                }
                else if (provider.ToLower().Equals(LN))
                {
                    verifyTokenEndPoint = string.Format(LN_LINK, accessToken);
                }
                else
                {
                    return null;
                }

                // Send request verify and get result
                var client = new HttpClient();
                var uri = new Uri(verifyTokenEndPoint);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                var response = client.GetAsync(uri).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;

                    dynamic jObj = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                    parsedToken = new ParsedExternalAccessToken();

                    // Xác thực thông tin
                    if (provider.ToLower().Equals(FB))
                    {
                        parsedToken.email = jObj["email"];

                        if (!string.Equals(email, parsedToken.email, StringComparison.OrdinalIgnoreCase))
                        {
                            return null;
                        }
                    }
                    else if (provider.ToLower().Equals(GG))
                    {
                        parsedToken.email = jObj["email"];
                        if (!string.Equals(email, parsedToken.email, StringComparison.OrdinalIgnoreCase))
                        {
                            return null;
                        }

                    }
                    else if (provider.ToLower().Equals(LN))
                    {
                        parsedToken.email = jObj["emailAddress"];
                        if (!string.Equals(email, parsedToken.email, StringComparison.OrdinalIgnoreCase))
                        {
                            return null;
                        }
                    }

                }

                return parsedToken;
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// Parse result for verify external login
        /// </summary>
        public class ParsedExternalAccessToken
        {
            public string user_id { get; set; }
            public string app_id { get; set; }
            public string email { get; set; }
        }
    }
}
