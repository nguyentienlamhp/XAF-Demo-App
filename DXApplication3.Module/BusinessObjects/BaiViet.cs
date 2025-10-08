


using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System.ComponentModel.DataAnnotations;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Core;
using DevExpress.Xpo;
using System.Runtime.CompilerServices;


namespace DXApplication3.Module.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Bài viết")]
    public class BaiViet : BaseObject, IXafEntityObject, IObjectSpaceLink
    {
        public BaiViet()  { }

        [System.ComponentModel.DataAnnotations.Required]
        [RuleRequiredField(DefaultContexts.Save)]
        [StringLength(200)]
        [XafDisplayName("Tiêu đề")]
        public virtual string TieuDe { get; set; }

        [XafDisplayName("Nội dung")]
        public virtual string NoiDung { get; set; }

        [XafDisplayName("Ngày tạo")]
        [ModelDefault("DisplayFormat", "{0:dd/MM/yyyy HH:mm:ss}")]
        [ModelDefault("EditMask", "dd/MM/yyyy HH:mm:ss")]
        public virtual DateTime NgayTao { get; set; } = DateTime.Now;

        // Người tạo (ApplicationUser kế thừa từ PermissionPolicyUser)
        [XafDisplayName("Người tạo")]
        [Browsable(false)]
        public virtual ApplicationUser NguoiTao { get; set; }

        // Property trạng thái mà StateMachine sẽ quản lý
        [XafDisplayName("Trạng thái")]
        public virtual WorkflowState TrangThai { get; set; } = WorkflowState.Draft;

        #region IObjectSpaceLink
        [Browsable(false)]
        [NotMapped]   
        public IObjectSpace ObjectSpace { get; set; }
        #endregion

        // 🟩 Lifecycle methods của IXafEntityObject
        public void OnCreated()
        {
            // có thể gán mặc định ở đây nếu cần
        }

        public void OnLoaded()
        {
            // không cần gì thêm
        }

        public void OnSaving()
        {
            // Kiểm tra nếu NguoiTao chưa được gán
            if (NguoiTao == null && SecuritySystem.CurrentUser != null)
            {
                var currentUserId = SecuritySystem.CurrentUserId;
                Tracing.Tracer.LogWarning("CurrentUserId: {0}", currentUserId.ToString());
                var currentUser = ObjectSpace.GetObjectByKey<ApplicationUser>(currentUserId);
                if (currentUser != null)
                {
                    NguoiTao = currentUser;
                }
            }
        }
    }
    // IXafEntityObject
    //Định nghĩa: IXafEntityObject là một interface của XAF dùng để làm việc với các Entity Framework entities.
    //Ý nghĩa: Khi bạn muốn tích hợp EF entities vào XAF, bạn có thể triển khai interface này để XAF biết cách quản lý lifecycle của entity.
    //[DefaultClassOptions]
    //public class BaiViet : IXafEntityObject, IObjectSpaceLink
    //{
    //    [Key]
    //    public virtual Guid Id { get; set; }

    //    [StringLength(255)]
    //    public virtual string TieuDe { get; set; }

    //    //[Column(TypeName = "nvarchar(max)")]
    //    public virtual string NoiDung { get; set; }

    //    [Appearance("PublishedColor", AppearanceItemType = "ViewItem", TargetItems = "TrangThai",
    //        Criteria = "TrangThai = 'Published'", BackColor = "Green", Priority = 1)]
    //    [Appearance("DraftColor", AppearanceItemType = "ViewItem", TargetItems = "TrangThai",
    //        Criteria = "TrangThai = 'Draft'", BackColor = "Gray", Priority = 1)]
    //    [Appearance("PendingColor", AppearanceItemType = "ViewItem", TargetItems = "TrangThai",
    //        Criteria = "TrangThai = 'Pending'", BackColor = "Yellow", Priority = 1)]
    //    [Appearance("ApprovedColor", AppearanceItemType = "ViewItem", TargetItems = "TrangThai",
    //        Criteria = "TrangThai = 'Approved'", BackColor = "LightBlue", Priority = 1)]
    //    [Appearance("RejectedColor", AppearanceItemType = "ViewItem", TargetItems = "TrangThai",
    //        Criteria = "TrangThai = 'Rejected'", BackColor = "Red", Priority = 1)]
    //    //Khong cho sua tren ui
    //    [ModelDefault("AllowEdit", "False")]
    //    public virtual WorkflowState TrangThai { get; set; }

    //    //[Browsable(false)]
    //    public virtual ApplicationUser NguoiTao { get; set; }
    //    public virtual DateTime NgayTao { get; set; }

    //    #region IXafEntityObject
    //    public void OnCreated() => NgayTao = DateTime.Now;
    //    public void OnSaving()
    //    {
    //        //gan mac dinh nguoi tao
    //        if (NguoiTao == null)
    //        {
    //            if (SecuritySystem.CurrentUser is ApplicationUser currentUser)
    //            {
    //                NguoiTao = currentUser;
    //            }
    //        }
    //    }
    //    public void OnLoaded() { }
    //    #endregion

    //    #region IObjectSpaceLink
    //    [Browsable(false)]
    //    [NotMapped]   //
    //    public IObjectSpace ObjectSpace { get; set; }
    //    #endregion

    //    //Tao 1 doi trang thai
    //    [Action(Caption = "Cập nhật trạng thái", AutoCommit = true, ImageName = "State_Task_Completed")]
    //    public void UpdateTrangThai()
    //    {
    //        var os = EFCoreObjectSpace.FindObjectSpaceByObject(this);
    //        if (os != null)
    //        {
    //            var currentUser = SecuritySystem.CurrentUser as PermissionPolicyUser;
    //            var roles = currentUser.Roles.OfType<PermissionPolicyRole>().Select(r => r.Name).ToList();
    //            //var roles = currentUser?.Roles.Select(r => r.Name).ToList() ?? new List<string>();


    //        }
    //    }
    //}

    public enum WorkflowState
    {
        Draft, Pending, Approved, Rejected, Published
    }
}
