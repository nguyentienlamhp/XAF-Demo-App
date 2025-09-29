using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EF.StateMachine;
using System.Security.AccessControl;
using DevExpress.Data.Filtering;
using DXApplication3.Module.Workflows;
using DevExpress.ExpressApp.EFCore;
using static System.Net.Mime.MediaTypeNames;


namespace DXApplication3.Module.BusinessObjects
{
    // IXafEntityObject
    //Định nghĩa: IXafEntityObject là một interface của XAF dùng để làm việc với các Entity Framework entities.
    //Ý nghĩa: Khi bạn muốn tích hợp EF entities vào XAF, bạn có thể triển khai interface này để XAF biết cách quản lý lifecycle của entity.
    [DefaultClassOptions]
    public class BaiViet : IXafEntityObject
    {
        [Key]
        public virtual Guid Id { get; set; }

        [StringLength(255)]
        public virtual string TieuDe { get; set; }

        //[Column(TypeName = "nvarchar(max)")]
        public virtual string NoiDung { get; set; }

        [Appearance("PublishedColor", AppearanceItemType = "ViewItem", TargetItems = "TrangThai",
            Criteria = "TrangThai = 'Published'", BackColor = "Green", Priority = 1)]
        [Appearance("DraftColor", AppearanceItemType = "ViewItem", TargetItems = "TrangThai",
            Criteria = "TrangThai = 'Draft'", BackColor = "Gray", Priority = 1)]
        [Appearance("PendingColor", AppearanceItemType = "ViewItem", TargetItems = "TrangThai",
            Criteria = "TrangThai = 'Pending'", BackColor = "Yellow", Priority = 1)]
        [Appearance("ApprovedColor", AppearanceItemType = "ViewItem", TargetItems = "TrangThai",
            Criteria = "TrangThai = 'Approved'", BackColor = "LightBlue", Priority = 1)]
        [Appearance("RejectedColor", AppearanceItemType = "ViewItem", TargetItems = "TrangThai",
            Criteria = "TrangThai = 'Rejected'", BackColor = "Red", Priority = 1)]
        //Khong cho sua tren ui
        [ModelDefault("AllowEdit", "False")]
        public virtual WorkflowState TrangThai { get; set; }

        //[Browsable(false)]
        public virtual ApplicationUser NguoiTao { get; set; }
        public virtual DateTime NgayTao { get; set; }

        public void OnCreated() => NgayTao = DateTime.Now;
        public void OnSaving()
        {
            //gan mac dinh nguoi tao
            if (NguoiTao == null && SecuritySystem.CurrentUser != null)
            {
                NguoiTao = (ApplicationUser)SecuritySystem.CurrentUser;
            }
        }
        public void OnLoaded() { }

        //Tao 1 doi trang thai
        [Action(Caption = "Cập nhật trạng thái", AutoCommit = true, ImageName = "State_Task_Completed")]
        public void UpdateTrangThai()
        {
            var os = EFCoreObjectSpace.FindObjectSpaceByObject(this);
            if (os != null)
            {
                var currentUser = SecuritySystem.CurrentUser as PermissionPolicyUser;
                var roles = currentUser.Roles.OfType<PermissionPolicyRole>().Select(r => r.Name).ToList();
                //var roles = currentUser?.Roles.Select(r => r.Name).ToList() ?? new List<string>();

                
            }
        }
    }

    public enum WorkflowState
    {
        Draft, Pending, Approved, Rejected, Published
    }
}
