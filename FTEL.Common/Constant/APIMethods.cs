using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEL.Common.Constant
{
    public static class APIMethods
    {
        //survey API
        public const string GET_SURVEY_API = "api/Survey/GetSurvey";
        public const string GET_QUESTIONID_API = "api/Survey/GetQuestionId";
        public const string INSERT_SURVEY_API = "api/Survey/InsertSurvey";

        //Contact API
        public const string GET_CONTACT_API = "api/Contact/GetContact";
        public const string GET_CONTACT_API_BY_ID = "api/Contact/GetContactById/{0}";
        public const string SEARCH_CONTACT = "api/Contact/SearchContact/{0}";

        // News API
        public const string GET_ARTICLES_BY_NEWS_API = "api/News/GetAllNews";
        public const string GET_PAGING_ARTICLES_BY_NEWS_API = "api/News/GetPagingAllNews";
        public const string GET_ARTICLES_BY_SHARES_API = "api/News/GetAllShares";
        public const string GET_ARTICLES_BY_FOOTER_API = "api/News/GetFooterNews";
        public const string GET_ARTICLES_HOTNEWS_API = "api/News/GetHotNews";
        public const string GET_ARTICLES_BY_ID_API = "api/News/Get/{0}";
        public const string GET_RELATIVE_ARTICLES_BY_NEWS_API = "api/News/GetRelativeNews";
        public const string GET_RELATIVE_ARTICLES_BY_SHARES_API = "api/News/GetRelativeShares";


        // Applicant API
        public const string LOGIN_TOKEN_API = "token";
        public const string REGISTER_APPLICANT_API = "api/Applicant/Register";
        public const string GET_PROFILE_APPLICANT_API = "api/Applicant/GetProfile";
        public const string UPDATE_PROFILE_APPLICANT_API = "api/Applicant/UpdateProfile";
        public const string APPLY_RECRUITMENT_APPLICANT_API = "api/Applicant/Apply";
        public const string APPLY_LIST_JOB_API = "api/Applicant/AppliedListJob";
        public const string CHANGE_PASSWORD_APPLICANT_API = "api/Applicant/ChangePassword";
        public const string GET_JOB_FOLLOW_API = "api/Applicant/GetJobFollow";
        public const string INSERT_JOB_FOLLOW_READ_API = "api/Applicant/InsertJobFollowRead";
        public const string GET_JOB_FOLLOW_INFO_API = "api/Applicant/GetJobFollowInfo";
        public const string DELETE_JOB_FOLLOW_INFO = "api/Applicant/DeleteJobFollowInfo";
        public const string INSERT_JOB_FOLLOW_INFO = "api/Applicant/InsertJobFollowInfo";
        public const string UPLOAD_AVATAR_API = "api/Applicant/UpdateAvatar";
        public const string EXTERNAL_LOGIN_TOKEN_API = "api/Applicant/ExternalLogin";
        public const string FORGET_PASSWORD_API = "api/Applicant/ForgetPassword";
        public const string RESET_PASSWORD_API = "api/Applicant/UpdatePassword";
        public const string GET_OTP_CODE_API = "api/Applicant/SendOTP";
        public const string CONFIRM_EMAIL = "api/Applicant/ConfirmEmail";
        public const string GET_INTERVIEW_ONLINE = "api/Applicant/GetInterviewOnline";
        public const string ACCEPT_INTERVIEW = "api/Applicant/AcceptInterview";
        public const string DELAY_INTERVIEW = "api/Applicant/DelayInterview";
        public const string DENY_INTERVIEW = "api/Applicant/DenyInterview";
        public const string ACCEPT_JOB = "api/Applicant/AcceptJob";
        public const string DELAY_JOB = "api/Applicant/DelayJob";
        public const string DENY_JOB = "api/Applicant/DenyJob";
        public const string GET_ALL_NOTIFY = "api/ApplicantNotifications/GetPaging";
        public const string MASK_AS_READ_NOTIFY = "api/ApplicantNotifications/MaskAsRead?id={0}";
        public const string NEW_NOTIFY = "api/ApplicantNotifications/NewNotify";

        // Job API
        public const string GET_RECRUITMENTNEWS_API = "api/RecruitmentNews/GetRecruitmentNews";
        public const string GET_RECRUITMENTNEWS_DETAILS_API = "api/RecruitmentNews/GetDetailRecruitmentNews/{0}";
        public const string GET_RECRUITMENTNEWS_HOME_API = "api/RecruitmentNews/GetHomeRecruitmentNews";
        public const string GET_RECRUITMENTNEWS_HOT_SUB_PAGE_API = "api/RecruitmentNews/GetHotRecruitmentNews";
        public const string SEARCH_JOB_HOT_API = "api/RecruitmentNews/SearchJobHots";
        public const string SEARCH_JOB_NEW_API = "api/RecruitmentNews/SearchJobNews";
        public const string SEARCH_JOB_MANAGERS_API = "api/RecruitmentNews/SearchManagers";
        public const string GET_RECRUITMENTNEWSPAGING_API = "api/RecruitmentNews/JobsPaging";
        public const string GET_TOP15TAG_API = "api/RecruitmentNews/Top15Tag";
        public const string RESULT_TAGRECRUIMENT_API = "api/RecruitmentNews/ResultTagRecruiment";
        public const string INSERT_SEARCHKEYWORD_API = "api/RecruitmentNews/InsertKeyword";
        public const string RECRUITMENT_SEARCH = "api/RecruitmentNews/RecruimentSearch";
        public const string GET_BRANCHADDRESS = "api/RecruitmentNews/GetBranchAddress/{0}";
        // Catalog API
        public const string GET_PROVINCES_API = "api/Catalog/GetAllProvinces";
        public const string GET_JOB_CATEGORIES_API = "api/Catalog/GetAllJobCategories";
        public const string GET_TOP_KEYWORDS_API = "api/Catalog/GetTopKeywords";
        public const string GET_DISTRICTS_BY_PROVINCE_CODE_API = "api/Catalog/GetDistrictsByProvinceCode/{0}";
        public const string GET_SCHOOLS_API = "api/Catalog/GetAllSchools";
        public const string GET_BANNERS_MAIN_PAGE_API = "api/Catalog/GetAllMainBanners";
        public const string GET_BANNERS_SUB_PAGE_API = "api/Catalog/GetAllSubBanners";
        public const string GET_MAJORS_API = "api/Catalog/GetAllMajors";
        public const string GET_CONFIG_API = "api/Catalog/GetConfig/{0}";
        public const string GET_INTRODUCTION_BY_TYPE = "api/Catalog/GetIntroduction/{0}";
        public const string GET_INTRODUCTION_ORG_COM = "api/Catalog/GetOrganizationCompany";
        public const string GET_INTRODUCTION_RECRUITMENT = "api/Catalog/GetRecruitmentProcess";
        public const string GET_RIGHT_ADVERTISE = "api/Catalog/GetRightAdvertise";
        public const string GET_FOOTER_ADVERTISE = "api/Catalog/GetFooterAdvertise";
        public const string GET_SUGGESTS_API = "api/Catalog/GetAllSuggest";
        public const string WRITE_LOG_ACTION_API = "api/Catalog/WriteActionLog";

        // Search API
        public const string GET_FULLSEARCH_API = "api/Search/FullSearch";
        public const string GET_FULLJOBSSEARCH_API = "api/Search/FullJobsSearch";
        public const string GET_FULLARTICLESEARCH_API = "api/Search/FullArticleSearch";
        public const string RECRUITMENTNEW_SEARCH_API = "api/Search/FullRecruimentSearch";
        public const string RECRUITMENTNEWS_SEARCH_API = "api/RecruitmentNews/Search";

        //Video call
        public const string GET_INTERVIEWER_CALENDAR = "api/InterviewOnline/GetInterviewCalendar?applyReId={0}";

        //FPT Person
        public const string GET_FPTPERSON_BY_FOOTER_API = "api/FPTPerson/GetFooterFPTPerson";
    }
    public static class ExamAPIMethods
    {
        //Exam API
        public const string CHECK_EXAM_EXISTS = "api/Exam/CheckExamExists";
        public const string GET_DETAIL_EXAM = "api/Exam/GetDetailExam";
        public const string GET_EXAM_QUESTION = "api/Exam/GetExamQuestion";
        public const string FINISH_EXAM = "api/Exam/FinishExam";
        public const string GET_TIME = "api/Exam/GetTime";
    }
}
