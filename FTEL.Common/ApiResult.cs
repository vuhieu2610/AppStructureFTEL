using FTEL.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEL.Common
{
    public class ApiResult<T>
    {
        #region Properties
        public List<ErrorObject> Errors { get; set; } = new List<ErrorObject>();
        public bool Succeeded { get; set; } = true;
        public int TotalRecords { get; set; }
        public string ReturnMesage { get; set; }
        public T Data { get; set; }
        public List<T> DataList { get; set; } = new List<T>();
        public List<T> DataList2 { get; set; } = new List<T>();
        public int OrderNumber { get; set; } = 1; //Để tính toán và hiển thị số thứ tự trong grid (thường sẽ gán từ BaseCondition.FromRecord)
        public string DataJson { set; get; }
        #endregion

        #region Functions
        //TuanVN4
        public bool HasError
        {
            get
            {
                return Errors.Count > 0;
            }
        }

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

        public bool HasDataList
        {
            get
            {
                if (DataList.Count == Constants.ZERO_RECORD)
                {

                    return Constants.NOT_HAS_DATA;
                }
                else
                {
                    return Constants.HAS_DATA;
                }
            }
        }

        public void Failed(params ErrorObject[] errors)
        {
            Succeeded = false;
            if (errors != null)
            {
                Errors.AddRange(errors);
            }
        }

        #endregion
    }
}
