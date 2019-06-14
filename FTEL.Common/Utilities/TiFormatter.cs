using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace FTEL.Common.Utilities
{
    public class TiFormatter
    {
        //var person = new { FirstName = "rune", LastName = "grimstad" };
        //string template = "<div><div><h1>Hello {FirstName} {LastName}</h1></div></div>";
        //string html = TiFormatter.TiFormat(template, person);
        public static string TiFormat(string format, object source)
        {
            try
            {
                return FormatWith(format, null, source);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.Message, ex);
                return "";
            }
        }
        public static string FormatWith(string format, IFormatProvider provider, object source)
        {
            if (format == null)
                throw new ArgumentNullException("format");
            var values = new List<object>();
            var rewrittenFormat = Regex.Replace(format,
                @"(?<start>\{)+(?<property>[\w\.\[\]]+)(?<format>:[^}]+)?(?<end>\})+",
                delegate(Match m)
                {
                    var startGroup = m.Groups["start"];
                    var propertyGroup = m.Groups["property"];
                    var formatGroup = m.Groups["format"];
                    var endGroup = m.Groups["end"];
                    values.Add((propertyGroup.Value == "0")
                      ? source
                      : Eval(source, propertyGroup.Value));
                    var openings = startGroup.Captures.Count;
                    var closings = endGroup.Captures.Count;
                    return openings > closings || openings % 2 == 0
                         ? m.Value
                         : new string('{', openings) + (values.Count - 1) + formatGroup.Value
                           + new string('}', closings);
                },
                RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
            return string.Format(provider, rewrittenFormat, values.ToArray());
        }
        private static object Eval(object source, string expression)
        {
            try
            {
                return DataBinder.Eval(source, expression);
            }
            catch (HttpException e)
            {
                throw new FormatException(null, e);
            }
        }
    }
}
