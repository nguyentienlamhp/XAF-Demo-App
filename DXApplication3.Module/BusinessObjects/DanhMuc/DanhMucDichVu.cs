using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication3.Module.BusinessObjects.DanhMuc
{
    [DefaultClassOptions]
    [NavigationItem("Danh mục")]
    [XafDisplayName("DM Dịch vụ")]
    [DefaultProperty(nameof(TenDichVu))]
    public class DanhMucDichVu
    {
        [Key]
        public virtual Guid ID { get; set; }
        public virtual string MaDichVu { get; set; }
        public virtual string TenDichVu { get; set; }
        public virtual DateTime NgayTao { get; set; }= DateTime.Now;

        [Browsable(false)]
        public virtual ApplicationUser NguoiTao { get; set; }
        
    }
}
