using FTEL.Common.Enums;
using FTEL.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEL.Common
{
    public class ResultInfo<T>
    {
        #region Properties
        public bool Result { get; set; } = false;
        public ResultMessageType MessageType { get; set; } = ResultMessageType.Message;
        public StringBuilder ListMessages { get; set; } = new StringBuilder();
        public int? TransInt { get; set; }
        public string ErrorCode { get; set; }
        public T Data { get; set; }
        public List<T> ListData { get; set; } = new List<T>();

        public bool HasData
        {
            get
            {
                if (Data == null)
                {
                    return Constants.NOT_HAS_DATA;
                }
                else
                {
                    return Constants.HAS_DATA;
                }
            }
        }

        public bool HasListData
        {
            get
            {
                if (ListData.Count == Constants.ZERO_RECORD)
                {
                    
                    return Constants.NOT_HAS_DATA;
                }
                else
                {
                    return Constants.HAS_DATA;
                }
            }
        }

        #endregion

        #region Constructors
        public ResultInfo()
        {

        }

        public ResultInfo(bool result, string message)
        {
            Result = result;
            if (!string.IsNullOrEmpty(message))
            {
                AddMessage(message);
            }
        }

        public ResultInfo(bool result, T data)
        {
            Result = result;
            Data = data;
        }

        public ResultInfo(bool result, List<T> listData)
        {
            Result = result;
            ListData = listData;
        }
        #endregion

        #region Public Methods
        public void AddMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ListMessages.Append(message);
            }
        }
        #endregion
        public string MsgAll
        {
            get
            {
                try
                {
                    if (ListMessages == null) return string.Empty;
                    return ListMessages.ToString();
                }
                catch (Exception ex)
                {
                    return "ResultInfor: " + ex.Message;
                }
            }
        }
    }
}
