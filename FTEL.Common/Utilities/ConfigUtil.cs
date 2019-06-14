using System;
using System.Configuration;
namespace FTEL.Common.Utilities
{
    public class ConfigUtil
    {
        // Domain
        public static string OutsiteDomain = GetConfigurationValueFromKey("OutsiteDomain", false);
        public static string InsiteDomain = GetConfigurationValueFromKey("InsiteDomain", false);

        public static string ConnectionString = GetConfigurationValueFromKey("ConnectionString", false);
        public static string cfgSCHEMA = GetConfigurationValueFromKey("cfgSCHEMA", false);
        public static string CacheTimeMinutes = GetConfigurationValueFromKey("CacheTimeMinutes", false);
        //----Directory--
        public static string ExcelDirectory = GetConfigurationValueFromKey("ExcelDirectory", false);
        public static string LogDirectory = GetConfigurationValueFromKey("LogDirectory", false);
        public static string WebApiOutside = GetConfigurationValueFromKey("WebApiOutside", false);
        public static string WebOutside = GetConfigurationValueFromKey("WebOutside", false);
        public static string WebApiInside = GetConfigurationValueFromKey("WebApiInside", false);
        public static string WebInside = GetConfigurationValueFromKey("WebInside", false);
        public static string PortraitImageDirectory = GetConfigurationValueFromKey("PortraitImageDirectory", false);
        public static string SyllDirectory = GetConfigurationValueFromKey("SyllDirectory", false);
        public static string VideoFileDirectory = GetConfigurationValueFromKey("VideoFileDirectory", false);
        public static string FileDirectory = GetConfigurationValueFromKey("FileDirectory", false);
        public static string HtmlEmailDirectory = GetConfigurationValueFromKey("HtmlEmailDirectory", false);
        public static string HttpImageFile = GetConfigurationValueFromKey("HttpImageFile", false);
        public static string VesionFile = GetConfigurationValueFromKey("VesionFile", false);
        //----Email----
        public static string Email_SendFlag = GetConfigurationValueFromKey("Email_SendFlag", false);
        public static string Email_DisplayName = GetConfigurationValueFromKey("Email_DisplayName", false);
        public static string Email_UserName = GetConfigurationValueFromKey("Email_UserName", false);
        public static string Email_PassWord = GetConfigurationValueFromKey("Email_PassWord", false);
        public static string Email_EmailSender = GetConfigurationValueFromKey("Email_EmailSender", false);
        public static string Email_SMTP_HOST = GetConfigurationValueFromKey("Email_SMTP_HOST", false);
        public static string Email_PORT = GetConfigurationValueFromKey("Email_PORT", false);
        public static string Email_EnableSsl = GetConfigurationValueFromKey("Email_EnableSsl", false);
        public static string Email_UseDefaultCredentials = GetConfigurationValueFromKey("Email_UseDefaultCredentials", false);
        //----SMS------       
        public static string SmsSendFlag = GetConfigurationValueFromKey("SmsSendFlag", false);
        public static string StaffIdSms = GetConfigurationValueFromKey("StaffId_SMS", false);
        public static string LocationIdSms = GetConfigurationValueFromKey("LocationId_SMS", false);
        public static string ResourceSms = GetConfigurationValueFromKey("Resource_SMS", true);
        public static string UrlPostSms = GetConfigurationValueFromKey("UrlPost_SMS", false);
        //-----Domain------
        public static string DomainName = GetConfigurationValueFromKey("DomainName", false);
        public static string SystemName = GetConfigurationValueFromKey("SystemName", false);
        public static string DomainBaseHttp = GetConfigurationValueFromKey("DomainBaseHttp", false);
        public static string LinkJob = GetConfigurationValueFromKey("LinkJob", false);
        public static string DomainBackEnd = GetConfigurationValueFromKey("DomainBackEnd", false);
        public static string HttpWebApi = GetConfigurationValueFromKey("WebApiHttp", false);
        public static string Storage_domain = GetConfigurationValueFromKey("storage_domain", false);
        public static string Outside_domain = GetConfigurationValueFromKey("outside_domain", false);
        public static string OutsideApiUrl = GetConfigurationValueFromKey("outsideApiUrl", false);
        public static string Outside_article_share_domain = GetConfigurationValueFromKey("outside_article_share_domain", false);
        public static string Outside_article_news_domain = GetConfigurationValueFromKey("outside_article_news_domain", false);

        //----MSMQ(microsoft message queue)-----------
        public static string article_email_message_queue_path = GetConfigurationValueFromKey("article_email_message_queue_path", false);

        //-----API Inside --------
        public static string InsideAPI = GetConfigurationValueFromKey("InsideApi", false);
        public static string InsideSupportApi = GetConfigurationValueFromKey("InsideSupportApi", false);
        public static string SignalRServer = GetConfigurationValueFromKey("signalrServer", false);

        //-------Import/Export-----
        public static string tmp_ExportTemp = GetConfigurationValueFromKey("tmp_ExportTemp", false);
        public static string tmp_ImportTemp = GetConfigurationValueFromKey("tmp_ImportTemp", false);
        public static string tmp_PrintTemp = GetConfigurationValueFromKey("tmp_PrintTemp", false);
        public static string tmp_Export_startWrite_row = GetConfigurationValueFromKey("tmp_Export_startWrite_row", false);
        public static string tmp_Import_startRead_row = GetConfigurationValueFromKey("tmp_Import_startRead_row", false);

        //nganh nghe
        public static string tmp_ImportTemp_jobcategory_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_jobcategory_fileName", false);
        public static string tmp_ExportTemp_jobcategory_fileName = GetConfigurationValueFromKey("tmp_ExportTemp_jobcategory_fileName", false);

        public static string tmp_ImportTemp_Applicant_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_Applicant_fileName", false);
        public static string tmp_ExportTemp_QuestionAnswer_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_QuestionAnswer_fileName", false);

        //nganh hoc
        public static string tmp_ImportTemp_major_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_major_fileName", false);
        public static string tmp_ExportTemp_major_fileName = GetConfigurationValueFromKey("tmp_ExportTemp_major_fileName", false);

        //chi nhanh lien he
        public static string tmp_ImportTemp_branchcontact_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_branchcontact_fileName", false);
        public static string tmp_ExportTemp_branchcontact_fileName = GetConfigurationValueFromKey("tmp_ExportTemp_branchcontact_fileName", false);

        //Quốc gia
        public static string tmp_ImportTemp_Country_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_Country_fileName", false);
        public static string tmp_ExportTemp_Country_fileName = GetConfigurationValueFromKey("tmp_ExportTemp_Country_fileName", false);

        //Tỉnh thành
        public static string tmp_ImportTemp_Province_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_Province_fileName", false);
        public static string tmp_ExportTemp_Province_fileName = GetConfigurationValueFromKey("tmp_ExportTemp_Province_fileName", false);

        //Quận huyện
        public static string tmp_ImportTemp_District_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_District_fileName", false);
        public static string tmp_ExportTemp_District_fileName = GetConfigurationValueFromKey("tmp_ExportTemp_District_fileName", false);
        public static string tmp_ImportTemp_Subject_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_subject_fileName", false);
        public static string tmp_ExportTemp_Subject_fileName = GetConfigurationValueFromKey("tmp_ExportTemp_subject_fileName", false);

        //danh sach den
        public static string tmp_ImportTemp_interview_blacklist_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_interview_blacklist_fileName", false);
        public static string tmp_ExportTemp_interview_blacklist_fileName = GetConfigurationValueFromKey("tmp_ExportTemp_interview_blacklist_fileName", false);

        //chuc danh
        public static string tmp_ImportTemp_jobposition_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_jobposition_fileName", false);
        public static string tmp_ExportTemp_jobposition_fileName = GetConfigurationValueFromKey("tmp_ExportTemp_jobposition_fileName", false);

        //cau hoi khao sat
        public static string tmp_ImportTemp_SurveyQuestion_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_SurveyQuestion_fileName", false);
        public static string tmp_ExportTemp_SurveyQuestion_fileName = GetConfigurationValueFromKey("tmp_ExportTemp_SurveyQuestion_fileName", false);

        public static string tmp_ImportTemp_SurveyAnswer_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_SurveyAnswer_fileName", false);
        public static string tmp_ImportTemp_Introduction_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_introduction_fileName", false);
        public static string tmp_ImportTemp_ExamStructure_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_ExamStructure_fileName", false);
        public static string tmp_ImportTemp_JobType_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_JobType_fileName", false);
        public static string tmp_ImportTemp_FptPerson_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_FptPerson_fileName", false);

        //Trường học
        public static string tmp_ImportTemp_School_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_School_fileName", false);
        public static string tmp_ExportTemp_School_fileName = GetConfigurationValueFromKey("tmp_ExportTemp_School_fileName", false);

        public static string tmp_ImportTemp_Skill_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_Skill_fileName", false);
        public static string tmp_ExportTemp_Skill_fileName = GetConfigurationValueFromKey("tmp_ExportTemp_Skill_fileName", false);
        public static string tmp_ImportTemp_QuestionVideo_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_QuestionVideo_fileName", false);
        public static string tmp_ExportTemp_QuestionVideo_fileName = GetConfigurationValueFromKey("tmp_ExportTemp_QuestionVideo_fileName", false);
        public static string tmp_ExportTemp_Question_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_Question_fileName", false);
        public static string tmp_ExportTemp_ReportOverall_fileName = GetConfigurationValueFromKey("tmp_ExportTemp_reportoverall_fileName", false);

        //cap can bo
        public static string tmp_ImportTemp_JobLevel_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_JobLevel_fileName", false);
        public static string tmp_ExportTemp_JobLevel_fileName = GetConfigurationValueFromKey("tmp_ExportTemp_JobLevel_fileName", false);

        public static string tmp_ExportTemp_Introduction_fileName = GetConfigurationValueFromKey("tmp_ExportTemp_Introduction_fileName", false);
        public static string tmp_ExportTemp_FPTPerson_fileName = GetConfigurationValueFromKey("tmp_ExportTemp_FPTPerson_fileName", false);
        public static string tmp_ExportTemp_LogAdminAction_fileName = GetConfigurationValueFromKey("tmp_ExportTemp_LogAdminAction_fileName", false);
        public static string tmp_ExportTemp_Danh_sach_banner_fileName = GetConfigurationValueFromKey("tmp_ExportTemp_Danh_sach_banner_fileName", false);
        public static string tmp_ImportTemp_Danh_sach_banner_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_Danh_sach_banner_fileName", false);

        //tin tuc - chi se
        public static string tmp_ImportTemp_Article_fileName = GetConfigurationValueFromKey("tmp_ImportTemp_Article_fileName", false);
        public static string tmp_ExportTemp_Article_fileName = GetConfigurationValueFromKey("tmp_ExportTemp_Article_fileName", false);

        public static string tmp_ImportExportTemp_Brach_fileName = GetConfigurationValueFromKey("tmp_ImportExportTemp_Brach_fileName", false);

        // API Url
        public static string UrlAPI = GetConfigurationValueFromKey("apiUrl", false);
        public static string InsideUrlAPI = GetConfigurationValueFromKey("insideApiUrl", false);

        // File accept
        public static string CvFileAccept = GetConfigurationValueFromKey("CvFile_accept", false);
        public static string AvtFileAccept = GetConfigurationValueFromKey("AvtFile_accept", false);

        // Google Client API
        public static string GoogleClientId = GetConfigurationValueFromKey("GoogleClientID", false);
        public static string GoogleClientSecret = GetConfigurationValueFromKey("GoogleClientSecret", false);

        // Facebook API
        public static string FBAppID = GetConfigurationValueFromKey("FBAppID", false);
        public static string FBAppSecret = GetConfigurationValueFromKey("FBAppSecret", false);
        public static string FBToken = GetConfigurationValueFromKey("FBToken", false);

        // LinkedIn API
        public static string LinkedInClientId = GetConfigurationValueFromKey("LinkedInClientId", false);
        public static string LinkedInClientSecret = GetConfigurationValueFromKey("LinkedInClientSecret", false);


        // Inside API
        public static string LDAP = ConfigUtil.GetConfigurationValueFromKey("ldapDomain", false);

        // Schedule
        public static string Schedule_Hour = GetConfigurationValueFromKey("Schedule_Hour", false);
        public static string Schedule_Minute = GetConfigurationValueFromKey("Schedule_Minute", false);
        // Schedule update
        public static string Update_hour = GetConfigurationValueFromKey("Update_hour", false);
        public static string Update_minute = GetConfigurationValueFromKey("Update_minute", false);

        // Security
        public static string EnableCors = GetConfigurationValueFromKey("EnableCors", false);

        // =============== Begin Live Chat ========================//
        public static string privateSignalRBearerToken = GetConfigurationValueFromKey("signalRPrivateToken", false);

        public static string liveChatPageIndex = GetConfigurationValueFromKey("signalRPageIndex", false);
        public static string liveChatPageSize = GetConfigurationValueFromKey("signalRPageSize", false);
        // =============== End Live Chat ========================//

        /// <summary>
        /// Lấy ra giá trị từ file cấu hình dựa trên tên của khóa đưa vào
        /// </summary>
        /// <param name="keyName">Tên key đầu vào</param>
        /// <param name="decrypt"></param>
        /// <returns>Nếu có trả về giá trị khóa. Nếu không có trả về chuỗi trắng</returns>
        public static string GetConfigurationValueFromKey(string keyName, bool decrypt)
        {
            var keyValue = string.Empty;
            try
            {
                keyValue = ConfigurationManager.AppSettings[keyName];
                if (decrypt && !string.IsNullOrEmpty(keyValue))
                {
                    keyValue = EncryptBase.Decrypt(EncryptBase.KeyEncrypt, keyValue);
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return keyValue;
        }
    }
}
