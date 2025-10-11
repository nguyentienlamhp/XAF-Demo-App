using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.XtraGauges.Core.Base;
using DXApplication3.Module.BusinessObjects.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication3.Module.BusinessObjects.DonHangs
{
    [DefaultClassOptions]
    [NavigationItem("Đơn hàng")]
    [XafDisplayName("Đơn hàng - Doanh thu")]
    public class DonHangDoanhThu: BaseObject, IXafEntityObject
    {
        [Key]
        public virtual Guid ID { get; set; }

        [XafDisplayName("Tên khoản thu")]
        public virtual string TenKhoanThu { get; set; }

        [XafDisplayName("Số tiền")]
        public virtual decimal SoTien { get; set; }

       

        [XafDisplayName("Loại khoản thu")]
        //Lien ket voi danh muc doanh thu
        public virtual DanhMucKhoanThu KhoanThus { get; set; }

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
