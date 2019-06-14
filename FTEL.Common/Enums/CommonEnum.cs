using System.ComponentModel;
using System.Reflection;

namespace FTEL.Common.Enums
{
    public enum ExamType
    {
        KhongThi,
        CoThi
    }

    /// <summary>
    /// Dùng để tự động set value cho 1 thuộc tính theo description của thuộc tính 
    /// trong class enum (vi dụ StatusName = "Hoạt động" khi Status = 1)
    /// </summary>
    /// <author>AnhVDT5</author>
    public class MapEnum
    {
        public static string GetEnumDescription<T>(/*Enum*/T value)
        {
            try
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes != null && attributes.Length > 0)
                    return attributes[0].Description;
                else
                    return value.ToString();
            }
            catch
            {
                return value.ToString();
            }
        }
    }
}
