using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DXApplication3.Module.BusinessObjects;
using DXApplication3.Module.BusinessObjects.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication3.Module.DatabaseUpdate
{
    public class DatabaseDanhMucs
    {
        internal static void Init(IObjectSpace ObjectSpace)
        {
            //Kiểm tra danh mục rỗng thì đẩy danh mục mặc định
            var hasAnyDMKT = ObjectSpace.GetObjectsQuery<DanhMucKhoanThu>().Any();
            if (!hasAnyDMKT)
            {
                string[] danhMucKhoanThus = new string[] {
                    "Vận chuyển",
                    "Lạch Huyện",
                    "Lưu ca",
                    "Chọn vỏ",
                    "Thu Vận tải Khác",
                    "Tiền DV HQ",
                    "Kiểm hóa",
                    "SỬA/HỦY Tờ khai",
                    "Luồng đỏ",
                    "BB",
                    "Nộp k phơi",
                    "Thu Hải quan khác",
                    "BACKCOM"
                };

                foreach (var ten in danhMucKhoanThus)
                {
                    var dm = ObjectSpace.CreateObject<DanhMucKhoanThu>();
                    dm.TenKhoanThu = ten;
                    dm.MaKhoanThu = "KT" + Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
                }

                ObjectSpace.CommitChanges();
            }
            //Danh mục khoản chi
            //Kiểm tra danh mục rỗng thì đẩy danh mục mặc định
            var hasAnyDMKC = ObjectSpace.GetObjectsQuery<DanhMucKhoanChi>().Any();
            if (!hasAnyDMKC)
            {
                string[] danhMucKhoanChi = new string[] {
                    "Tiếp nhận",
                    "Bóc TK",
                    "Nộp TK/ Ký GS",
                    "Sửa/ Hủy Tờ khai",
                    "Lệ phí hải quan",
                    "Nộp k phơi",
                    "Hải quan ngoài giờ",
                    "Chi phí hải quan khác",
                    "BDNV",
                    "Luồng đỏ",
                    "BB, kẹp chì",
                    "Tiền đường",
                    "Tiền vé",
                    "Tiền dầu",
                    "Mooc 2",
                    "Quay đầu",
                    "Công làm chủ nhật",
                    "Lưu ca xe",
                    "Chọn vỏ",
                    "Chi phí vận tải khác",
                    "Phí hạ tầng",
                    "Lấy lệnh",
                    "Chi phí xử lý lô hàng",
                    "Lệnh/Lưu bãi",
                    "Chi hộ khác",
                    "Phí nâng",
                    "Phí hạ"
                };

                foreach (var ten in danhMucKhoanChi)
                {
                    var dm = ObjectSpace.CreateObject<DanhMucKhoanChi>();
                    dm.TenKhoanChi = ten;
                    dm.MaKhoanChi = "KC" + Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
                }

                ObjectSpace.CommitChanges();
            }
            //Kiểm tra danh mục loai dich vu
            var hasAnyLoaiDV = ObjectSpace.GetObjectsQuery<DanhMucDichVu>().Any();
            if (!hasAnyLoaiDV)
            {
                string[] danhMucLoaiDichVu = new string[] {
                    "Full dịch vụ",
                    "Dịch vụ Vận chuyển",
                    "Dịch vụ Hải Quan"
                };

                foreach (var ten in danhMucLoaiDichVu)
                {
                    var dm = ObjectSpace.CreateObject<DanhMucDichVu>();
                    dm.TenDichVu = ten;
                    dm.MaDichVu = "DV" + Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
                }

                ObjectSpace.CommitChanges();
            }
        }
    }
}
