using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEL.Common.Utilities
{
    public class InsidePaging
    {
        public static void GetPagerSettings(int currentPage, int pageSize, out int rowStart, out int rowEnd)
        {
            var currPage = currentPage;
            var pSize = pageSize;
            if (currPage == 0)
                currPage = 1;
            var prevPage = currPage - 1;
            rowStart = ((prevPage * pSize) + 1);
            rowEnd = ((prevPage * pSize) + pSize);
        }
        //Nguon: http://www.mostefaiamine.com/post/2013/10/26/Creating-an-adaptive-pager-using-ASPNET-MVC-4-HTML-helpers-and-Twitter-Bootstrap-3
        public static string RenderPager(int recordNumber, int pageSize, int currentPage)
        {
            if (currentPage == 0)
                currentPage = 1;
            // calculate the number of pages
            var numberOfPages = recordNumber / pageSize;
            if (recordNumber % pageSize != 0)
                ++numberOfPages;
            if (numberOfPages < 2)
            {
                return string.Empty; //khong hien thi phan trang khi co 1 trang
            }
            // create an URL helper to generate urls
            //var urlHelper = new UrlHelper(html.ViewContext.RequestContext, html.RouteCollection);
            //var link = urlHelper.Action(actionName, controllerName);
            // create a string builder to generate HTML
            var builder = new StringBuilder();
            builder.Append("<ul class=\"pagination\">");
            // generate the previous "link"
            if (currentPage > 1)
            {
                AppendPagerTag(builder, currentPage - 1, currentPage, "&laquo;"); // Hiện icon trang trước khi trang hien tai > 1
            }

            // the first section contains the first pages
            IEnumerable<int> section1 = new[] { 1, 2, 3 }.ToList(); //Phien dau tien hien thi ...
            // the last section contains the last pages
            IEnumerable<int> section3 = new[] { numberOfPages - 2, numberOfPages - 1, numberOfPages }.ToList();// Phien cac trang cuoi
            // calculate the floating middle section. If the current page is in the middle, the floating section is a region that
            // contains the current page otherwise, it"s the region that contains the middle pages
            int middleStart;// Trang o giua
            if ((currentPage <= 2) || (currentPage >= numberOfPages - 1)) //Hien cac trang o giua khi o cac trang dau tien
            {
                middleStart = numberOfPages / 2;
                if (middleStart < 5)
                    middleStart = 5;
            }
            else
                if ((currentPage >= 3) && (currentPage < 6) && (currentPage < numberOfPages - 2))
                {
                    middleStart = 5;
                }
                else
                    middleStart = currentPage;
            var middle = new[] { middleStart - 1, middleStart, middleStart + 1 }; //Hien thi cac trang o giua
            // create the list of pages that are composed of the three sections and eventual separators that are represented by negative numbers (-99 and -98)
            IEnumerable<int> pages = section1; // Hien 3 trang o dau
            if (middle.First() > 4)
                pages = pages.Union(new[] { -98 }); //Hien "..." o dau
            pages = pages.Union(middle); // Hien 3 trang o giua
            if (middle.Last() < numberOfPages - 3)
                pages = pages.Union(new[] { -99 }); //Hien "..." o cuoi
            pages = pages.Union(section3); //Hien 3 trang o cuoi
            // filter the pages to take into account only the coherent pages by eliminating redundancies and illogical pages
            foreach (var page in pages.Where(e => (e <= numberOfPages && e > 0) || e == -99 || e == -98).Distinct())
            {
                if (page > 0)
                    AppendPagerTag(builder, page, currentPage); //Hien danh sach so trang
                else
                    AppendPagerTag(builder, page, currentPage, "..."); //Hien dau ...
            }
            // generate the next page if we are not in the last page
            if (currentPage < numberOfPages)
                AppendPagerTag(builder, currentPage + 1, currentPage, "&raquo;"); //Hien icon trang sau
            builder.AppendFormat("</ul>");
            return builder.ToString();
        }

        public static dynamic RenderPagerAjax(object totalCount, object pageSize, object pageIndex)
        {
            throw new NotImplementedException();
        }

        private static void AppendPagerTag(StringBuilder builder, int targetPage, int currentPage, string tagText = null)
        {
            // the link markup
            string linkTag;
            // the active css
            var activeCss = "";
            // the page text
            if (tagText == null)
                tagText = targetPage.ToString();
            // a positive value of targetPage points to a real page while a negative value points to a simple text (span)
            if (targetPage > 0)
            {
                // if the target page is the current page, then we"ll add the "active" class to the item
                if (targetPage == currentPage)
                    activeCss = "active";
                var link = CommonUtil.ReplaceQueryString("trang", targetPage.ToString());// urlHelper + targetPage;                
                // generate the link markup
                linkTag = string.Format("<a href=\"{1}\">{0}</a>", tagText, link);
            }
            else
            {
                // generates the separator markup
                linkTag = string.Format("<span>{0}</span>", tagText);
            }
            // embed the generated markup in a list item
            builder.AppendFormat("<li class=\"{1}\">{0}</li>", linkTag, activeCss);
        }

        public static string RenderPagerAjax(int recordNumber, int pageSize, int currentPage)
        {
            if (currentPage == 0)
                currentPage = 1;
            // calculate the number of pages
            if (pageSize == 0)
            {
                return string.Empty;
            }
            var numberOfPages = recordNumber / pageSize;
            if (recordNumber % pageSize != 0)
                ++numberOfPages;
            if (numberOfPages < 2)
            {
                return string.Empty; //khong hien thi phan trang khi co 1 trang
            }
            // create an URL helper to generate urls
            //var urlHelper = new UrlHelper(html.ViewContext.RequestContext, html.RouteCollection);
            //var link = urlHelper.Action(actionName, controllerName);
            // create a string builder to generate HTML
            var builder = new StringBuilder();
            builder.Append("<ul class=\"pagination\">");
            // generate the previous "link"
            if (currentPage > 1)
            {
                AppendPagerTagAjax(builder, currentPage - 1, currentPage, "&laquo;"); // Hiện icon trang trước khi trang hien tai > 1
            }

            // the first section contains the first pages
            IEnumerable<int> section1 = new[] { 1, 2, 3 }.ToList(); //Phien dau tien hien thi ...
            // the last section contains the last pages
            IEnumerable<int> section3 = new[] { numberOfPages - 2, numberOfPages - 1, numberOfPages }.ToList();// Phien cac trang cuoi
            // calculate the floating middle section. If the current page is in the middle, the floating section is a region that
            // contains the current page otherwise, it"s the region that contains the middle pages
            int middleStart;// Trang o giua
            if ((currentPage <= 2) || (currentPage >= numberOfPages - 1)) //Hien cac trang o giua khi o cac trang dau tien
            {
                middleStart = numberOfPages / 2;
                if (middleStart < 5)
                    middleStart = 5;
            }
            else
                if ((currentPage >= 3) && (currentPage < 6) && (currentPage < numberOfPages - 2))
                {
                    middleStart = 5;
                }
                else
                    middleStart = currentPage;
            var middle = new[] { middleStart - 1, middleStart, middleStart + 1 }; //Hien thi cac trang o giua
            // create the list of pages that are composed of the three sections and eventual separators that are represented by negative numbers (-99 and -98)
            IEnumerable<int> pages = section1; // Hien 3 trang o dau
            if (middle.First() > 4)
                pages = pages.Union(new[] { -98 }); //Hien "..." o dau
            pages = pages.Union(middle); // Hien 3 trang o giua
            if (middle.Last() < numberOfPages - 3)
                pages = pages.Union(new[] { -99 }); //Hien "..." o cuoi
            pages = pages.Union(section3); //Hien 3 trang o cuoi
            // filter the pages to take into account only the coherent pages by eliminating redundancies and illogical pages
            foreach (var page in pages.Where(e => (e <= numberOfPages && e > 0) || e == -99 || e == -98).Distinct())
            {
                if (page > 0)
                    AppendPagerTagAjax(builder, page, currentPage); //Hien danh sach so trang
                else
                    AppendPagerTagAjax(builder, page, currentPage, "..."); //Hien dau ...
            }
            // generate the next page if we are not in the last page
            if (currentPage < numberOfPages)
                AppendPagerTagAjax(builder, currentPage + 1, currentPage, "&raquo;"); //Hien icon trang sau
            builder.AppendFormat("</ul>");
            return builder.ToString();
        }

        private static void AppendPagerTagAjax(StringBuilder builder, int targetPage, int currentPage, string tagText = null)
        {
            // the link markup
            string linkTag;
            // the active css
            var activeCss = "";
            // the page text
            if (tagText == null)
                tagText = targetPage.ToString();
            // a positive value of targetPage points to a real page while a negative value points to a simple text (span)
            if (targetPage > 0)
            {
                // if the target page is the current page, then we"ll add the "active" class to the item
                if (targetPage == currentPage)
                    activeCss = "active";
                //var link = Common.ReplaceQueryString("trang", targetPage.ToString());// urlHelper + targetPage;                
                // generate the link markup
                linkTag = string.Format("<a data-id=\"" + targetPage + "\" href=\"#page-" + targetPage + "\">{0}</a>", tagText);
            }
            else
            {
                // generates the separator markup
                linkTag = string.Format("<span>{0}</span>", tagText);
            }
            // embed the generated markup in a list item
            builder.AppendFormat("<li class=\"{1}\">{0}</li>", linkTag, activeCss);
        }
    }
}
