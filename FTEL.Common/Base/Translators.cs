using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FTEL.Common.Utilities;
namespace FTEL.Common.Base
{
    public class Translators
    {
        private static DataSet _dataSource;
        private static string _language = "vi";
        private static string _screenName = "";
        //private static Translators objTrans = null;
        //private Translators()
        //{
        //}
        public static string LanguageSelected()
        {
            var lang = CookieUtil.GetCookie("lang");  //HttpContext.Current.Request.QueryString["lang"];
            if (string.IsNullOrEmpty(lang))
            {
                lang = "vi";
            }
            return lang;
        }
        public static string LangShow(string textVi, string textEn)
        {
            return LanguageSelected() == "en" ? textEn : textVi;
        }
        //public static string LanguageSelected()
        //{
        //    return _language;
        //}
        //public static Translators Instance()
        //{
        //    if (null == objTrans)
        //    {
        //        return new Translators();
        //    }
        //    else
        //    {
        //        return objTrans;
        //    }
        //}

        private static DataSet GetDataSource(string language, string screenName)
        {
            if (/*(null == _dataSource) ||*/ (_screenName != screenName) || (_language != language))
            {
                _dataSource = new DataSet();
                _language = language;
                _screenName = screenName;
                try
                {
                    var ds = new DataSet();
                    var dt1 = new DataSet();
                    var strXml = AppDomain.CurrentDomain.BaseDirectory + "\\XMLs\\Languages\\" + language + ".xml";                    
                    dt1.ReadXml(strXml);
                    ds.Merge(dt1);
                    if (!string.IsNullOrEmpty(screenName))
                    {
                        if (screenName.Contains("|"))
                        {
                            foreach (var ptu in screenName.Split('|'))
                            {
                                try
                                {
                                    var dt3 = new DataSet();
                                    dt3.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\XMLs\\Languages\\" + ptu + "_" + language + ".xml");                                  
                                    ds.Merge(dt3);
                                }
                                catch (Exception)
                                { }
                            }
                        }
                        else
                        {
                            try
                            {                              
                                var dt2 = new DataSet();
                                dt2.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\XMLs\\Languages\\" + screenName + "_" + language + ".xml");
                             
                                ds.Merge(dt2);
                            }
                            catch (Exception)
                            { }
                        }

                    }
                    if (ds.Tables.Count > 0)
                    {
                        _dataSource = ds;
                    }
                }
                catch (Exception ex)
                {
                    //Common.Libs.WriteLog("zzz", ex.ToString());
                    // ignored
                }
            }
            return _dataSource;
        }
        public static void Translate(ref string html, string screenName)
        {
            //var lang = HttpContext.Current.Request.QueryString["lang"];
            //if (string.IsNullOrEmpty(lang))
            //{
            //    lang = "vi";
            //}          
            try
            {               
                var dt = GetDataSource(LanguageSelected(), screenName).Tables["TransCode"];
                if (null == dt) return;               
                html = dt.Rows.Cast<DataRow>().Where(dr => Convert.ToString(dr["TranCode"]) != "").Aggregate(html, (current, dr) => current.Replace(Convert.ToString(dr["TranCode"]), Convert.ToString(dr["Value"])));

                //if (null != dt)
                //{
                //    foreach (DataRow dr in dt.Rows)
                //    {
                //        if (Convert.ToString(dr["TranCode"]) != "")
                //        {
                //            Html = Html.Replace(Convert.ToString(dr["TranCode"]), Convert.ToString(dr["Value"]));
                //        }
                //    }
                //}
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// Lấy multiple ngôn ngữ bằng mã code chuyển đổi
        /// </summary>
        /// <param name="key">Mã code chuyển đổi</param>
        /// <param name="defaultValue">Giá trị mặc định trong tường hợp không tìm thấy</param>
        /// <returns>Ngôn ngữ tương ứng với mã code</returns>
        public static string Single(string key, string defaultValue)
        {
            string value = string.Empty;
            try
            {
                var dt = GetDataSource(LanguageSelected(), null).Tables["TransCode"];
                var dr = dt.Rows.Cast<DataRow>().Where(drow => Convert.ToString(drow["TranCode"]) == key).FirstOrDefault();
                if (dr != null && dr[1] != null)
                {
                    value = dr[1].ToString();
                    return value;
                }
            }
            catch (Exception)
            {
                // Write log exception
            }

            return defaultValue;
        }
    }
}