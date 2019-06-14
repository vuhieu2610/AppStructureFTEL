using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEL.Common.Utilities
{
    public class Constants
    {
        public const bool HAS_DATA = true;
        public const bool NOT_HAS_DATA = false;
        public const int ZERO_RECORD = 0;


        public const string SUCCESS = "0";
        public const string FAILED = "1";

        // News type
        public const string ARTICLE_TYPE_NEWS = "news";
        public const string ARTICLE_TYPE_SHARE = "share";
        public const string ARTICLE_TYPE_FOOTER = "footer";

        // Get top recruitment news
        public const int GET_HOT_RECRUITMENT_NEWS_HOME_PAGE = 20;
        public const int GET_HOT_RECRUITMENT_NEWS_SUB_PAGE = 5;

        // Token
        public const string USER_NAME = "username";
        public const string PASSWORD = "password";
        public const string GRANT_TYPE_PASSWORD = "password";
        public const string GRANT_TYPE_REFRESH = "refresh";

        // Banners
        public const string BANNERS_TYPE_HEADER = "top";
        public const string BANNERS_TYPE_NAV = "nav";
        public const int BANNERS_HOME_PAGE = 1;
        public const int BANNSERS_OTHER_PAGE = 0;

        // Introduction
        public const int INTRODUCTION_TYPE_COMPANY = 0;
        public const string INTRODUCTION_FTEL = "Introduction";

        // ErrorCode
        public const string ERR_EXCEPTION = "EXCEPTION";
        public const string ERR_EXCEPTION_SQLEXCEPTION = "ERR_EXCEPTION_SQLEXCEPTION";
        public const string ERR_API_CONNECTION_ERROR = "ERR_API_CONNECTION_ERROR";
        public const string NOT_FOUND = "NOT_FOUND";
        public const string ERROR_THERE_IS_DATA_REFERENCE_CANNOT_DELETE = "ERROR_THERE_IS_DATA_REFERENCE_CANNOT_DELETE";

        //Error message
        public const string ERR_EXCEPTION_SQLEXCEPTION_MESSAGE = "Không thể kết nối đến cơ sở dữ liệu";
        public const string ERR_API_CONNECTION_MESSAGE = "Không thể kết nối đến API";
        //Error Number
        public const int SQL_CONNECTION_ERROR_NUMBER = 121;
        public const int SQL_AUTHENTICATION_ERROR_NUMBER = 18456;

        // User constants
        public const int ACTIVATED = 1;
        public const int DEACTIVATED = 0;
        public const string ACTIVATED_STRING = "ACTIVATED";
        public const string DEACTIVATED_STRING = "DEACTIVATED";
        public const string CustomerLogin = "CUSTOMER_LOGIN";

        // Interview type
        public const int TYPE_CALENDAR_ONLINE = 0;              //Phỏng vấn Online
        public const int TYPE_CALENDAR_OFFLINE = 1;             //Phỏng vấn trực tiếp
        public const int TYPE_EXAM_VIDEO = 2;                   //Bài thi video
        public const int TYPE_CALENDAR_GET_JOB = 99;            //Đặt lịch nhận việc

        // Example type
        public const int CO_THI = 1;
        public const int KO_THI = 0;

        // Title email
        public const string EMAIL_TITLE_APPLICANT_APPLY = "Thông báo ứng tuyển thành công vị trí {0}";
        public const string EMAIL_TITLE_REPORT_APPLY_HR = "Có ứng viên {0} ứng tuyển cho tin tuyển dụng {1}";

        // Email template html
        public const string EMAIL_TEMPLATE_REPORT = "ThongBaoUngTuyen.html";
        public const string EMAIL_TEMPLATE_APPLY_NO_EXAME = "UngTuyenKoThi.html";
        public const string EMAIL_TEMPLATE_APPLY_EXAME = "UngTuyenCoThi.html";

        // Url example
        public const string URL_EXAME = "{0}/ung-vien/danh-sach-da-ung-tuyen";

        //MSMQ
        public const string LAST_MESSAGE_IN_QUEUE = "1";

        //Introduction
        public const int TYPE_INTROCOMPANY = 0;              //Phỏng vấn Online
        public const int TYPE_INTRODEPARTMENT = 1;             //Phỏng vấn trực tiếp
        public const int TYPE_RECRUIMENTPROCESS = 2;
    }
}
