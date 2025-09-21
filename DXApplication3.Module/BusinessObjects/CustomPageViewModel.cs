using DevExpress.ExpressApp.Data;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication3.Module.BusinessObjects
{
    [DomainComponent]//Tạo một non-persistent type (không map vào DB).
    //Nếu là non-persistent object bạn cần đăng ký trong Module.cs:  AdditionalExportedTypes.Add(typeof(CustomPageViewModel));
    
    [DefaultClassOptions]
    [ImageName("BO_User")]
    public class CustomPageViewModel
    {
        // Khóa bắt buộc cho non-persistent
        [Key]
        public Guid Oid { get; set; } = Guid.NewGuid();
        public virtual string Dummy { get; set; }
    }
}
