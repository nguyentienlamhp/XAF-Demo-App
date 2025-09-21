using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication3.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Audit_Trail")]
    [NavigationItem("Lịch sử")]
    [DomainComponent]
    public class HistoryLog : BaseObject
    {
        //[Key]
        //public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public virtual string Action { get; set; } // Ví dụ: "Tạo", "Sửa", "Xóa"

        [Required]
        public virtual DateTime Timestamp { get; set; }

        [StringLength(100)]
        public virtual string UserName { get; set; } // Tên user thực hiện hành động

        [StringLength(100)]
        public virtual string ObjectType { get; set; } // Ví dụ: "Task", "Employee"

        public virtual Guid ObjectId { get; set; } // ID của đối tượng liên quan

        [StringLength(500)]
        public virtual string Description { get; set; } // Mô tả chi tiết hành động
    }
}
