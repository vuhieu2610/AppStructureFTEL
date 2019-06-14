using System;
using System.Threading.Tasks;

namespace FTEL.Common.Utilities
{
    public class SmsUtil
    {
        public static void SmsSend(string phoneNumber, string msg, out string rpData)
        {                  
            rpData = "default";
            try
            {
                if (ConfigUtil.SmsSendFlag == "true")
                {
                    if (!string.IsNullOrEmpty(phoneNumber) && !string.IsNullOrEmpty(msg))
                    {
                        phoneNumber = "84" + phoneNumber.Trim().Substring(1, phoneNumber.Length - 1);
                        var dataPost = "StaffID=" + ConfigUtil.StaffIdSms;
                        dataPost += "&PhoneNumber=" + phoneNumber;
                        dataPost += "&Message=" + msg;
                        dataPost += "&LocationID=" + ConfigUtil.LocationIdSms;
                        dataPost += "&Resource=" + ConfigUtil.ResourceSms;
                        var urlPost = ConfigUtil.UrlPostSms;
                        string responseData;
                        var objPost = HttpUtilities.HttpPost(urlPost, dataPost, out responseData);
                        rpData = responseData;
                        LogUtil.Info(responseData);
                    }
                }
            }
            catch (Exception ex)
            {
                rpData = "ex:" + ex.ToString();
                LogUtil.Error(ex.Message, ex);
            }
        }
        public static async Task SmsSendAsync(string phoneNumber, string msg)
        {
            try
            {
                if (ConfigUtil.SmsSendFlag == "true")
                {
                    if (!string.IsNullOrEmpty(phoneNumber) && !string.IsNullOrEmpty(msg))
                    {
                        phoneNumber = "84" + phoneNumber.Trim().Substring(1, phoneNumber.Length - 1);
                        var dataPost = "StaffID=" + ConfigUtil.StaffIdSms;
                        dataPost += "&PhoneNumber=" + phoneNumber;
                        dataPost += "&Message=" + msg;
                        dataPost += "&LocationID=" + ConfigUtil.LocationIdSms;
                        dataPost += "&Resource=" + ConfigUtil.ResourceSms;
                        var urlPost = ConfigUtil.UrlPostSms;
                        var result = await HttpUtilities.HttpPostAsync(urlPost, dataPost);
                        LogUtil.Info(result);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.Message, ex);
            }
        }
    }
}
