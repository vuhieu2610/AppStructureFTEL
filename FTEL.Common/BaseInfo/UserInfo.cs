using System;

namespace FTEL.Common.BaseInfo
{
    [Serializable]
    public class UserInfo
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string TokenCode { get; set; }
        public string ClientToken { get; set; }
        //public string InterviewerCode { get; set; }
        public string Branchs { get; set; }
        public string Regions { get; set; }
        public int BrachId { get; set; }
        public int IsAdmin { get; set; }
        public bool IsLogin { get; set; }
    }
}
