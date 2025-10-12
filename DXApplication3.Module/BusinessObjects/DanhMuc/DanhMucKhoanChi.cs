using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication3.Module.BusinessObjects.DanhMuc
{
    [NavigationItem("Danh mục")]
    [XafDisplayName("DM Khoản chi")]
    [DefaultProperty(nameof(TenKhoanChi))] // 👈 Trường mặc định đại diện cho class
    public class DanhMucKhoanChi
    {
        [System.ComponentModel.DataAnnotations.Key]
        public virtual Guid ID { get; set; }

        [RuleUniqueValue(
           DefaultContexts.Save,
           CustomMessageTemplate = "Mã này đã tồn tại!"
       )]
        [Indexed(Unique = true)]
        public virtual string MaKhoanChi { get; set; }
        public virtual string TenKhoanChi { get; set; }

        public virtual decimal SoTien { get; set; }

        public virtual Guid NhomID { get; set; }

        public virtual bool isChiHo { get; set; }

        [XafDisplayName("Người tạo")]
        [Browsable(false)]
        public virtual ApplicationUser NguoiTao { get; set; }

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
