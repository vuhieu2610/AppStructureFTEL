using FTEL.Common.Base;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FTEL.Common.Utilities
{
    public class XmlUtil
    {
        public List<XElement> ReadXmlFile(string filePath)
        {
            List<XElement> xmlContent = new List<XElement>();
            try
            {
                XDocument xdoc = XDocument.Load(filePath);
                xmlContent.AddRange(xdoc.Elements());
            }
            catch (Exception e)
            {
                LogUtil.Error($"filePath: {filePath}", e);
            }

            return xmlContent;
        }
    }
}
