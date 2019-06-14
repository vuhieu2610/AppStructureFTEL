using FTEL.Common.BaseInfo;
using FTEL.Common.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FTEL.Common.Utilities
{
    public class CommonUtil
    {
        public static Dictionary<string, string> GetTokenModel(
            string userName,
            string password,
            string grantType,
            string ip = null)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("username", userName);
            keyValues.Add("password", password);
            keyValues.Add("grant_type", grantType);
            keyValues.Add("ip", ip);
            return keyValues;
        }

        /// <summary>
        /// Upload file lên server - Tuanvn4
        /// </summary>
        /// <param name="file">File</param>
        /// <param name="keyGetDirectoryPath">Key để lấy đường dẫn thư mục từ webconfig</param>
        /// <param name="fileName">Tên file tự đặt - mặc định sẽ là tên file tải lên</param>
        /// <param name="fileTypesAllowed">Danh sách phần mở rộng của file được cho phép</param>
        /// <param name="maxFileSize">Key lấy file size tối đa đơn vị megabytes MB - mặc định là 20MB</param>
        /// <returns>Expando Object chứa mã lỗi, thông báo lỗi, đường dẫn của file sau khi lưu thành công</returns>
        public static UploadFileResult UploadVideoP2P(HttpPostedFileBase file, string interviewerCode, int fromWho)
        {
            try
            {
                //Lấy ra max file size config từ web.config
                int maxFileSize = int.Parse(ConfigUtil.GetConfigurationValueFromKey("P2PVideoCallMaxFileSize", false).Trim());

                //Tính toán max file size cho phép
                maxFileSize = (maxFileSize > 0)
                                ? maxFileSize * 1024 * 1024
                                : 20 * 1024 * 1024;

                //Khởi tạo kết quả trả về
                UploadFileResult Result = null;

                //Kiểm tra file tải lên có rỗng hay không
                if (file == null || file.ContentLength == 0)
                {
                    Result = new UploadFileResult("FIEMP", "FILE_IS_EMPTY", string.Empty, string.Empty, string.Empty);
                    return Result;
                }

                //Kiểm tra kiểu của file tải lên có phải webm hay không
                if (!file.ContentType.Contains("/webm"))
                {
                    Result = new UploadFileResult("FTINA", "FILE_TYPE_IS_NOT_ALLOWED", string.Empty, string.Empty, string.Empty);
                    return Result;
                }

                //Kiểm tra độ lớn của file
                if (file.ContentLength > maxFileSize)
                {
                    Result = new UploadFileResult("FITL", "FILE_IS_TOO_LARGE", string.Empty, string.Empty, string.Empty);
                    return Result;
                }

                string filePath = ConfigUtil.GetConfigurationValueFromKey("P2PVideoCall", false).Trim();

                //Kiểm tra xem có đường dẫn thư mục hay không
                if (string.IsNullOrEmpty(filePath))
                {
                    Result = new UploadFileResult("DDNE", "DIRECTORY_DOES_NOT_EXISTS", string.Empty, string.Empty, string.Empty);
                    return Result;
                }

                filePath += $"/interviewer_{interviewerCode}";

                string serverFilePath = HttpContext.Current.Server.MapPath(filePath);
                Directory.CreateDirectory(serverFilePath);

                string fileName = (fromWho == 0) ? "hrP2P_" : "interP2P_";
                fileName += DateTime.Now.ToString("yyyyMMddHHmmssffff");
                fileName += ".webm";

                file.SaveAs(Path.Combine(serverFilePath, fileName));

                Result = new UploadFileResult(string.Empty, "FILE_UPLOADED", serverFilePath + "/" + fileName, filePath + "/" + fileName, fileName);

                return Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Upload file lên server - Tuanvn4
        /// </summary>
        /// <param name="file">File</param>
        /// <param name="keyGetDirectoryPath">Key để lấy đường dẫn thư mục từ webconfig</param>
        /// <param name="fileName">Tên file tự đặt - mặc định sẽ là tên file tải lên</param>
        /// <param name="fileTypesAllowed">Danh sách phần mở rộng của file được cho phép</param>
        /// <param name="maxFileSize">Key lấy file size tối đa đơn vị megabytes MB - mặc định là 20MB</param>
        /// <returns>Expando Object chứa mã lỗi, thông báo lỗi, đường dẫn của file sau khi lưu thành công</returns>
        public static UploadFileResult UploadFile(HttpPostedFileBase file, string keyGetDirectoryPath, string fileName, string[] fileTypesAllowed, string keyGetMaxFileSize)
        {
            try
            {
                //Lấy ra max file size config từ web.config
                int maxFileSize = (!string.IsNullOrEmpty(keyGetMaxFileSize))
                                ? int.Parse(ConfigUtil.GetConfigurationValueFromKey(keyGetMaxFileSize.Trim(), false))
                                : 0;

                //Tính toán max file size cho phép
                maxFileSize = (maxFileSize > 0)
                                ? maxFileSize * 1024 * 1024
                                : 20 * 1024 * 1024;

                //Khởi tạo kết quả trả về
                UploadFileResult Result = null;

                //Kiểm tra file tải lên có rỗng hay không
                if (file == null || file.ContentLength == 0)
                {
                    Result = new UploadFileResult("FIEMP", "FILE_IS_EMPTY", string.Empty, string.Empty, string.Empty);
                    return Result;
                }

                //Lấy ra kiểu của file
                string fileType = Path.GetExtension(file.FileName).ToLower();
                //string[] fileTypes = { FileType.WORD_DOC, FileType.WORD_DOCX, FileType.JPEG, FileType.JPG, FileType.PDF, FileType.PNG };

                //Kiểm tra xem file có được cho phép hay không
                if (!Array.Exists(fileTypesAllowed, element => element == fileType))
                {
                    Result = new UploadFileResult("FTINA", "FILE_TYPE_IS_NOT_ALLOWED", string.Empty, string.Empty, string.Empty);
                    return Result;
                }

                //Kiểm tra độ lớn của file
                if (file.ContentLength > maxFileSize)
                {
                    Result = new UploadFileResult("FITL", "FILE_IS_TOO_LARGE", string.Empty, string.Empty, string.Empty);
                    return Result;
                }

                string filePath = (!string.IsNullOrEmpty(keyGetDirectoryPath))
                                ? ConfigUtil.GetConfigurationValueFromKey(keyGetDirectoryPath.Trim(), false).Trim()
                                : string.Empty;

                //Kiểm tra xem có đường dẫn thư mục hay không
                if (string.IsNullOrEmpty(filePath))
                {
                    Result = new UploadFileResult("DDNE", "DIRECTORY_DOES_NOT_EXISTS", string.Empty, string.Empty, string.Empty);
                    return Result;
                }

                string serverFilePath = HttpContext.Current.Server.MapPath(filePath);
                Directory.CreateDirectory(serverFilePath);

                fileName = (string.IsNullOrEmpty(fileName)) ? file.FileName.Trim() : fileName.Trim();

                file.SaveAs(Path.Combine(serverFilePath, fileName));

                Result = new UploadFileResult(string.Empty, "FILE_UPLOADED", serverFilePath + "/" + fileName, filePath + "/" + fileName, fileName);

                return Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Upload file lên server - Tuanvn4
        /// </summary>
        /// <param name="file">File</param>
        /// <param name="directoryPath">Đường dẫn thư mục</param>
        /// <param name="fileName">Tên file tự đặt - mặc định sẽ là tên file tải lên</param>
        /// <param name="fileTypesAllowed">Danh sách phần mở rộng của file được cho phép</param>
        /// <param name="maxFileSize">Key lấy file size tối đa đơn vị megabytes MB - mặc định là 20MB</param>
        /// <returns>Expando Object chứa mã lỗi, thông báo lỗi, đường dẫn của file sau khi lưu thành công</returns>
        public static UploadFileResult UploadFile(HttpPostedFileBase file, string directoryPath, string fileName, string[] fileTypesAllowed, int maxFileSize = 0)
        {
            try
            {
                //Tính toán max file size cho phép
                maxFileSize = (maxFileSize > 0)
                                ? maxFileSize * 1024 * 1024
                                : 20 * 1024 * 1024;

                //Khởi tạo kết quả trả về
                UploadFileResult Result = null;

                //Kiểm tra file tải lên có rỗng hay không
                if (file == null || file.ContentLength == 0)
                {
                    Result = new UploadFileResult("FIEMP", "FILE_IS_EMPTY", string.Empty, string.Empty, string.Empty);
                    return Result;
                }

                //Lấy ra kiểu của file
                string fileType = Path.GetExtension(file.FileName).ToLower();
                //string[] fileTypes = { FileType.WORD_DOC, FileType.WORD_DOCX, FileType.JPEG, FileType.JPG, FileType.PDF, FileType.PNG };

                //Kiểm tra xem file có được cho phép hay không
                if (!Array.Exists(fileTypesAllowed, element => element == fileType))
                {
                    Result = new UploadFileResult("FTINA", "FILE_TYPE_IS_NOT_ALLOWED", string.Empty, string.Empty, string.Empty);
                    return Result;
                }

                //Kiểm tra độ lớn của file
                if (file.ContentLength > maxFileSize)
                {
                    Result = new UploadFileResult("FITL", "FILE_IS_TOO_LARGE", string.Empty, string.Empty, string.Empty);
                    return Result;
                }

                string filePath = (!string.IsNullOrEmpty(directoryPath))
                                ? directoryPath.Trim()
                                : string.Empty;

                //Kiểm tra xem có đường dẫn thư mục hay không
                if (string.IsNullOrEmpty(filePath))
                {
                    Result = new UploadFileResult("DDNE", "DIRECTORY_DOES_NOT_EXISTS", string.Empty, string.Empty, string.Empty);
                    return Result;
                }

                string serverFilePath = HttpContext.Current.Server.MapPath(filePath);
                Directory.CreateDirectory(serverFilePath);

                fileName = (string.IsNullOrEmpty(fileName)) ? file.FileName.Trim() : fileName.Trim();

                file.SaveAs(Path.Combine(serverFilePath, fileName));

                Result = new UploadFileResult(string.Empty, "FILE_UPLOADED", serverFilePath + "/" + fileName, filePath + "/" + fileName, fileName);

                return Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Upload file to server
        /// </summary>
        /// <param name="file">File stream</param>
        /// <returns>Result (errorCode, filePath)</returns>
        public static dynamic UploadFile(HttpPostedFileBase file)
        {
            const int MAX_SIZE_FILE = 20485760; // 10MB
            dynamic Result = new ExpandoObject();
            try
            {
                if (file == null || file.ContentLength == 0)
                {
                    Result.ErrorCode = 1;
                    Result.ReturnMsg = "FILE_EMPTY";
                    Result.filePath = string.Empty;
                    return Result;
                }

                string fileType = Path.GetExtension(file.FileName).ToLower();
                string[] fileTypes = { FileType.WORD_DOC, FileType.WORD_DOCX, FileType.JPEG, FileType.JPG, FileType.PDF, FileType.PNG };
                if (!Array.Exists(fileTypes, element => element == fileType))
                {
                    Result.ErrorCode = 1;
                    Result.ReturnMsg = "FILE_TYPE_NOT_SUPPORT";
                    Result.filePath = string.Empty;
                    return Result;
                }

                if (file.ContentLength > MAX_SIZE_FILE)
                {
                    Result.ErrorCode = 1;
                    Result.ReturnMsg = "FILE_TOO_BIG";
                    Result.filePath = string.Empty;
                    return Result;
                }

                var dateNow = DateTime.Now;
                var dicPath = "/" + dateNow.Year + "/" + dateNow.Month + "/" + dateNow.Day;
                var path = HttpContext.Current.Server.MapPath(ConfigUtil.SyllDirectory + dicPath);
                var directory = new DirectoryInfo(path);
                if (directory.Exists == false)
                {
                    directory.Create();
                }

                var checkFileName = CleanUrl(file.FileName.Split('.')[0]);
                if (checkFileName.Length > 50)
                {
                    checkFileName = checkFileName.Substring(0, 45);
                }
                var fileName = dateNow.ToString("yyyyMMdd_HHmmss_") + RandomNumber(111, 999) + "_" + checkFileName + fileType;
                file.SaveAs(Path.Combine(path, fileName));

                Result.ErrorCode = 0;
                Result.ReturnMsg = string.Empty;
                Result.filePath = dicPath + "/" + fileName;

                return Result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Upload file CV Của ứng viên
        /// </summary>
        /// <param name="cvFile">CvFile</param>
        /// <param name="UserId">UserId</param>
        /// <returns></returns>
        public static dynamic UploadCVFile(HttpPostedFileBase file, int UserId = 0)
        {
            const int MAX_SIZE_FILE = 20 * 1024 * 1024; // 20 MB
            dynamic Result = new ExpandoObject();
            try
            {
                if (file == null || file.ContentLength == 0)
                {
                    Result.ErrorCode = 1;
                    Result.ReturnMsg = "FILE_EMPTY";
                    Result.filePath = string.Empty;
                    return Result;
                }

                string fileType = Path.GetExtension(file.FileName).ToLower();
                string[] fileTypes = { FileType.WORD_DOC, FileType.WORD_DOCX, FileType.JPEG, FileType.JPG, FileType.PDF, FileType.PNG };
                if (!Array.Exists(fileTypes, element => element == fileType))
                {
                    Result.ErrorCode = 1;
                    Result.ReturnMsg = "FILE_TYPE_NOT_SUPPORT";
                    Result.filePath = string.Empty;
                    return Result;
                }

                if (file.ContentLength > MAX_SIZE_FILE)
                {
                    Result.ErrorCode = 1;
                    Result.ReturnMsg = "FILE_TOO_BIG";
                    Result.filePath = string.Empty;
                    return Result;
                }

                var dateNow = DateTime.Now;
                var dicPath = "/CV/" + UserId;
                var path = HttpContext.Current.Server.MapPath(ConfigUtil.SyllDirectory + dicPath);
                var directory = new DirectoryInfo(path);
                if (directory.Exists == false)
                {
                    directory.Create();
                }

                //var checkFileName = CleanUrl(file.FileName.Split('.')[0]);
                //if (checkFileName.Length > 50)
                //{
                //    checkFileName = checkFileName.Substring(0, 45);
                //}
                var fileName = dateNow.ToString("yyyyMMdd_HHmmss_") + RandomNumber(1111, 9999) + "_" + UserId + fileType;
                file.SaveAs(Path.Combine(path, fileName));

                Result.ErrorCode = 0;
                Result.ReturnMsg = string.Empty;
                Result.filePath = dicPath + "/" + fileName;

                return Result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            //dynamic Result = new ExpandoObject();
            //try
            //{
            //    if (cvFile == null || cvFile.ContentLength == 0)
            //    {
            //        Result.ErrorCode = 1;
            //        Result.ReturnMsg = "FILE_EMPTY";
            //        Result.filePath = string.Empty;
            //        return Result;
            //    }
            //    if (cvFile.ContentLength > MAX_SIZE_FILE)
            //    {
            //        Result.ErrorCode = 1;
            //        Result.ReturnMsg = "FILE_TOO_BIG";
            //        Result.filePath = string.Empty;
            //        return Result;
            //    }
            //    if (UserId == 0)
            //    {
            //        Result.ErrorCode = 1;
            //        Result.ReturnMsg = "UserID Not Found";
            //        Result.filePath = string.Empty;
            //        return Result;
            //    }

            //    var dicPath = HttpContext.Current.Server.MapPath(ConfigUtil.SyllDirectory + "/CV/" + UserId);
            //    var directory = new DirectoryInfo(dicPath);
            //    if (!directory.Exists)
            //    {
            //        directory.Create();
            //    }
            //    cvFile.SaveAs(Path.Combine(dicPath, cvFile.FileName));

            //    Result.ErrorCode = 0;
            //    Result.ReturnMsg = string.Empty;
            //    Result.filePath = "/CV/" + UserId + "/" + cvFile.FileName;

            //    return Result;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        public static int RandomNumber(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }

        public static string CleanUrl(string value)
        {
            var result = "";
            if (string.IsNullOrEmpty(value)) return result;
            // replace hyphens to spaces, remove all leading and trailing whitespace
            value = value.Replace("-", " ").Trim().ToLower();
            // replace multiple whitespace to one hyphen
            value = Regex.Replace(value, @"[\s]+", "-");
            // replace umlauts and eszett with their equivalent
            value = value.Replace("ß", "ss");
            value = value.Replace("ä", "ae");
            value = value.Replace("ö", "oe");
            value = value.Replace("ü", "ue");
            // removes diacritic marks (often called accent marks) from characters
            value = RemoveDiacritics(value);
            // remove all left unwanted chars (white list)
            value = Regex.Replace(value, @"[^a-z0-9\s-]", String.Empty);
            result = value;
            return result;
        }

        public static string RemoveDiacritics(string value)
        {
            var normalized = value.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var c in normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
            {
                sb.Append(c);
            }
            var nonunicode = Encoding.GetEncoding(850);
            var unicode = Encoding.Unicode;
            var nonunicodeBytes = Encoding.Convert(unicode, nonunicode, unicode.GetBytes(sb.ToString()));
            var nonunicodeChars = new char[nonunicode.GetCharCount(nonunicodeBytes, 0, nonunicodeBytes.Length)];
            nonunicode.GetChars(nonunicodeBytes, 0, nonunicodeBytes.Length, nonunicodeChars, 0);
            return new string(nonunicodeChars);
        }

        public static string ReplaceQueryString(string key, string value)
        {
            var nUrl = "";
            try
            {
                var url = HttpContext.Current.Request.RawUrl;
                var uri = new Uri("http://" + HttpContext.Current.Request.Url.Host + url);
                var separateUrl = url.Split('?');
                var queryString = HttpUtility.ParseQueryString(uri.Query);
                queryString[key] = value; // Update Query
                nUrl = separateUrl[0] + "?" + queryString;
            }
            catch (Exception ex)
            {
                nUrl = HttpContext.Current.Request.RawUrl;
            }
            return HttpContext.Current.Server.UrlDecode(nUrl);
        }

        /// <summary>
        /// Tải lên logo chi nhánh đơn vị
        /// </summary>
        /// <param name="inputBase64Str"></param>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <param name="extension"></param>
        /// <param name="maxFileSize"></param>
        /// <returns></returns>
        public static UploadFileResult UploadBranchLogo(string inputBase64Str, string fileName, string filePath, ImageFormat extension, int maxFileSize = 0)
        {
            try
            {
                UploadFileResult result = null;

                if (string.IsNullOrEmpty(inputBase64Str) || string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(filePath)) return null;

                //Tính toán max file size cho phép
                maxFileSize = (maxFileSize > 0)
                                ? maxFileSize * 1024 * 1024
                                : 20 * 1024 * 1024;

                //Convert base64 to byte[]
                inputBase64Str = inputBase64Str.Trim().Substring(inputBase64Str.Trim().IndexOf(',') + 1);
                inputBase64Str = inputBase64Str.Trim('\0');

                string serverFilePath = string.Empty;
                bool uploadStatus = false;

                using (var ms = new MemoryStream(Convert.FromBase64String(inputBase64Str)))
                {
                    using (var bm = new Bitmap(ms))
                    {
                        serverFilePath = HttpContext.Current.Server.MapPath(filePath);

                        Directory.CreateDirectory(serverFilePath.Trim());

                        bm.Save(Path.Combine(serverFilePath, fileName), extension);

                        uploadStatus = true;

                        result = new UploadFileResult(string.Empty, "FILE_UPLOADED", serverFilePath + "/" + fileName, filePath + "/" + fileName, fileName);
                    }
                }

                if (uploadStatus)
                {
                    DirectoryInfo d = (!string.IsNullOrEmpty(serverFilePath)) ? new DirectoryInfo(@serverFilePath) : null;
                    FileInfo file = d.GetFiles(fileName)[0];

                    if (file.Length > maxFileSize)
                    {
                        file.Delete();
                        result = new UploadFileResult("FITL", "FILE_IS_TOO_LARGE", string.Empty, string.Empty, string.Empty);
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool DeleteFile(string directoryPath, string searchFilePattern)
        {
            try
            {
                if (string.IsNullOrEmpty(directoryPath) || string.IsNullOrEmpty(searchFilePattern)) return false;

                if (!File.Exists(directoryPath)) return false;

                directoryPath = HttpContext.Current.Server.MapPath(directoryPath.Trim());

                DirectoryInfo d = new DirectoryInfo(@directoryPath);
                FileInfo file = d.GetFiles(searchFilePattern.Trim())[0];

                file.Delete();

                return true;
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.Message, ex);
                return false;
            }
        }

        public static List<ErrorObject> GetValidateError(ModelStateDictionary modelStateError)
        {
            List<ErrorObject> errorList = new List<ErrorObject>();

            // Lỗi validate dữ liệu
            foreach (string key in modelStateError.Keys)
            {
                ModelState current = modelStateError[key];
                foreach (ModelError error in current.Errors)
                {
                    errorList.Add(new ErrorObject()
                    {
                        Code = key,
                        Description = error.ErrorMessage
                    });
                }
            }

            return errorList;
        }


        public static string GetPlainTextFromHtml(string htmlString)
        {
            htmlString = HttpUtility.HtmlDecode(htmlString).ToString();
            string htmlTagPattern = "<.*?>";
            var regexCss = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            htmlString = regexCss.Replace(htmlString, string.Empty);
            htmlString = Regex.Replace(htmlString, htmlTagPattern, string.Empty);
            htmlString = Regex.Replace(htmlString, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
            htmlString = htmlString.Replace("&nbsp;", string.Empty);

            return htmlString;
        }
    }
}
