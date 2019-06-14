using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEL.Common.Constant
{
    public class InsideApiMethods
    {
        // Exam structure Api
        public const string GET_ALL_EXAM_STRUCTURE_API = "api/ExamStructure/GetAllExamStructure";
        public const string GET_ALL_EXAM_STRUCTURE_SEARCH_PAGING_API = "api/ExamStructure/GetAllExamStructureSearchPaging";
        public const string GET_EXAM_STRUCTURE_BY_ID_API = "api/ExamStructure/GetExamStructureById/{0}";
        public const string ADD_NEW_EXAM_STRUCTURE_API = "api/ExamStructure/AddNewExamStructure";
        public const string UPDATE_EXAM_STRUCTURE_API = "api/ExamStructure/UpdateExamStructure";
        public const string UPDATE_EXAM_STRUCTURE_STATUS_API = "api/ExamStructure/UpdateStatus";
        public const string IMPORT_EXAM_STRUCTURE_API = "api/ExamStructure/Import";
        public const string DELETE_EXAM_STRUCTURE_API = "api/ExamStructure/DeleteExamStructureById";

        // Users API
        public const string LOGIN_API = "token";

        //Survey API
        public const string GET_ALL_SURVEY_QUESTION_API = "api/Survey/GetAllQuestion";
        public const string GET_ALL_CREATED_USER_QUESTION_API = "api/Survey/GetAllCreatedUserQuestion";
        public const string GET_ALL_MODIFIED_USER_QUESTION_API = "api/Survey/GetAllModifiedUserQuestion";
        public const string GET_ALL_SEARCH_PAGING_QUESTION_API = "api/Survey/GetAllWithSearchPagingQuestion";
        public const string GET_QUESTION_BY_ID_API = "api/Survey/GetItemQuestionById/{0}";
        public const string INSERT_QUESTION_API = "api/Survey/InsertQuestion";
        public const string SERVEY_QUESTION_INSERT_AND_UPDATE_STATUS = "api/Survey/SurveyQuestion_Insert_And_Update_Status";
        public const string UPDATE_QUESTION_API = "api/Survey/UpdateQuestion";
        public const string UPDATE_STATUS_QUESTION_API = "api/Survey/UpdateStatusQuestion";
        public const string DELETE_QUESTION_API = "api/Survey/DeleteQuestion";
        public const string IMPORTEXCEL_QUESTION_API = "api/Survey/ImportExcelQuestion";
        public const string SURVEY_QUESTION_INSERT_LIST = "api/Survey/InsertQuestionList";


        public const string GET_ALL_SURVEY_ANSWER_API = "api/Survey/GetAllAnswer/{0}";
        public const string GET_ALL_CREATED_USER_ANSWER_API = "api/Survey/GetAllCreatedUserAnswer/{0}";
        public const string GET_ALL_MODIFIED_USER_ANSWER_API = "api/Survey/GetAllModifiedUserAnswer/{0}";
        public const string GET_ALL_SEARCH_PAGING_ANSWER_API = "api/Survey/GetAllWithSearchPagingAnswer/{0}";
        public const string GET_ANSWER_BY_ID_API = "api/Survey/GetItemAnswerById/{0}";
        public const string INSERT_ANSWER_API = "api/Survey/InsertAnswer";
        public const string UPDATE_ANSWER_API = "api/Survey/UpdateAnswer";
        public const string UPDATE_STATUS_ANSWER_API = "api/Survey/UpdateStatusAnswer";
        public const string DELETE_ANSWER_API = "api/Survey/DeleteAnswer";
        public const string IMPORTEXCEL_ANSWER_API = "api/Survey/ImportExcelAnswer/{0}";

        // School API
        public const string GET_ALL_SCHOOL_API = "api/Schools/GetAllSchools";
        public const string GET_ALL_SEARCH_API = "api/Schools/GetAllSearch";
        public const string GET_SCHOOL_BY_ID_API = "api/Schools/GetSchoolById/{0}";
        public const string ADD_NEW_SCHOOL_API = "api/Schools/AddNewSchool";
        public const string UPDATE_SCHOOL_API = "api/Schools/UpdateSchool";
        public const string UPDATE_SCHOOL_STATUS_API = "api/Schools/UpdateStatus";
        public const string DELETE_SCHOOL_API = "api/Schools/DeleteSchool";
        public const string IMPORT_SCHOOL_API = "api/Schools/ImportExcel";

        //Province API
        public const string GET_ALL_PROVINCE_API = "api/Provinces/GetAll";
        public const string GET_ALL_PROVINCE_SEARCH_PAGING_API = "api/Provinces/GetAllSearchPaging";
        public const string GET_PROVINCE_BY_ID_API = "api/Provinces/GetItemById/{0}";
        public const string INSERT_PROVINCE_API = "api/Provinces/Insert";
        public const string UPDATE_PROVINCE_API = "api/Provinces/Update";
        public const string UPDATESTATUS_PROVINCE_API = "api/Provinces/UpdateStatus";
        public const string DELETE_PROVINCE_API = "api/Provinces/Delete";
        public const string IMPORTEXCEL_PROVINCE_API = "api/Provinces/ImportExcel";


        //District API
        public const string GET_ALL_DISTRICT_API = "api/District/GetAll";
        public const string GET_ALL_DISTRICT_SEARCH_PAGING_API = "api/District/GetAllSearchPaging";
        public const string GET_DISTRICT_BY_ID_API = "api/District/GetItemById/{0}";
        public const string INSERT_DISTRICT_API = "api/District/Insert";
        public const string UPDATE_DISTRICT_API = "api/District/Update";
        public const string UPDATESTATUS_DISTRICT_API = "api/District/UpdateStatus";
        public const string DELETE_DISTRICT_API = "api/District/Delete";
        public const string IMPORTEXCEL_DISTRICT_API = "api/District/ImportExcel";

        // Country API
        public const string GET_ALL_COUNTRY_API = "api/Country/GetAllCountry";
        public const string GET_ALL_SEARCH_COUNTRY_API = "api/Country/GetAllSearch";
        public const string INSERT_COUNTRY_API = "api/Country/Insert";
        public const string UPDATE_COUNTRY_API = "api/Country/Update";
        public const string UPDATE_COUNTRY_STATUS_API = "api/Country/Update_Status";
        public const string GET_COUNTRYBYID_API = "api/Country/GetCountryByID/{0}";
        public const string DELETE_COUNTRY_API = "api/Country/Delete";
        public const string IMPORTEXCEL_COUNTRY_API = "api/Country/ImportExcel";

        // Dashboard Statistic
        public const string GET_ALL_STATISTICS = "api/Statistics/GetAll";


        // Articles API
        public const string GET_ALL_NEWS_ARTICLES_API = "api/Article/GetAllNewsArticles";
        public const string GET_ALL_NEWS_ARTICLETITLE_API = "api/Article/GetAllArticlesTitle";
        public const string GET_ALL_NEWS_ARTICLECREATEBY_API = "api/Article/GetAllArticlesCreateBy";
        public const string GET_ALL_NEWS_SEARCH_ARTICLE_API = "api/Article/GetAllSearch";
        public const string GET_NEWS_ARTICLE_BY_ID_API = "api/Article/GetArticleById/{0}";
        public const string ADD_NEW_NEWS_ARTICLE_API = "api/Article/InsertArticle";
        public const string ADD_NEW_LIST_NEWS_ARTICLE_API = "api/Article/InsertListArticle";
        public const string UPDATE_NEWS_ARTICLE_API = "api/Article/UpdateArticle";
        public const string UPDATE_NEWS_ARTICLE_STATUS_API = "api/Article/UpdateStatus";
        public const string DELETE_NEWS_ARTICLE_API = "api/Article/DeleteArticle";
        public const string PIN_AND_UN_PIN_NEWS_ARTICLE_API = "api/Article/PinAndUnPin";

        // Articles SHARE API
        public const string GET_ALL_SHARE_ARTICLES_API = "api/ArticleShare/GetAllArticles";
        public const string GET_ALL_SHARE_ARTICLETITLE_API = "api/ArticleShare/GetAllArticlesTitle";
        public const string GET_ALL_SHARE_ARTICLECREATEBY_API = "api/ArticleShare/GetAllArticlesCreateBy";
        public const string GET_ALL_SHARE_SEARCH_ARTICLE_API = "api/ArticleShare/GetAllSearch";
        public const string GET_SHARE_ARTICLE_BY_ID_API = "api/ArticleShare/GetArticleById/{0}";
        public const string ADD_SHARE_ARTICLE_API = "api/ArticleShare/InsertArticle";
        public const string ADD_SHARE_LIST_ARTICLE_API = "api/ArticleShare/InsertListArticle";
        public const string UPDATE_SHARE_ARTICLE_API = "api/ArticleShare/UpdateArticle";
        public const string UPDATE_SHARE_ARTICLE_STATUS_API = "api/ArticleShare/UpdateStatus";
        public const string DELETE_SHARE_ARTICLE_API = "api/ArticleShare/DeleteArticle";
        public const string PIN_AND_UN_PIN_SHARE_ARTICLE_API = "api/ArticleShare/PinAndUnPin";

        // Banners API
        public const string GET_ALL_BANNER_API = "api/Banners/GetAllBanners";
        public const string GET_ALL_SEARCH_BANNER_API = "api/Banners/GetAllSearch";
        public const string GET_BANNER_BY_ID_API = "api/Banners/GetBannerById/{0}";
        public const string ADD_NEW_BANNER_API = "api/Banners/InsertBanner";
        public const string IMPORT_BANNER_API = "api/Banners/ImportBanner";
        public const string UPDATE_BANNER_API = "api/Banners/UpdateBanner";
        public const string UPDATE_BANNER_STATUS_API = "api/Banners/UpdateStatus";
        public const string DELETE_BANNER_API = "api/Banners/DeleteBanner";


        //Branch
        public const string GET_PAGING_BRANCH_API = "api/Branch/GetPaging";
        //public const string GET_ALL_BRANCH_API = "api/Branch/GetAll";
        public const string GET_CREATE_USERS = "api/Branch/GetListCreateUserPaging";
        public const string GET_MODIFIED_USERS = "api/Branch/GetListModifiedUserPaging";
        public const string GET_BRANCH_API = "api/Branch/GetBranch/{0}";
        public const string INSERT_BRANCH_API = "api/Branch/InsertBranch";
        public const string UPDATE_BRANCH_API = "api/Branch/UpdateBranch";
        public const string DELETE_BRANCH_API = "api/Branch/DeleteBranch/{0}";
        public const string UPDATE_BRANCH_STATUS_API = "api/Branch/UpdateBranchStatus";
        public const string IMPORT_BRANCHS_FROM_EXCEL = "api/Branch/ImportFromExcel";

        //JobType
        public const string GET_PAGING_JOBTYPE_API = "/JobType/GetAllSearch";
        public const string GET_JOBTYPE_BY_ID_API = "/JobType/GetJobTypeById";
        public const string CREATE_JOBTYPE = "/JobType/CreateNewJobType";
        public const string UPDATE_JOBTYPE = "/JobType/UpdateJobType";
        public const string GET_ALL_JOB_TYPE = "/JobType/GetAllJobType";
        public const string UPDATE_JOB_TYPE_STATUS = "/JobType/UpdateJobTypeStatus";
        public const string DELETE_JOB_TYPE_BY_ID = "/JobType/DeleteJobTypeById";
        public const string IMPORT_JOB_TYPE = "/JobType/ImportExcel";

        //QuestionLevel
        public const string GET_PAGING_QUESTIONLEVEL_API = "/QuestionLevel/GetPaging";

        //Questionnaire
        public const string GET_ALL_QUESTIONNAIRE_API = "api/Question/GetAll";
        public const string GET_ALL_QUESTION_TYPE_API = "api/Question/GetAllQuestionType";
        public const string GET_ALL_QUESTIONNAIRE_SEARCH_API = "api/Question/Search";
        public const string GET_QUESTIONNAIRE_BY_ID_API = "api/Question/GetById/{0}";
        public const string ADD_NEW_QUESTIONNAIRE_API = "api/Question/AddNew";
        public const string UPDATE_QUESTIONNAIRE_API = "api/Question/Update";
        public const string UPDATE_STATUS_QUESTIONNAIRE_API = "api/Question/UpdateStatus";
        public const string DELETE_QUESTIONNAIRE_API = "api/Question/Delete";
        public const string IMPORT_QUESTIONNAIRE_API = "api/Question/ImportExcel";

        // Answer
        public const string GET_ALL_QANSWER_BY_QCODE_API = "api/Question/GetAllByQuestionnaireCode/{0}";
        public const string GET_ALL_QANSWER_BY_QCODE_SEARCH_API = "api/Question/SearchAnswer/{0}";
        public const string GET_QANSWER_BY_ID_API = "api/Question/GetByAnswerId/{0}";
        public const string ADD_NEW_QANSWER_API = "api/Question/AddNewAnswer";
        public const string UPDATE_QANSWER_API = "api/Question/UpdateAnswer";
        public const string UPDATE_STATUS_QANSWER_API = "api/Question/UpdateStatusAnswer";
        public const string DELETE_QANSWER_API = "api/Question/DeleteAnswer";
        public const string IMPORT_QANSWER_API = "api/Question/ImportAnswerExcel";

        //RecruitmentNews
        public const string GET_PAGING_RecruitmentNews_API = "/RecruitmentNews/GetAllSearch";
        public const string GET_RECRUITMENTNEW_BY_JOBCODE = "/RecruitmentNews/GetRecruitmentNewByJobCode";
        public const string GET_ALL_RECRUITMENTNEW_TITLE = "RecruitmentNews/GetALLRecruitmentNewsTitle";

        // Skills API
        public const string GET_ALL_SKILLS_API = "/api/Skills/GetAllSkills";
        public const string GET_ALL_SKILLS_SEARCH_API = "/api/Skills/GetAllSearch";
        public const string GET_SKILL_BY_ID_API = "/api/Skills/GetSkillByID/{0}";
        public const string INSERT_SKILLS_API = "/api/Skills/Insert";
        public const string UPDATE_SKILLS_API = "/api/Skills/Update";
        public const string DELETE_SKILL_API = "/api/Skills/Delete";
        public const string UPDATE_SKILLS_STATUS_API = "/api/Skills/Update_Status";
        public const string IMPORTEXCEL_SKILL_API = "/api/Skills/ImportExcel";

        // BranchContact 
        public const string GET_ALL_BRANCH_CONTACT_API = "api/BranchContact/GetAllBranchContact";
        public const string GET_ALL_BRANCH_CONTACT_SEARCH_API = "api/BranchContact/GetAllSearch";
        public const string BRANCH_CONTACT_EXPORT_EXCEL_API = "api/BranchContact/ExportExcel";
        public const string ADD_NEW_BRANCH_CONTACT_API = "api/BranchContact/AddNewBranchContact";
        public const string UPDATE_BRANCH_CONTACT_API = "api/BranchContact/UpdateBranchContact";
        public const string UPDATE_BRANCH_CONTACT_STATUS_API = "api/BranchContact/UpdateStatus";
        public const string GET_BRANCH_CONTACT_BY_ID_API = "api/BranchContact/GetBranchContactById/{0}";
        public const string DELETE_BRANCH_CONTACT_API = "api/BranchContact/Delete";
        public const string GET_BRANCH_CONTACT_BY_ID_API_V2 = "api/BranchContact/GetById/{0}";
        public const string GET_ALL_BRANCH_CONTACT_GETALLPROVINCES_API = "api/BranchContact/GetAllProvince";
        public const string ADD_NEW_LIST_BRANCH_CONTACT_API = "api/BranchContact/AddNewListBranchContact";
        public const string GET_ALL_BRANCH_CONTACT_EMAIL_API = "api/BranchContact/GetAllBranchContactEmail";
        public const string GET_ALL_BRANCH_CONTACT_NAME_API = "api/BranchContact/GetAllBranchContactName";
        public const string GET_ALL_BRANCH_CONTACT_CONTACT_NAME_API = "api/BranchContact/GetAllBranchContact_ContactName";

        //JobCategory API
        public const string GET_ALL_JOBCATEGORIES_API = "api/JobCategory/GetAllJobCategories";
        public const string GET_ALL_JOBCATEGORIES_SEARCH_API = "api/JobCategory/GetAllSearch";
        public const string GET_JOBCATEGORY_BY_ID_API = "api/JobCategory/GetJobCategoryById/{0}";
        public const string ADD_NEW_JOBCATEGORY_API = "api/JobCategory/AddNewJobCategory";
        public const string UPDATE_JOBCATEGORY_API = "api/JobCategory/UpdateJobCategory";
        public const string UPDATE_JOBCATEGORY__STATUS_API = "api/JobCategory/UpdateStatus";
        public const string DELETE_JOBCATEGORY_API = "api/JobCategory/DeleteJobCategory";
        public const string ADD_NEW_JOBCATEGORIES_LIST_API = "api/JobCategory/AddNewListJobCategory";

        //HashTags API
        public const string GET_ALL_HASHTAGS_API = "api/HashTags/GetAllHashTags";
        public const string ADD_NEW_HASHTAGS_API = "api/HashTags/AddnewHashTags";

        // Subject API
        public const string GET_ALL_SUBJECT_API = "api/Subjects/GetAllSubjects";
        public const string GET_ALL_SUBJECT_SEARCH_PAGING_API = "api/Subjects/GetAllSubjectsSearchPaging";
        public const string GET_SUBJECT_BY_ID_API = "api/Subjects/GetSubjectById/{0}";
        public const string ADD_NEW_SUBJECT_API = "api/Subjects/AddNewSubject";
        public const string UPDATE_SUBJECT_API = "api/Subjects/UpdateSubject";
        public const string UPDATE_SUBJECT_STATUS_API = "api/Subjects/UpdateStatus";
        public const string DELETE_SUBJECT_API = "api/Subjects/DeleteSubject";
        public const string IMPORT_SUBJECT_API = "api/Subjects/ImportSubjects";
        public const string EXPORT_SUBJECT_API = "api/Subjects/ExportSubjects";
        public const string IMPORTNEW_SUBJECT_API = "api/Subjects/ImportExcel";

        //ApplicantApply
        public const string GET_APPLICANT_PAGING = "ApplicantApply/GetAllSearch";
        public const string UPDATE_APPLICANT_STATUS = "ApplicantApply/UpdateStatus";
        public const string SET_INTERVIEW_SCHEDULE = "ApplicantApply/SetInterviewSchedule";
        public const string SET_MULTI_INTERVIEW_SCHEDULE = "ApplicantApply/SetMultiInterviewSchedule";
        public const string GET_INTERVIEW_INFORMATION = "ApplicantApply/GetInterviewInformation/{0}";
        public const string GET_APPLICANT_BY_ID = "ApplicantApply/GetApplicantById";
        public const string INSERT_INTERVIEW_REVIEW = "ApplicantApply/InsertApplicantReview";
        public const string GET_INTERVIEW_REVIEW = "ApplicantApply/GetApplicantReview";
        public const string GET_INTERVIEW_VIDEO = "ApplicantApply/GetAnswerVideo";
        //sonnt85 14/12/2018
        public const string GET_ALL_APPLICANTS_APPLIED_PAGING = "ApplicantApply/GetAllApplicantsAppliedPaging";
        public const string GET_ALL_APPLIED_POSITIONS_PAGING = "ApplicantApply/GetAllAppliedPositionsPaging";


        //Major API
        public const string GET_ALL_MAJORS_API = "api/major/GetAllMajors";
        public const string GET_ALL_MAJORS_SEARCH_API = "api/major/GetAllSearch";
        public const string GET_MAJOR_BY_ID_API = "api/major/GetMajorById/{0}";
        public const string ADD_NEW_MAJOR_API = "api/major/AddNewMajor";
        public const string UPDATE_MAJOR_API = "api/major/UpdateMajor";
        public const string UPDATE_STATUS_MAJOR_API = "api/major/UpdateMajorStatus";
        public const string DELETE_MAJOR_API = "api/major/DeleteMajor";
        public const string GET_MAJORS_BY_JOBCTEGORY_ID = "api/major/GetMajosrByJobCategoryId";
        public const string ADD_NEW_LIST_MAJOR_API = "api/major/AddNewListMajor";
        public const string IMPORT_EXCEL_MAJOR_API = "api/major/ImportExcel";

        // InterviewBlackList API
        public const string GET_ALL_INTERVIEWBLACKLIST_API = "api/InterviewBlackList/GetAllBlackList";
        public const string GET_ALL_INTERVIEWBLACKLIST_SEARCH_API = "api/InterviewBlackList/GetAllSearch";
        public const string GET_INTERVIEWBLACKLIST_BY_ID_API = "api/InterviewBlackList/GetBlackListById/{0}";
        public const string ADD_NEW_INTERVIEWBLACKLIST_API = "api/InterviewBlackList/AddNewBlackList";
        public const string UPDATE_INTERVIEWBLACKLIST_API = "api/InterviewBlackList/UpdateBlackList";
        public const string DELETE_INTERVIEWBLACKLIST_API = "api/InterviewBlackList/DeleteBlackList";
        public const string ADD_NEW_LIST_INTERVIEWBLACKLIST_API = "api/InterviewBlackList/AddNewListBlackList";

        // EmailHr API
        public const string GET_ALL_EMAILHR_API = "api/EmailHr/GetAllEmailHr";
        public const string GET_FIRST_EMAILHR_API = "api/EmailHr/GetFirstEmailHr";
        public const string GET_ALL_EMAILHR_SEARCH_API = "api/EmailHr/GetAllSearch";
        public const string GET_EMAILHR_BY_ID_API = "api/EmailHr/GetEmailHrById/{0}";
        public const string ADD_NEW_EMAILHR_API = "api/EmailHr/AddNewEmailHr";
        public const string GET_ALL_EMAILHR_EXPIRES = "api/EmailHr/GetAllEmailHrExpires";
        public const string UPDATE_EMAILHR_API = "api/EmailHr/UpdateEmailHr";
        public const string UPDATE_EMAILHR_STATUS_API = "api/EmailHr/UpdateEmailHrStatus";
        public const string DELETE_EMAILHR_API = "api/EmailHr/DeleteEmailHr";
        public const string UPDATE_EMAILHR_PASSWORD_API = "api/EmailHr/UpdateEmailHrPassword";

        // ReportOverall API
        public const string GET_ALL_REPORTOVERALL_API = "api/ReportOverall/ReportOverall_GetAll";
        public const string GET_ALL_REPORTOVERALL_SEARCH_PAGING_API = "api/ReportOverall/ReportOverall_GetSearchPaging";

        // ReportKPI API
        public const string GET_ALL_REPORTKPI_INTRO_SEARCH_PAGING_API = "api/ReportKPIIntro/GetSearchPaging";
        public const string GET_ALL_REPORTKPI_APPLICANTS_SEARCH_PAGING_API = "api/ReportKPIApplicants/GetSearchPaging";
        public const string GET_ALL_REPORTKPI_HUMANRESOURCE_SEARCH_PAGING_API = "api/ReportKPIHumanResource/GetSearchPaging";
        public const string GET_ALL_REPORTKPI_HUMANRESOURCE_EXPORT_DETAIL_API = "api/ReportKPIHumanResource/ExportDetail";
        public const string GET_ALL_REPORTKPI_RECRUITMENTNEWS_SEARCH_PAGING_API = "api/ReportKPIRecruitmentNews/GetSearchPaging";
        public const string GET_ALL_REPORTKPI_CLICKWEBSITE_SEARCH_PAGING_API = "api/ReportKpiWebsite/GetReportKpiWebsiteSearchPaging";
        public const string GET_ALL_REPORTKPI_Article_SEARCH_PAGING_API = "api/ReportKpiArticle/GetReportKpiArticleSearchPaging";


        // Introduction
        public const string GET_PAGING_INTRODUCTION_API = "api/Introduction/GetPaging";
        public const string GET_INTRODUCTION_BY_ID_API = "api/Introduction/GetIntroductionById";
        public const string ADDNEW_INTRODUCTION_API = "api/Introduction/AddNewIntroduction";
        public const string UPDATE_INTRODUCTION_API = "api/Introduction/UpdateIntroduction";
        public const string DELETE_INTRODUCTION_API = "api/Introduction/DeleteIntroduction";
        public const string GET_ALL_INTRODUCTION_API = "api/Introduction/GetAllIntroduction";
        public const string GET_ALL_INTRODUCTION_SEARCH_PAGING_API = "api/Introduction/GetAllIntroductionSearchPaging";
        public const string IMPORT_INTRODUCTION_API = "api/Introduction/ImportExcelIntroduction";
        public const string GET_INTRODUCTIONOUTSIDE_API = "api/Introduction/GetIntroductionOutside";
        public const string GET_INTRODUCTIONOUTSIDE_SEARCH_PAGING_API = "api/Introduction/GetIntroductionOutsideSearchPaging";
        public const string GET_INTRODUCTIONOUTSIDE_BY_ID_API = "api/Introduction/GetIntroductionOutsideById";
        public const string UPDATE_INTRODUCTIONOUTSIDE_API = "api/Introduction/UpdateIntroductionOutside";
        public const string UPDATE_INTRODUCTION_STATUS_API = "api/Introduction/UpdateStatus";
        public const string GET_ALL_INTRODUCTION_SEARCH_PAGINGINTROCOMPANY_API = "api/Introduction/GetAllIntroductionSearchPagingByIntroCompany";
        public const string GET_ALL_INTRODUCTION_SEARCH_PAGINGINTRODEPARTMENT_API = "api/Introduction/GetAllIntroductionSearchPagingByIntroDepartment";
        public const string GET_ALL_INTRODUCTION_SEARCH_PAGINGRECRUIMENTPROCESS_API = "api/Introduction/GetAllIntroductionSearchPagingByRecruitmentProcess";
        // Users API
        public const string GET_USER_INFO_API = "api/Users/GetUserInfo";

        // VideoCall API
        public const string CHECK_AUTHORIZE_VIDEO_CALL = "api/VideoCall/CheckAuthorize";
        public const string GET_STAFF_INTERVIEW_CALENDAR = "api/Interview/GetListInterviewOnline";
        public const string INSERT_VIDEO_FILE_NAME_P2P = "api/Interview/Insert";
        public const string UPDATE_INTERVIEW_STATUS_P2P = "api/Interview/UpdateInterviewStatus";

        // LiveChat API
        public const string GET_LIVE_CHAT_GROUP_BY_ID = "api/LiveChat/GetLiveChatGroupById/{0}";
        public const string GET_LIVE_CHAT_GROUP_PAGING = "api/LiveChat/GetLiveChatGroupPaging";
        //public const string GET_LIVE_CHAT_GROUP_BY_ID = "api/LiveChat/GetLiveChatGroupById";

        // JobPosition API
        public const string GET_ALL_JOBPOSITIONS_API = "api/JobPosition/GetAllJobPostions";
        public const string GET_ALL_JOBPOSITIONS_SEARCH_API = "api/JobPosition/GetAllSearch";
        public const string GET_JOBPOSITION_BY_ID_API = "api/JobPosition/GetSJobPositionById/{0}";
        public const string ADD_NEW_JOBPOSITION_API = "api/JobPosition/AddNewJobPosition";
        public const string UPDATE_JOBPOSITION_API = "api/JobPosition/UpdateJobPosition";
        public const string UPDATE_JOBPOSITION_STATUS_API = "api/JobPosition/UpdateJobPositionStatus";
        public const string DELETE_JOBPOSITION_API = "api/JobPosition/DeleteJobPosition";
        public const string ADD_NEW_LIST_JOBPOSITION_API = "api/JobPosition/AddNewListJobPosition";
        public const string IMPORT_EXCEL_JOBPOSITION_API = "api/JobPosition/ImportExcel";

        //QuestionnaireLevel API
        public const string GET_ALL_QUESTIONNAIRE_LEVELS_API = "api/QuestionnaireLevel/GetAllQuestionnaireLevels";
        public const string GET_ALL_QUESTIONNAIRE_LEVELS_SEARCH_API = "api/QuestionnaireLevel/GetAllSearch";
        public const string GET_QUESTIONNAIRE_LEVEL_BY_ID_API = "api/QuestionnaireLevel/GetSQuestionnaireLevelById/{0}";
        public const string ADD_NEW_JQUESTIONNAIRE_LEVEL_API = "api/QuestionnaireLevel/AddNewQuestionnaireLevel";
        public const string UPDATE_QUESTIONNAIRE_LEVEL_API = "api/QuestionnaireLevel/UpdateQuestionnaireLevel";
        public const string DELETE_QUESTIONNAIRE_LEVEL_API = "api/QuestionnaireLevel/DeleteQuestionnaireLevel";
        public const string UPDATE_QUESTIONAIRELEVL__STATUS_API = "api/QuestionnaireLevel/UpdateStatus";


        //FPT Person
        public const string GET_ALL_FPTPerson_API = "api/FPTPerson/GetAllFPTPerson";
        public const string GET_ALL_FPTPerson_SEARCH_PAGING_API = "api/FPTPerson/GetAllFPTPersonSearchPaging";
        public const string DELETE_FPTPerson_API = "api/FPTPerson/DeleteFPTPerson";
        public const string GET_FPTPerson_BY_ID_API = "api/FPTPerson/GetFPTPersonById";
        public const string ADDNEW_FPTPerson_API = "api/FPTPerson/AddNewFPTPerson";
        public const string UPDATE_FPTPerson_API = "api/FPTPerson/UpdateFPTPerson";
        public const string IMPORT_FPTPersons_API = "api/FPTPerson/ImportExcelFptPerson";
        public const string GET_ALL_SOLOGAN_API = "api/FPTPerson/GetAllSologan";
        public const string GET_ALL_SOLOGAN_SEARCH_PAGING_API = "api/FPTPerson/GetAllSologanSearchPaging";
        public const string GET_SOLOGAN_BY_ID_API = "api/FPTPerson/GetSologanById";
        public const string UPDATE_SOLOGAN_API = "api/FPTPerson/UpdateSologan";
        public const string UPDATE_FPTPerson_STATUS_API = "api/FPTPerson/UpdateStatus";

        //Applicant API
        public const string GET_ALL_APPLICANT_API = "api/Applicants/GetAllApplicants";
        public const string GET_ALL_APPLICANT_EMAIL_BY_JOBCATEGORYIDS = "api/Applicants/GetAllApplicantEmailByJobCategoryId";
        public const string GET_ALL_APPLICANT_BY_EMAIL = "api/Applicants/GetAllApplicantsByEmail";
        public const string GET_ALL_APPLICANT_SEARCH_API = "api/Applicants/GetAllSearch";
        public const string GET_ALL_APPLICANT_FULLNAME = "api/Applicants/GetAllFullName";
        public const string GET_ALL_APPLICANT_CAN_BO_TUYEN_DUNG = "api/Applicants/GetAllCreatedUser";
        public const string GET_ALL_APPLICANT_PHONE_NUMBERS = "api/Applicants/GetAllPhoneNumbers";

        // QuestionVideo API
        public const string GET_ALL_QUESTION_VIDEO_API = "api/QuestionVideo/GetAll";
        public const string GET_ALL_COLLECTION_API = "api/QuestionVideo/GetCollection";
        public const string GET_ALL_QUESTION_VIDEO_SEARCH_API = "api/QuestionVideo/Search";
        public const string GET_QUESTION_VIDEO_BY_ID_API = "api/QuestionVideo/GetById/{0}";
        public const string ADD_NEW_QUESTION_VIDEO_API = "api/QuestionVideo/AddNew";
        public const string UPDATE_QUESTION_VIDEO_API = "api/QuestionVideo/Update";
        public const string UPDATE_STATUS_QUESTION_VIDEO_API = "api/QuestionVideo/UpdateStatus";
        public const string DELETE_QUESTION_VIDEO_API = "api/QuestionVideo/Delete";
        public const string IMPORT_QUESTION_VIDEO_API = "api/QuestionVideo/ImportExcel";

        //JobLevel API
        public const string GET_ALL_JOBLEVEL_API = "api/JobLevel/GetAllJobLevel";
        public const string GET_ALL_JOBLEVEL_SEARCH_API = "api/JobLevel/GetAllSearch";
        public const string GET_JOBLEVEL_BY_ID_API = "api/JobLevel/GetJobLevelById/{0}";
        public const string ADD_NEW_JOBLEVEL_API = "api/JobLevel/AddNewJobLevel";
        public const string UPDATE_JOBLEVEL_API = "api/JobLevel/UpdateJobLevel";
        public const string UPDATE_JOBLEVEL__STATUS_API = "api/JobLevel/UpdateStatus";
        public const string DELETE_JOBLEVEL_API = "api/JobLevel/DeleteJobLevel";
        public const string ADD_NEW_JOBCLEVEL_LIST_API = "api/JobLevel/AddNewListJobLevel";
        public const string IMPORT_EXCEL_JOBLEVEL_API = "api/JobLevel/ImportExcel";

        // BranchHr API
        public const string GET_ALL_BRANCHHR_PAGING = "api/BranchHr/GetListBranchHrPaging";

        //Schedule
        public const string SCHEDULE_GET_MAILS = "Schedule/GetMails";
        public const string SCHEDULE_CHECK_OVERDATE = "Schedule/CheckOverdateInterviewCalendar";
        public const string SET_MULTI_Recruitment_SCHEDULE = "ApplicantApply/SetRecruitmentNewsSchedule";

        //ParameterConfig
        public const string GET_ALL_PARAMETERCONFIG_SEARCH_PAGING_API = "api/ParameterConfig/GetAllParameterConfigSearchPaging";
        public const string GET_PARAMETERCONFIG_BY_ID_API = "api/ParameterConfig/GetParameterConfigById";
        public const string UPDATE_PARAMETERCONFIG_API = "api/ParameterConfig/UpdateParameterConfig";

        //User
        public const string GET_ALL_USER_API = "api/Users/GetAllUser";

        //Notification
        public const string NEW_NOTIFY = "api/ApplicantNotify/NewNotify";

        //Test delete applicant apply
        public const string TEST_DELETE_APPLY = "api/Test/DeleteApply";
        
        //
        public const string GET_CONFIG_API = "api/ParameterConfig/GetConfig/{0}";
    }
}
