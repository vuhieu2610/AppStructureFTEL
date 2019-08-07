using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class ApiReturnResult<T>
    {
        #region Properties
        public List<Error> ErrorList { get; set; } = new List<Error>();
        public bool isSuccess { get; set; }
        public string ReturnMessage { get; set; }
        public string ErrorCode { get; set; }
        public List<T> DataList { get; set; } = new List<T>();
        public int TotalRecords { get; set; }
        public string JsonData { get; set; }
        #endregion

        #region Functions
        public bool HasError
        {
            get
            {
                return ErrorList.Count > 0;
            }
        }

        public bool HasData
        {
            get
            {
                if (DataList.Count > 0)
                {
                    return Constant.HAS_DATA;
                }
                else
                {
                    return Constant.HAS_NO_DATA;
                }
            }
        }
        
        public void Failed(params Error[] errors)
        {
            isSuccess = false;
            if(errors != null)
            {
                ErrorList.AddRange(errors);
            }
        }
        #endregion
    }
}
