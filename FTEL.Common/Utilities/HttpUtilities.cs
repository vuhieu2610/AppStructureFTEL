using FTEL.Common.Constant;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FTEL.Common.Utilities
{
    public class HttpUtilities
    {
        /// <summary>
        /// Call GET API async method from client
        /// </summary>
        /// <typeparam name="T">Data type return</typeparam>
        /// <param name="url">API url </param>
        /// <param name="token">token authen</param>
        /// <returns>Data type return</returns>
        public static async Task<T> GetAPIAsync<T>(string url, string token = null) where T : new()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.BaseAddress = new Uri(url);
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    }
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<T>();
                    }
                    else
                    {
                        HttpContext.Current.Response.StatusCode = (int)response.StatusCode;
                        LogUtil.Error(string.Format("StatusCode:{0}. Response: {1}. Data: {2}", response.StatusCode, response.Content.ReadAsStringAsync().Result, url), null);
                        return new T();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(string.Format("Message: {0}. Url: {1}", ex.Message, url), ex);
                return new T();
            }
        }

        /// <summary>
        /// Call GET API sync method from client
        /// </summary>
        /// <typeparam name="T">Data type return</typeparam>
        /// <param name="url">API url </param>
        /// <param name="token">token authen</param>
        /// <returns>Data type return</returns>
        public static T GetAPISync<T>(string url, string token = null) where T : new()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.BaseAddress = new Uri(url);
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    }
                    HttpResponseMessage response = httpClient.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ReadAsAsync<T>().Result;
                    }
                    else
                    {
                        HttpContext.Current.Response.StatusCode = (int)response.StatusCode;
                        LogUtil.Error(string.Format("StatusCode:{0}. Response: {1}. Data: {2}", response.StatusCode, response.Content.ReadAsStringAsync().Result, url), null);
                        return new T();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(string.Format("Message: {0}. Url: {1}", ex.Message, url), ex);
                return new T();
            }
        }

        /// <summary>
        /// GetAPISync Không cần token
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T GetAPISync<T>(string url) where T : new()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(url);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ReadAsAsync<T>().Result;
                    }
                    else
                    {
                        HttpContext.Current.Response.StatusCode = (int)response.StatusCode;
                        LogUtil.Error(string.Format("StatusCode:{0}. Response: {1}. Data: {2}", response.StatusCode, response.Content.ReadAsStringAsync().Result, url), null);
                        return new T();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(string.Format("Message: {0}. Url: {1}", ex.Message, url), ex);
                return new T();
            }
        }

        /// <summary>
        /// Call POST API method from client
        /// </summary>
        /// <typeparam name="T">Data type return</typeparam>
        /// <param name="url">API url </param>
        /// <param name="data">Post data</param>
        /// <param name="token">token authen</param>
        /// <returns>Data type return</returns>
        public static async Task<T> PostAPIAsync<T>(string url, string data = null, string token = null) where T : new()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // Gui kem token xac thuc voi cac api yeu cau login
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    // Gui body cua post la trong
                    if (string.IsNullOrEmpty(data))
                    {
                        data = string.Empty;
                    }
                    StringContent content = new StringContent(data, Encoding.UTF8, ContentTypes.Json);
                    HttpResponseMessage response = await httpClient.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<T>();
                    }
                    else
                    {
                        if (HttpContext.Current.Response != null)
                            HttpContext.Current.Response.StatusCode = (int)response.StatusCode;
                        LogUtil.Error(string.Format("StatusCode:{0}. Response: {1}. Data: {2}. Url: {3}", response.StatusCode, response.Content.ReadAsStringAsync().Result, data, url), null);
                        return new T();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(string.Format("Message: {0}. Url: {1}. Data: {2}", ex.Message, url, data), ex);
                return new T();
            }
        }

        /// <summary>
        /// Call POST API method from client
        /// </summary>
        /// <typeparam name="T">Data type return</typeparam>
        /// <param name="url">API url </param>
        /// <param name="data">Post data</param>
        /// <param name="token">token authen</param>
        /// <returns>Data type return</returns>
        public static T PostAPISync<T>(string url, string data = null, string token = null) where T : new()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // Gui kem token xac thuc voi cac api yeu cau login
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    // Gui body cua post la trong
                    if (string.IsNullOrEmpty(data))
                    {
                        data = string.Empty;
                    }

                    StringContent content = new StringContent(data, Encoding.UTF8, ContentTypes.Json);
                    HttpResponseMessage response = httpClient.PostAsync(url, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ReadAsAsync<T>().Result;
                    }
                    else
                    {
                        HttpContext.Current.Response.StatusCode = (int)response.StatusCode;
                        LogUtil.Error(string.Format("StatusCode:{0}. Response: {1}. Data: {2}. Url: {3}", response.StatusCode, response.Content.ReadAsStringAsync().Result, data, url), null);
                        return new T();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(string.Format("Message: {0}. Url: {1}. Data: {2}", ex.Message, url, data), ex);
                return new T();
            }
        }

        public static T _download_serialized_json_data<T>(string url, string token = null) where T : new()
        {
            using (var w = new WebClient())
            {
                w.Encoding = Encoding.UTF8;
                w.Headers.Add("Authorization", "Bearer " + token);
                var jsonData = string.Empty;
                try
                {
                    jsonData = w.DownloadString(url);
                }
                catch (Exception ex)
                {
                    LogUtil.Error(string.Format("Message: {0}. Url: {1}", ex.Message, url), ex);
                }
                return !string.IsNullOrEmpty(jsonData) ? Libs.DeserializeObject<T>(jsonData) : new T();
            }
        }

        /// <summary>
        /// Get url api
        /// </summary>
        /// <param name="baseUrl">Base url api</param>
        /// <param name="actionApi">Action of api</param>
        /// <returns>Full url api</returns>
        public static string UrlAPI(string baseUrl, string actionApi)
        {
            StringBuilder sb = new StringBuilder(baseUrl);
            sb.Append(actionApi);
            return sb.ToString();
        }

        /// <summary>
        /// Get html template
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string DownloadHtmlTemplate(string url)
        {
            using (var w = new WebClient())
            {
                w.Encoding = Encoding.UTF8;
                var jsonData = string.Empty;
                try
                {
                    jsonData = w.DownloadString(url);
                }
                catch (Exception ex)
                {
                    LogUtil.Error(string.Format("Message: {0}. Url: {1}", ex.Message, url), ex);
                }
                return jsonData;
            }
        }

        public static int HttpPost(string linkRequest, string dataPost, out string resultData)
        {
            var result = 0;
            resultData = "";
            var encoding = new UTF8Encoding();
            var byteData = encoding.GetBytes(dataPost);
            var httpRequest = (HttpWebRequest)WebRequest.Create(linkRequest);
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
            httpRequest.Method = "POST";
            httpRequest.ContentLength = byteData.Length;
            httpRequest.Referer = "";
            try
            {
                var dataStream = httpRequest.GetRequestStream();
                dataStream.Write(byteData, 0, byteData.Length);
                dataStream.Close();
                var httpResponse = httpRequest.GetResponse();
                dataStream = httpResponse.GetResponseStream();
                if (dataStream != null)
                {
                    var reader = new StreamReader(dataStream);
                    resultData = reader.ReadToEnd();
                    result = 1;
                    reader.Close();
                    dataStream.Close();
                }
                httpResponse.Close();
            }
            catch (Exception ex)
            {
                LogUtil.Error(string.Format("Message: {0}. Url: {1}. Data: {2}", ex.Message, linkRequest, dataPost), ex);
                resultData = ex.Message;
            }
            return result;
        }

        public static async Task<string> HttpPostAsync(string linkRequest, string dataPost)
        {
            var resultData = "000";
            var encoding = new UTF8Encoding();
            var byteData = encoding.GetBytes(dataPost);
            var httpRequest = (HttpWebRequest)WebRequest.Create(linkRequest);
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
            httpRequest.Method = "POST";
            httpRequest.ContentLength = byteData.Length;
            httpRequest.Referer = "";
            try
            {
                var dataStream = httpRequest.GetRequestStream();
                dataStream.Write(byteData, 0, byteData.Length);
                dataStream.Close();
                var httpResponse = await httpRequest.GetResponseAsync();//.GetResponse();
                dataStream = httpResponse.GetResponseStream();
                if (dataStream != null)
                {
                    var reader = new StreamReader(dataStream);
                    resultData = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                }
                httpResponse.Close();
            }
            catch (Exception ex)
            {
                LogUtil.Error(string.Format("Message: {0}. Url: {1}. Data: {2}", ex.Message, linkRequest, dataPost), ex);
            }
            return resultData;
        }
    }
}
