using FTEL.Common.BaseInfo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FTEL.Common.Utilities
{
    public static class ImageUtil
    {
        public const int FIXED_IMAGE_WIDTH = 500;

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static UploadFileResult ResizeImageAndSave(HttpPostedFileBase file, string keyGetDirectoryPath, string fileName, string[] fileTypesAllowed, string keyGetMaxFileSize)
        {
            string subFileName = DateTime.Now.Date.ToString().Replace("/", "") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            fileName = subFileName.Replace(" ", "").Replace(":", "") + fileName;
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
            Image img = Image.FromStream(file.InputStream, true, true);
            //var oldWidth = img.Width;
            //if (oldWidth > FIXED_IMAGE_WIDTH)
            //{
            //    var oldHeight = img.Height;
            //    var scale = (FIXED_IMAGE_WIDTH * 100) / oldWidth;
            //    var newHeight = oldHeight - ((oldHeight / 100) * scale);
            //    img = ResizeImage(img, FIXED_IMAGE_WIDTH, newHeight);
            //}
            //img = Crop(img, new Rectangle { Width = 300, Height = 300 });
            img.Save(Path.Combine(serverFilePath, fileName), GetImageFormat(fileType));
            //file.SaveAs(Path.Combine(serverFilePath, fileName));

            Result = new UploadFileResult(string.Empty, "FILE_UPLOADED", serverFilePath + "/" + fileName, filePath + "/" + fileName, fileName);
            return Result;
        }


        public static ImageFormat GetImageFormat(string fileType)
        {
            fileType = fileType.ToLower();
            switch (fileType)
            {
                case ".jpeg":
                    return ImageFormat.Jpeg;
                case ".jpg":
                    return ImageFormat.Jpeg;
                case ".bmp":
                    return ImageFormat.Bmp;
                case ".emf":
                    return ImageFormat.Emf;
                case ".exif":
                    return ImageFormat.Exif;
                case ".gif":
                    return ImageFormat.Gif;
                case ".icon":
                    return ImageFormat.Icon;
                case ".memorybmp":
                    return ImageFormat.MemoryBmp;
                case ".png":
                    return ImageFormat.Png;
                case ".tiff":
                    return ImageFormat.Tiff;
                case ".wmf":
                    return ImageFormat.Wmf;
                default:
                    return ImageFormat.Jpeg;
            }
        }

        /// <summary>
        /// Crops an image according to a selection rectangel
        /// </summary>
        /// <param name="image">
        /// the image to be cropped
        /// </param>
        /// <param name="selection">
        /// the selection
        /// </param>
        /// <returns>
        /// cropped image
        /// </returns>
        public static Image Crop(this Image image, Rectangle selection)
        {
            Bitmap bmp = image as Bitmap;

            // Check if it is a bitmap:
            if (bmp == null)
                throw new ArgumentException("No valid bitmap");

            // Crop the image:
            Bitmap cropBmp = bmp.Clone(selection, bmp.PixelFormat);

            // Release the resources:
            image.Dispose();

            return cropBmp;
        }

    }
}
