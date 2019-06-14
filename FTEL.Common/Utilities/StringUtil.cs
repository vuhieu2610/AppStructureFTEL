using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEL.Common.Utilities
{
    public class MapUtils
    {
        public static string GetDescription<T>(string fieldName)
        {
            try
            {
                ////var a = TypeDescriptor.GetProperties(typeof(T));
                ////AttributeCollection attributes = TypeDescriptor.GetProperties(typeof(T))[fieldName].Attributes;
                ////DescriptionAttribute myAttribute = (DescriptionAttribute)attributes[typeof(DescriptionAttribute)]; 

                PropertyDescriptor pro = TypeDescriptor.GetProperties(typeof(T))[fieldName];
                if (pro != null)
                    return pro.Description;
                else
                    return string.Empty;
            }
            catch { return string.Empty; }
        }
        public static string GetDescription(Type tp, string fieldName)
        {
            try
            {
                ////var a = TypeDescriptor.GetProperties(typeof(T));
                ////AttributeCollection attributes = TypeDescriptor.GetProperties(typeof(T))[fieldName].Attributes;
                ////DescriptionAttribute myAttribute = (DescriptionAttribute)attributes[typeof(DescriptionAttribute)]; 

                PropertyDescriptor pro = TypeDescriptor.GetProperties(tp)[fieldName];
                if (pro != null)
                    return pro.Description;
                else
                    return string.Empty;
            }
            catch { return string.Empty; }
        }
    }

    public class StringUtil
    {
        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] {
                "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ",
                "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ", "đ", "é", "è",
                "ẻ", "ẽ", "ẹ", "ê", "ế", "ề", "ể", "ễ", "ệ", "í",
                "ì", "ỉ", "ĩ", "ị", "ó", "ò", "ỏ", "õ", "ọ", "ô",
                "ố", "ồ", "ổ", "ỗ", "ộ", "ơ", "ớ", "ờ", "ở", "ỡ",
                "ợ", "ú", "ù", "ủ", "ũ", "ụ", "ư", "ứ", "ừ", "ử",
                "ữ", "ự", "ý", "ỳ", "ỷ", "ỹ", "ỵ"};

            string[] arr2 = new string[] {
                "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                "a", "a", "a", "a", "a", "a", "a", "d", "e", "e",
                "e", "e", "e", "e", "e", "e", "e", "e", "e", "i",
                "i", "i", "i", "i", "o", "o", "o", "o", "o", "o",
                "o", "o", "o", "o", "o", "o", "o", "o", "o", "o",
                "o", "u", "u", "u", "u", "u", "u", "u", "u", "u",
                "u", "u", "y", "y", "y", "y", "y"};

            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
    }
}
