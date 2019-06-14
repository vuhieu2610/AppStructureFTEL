using FTEL.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FTEL.Common.Enums
{
    public class ResourcesApplicant
    {
        /// <summary>
        /// Khởi tạo từ điển các nguồn tuyển dụng
        /// </summary>
        /// <returns>Từ điển nguồn tuyển dụng</returns>
        public static Dictionary<string, string> GetListResources()
        {
            Dictionary<string, string> resourceDict = new Dictionary<string, string>();

            resourceDict.Add("1", "FPTJobs.com");
            resourceDict.Add("2", "Tuyendung.fpt.com.vn");
            resourceDict.Add("3", "Timviecnhanh.com");
            resourceDict.Add("4", "Careerlink.vn");
            resourceDict.Add("5", "Vieclam24h.vn");
            resourceDict.Add("6", "Vietnamworks.com");
            resourceDict.Add("7", "Linkedin");
            resourceDict.Add("8", "Facebook");
            resourceDict.Add("9", "Mywork.com.vn");
            resourceDict.Add("10", "Tuyendung.com.vn");
            resourceDict.Add("11", "Jobstreet.vn");
            resourceDict.Add("12", "Itviec.com");
            resourceDict.Add("13", "Careerbuilder.vn");
            resourceDict.Add("14", "YBOX");
            resourceDict.Add("15", "TOPCV");
            resourceDict.Add("16", Translators.Single("[EMAIL_MOI_UNG_TUYEN]", "Email mời tuyển dụng"));
            resourceDict.Add("17", Translators.Single("[BANDEROLE_POSTER_TO_ROI]", "Tờ rơi"));
            resourceDict.Add("18", Translators.Single("[BAO_GIAY]", "Báo giấy"));
            resourceDict.Add("19", Translators.Single("[HOI_CHO_VIEC_LAM]", "Hội chợ việc làm"));
            resourceDict.Add("20", Translators.Single("[TRUONG]", "Trường"));
            resourceDict.Add("21", Translators.Single("[EVENT_TUYEN_DUNG]", "Event Tuyển dụng"));
            resourceDict.Add("22", Translators.Single("[NGUOI_THAN_GIOI_THIEU]", "Người thân giới thiệu"));

            return resourceDict;
        }

        /// <summary>
        /// Lấy text của nguồn tuyển dụng của ứng viên
        /// </summary>
        /// <param name="key">Key nguồn tuyển dụng</param>
        /// <returns>Text nguồn tuyển dụng</returns>
        public static string GetTextResource(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }

            Dictionary<string, string> resourcesDic = GetListResources();

            if (resourcesDic.ContainsKey(key))
            {
                return resourcesDic[key];
            }
            else
            {
                return key;
            }
        }

        /// <summary>
        /// Khởi tạo từ điển các hệ học
        /// </summary>
        /// <returns>Từ điển hệ học</returns>
        public static Dictionary<string, string> GetListQualifications()
        {
            Dictionary<string, string> quanDict = new Dictionary<string, string>();

            quanDict.Add("2", Translators.Single("[TIEN_SI]", "Tiến sĩ"));
            quanDict.Add("3", Translators.Single("[THAC_SI]", "Thạc sĩ"));
            quanDict.Add("4", Translators.Single("[DAI_HOC]", "Đại học"));
            quanDict.Add("5", Translators.Single("[CAO_DANG]", "Cao Đẳng"));
            quanDict.Add("6", Translators.Single("[TC]", "Trung cấp"));
            quanDict.Add("7", Translators.Single("[THPT]", "Trung Học Phổ Thông"));
            quanDict.Add("8", Translators.Single("[THCS]", "Trung Học Cơ Sở"));


            return quanDict;
        }

        /// <summary> 
        /// Lấy text của hệ học của ứng viên
        /// </summary>
        /// <param name="key">Key hệ học</param>
        /// <returns>Text hệ học</returns>
        public static string GetTextQualification(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }

            Dictionary<string, string> quanDict = GetListQualifications();
            if (quanDict.ContainsKey(key))
            {
                return quanDict[key];
            }
            else
            {
                return key;
            }
        }

        /// <summary>
        /// Khởi tạo từ điển Tình trạng hôn nhân
        /// </summary>
        /// <returns>Từ điển Tình trạng hôn nhân</returns>
        /// <author>sonnt85</author>
        /// <createDate>12/11/2018</createDate>
        public static Dictionary<string, string> GetListMaritalStatus()
        {
            Dictionary<string, string> maritalStatus = new Dictionary<string, string>();

            maritalStatus.Add("S", Translators.Single("[DOC_THAN]", "Độc thân"));
            maritalStatus.Add("M", Translators.Single("[KET_HON]", "Đã kết hôn"));
            return maritalStatus;
        }

        /// <summary>
        /// Lấy text của Tình trạng hôn nhân
        /// </summary>
        /// <param name="key">Key Tình trạng hôn nhân</param>
        /// <returns>Text Tình trạng hôn nhân</returns>
        /// <author>sonnt85</author>
        /// <createDate>12/11/2018</createDate>
        public static string GetTextMaritalStatus(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                return string.Empty;
            }

            Dictionary<string, string> maritalStatusDict = GetListMaritalStatus();
            if (maritalStatusDict.ContainsKey(key))
            {
                return maritalStatusDict[key];
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Khởi tạo từ điển Giới tính
        /// </summary>
        /// <returns>Từ điển Giới tính</returns>
        /// <author>sonnt85</author>
        /// <createDate>06/11/2018</createDate>
        public static Dictionary<int, string> GetListSexes()
        {
            Dictionary<int, string> sex = new Dictionary<int, string>();

            sex.Add(1, Translators.Single("[GIOI_TINH_NAM]", "Nam"));
            sex.Add(2, Translators.Single("[GIOI_TINH_NU]", "Nữ"));
            sex.Add(3, Translators.Single("[GIOI_TINH_KHAC]", "Khác"));
            return sex;
        }

        /// <summary>
        /// Lấy về text của giới tính
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetTextSex(int key)
        {
            if (key == 0)
            {
                return string.Empty;
            }

            Dictionary<int, string> sex = GetListSexes();

            if (sex.ContainsKey(key))
            {
                return sex[key];
            }
            else
            {
                return string.Empty;
            }
        }
    }
}