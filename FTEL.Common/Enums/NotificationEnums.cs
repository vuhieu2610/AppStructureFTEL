using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEL.Common.Enums
{
    public enum NotificationEnums
    {
        BinhThuong,
        TinTuyenDungPhuHopHoacTheoDoi,
        TrangThaiThiTuyen,
        SEO
    }

    public class NotificationType
    {
        public static Dictionary<int, string> NotificationTypeDictionary()
        {
            return new Dictionary<int, string>()
            {
                {(int)NotificationEnums.BinhThuong, string.Empty},
                {(int)NotificationEnums.TinTuyenDungPhuHopHoacTheoDoi, "Có <span class='c-blue'>1</span> công việc phù hợp với bạn"},
                {(int)NotificationEnums.TrangThaiThiTuyen, string.Empty},
                {(int)NotificationEnums.SEO, string.Empty}
            };
        }
    }
}