using System;
using System.ComponentModel;

namespace FTEL.Common.Enums
{
    public enum TableIndex
    {
        [Description("Nhóm quyền")]
        Roles = 1,
        [Description("Menu")]
        Menu = 2,
        [Description("User")]
        User = 3,
        Majors = 4,
        Province = 5,
        RecruitmentNews = 6,
        EXT_LOG_ACTIONS = 7,
        RecruitmentSkills = 8,
        EXT_LOG_HISTORYS = 9,
        RoleJob = 10,
        RoleMenu = 11,
        RoleUser = 12,
        Schools = 13,
        SearchKeywords = 14,
        SelectedInterviewers = 15,
        SurveyAnswer = 16,
        SurveyQuestion = 17,
        SurveyResult = 18,
        SysParameters = 19,
        UserBranch = 20,
        InterviewerCalendars = 21,
        random_val_view = 22,
        ApplicantApplyRecruitments = 23,
        Applicants = 24,
        ApproveInterviewers = 25,
        Banners = 26,
        Branch = 27,
        Articles = 28,
        BranchAddress = 29,
        BranchHr = 30,
        InterviewerCalendarHistory = 31,
        Conversations = 32,
        CouncilMembers = 33,
        Country = 34,
        District = 35,
        EmailHR = 36,
        EmailsSend = 37,
        HashTags = 38,
        HR_ExamAnswerVideo = 39,
        HR_ExamResult = 40,
        BranchContacts = 41,
        ApplicantMajors = 42,
        HR_Exams = 43,
        EmailProvince = 44,
        BranchContactAddress = 45,
        HR_ExamStrucSubjectQuestionLevel = 46,
        sysdiagrams = 47,
        HR_ExamStructures = 48,
        LogAdminActions = 49,
        HR_ExamsVideo = 50,
        HR_ExamVideoResult = 51,
        HR_ExamVideoStructures = 52,
        HR_QuestionAnswer = 53,
        HR_Questionnaire = 54,
        HR_QuestionnaireLevel = 55,
        HR_QuestionType = 56,
        HR_QuestionVideo = 57,
        InterviewerBlackLists = 58,
        InterviewerCouncils = 59,
        InterviewerJob = 60,
        InterviewerReviews = 61,
        Introduction = 62,
        JobBranch = 63,
        JobCategory = 64,
        StatusHistory = 65,
        JobFollowInfo = 66,
        JobLevel = 67,
        JobPosition = 68,
        HR_Subjects = 69,
        JobType = 70,
        LiveChatUsers = 71,
        Skills = 72,
        LoginLogs = 73,
        LogUsers = 74

    }
    public class TableIndexInfo
    {
        public Int32 mVALUE { get; set; }
        public String Description { get; set; }
        public TableIndexInfo()
        {

        }
    }
}
