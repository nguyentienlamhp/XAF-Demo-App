using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraGauges.Core.Base;
using DevExpress.ExpressApp.Model;
using System.Collections.ObjectModel;
using DevExpress.Xpo;
using System.ComponentModel;
using DXApplication3.Module.BusinessObjects.DanhMuc;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Persistent.Validation;

namespace DXApplication3.Module.BusinessObjects.DonHangs
{
    [DefaultClassOptions]
    [NavigationItem("Đơn hàng")]
    [XafDisplayName("Đơn hàng")]
    [DefaultProperty(nameof(TenDonHang))] // 👈 Trường mặc định đại diện cho class
    public class DonHang : BaseObject, IXafEntityObject
    {
        //public DonHang() { }

        [Key]
        public virtual Guid ID { get; set; }
        public virtual string MaDonHang { get; set; }

        //Yêu cầu phải chọn
        [System.ComponentModel.DataAnnotations.Required]
        [RuleRequiredField(DefaultContexts.Save)]
        public virtual DanhMucDichVu LoaiDichVu { get; set; }

        //Hiển thị khi chọn ID đơn hàng
        public virtual string TenDonHang { get; set; }
        [XafDisplayName("Ngày tạo")]
        [ModelDefault("DisplayFormat", "{0:dd/MM/yyyy HH:mm:ss}")]
        [ModelDefault("EditMask", "dd/MM/yyyy HH:mm:ss")]
        public virtual DateTime NgayTao { get; set; } = DateTime.Now;
        //public virtual IList<DanhMucKhoanThu> KhoanThu { get; set; } = new ObservableCollection<DanhMucKhoanThu>();
        //[XafDisplayName("Chi phí đơn hàng")]
        //public virtual ICollection<ChiPhiDonHang> ChiPhiDonHangs { get; set; }

        // 🔥 Thêm dòng này — tạo liên kết với bảng DoanhThu, trên giao diện doanhthu sẽ hiển thị liên kết với doanhthu-chiphi
        [XafDisplayName("Bảng doanh thu")]
        public virtual IList<DonHangDoanhThu> DonHangDoanhThus { get; set; } = new ObservableCollection<DonHangDoanhThu>();

 
        [NotMapped]
        [XafDisplayName("Tổng doanh thu")]
        public decimal TongDoanhThu => DonHangDoanhThus?.Where(x=>x.KhoanThus.isBackCom==false)?.Sum(x => x.SoTien) ?? 0;

        [XafDisplayName("Bảng chi phí")]
        public virtual IList<DonHangChiPhi> DonHangChiPhis { get; set; } = new ObservableCollection<DonHangChiPhi>();

        [NotMapped]
        [XafDisplayName("Tổng chi phí")]
        public decimal TongChiPhi => DonHangChiPhis?.Where(x => x.KhoanChis.isChiHo == false)?.Sum(x => x.SoTien) ?? 0;

        public void OnCreated()
        {

        }

        public void OnLoaded()
        {

        }

        public void OnSaving()
        {
        }
    }
}
