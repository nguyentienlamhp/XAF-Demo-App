using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DXApplication3.Module.BusinessObjects.DanhMuc;
using DXApplication3.Module.BusinessObjects.DonHangs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication3.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class ChiTietKhoanThu
    {
        [Key]
        public virtual Guid ID { get; set; }

        [XafDisplayName("Đơn hàng")]
        public virtual Guid DonHangID { get; set; }

        [ForeignKey(nameof(DonHangID))]
        public virtual DonHang DonHang { get; set; }

        [XafDisplayName("Khoản thu gốc")]
        public virtual Guid DanhMucKhoanThuID { get; set; }

        [ForeignKey(nameof(DanhMucKhoanThuID))]
        [XafDisplayName("Danh mục khoản thu")]
        //public virtual DanhMucKhoanThu DanhMucKhoanThu { get; set; }

        //Tu dong lay gia tri khi chon danh muc khoan thu
        private DanhMucKhoanThu _danhMucKhoanThu;
        [ImmediatePostData]
        public virtual DanhMucKhoanThu DanhMucKhoanThu
        {
            get => _danhMucKhoanThu;
            set
            {
                _danhMucKhoanThu = value;
                if (value != null)
                {
                    TenKhoanThu = value.TenKhoanThu;
                    SoTien = value.SoTien;
                }
            }
        }

        [XafDisplayName("Tên khoản thu")]
        public virtual string TenKhoanThu { get; set; }

        [XafDisplayName("Số tiền")]
        public virtual decimal SoTien { get; set; }

        // Khi chọn danh mục khoản thu → tự động copy thông tin
        [Action(Caption = "Lấy từ danh mục")]
        public void CapNhatTuDanhMuc()
        {
            if (DanhMucKhoanThu != null)
            {
                TenKhoanThu = DanhMucKhoanThu.TenKhoanThu;
                SoTien = DanhMucKhoanThu.SoTien;
            }
        }
    }
}
