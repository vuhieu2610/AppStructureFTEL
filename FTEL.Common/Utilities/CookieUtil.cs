/*
 *  **********************************************************************************
 Company: FTEL
 Author: VinhND14   
 Create Date: 1/10/2018
 Purpose: Cung cấp hàm xử lý đối với cookie
 *  **********************************************************************************  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FTEL.Common.Utilities
{
    public class CookieUtil
    {
        /// <summary>
        /// Tạo mới cookie
        /// </summary>
        /// <param name="cookieName">Tên cookie</param>
        /// <param name="value">Giá trị</param>
        /// <param name="expiredDate">Thời gian tồn tại kể từ lúc tạo tính theo phút</param>
        public static void SetCookie(string cookieName, string value, int expiredDate)
        {
            HttpCookie cookie = new HttpCookie(cookieName)
            {
                Value = value,
                Expires = DateTime.Now.AddMinutes(expiredDate),
                HttpOnly = true
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static string GetCookie(string cookieName)
        {
            var result = "";
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                result = cookie.Value;
            }
            return result;
        }
        public static void RemoveCookie(string cookieName)
        {
            HttpCookie nameCookie = HttpContext.Current.Request.Cookies[cookieName];
            if (nameCookie != null)
            {
                //Set the Expiry date to past date.
                nameCookie.Expires = DateTime.Now.AddDays(-1);
                //Update the Cookie in Browser.
                HttpContext.Current.Response.Cookies.Add(nameCookie);
            }

        }
    }
}
