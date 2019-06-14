using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEL.Common.Enums
{
    public enum TrangThaiUngVien
    {
        /// <summary>
        /// Trạng thái ứng viên sau khi ứng tuyển thành công với loại công việc không thi
        /// </summary>
        UngTuyenKhongThi,

        /// <summary>
        /// Trạng thái ứng viên sau khi ứng tuyển thành công với loại công việc có thi
        /// </summary>
        ChuaThi,

        /// <summary>
        /// Trạng thái ứng viên đang làm bài test trắc nghiệm
        /// </summary>
        DangThiTuyen,

        /// <summary>
        /// Trạng thái ứng viên đã thi đạt bài test trắc nghiệm
        /// </summary>
        ThiDat,

        /// <summary>
        /// Trạng thái ứng viên thi không đạt bài test trắc nghiệm
        /// </summary>
        ThiKhongDat,

        ///// <summary>
        ///// Trạng thái ứng viên hết thời gian tham gia phỏng vấn người máy (5 phút)
        ///// </summary>
        //HetThoiGianThamGiaPVNguoiMay,

        /// <summary>
        /// Trạng thái ứng viên không tham gia phỏng vấn người máy
        /// </summary>
        KhongThamGiaPVNguoiMay,

        /// <summary>
        /// Trạng thái ứng viên đang tham gia phỏng vấn người máy
        /// </summary>
        DangPVNguoiMay,

        /// <summary>
        /// Ứng viên đã tham gia phỏng vấn người máy
        /// </summary>
        DaPVNguoiMay,

        /// <summary>
        /// Trạng thái HR duyệt hồ sơ của ứng viên
        /// </summary>
        HRDuyetHoSo,

        /// <summary>
        /// Trạng thái chờ xếp lịch phỏng vấn online
        /// </summary>
        XepLichPVOnline,

        /// <summary>
        /// Trạng thái chờ xác nhận tham gia phỏng vấn online
        /// </summary>
        ChoXacNhanLichOnline,

        /// <summary>
        /// Ứng viên từ chối phỏng vấn online
        /// </summary>
        TuChoiPVOnline,

        /// <summary>
        /// Ứng viên chấp nhận phỏng vấn online
        /// </summary>
        ChapNhanPVOnline,

        /// <summary>
        /// Ứng viên hoãn phỏng vấn online
        /// </summary>
        HoanLichPVOnline,

        /// <summary>
        /// Ứng viên không tham gia phỏng vấn online
        /// </summary>
        KhongThamGiaPVOnline,

        /// <summary>
        /// Ứng viên đã tham gia phỏng vấn online
        /// </summary>
        DaPVOnline,

        /// <summary>
        /// Trạng thái chờ xếp lịch phỏng vấn trực tiếp
        /// </summary>
        XepLichPVTrucTiep,

        /// <summary>
        /// Trạng thái chờ xác nhận lịch phỏng vấn trực tiếp
        /// </summary>
        ChoXacNhanLich,

        /// <summary>
        /// Ứng viên từ chối phỏng vấn từ chối
        /// </summary>
        TuChoiPVTrucTiep,

        /// <summary>
        /// Ứng viên chấp nhận phỏng vấn trực tiếp
        /// </summary>
        ChapNhanPVTrucTiep,

        /// <summary>
        /// Trạng thái hoãn lịch phỏng vấn trực tiếp
        /// </summary>
        HoanLichPVTrucTiep,

        /// <summary>
        /// Trạng thái ứng viên không tham gia phỏng vấn trực tiếp
        /// </summary>
        KhongThamGiaPVTrucTiep,

        /// <summary>
        /// Trạng thái ứng viên đã tham gia phỏng vấn trực tiếp
        /// </summary>
        DaPVTrucTiep,

        /// <summary>
        /// Trạng thái chờ HR đánh giá kết quả của PV người máy, online, trực tiếp
        /// </summary>
        ChoDanhGiaKetQua,

        /// <summary>
        /// Trạng thái HR đánh giá ứng viên đạt
        /// </summary>
        UngVienDat,

        /// <summary>
        /// Trạng thái HR đánh giá ứng viên không đạt
        /// </summary>
        UngVienKhongDat,

        /// <summary>
        /// Trạng thái HR mời ứng viên nhận việc
        /// </summary>
        MoiNhanViec,

        /// <summary>
        /// Trạng thái ứng viên từ chối nhận việc
        /// </summary>
        TuChoiNhanViec,

        /// <summary>
        /// Trạng thái ứng viên nhận việc
        /// </summary>
        ChapNhanNhanViec,

        /// <summary>
        /// Trạng thái ứng viên nhận việc
        /// </summary>
        HoanNhanViec,

        /// <summary>
        /// Trạng thái ứng viên đang phỏng vấn Online
        /// </summary>
        DangPVOnline
    }
    //hinh thuc thi(Examtype)
    public enum HinhThucThi
    {
        /// <summary>
        /// Trạng thái ứng viên không thi
        /// </summary>
        KhongThi,

        /// <summary>
        /// Trạng thái ứng viên thi
        /// </summary>
        Thi
    }
    public enum ApplicantApplyReview
    {
        [Description("Phù hợp")]
        PhuHop,
        [Description("Không phù hợp")]
        KhongPhuHop,
        [Description("Cân nhắc")]
        CanNhac
    }
    public class ApplicantApplyStatus
    {
        /// <summary>
        /// Khởi tạo từ điển các trạng thái
        /// </summary>
        /// <returns>Từ điển trạng thái của ứng viên</returns>
        public static Dictionary<int, string> GetListStaus()
        {
            Dictionary<int, string> statusDic = new Dictionary<int, string>
            {

                // Trạng thái phỏng vấn người máy hiện lên cho ứng viên
                { (int)TrangThaiUngVien.ChuaThi, "Tham gia thi tuyển" },
                { (int)TrangThaiUngVien.DangThiTuyen, "Tham gia thi tuyển" },
                { (int)TrangThaiUngVien.ThiDat, "Tham gia phỏng vấn" },
                { (int)TrangThaiUngVien.ThiKhongDat, "Không đạt thi tuyển" },
                { (int)TrangThaiUngVien.KhongThamGiaPVNguoiMay, "Không tham gia phỏng vấn" },
                { (int)TrangThaiUngVien.DangPVNguoiMay, "Tham gia phỏng vấn" },
                { (int)TrangThaiUngVien.DaPVNguoiMay, "Chờ xét duyệt" },

                 // Trạng thái phỏng vấn người - người
                { (int)TrangThaiUngVien.UngTuyenKhongThi, "Chờ xét duyệt" },
                { (int)TrangThaiUngVien.HRDuyetHoSo, "Chờ xếp lịch phỏng vấn" },
                { (int)TrangThaiUngVien.ChoXacNhanLichOnline, "Chờ xác nhận lịch phỏng vấn online" },
                { (int)TrangThaiUngVien.TuChoiPVOnline, "Từ chối phỏng vấn online" },
                { (int)TrangThaiUngVien.ChapNhanPVOnline, "Chờ phỏng vấn online" },
                { (int)TrangThaiUngVien.HoanLichPVOnline, "Hoãn lịch phỏng vấn online" },
                { (int)TrangThaiUngVien.DaPVOnline, "Chờ kết quả phỏng vấn online" },
                { (int)TrangThaiUngVien.DangPVOnline, "Đang phỏng vấn online" },
                { (int)TrangThaiUngVien.KhongThamGiaPVOnline, "Không tham gia phỏng vấn online" },

                // Trạng thái phỏng vấn trực tiếp
                { (int)TrangThaiUngVien.XepLichPVTrucTiep, "Chờ xếp lịch phỏng vấn trực tiếp" },
                { (int)TrangThaiUngVien.ChoXacNhanLich, "Chờ xác nhận lịch phỏng vấn trực tiếp" },
                { (int)TrangThaiUngVien.TuChoiPVTrucTiep, "Từ chối phỏng vấn trực tiếp" },
                { (int)TrangThaiUngVien.HoanLichPVTrucTiep, "Hoãn phỏng vấn trực tiếp" },
                { (int)TrangThaiUngVien.ChapNhanPVTrucTiep, "Chờ phỏng vấn trực tiếp" },
                { (int)TrangThaiUngVien.DaPVTrucTiep, "Chờ kết quả phỏng vấn trực tiếp" },

                // Kết quả đánh gia HR
                { (int)TrangThaiUngVien.UngVienKhongDat, "Không đạt" },
                { (int)TrangThaiUngVien.UngVienDat, "Đạt" },

                // Nhận việc
                { (int)TrangThaiUngVien.MoiNhanViec, "Đã mời nhận việc" },
                { (int)TrangThaiUngVien.HoanNhanViec, "Hoãn nhận việc" },
                { (int)TrangThaiUngVien.ChapNhanNhanViec, "Đã nhận việc" },
                { (int)TrangThaiUngVien.TuChoiNhanViec, "Không nhận việc" }

            };

            return statusDic;
        }
        /// <summary>
        /// Lấy text của trạng thái ứng viên theo mã trạng thái
        /// </summary>
        /// <param name="status">Mã trạng thái</param>
        /// <returns>Text trạng thái</returns>
        public static Dictionary<int, string> GetExamtypeStatus()
        {
            Dictionary<int, string> statusDic = new Dictionary<int, string>
            {
                { (int)HinhThucThi.Thi, "Thi" },
                { (int)HinhThucThi.KhongThi, "Không thi" },

            };
            return statusDic;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetApplicantApplyReview()
        {
            Dictionary<int, string> reviewDic = new Dictionary<int, string>
            {
                { (int)ApplicantApplyReview.CanNhac, "Cân nhắc" },
                { (int)ApplicantApplyReview.KhongPhuHop, "Không phù hợp" },
                { (int)ApplicantApplyReview.PhuHop, "Phù hợp" }
            };
            return reviewDic;
        }
        public static string GetTextApplicantApplyReview(int status)
        {
            Dictionary<int, string> reviewDic = GetApplicantApplyReview();

            if (reviewDic.ContainsKey(status))
            {
                return reviewDic[status];
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Lấy text của trạng thái ứng viên theo mã trạng thái
        /// </summary>
        /// <param name="status">Mã trạng thái</param>
        /// <returns>Text trạng thái</returns>
        public static string GetTextStatus(int status)
        {
            Dictionary<int, string> statusDic = GetListStaus();

            if (statusDic.ContainsKey(status))
            {
                return statusDic[status];
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Lấy text của trạng thái ứng viên theo mã trạng thái
        /// </summary>
        /// <param name="status">Mã trạng thái</param>
        /// <returns>Text trạng thái</returns>
        public static string GetTextStatusHinhThucThi(int status)
        {
            Dictionary<int, string> statusDic = GetExamtypeStatus();

            if (statusDic.ContainsKey(status))
            {
                return statusDic[status];
            }
            else
            {
                return string.Empty;
            }
        }
        public static string DisplayButtonByStatus(int status)
        {
            switch (status)
            {
                case (int)TrangThaiUngVien.ChuaThi:
                case (int)TrangThaiUngVien.DangThiTuyen:
                    return "THI_TUYEN";

                case (int)TrangThaiUngVien.ThiDat:
                case (int)TrangThaiUngVien.DangPVNguoiMay:
                    return "PV_MAY";

                case (int)TrangThaiUngVien.ChoXacNhanLichOnline:
                case (int)TrangThaiUngVien.ChoXacNhanLich:
                    return "XAC_NHAN_LICH_PV";
                case (int)TrangThaiUngVien.ChapNhanPVOnline:
                    //case (int)TrangThaiUngVien.ChapNhanPVTrucTiep:
                    return "CHAP_NHAN_PV";
                case (int)TrangThaiUngVien.MoiNhanViec:
                    return "NHAN_VIEC";
                case (int)TrangThaiUngVien.DangPVOnline://Tuanvn4
                    return "DANG_PV_ONLINE";
                case (int)TrangThaiUngVien.TuChoiPVOnline://Tuanvn4
                    return "TU_CHOI_PV_ONLINE";
                default:
                    return string.Empty;
            }
        }
    }
}
