using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.XtraGauges.Core.Base;
using DevExpress.XtraPrinting.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication3.Module.BusinessObjects.DanhMuc
{
    [DefaultClassOptions]
    [NavigationItem("Danh mục")]
    [XafDisplayName("DM Khoản thu")]
    [DefaultProperty(nameof(TenKhoanThu))] // 👈 Trường mặc định đại diện cho class
    public class DanhMucKhoanThu :  IXafEntityObject
    {
        [System.ComponentModel.DataAnnotations.Key]
        public virtual Guid ID { get; set; }

      
        public virtual string TenKhoanThu { get; set; }

        [Indexed(Unique = true)]
        [RuleUniqueValue(
              DefaultContexts.Save,
              CustomMessageTemplate = "Mã này đã tồn tại!"
          )]
        public virtual string MaKhoanThu { get; set; }

        public virtual decimal SoTien { get; set; }

        public virtual Guid NhomID { get; set; }

        public virtual bool isBackCom { get; set; }

        [XafDisplayName("Người tạo")]
        [Browsable(false)]
        public virtual ApplicationUser NguoiTao { get; set; }

        // Quan hệ ngược với ChiPhiDonHang
        //public virtual ICollection<ChiPhiDonHang> ChiPhiDonHangs { get; set; }

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
