using FTEL.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FTEL.Common.Utilities
{
    public class EmailUtility
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        //public static async Task SendMailAsync(string nameEmail, string mailTo, string subject, string messages)
        //{
        //    try
        //    {
        //        if (ConfigUtil.EmailSendFlag == "true")
        //        {
        //            if (!string.IsNullOrEmpty(messages) && !string.IsNullOrEmpty(subject) && !string.IsNullOrEmpty(mailTo))
        //            {
        //                //return false;
        //                var msg = new MailMessage { From = new MailAddress(ConfigUtil.EmailSend, nameEmail) };
        //                msg.To.Add(mailTo);
        //                msg.Subject = subject;
        //                msg.Body = messages;
        //                msg.IsBodyHtml = true;
        //                using (var client = new SmtpClient())
        //                {
        //                    client.EnableSsl = Convert.ToBoolean(ConfigUtil.EnableSsl);
        //                    client.UseDefaultCredentials = Convert.ToBoolean(ConfigUtil.UseDefaultCredentials);
        //                    client.Credentials = new NetworkCredential(ConfigUtil.EmailSend, ConfigUtil.PassMailSend);
        //                    client.Host = ConfigUtil.HostMail;
        //                    client.Port = Convert.ToInt32(ConfigUtil.PortMail);
        //                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //                    client.Timeout = 5000;
        //                    await client.SendMailAsync(msg);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public static bool SendMail(string nameEmail, string mailTo, string subject, string messages)
        {
            var result = false;
            try
            {
                if (ConfigUtil.Email_SendFlag == "true")
                {
                    if (string.IsNullOrEmpty(messages) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(mailTo))
                        return false;
                    var msg = new MailMessage { From = new MailAddress(ConfigUtil.Email_EmailSender, nameEmail) };
                    msg.To.Add(mailTo);
                    msg.Subject = subject;
                    msg.Body = messages;
                    msg.IsBodyHtml = true;
                    using (var client = new SmtpClient())
                    {
                        client.EnableSsl = Convert.ToBoolean(ConfigUtil.Email_EnableSsl);
                        client.UseDefaultCredentials = Convert.ToBoolean(ConfigUtil.Email_UseDefaultCredentials);
                        client.Credentials = new NetworkCredential(ConfigUtil.Email_UserName, ConfigUtil.Email_PassWord);
                        client.Host = ConfigUtil.Email_SMTP_HOST;
                        client.Port = Convert.ToInt32(ConfigUtil.Email_PORT);
                        client.Timeout = 5000;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Send(msg);
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.Message, ex);
                result = false;
            }
            return result;
        }
        public static async Task SendMail(string nameEmail, string mailTo, string mailCC, string subject, string messages)
        {
            try
            {
                if (ConfigUtil.Email_SendFlag == "true")
                {
                    var UserName = ConfigUtil.Email_UserName;
                    var PassWord = ConfigUtil.Email_PassWord;
                    var EmailSender = ConfigUtil.Email_EmailSender;
                    var DisplayName = ConfigUtil.Email_DisplayName;
                    var SMTP_HOST = ConfigUtil.Email_SMTP_HOST;
                    var PORT = ConfigUtil.Email_PORT;
                    var EnableSsl = ConfigUtil.Email_EnableSsl;
                    var UseDefaultCredentials = ConfigUtil.Email_UseDefaultCredentials;

                    var msg = new MailMessage { From = new MailAddress(EmailSender) };
                    msg.To.Add(mailTo);
                    if (!string.IsNullOrEmpty(mailCC))
                    {
                        msg.CC.Add(mailCC);
                    }
                    msg.Subject = subject;
                    msg.Body = messages;
                    msg.IsBodyHtml = true;

                    using (var client = new SmtpClient())
                    {
                        client.EnableSsl = Convert.ToBoolean(EnableSsl);
                        client.UseDefaultCredentials = Convert.ToBoolean(UseDefaultCredentials);
                        client.Credentials = new NetworkCredential(UserName, PassWord);
                        client.Host = SMTP_HOST;
                        client.Port = Convert.ToInt32(PORT);
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Send(msg);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.Message, ex);
            }
        }

        public static async Task SendMailOutsite(EmailObject email)
        {
            try
            {
                if (ConfigUtil.Email_SendFlag == "true")
                {
                    var SMTP_HOST = ConfigUtil.Email_SMTP_HOST;
                    var PORT = ConfigUtil.Email_PORT;
                    var EnableSsl = ConfigUtil.Email_EnableSsl;
                    var UseDefaultCredentials = ConfigUtil.Email_UseDefaultCredentials;

                    var msg = new MailMessage { From = new MailAddress(email.EmailSender) };
                    msg.To.Add(email.EmailTo);
                    if (!email.NotCC)
                    {
                        msg.CC.Add(email.EmailSender);
                    }
                    msg.Subject = email.Subject;
                    msg.Body = email.Body;
                    msg.IsBodyHtml = true;

                    using (var client = new SmtpClient())
                    {
                        client.EnableSsl = Convert.ToBoolean(EnableSsl);
                        client.UseDefaultCredentials = Convert.ToBoolean(UseDefaultCredentials);
                        client.Credentials = new NetworkCredential(email.EmailSender, email.EmailPassword);
                        client.Host = SMTP_HOST;
                        client.Port = Convert.ToInt32(PORT);
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Send(msg);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.Message, ex);
            }
        }

        //public static bool SendMailList(string nameEmail, List<string> mailToList, string subject, string messages)
        //{
        //    var result = false;
        //    try
        //    {
        //        if (ConfigUtil.EmailSendFlag == "true")
        //        {
        //            if (string.IsNullOrEmpty(messages) || string.IsNullOrEmpty(subject) || mailToList.Count <= 0)
        //                return false;
        //            var emailListChecked = new List<string>();
        //            for (var i = 0; i <= mailToList.Count - 1; i++)
        //            {
        //                if (IsValidEmail(mailToList[i]))
        //                {
        //                    emailListChecked.Add(mailToList[i]);
        //                }
        //            }
        //            if (emailListChecked.Count > 0)
        //            {
        //                var msg = new MailMessage { From = new MailAddress(ConfigUtil.EmailSend, nameEmail) };
        //                for (var i = 0; i <= emailListChecked.Count - 1; i++)
        //                {
        //                    msg.To.Add(emailListChecked[i]);
        //                }
        //                msg.Subject = subject;
        //                msg.Body = messages;
        //                msg.IsBodyHtml = true;
        //                using (var client = new SmtpClient())
        //                {
        //                    client.EnableSsl = Convert.ToBoolean(ConfigUtil.EnableSsl);
        //                    client.UseDefaultCredentials = Convert.ToBoolean(ConfigUtil.UseDefaultCredentials);
        //                    client.Credentials = new NetworkCredential(ConfigUtil.EmailSend, ConfigUtil.PassMailSend);
        //                    client.Host = ConfigUtil.HostMail;
        //                    client.Port = Convert.ToInt32(ConfigUtil.PortMail);
        //                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //                    client.Timeout = 5000;
        //                    client.Send(msg);
        //                }
        //                result = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}
        //public static async Task SendMailListAsync(string nameEmail, List<string> mailToList, string subject, string messages)
        //{
        //    try
        //    {
        //        if (ConfigUtil.EmailSendFlag == "true")
        //        {
        //            if (!string.IsNullOrEmpty(messages) && !string.IsNullOrEmpty(subject) && mailToList.Count > 0)
        //            {
        //                var emailListChecked = new List<string>();
        //                for (var i = 0; i <= mailToList.Count - 1; i++)
        //                {
        //                    if (IsValidEmail(mailToList[i]))
        //                    {
        //                        emailListChecked.Add(mailToList[i]);
        //                    }
        //                }
        //                if (emailListChecked.Count > 0)
        //                {
        //                    var msg = new MailMessage { From = new MailAddress(ConfigUtil.EmailSend, nameEmail) };
        //                    for (var i = 0; i <= emailListChecked.Count - 1; i++)
        //                    {
        //                        msg.To.Add(emailListChecked[i]);
        //                    }
        //                    msg.Subject = subject;
        //                    msg.Body = messages;
        //                    msg.IsBodyHtml = true;
        //                    using (var client = new SmtpClient())
        //                    {
        //                        client.EnableSsl = Convert.ToBoolean(ConfigUtil.EnableSsl);
        //                        client.UseDefaultCredentials = Convert.ToBoolean(ConfigUtil.UseDefaultCredentials);
        //                        client.Credentials = new NetworkCredential(ConfigUtil.EmailSend, ConfigUtil.PassMailSend);
        //                        client.Host = ConfigUtil.HostMail;
        //                        client.Port = Convert.ToInt32(ConfigUtil.PortMail);
        //                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //                        client.Timeout = 5000;
        //                        await client.SendMailAsync(msg);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public static async Task SendMail(string EmailDisplay, string EmailName, string EmailPass, string subject, List<string> listMailTo, List<string> listMailCC, string body)
        {
            try
            {
                if (ConfigUtil.Email_SendFlag == "true")
                {
                    var PassWord = EmailPass;
                    var EmailSender = EmailName;
                    var DisplayName = EmailDisplay;
                    var SMTP_HOST = ConfigUtil.Email_SMTP_HOST;
                    var PORT = ConfigUtil.Email_PORT;
                    var EnableSsl = ConfigUtil.Email_EnableSsl;
                    var UseDefaultCredentials = ConfigUtil.Email_UseDefaultCredentials;

                    var msg = new MailMessage { From = new MailAddress(EmailSender) };

                    foreach (var item in listMailTo.FindAll(c => !string.IsNullOrEmpty(c)).Distinct())
                    {
                        if (IsValidEmail(item))
                        {
                            msg.To.Add(item);
                        }
                    }
                    if (listMailCC != null)
                    {
                        foreach (var item in listMailCC.FindAll(c => !string.IsNullOrEmpty(c)).Distinct())
                        {
                            if (IsValidEmail(item))
                            {
                                msg.CC.Add(item);
                            }
                        }
                    }
                    //msg.CC.Add("vinhnd14@fpt.com.vn");
                    msg.CC.Add(EmailSender);
                    msg.Subject = subject;
                    msg.Body = body;
                    msg.IsBodyHtml = true;

                    using (var client = new SmtpClient())
                    {
                        client.EnableSsl = Convert.ToBoolean(EnableSsl);
                        client.UseDefaultCredentials = Convert.ToBoolean(UseDefaultCredentials);
                        client.Credentials = new NetworkCredential(EmailSender, PassWord);
                        client.Host = SMTP_HOST;
                        client.Port = Convert.ToInt32(PORT);
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Send(msg);
                    }
                }
            }
            catch (Exception ex)
            {
                Libs.WriteLog("SendMail", ex.Message);
            }
        }
    }
}
