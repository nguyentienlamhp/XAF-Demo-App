using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DXApplication3.Module.BusinessObjects.DonHangs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication3.Module.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Chi phí đơn hàng")]
    public class ChiPhiDonHang: BaseObject, IXafEntityObject
    {
        [Key]
        public virtual Guid ID { get; set; }
        //public virtual DonHang DonHang { get; set; }
        //public virtual DanhMucKhoanThu DanhMucKhoanThu { get; set; }
        [XafDisplayName("Đơn hàng")]
        public virtual Guid DonHangID { get; set; }

        [ForeignKey(nameof(DonHangID))]
        public virtual DonHang DonHang { get; set; }

//        private DonHang _donHang;
//[ImmediatePostData]
//public virtual DonHang DonHang {
//    get => _donHang;
//    set {
//        _donHang = value;
//        if (value != null) {
//            SessionContext.CurrentDonHangID = value.ID;
//        }
//    }
//}

        //[XafDisplayName("Khoản thu")]
        //public virtual Guid DanhMucKhoanThuID { get; set; }

        //[ForeignKey(nameof(DanhMucKhoanThuID))]
        //public virtual DanhMucKhoanThu DanhMucKhoanThu { get; set; }
        public virtual IList<ChiTietKhoanThu> KhoanThu { get; set; } = new ObservableCollection<ChiTietKhoanThu>();

      
        public void OnCreated()
        {

        }
    }
}
