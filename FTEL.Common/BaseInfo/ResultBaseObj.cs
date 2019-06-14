using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEL.Common
{
    public class ResultBaseObj
    {
        public int ResultInt { get; set; }
        public string ResultMsg { get; set; }
        public object ResultObj { get; set; }
    }
    public class ResultInfor
    {
        public enum ResultMessageType
        {
            Message = 0,
            Warrning = 1
        }

        public Boolean rtResult { get; set; } = false;

        public ResultMessageType MsgType { get; set; } = ResultMessageType.Message;

        public StringBuilder vListMSG { get; set; } = new StringBuilder();

        public Int64? TransInt { get; set; }
        //public Int64? rows { get; set; }

        public string ERR_CODE { get; set; }

        public string ERR_CODE2
        {
            set
            {
                ERR_CODE = value;
                if (!string.IsNullOrEmpty(value) && value != "0")
                    rtResult = false;
                else
                    rtResult = true;
            }
        }

        public ResultInfor()
        {
            ERR_CODE = string.Empty;
        }

        public ResultInfor(bool iResult, string iMsg)
        {
            rtResult = iResult;
            if (iMsg != "")
                AddMSG(iMsg);
        }

        public void AddMSG(string iMSG)
        {
            try
            {
                if (string.IsNullOrEmpty(iMSG)) return;
                if (vListMSG == null) vListMSG = new StringBuilder();
                vListMSG.AppendLine(iMSG);
            }
            catch
            {
            }
        }

        public string AddMSG2
        {
            set
            {
                AddMSG(value);
            }
        }

        public string MsgAll
        {
            get
            {
                try
                {
                    if (vListMSG == null) return string.Empty;
                    return vListMSG.ToString();
                }
                catch (Exception ex)
                {
                    return "ResultInfor: " + ex.Message;
                }
            }
        }
        ////public string MsgAll2(string in_PathXML = "", string in_Check = @"[(")
        ////{
        ////    try
        ////    {
        ////        if (!HasMulLanguage(in_Check)) return MsgAll;
        ////        return vListMSG.ToString();
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        return "ResultInfor: " + ex.Message;
        ////    }
        ////}

        public bool HasMsg
        {
            get
            {
                if (vListMSG == null || string.IsNullOrEmpty( vListMSG.ToString().Trim()))
                    return false;
                return true;
            }
        }
        public bool HasMulLanguage(string in_Check = @"[(")
        {
            return (vListMSG != null && vListMSG.ToString().Contains(@"[("));
        }


        public ResultInfor GetToNewResultInfo()
        {
            return new ResultInfor()
            {
                rtResult = this.rtResult,
                MsgType = this.MsgType,
                ERR_CODE = this.ERR_CODE,
                TransInt = (this.TransInt.HasValue ? this.TransInt : default(long?)),
                //rows = (this.rows.HasValue ? this.rows: default(long?)),
                AddMSG2 = this.MsgAll
            };
        }

        public string EmailMsg { get; set; }

        /// <summary>
        /// Nối chuỗi thông báo EmailMsg
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public void AddEmailMsg(string message)
        {
            EmailMsg = (string.IsNullOrEmpty(EmailMsg)) ? message : EmailMsg + message;
        }

        /// <summary>
        /// Kiểm tra xem có thông báo email hay không
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool HasEmailMsg
        {
            get
            {
                return string.IsNullOrEmpty(EmailMsg);
            }
        }


    }
    public class ResultInfor<T> : ResultInfor
    {
        public T RtData { get; set; }
        public List<T> ListData { get; set; }
        public ResultInfor() : base()
        { }
        public ResultInfor(bool iResult, string iMsg) : this()
        {
            rtResult = iResult;
            if (iMsg != "") AddMSG(iMsg);
        }
        public ResultInfor(bool iResult, T irtData) : this()
        {
            rtResult = iResult;
            RtData = irtData;
        }
        public ResultInfor(bool iResult, List<T> irtData) : this()
        {
            rtResult = iResult;
            ListData = irtData;
        }

        public bool HasData
        {
            get
            {
                if (RtData == null) return false;
                return true;
            }
        }
        public bool HasListData
        {
            get
            {
                if (ListData == null || ListData.Count == 0)
                    return false;
                return true;
            }
        }
        public List<T> ListDataDefault()
        {
            if (ListData == null) return new List<T>();
            else
                return ListData;
        }
        public ResultInfor<T2> GetToNewResultInfo<T2>()
        {
            return new ResultInfor<T2>()
            {
                rtResult = this.rtResult,
                MsgType = this.MsgType,
                ERR_CODE = this.ERR_CODE,
                TransInt = (this.TransInt.HasValue ? this.TransInt : default(long?)),
                //rows = (this.rows.HasValue ? this.rows: default(long?)),
                AddMSG2 = this.MsgAll
            };
        }

    }

    public class rsMessages
    {
        public const string ProcessAbort = "ProcessAbort";
        public const string NotFoundObject = "Không tìm thấy dữ liệu";
    }
}
