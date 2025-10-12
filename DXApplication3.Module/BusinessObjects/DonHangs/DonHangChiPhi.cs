using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.XtraGauges.Core.Base;
using DXApplication3.Module.BusinessObjects.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication3.Module.BusinessObjects.DonHangs
{
    [DefaultClassOptions]
    [NavigationItem("Đơn hàng")]
    [XafDisplayName("Đơn hàng - Chi Phí")]
    public class DonHangChiPhi: BaseObject, IXafEntityObject
    {
        [System.ComponentModel.DataAnnotations.Key]
        public virtual Guid ID { get; set; }

        [XafDisplayName("Tên khoản chi")]
        public virtual string TenKhoanChi { get; set; }

        [XafDisplayName("Số tiền")]
        public virtual decimal SoTien { get; set; }


        //Muc dich cho loai khoan thu la duy nhat
        [XafDisplayName("Loại khoản chi")]
        //[Indexed(Unique = true)]
        [Browsable(false)]
        public virtual Guid LoaiKhoanChiID { get; set; }
        [ForeignKey(nameof(LoaiKhoanChiID))]
        //Lien ket voi danh muc doanh thu
        public virtual DanhMucKhoanChi KhoanChis { get; set; }

        ////Tu dong lay gia tri khi chon danh muc khoan thu
        //private DanhMucKhoanThu _khoanThu;
        //[ImmediatePostData]
        //public virtual DanhMucKhoanThu KhoanThus
        //{
        //    get => _khoanThu;
        //    set
        //    {
        //        _khoanThu = value;
        //        if (value != null)
        //        {
        //            TenKhoanThu = value.TenKhoanThu;
        //            SoTien = value.SoTien;
        //        }
        //    }
        //}



        //Mục đich id don hàng và id danh muc khoản thu là bản ghi duy nhất
        //Foreign key
        [Indexed(Unique = true)]
        [Browsable(false)]
        public virtual Guid DonHangID { get; set; }
        [ForeignKey(nameof(DonHangID))]
        //Lien ket voi don hang
        public virtual DonHang DonHang { get; set; }


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
