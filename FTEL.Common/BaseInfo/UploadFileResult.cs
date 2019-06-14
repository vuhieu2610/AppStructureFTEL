using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEL.Common.BaseInfo
{
    public class UploadFileResult
    {
        //Properties
        public string ErrorCode { get; }
        public string ErrorMessage { get; }
        public string ServerFolderPath { get; }
        public string FilePath { get; }
        public string FileName { get; }

        //Methods
        public bool HasError
        {
            get
            {
                return !string.IsNullOrEmpty(ErrorCode);
            }
        }

        public UploadFileResult (string errorCode, string errorMessage, string serverFilePath, string filePath, string fileName)
        {
            ErrorCode = (string.IsNullOrEmpty(errorCode)) ? string.Empty : errorCode;
            ErrorMessage = (string.IsNullOrEmpty(errorMessage)) ? string.Empty : errorMessage;
            ServerFolderPath = (string.IsNullOrEmpty(serverFilePath)) ? string.Empty : serverFilePath;
            FilePath = (string.IsNullOrEmpty(filePath)) ? string.Empty : filePath;
            FileName = (string.IsNullOrEmpty(fileName)) ? string.Empty : fileName;
        }
    }
}
