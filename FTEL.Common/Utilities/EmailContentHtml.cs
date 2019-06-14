using System;

namespace FTEL.Common.Utilities
{
    public class EmailContentHtml
    {
        public static string EmailContentFormat(dynamic objData, string htmlFileName)
        {
            var result = "";
            var url = AppDomain.CurrentDomain.BaseDirectory + ConfigUtil.HtmlEmailDirectory + "/" + htmlFileName;
            var htmlContent = HttpUtilities.DownloadHtmlTemplate(url);           
            result = TiFormatter.TiFormat(htmlContent, objData);
            return result;
        }
    }
}
