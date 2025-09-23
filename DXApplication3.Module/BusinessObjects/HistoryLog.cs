using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Map.Native;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DevExpress.DataProcessing.InMemoryDataProcessor.AddSurrogateOperationAlgorithm;

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

     
        [StringLength(100, ErrorMessage = "Hành động không được vượt quá 100 ký tự")]
        [Display(Name = "Hành động")]
        public virtual string Action { get; set; } // Ví dụ: "Tạo", "Sửa", "Xóa"

        [ModelDefault("DisplayFormat", "{0:dd/MM/yyyy HH:mm:ss}")]
        [ModelDefault("EditMask", "dd/MM/yyyy HH:mm:ss")]
        [XafDisplayName("Thời gian")]
        [RuleRequiredField("MyEntity_Timestamp_Required", DefaultContexts.Save, "Thời gian là bắt buộc")]
        public virtual DateTime Timestamp { get; set; }

        [StringLength(100, ErrorMessage = "Tên người dùng không được vượt quá 100 ký tự")]
        [Display(Name = "Tên người dùng")]
        public virtual string UserName { get; set; } // Tên user thực hiện hành động

        [StringLength(100, ErrorMessage = "Loại đối tượng không được vượt quá 100 ký tự")]
        [Display(Name = "Loại đối tượng")]
        public virtual string ObjectType { get; set; } // Ví dụ: "Task", "Employee"

        [Display(Name = "ID đối tượng")]
        public virtual Guid ObjectId { get; set; } // ID của đối tượng liên quan

        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
        [Display(Name = "Mô tả")]
        public virtual string Description { get; set; } // Mô tả chi tiết hành động
    }
}
