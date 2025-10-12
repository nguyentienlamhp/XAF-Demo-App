using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication3.Module.NonPersistent
{
    [DomainComponent] // Không lưu vào DB
    [DefaultClassOptions]
    [ImageName("BO_Invoice")]
    [NavigationItem("Thống kê")]
    [XafDisplayName("Thống kê kinh doanh")]
    public class ThongKeKinhDoanh
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();

        [Browsable(false)]
        public Guid DonHangId { get; set; }

        public string MaDon { get; set; }
        public string TenKhachHang { get; set; }
        public string TenKhoanThu { get; set; }
        public decimal SoTien { get; set; }
        public decimal TongSoTien { get; set; } // ✅ cột mới
    }
}
