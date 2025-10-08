using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DXApplication3.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication3.Module.Controllers
{
    //tự kiểm tra role trước khi cho chuyển trạng thái
    public class CustomTransitionController : ViewController<ObjectView>
    {
        public CustomTransitionController()
        {
            TargetObjectType = typeof(BaiViet);
            var simpleAction = new SimpleAction(this, "ApproveArticle2", PredefinedCategory.Edit)
            {
                Caption = "Duyệt bài viết",
                ConfirmationMessage = "Bạn có đồng ý không",
                SelectionDependencyType = SelectionDependencyType.RequireSingleObject
            };
            simpleAction.Execute += SimpleAction_Execute;

        }

        protected override void OnActivated()
        {
            base.OnActivated();
        }

        private void SimpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var baiViet = (BaiViet)e.CurrentObject;
            var currentUser = SecuritySystem.CurrentUser as PermissionPolicyUser;

            if (currentUser != null)
            {
                foreach (var role in currentUser.Roles.OfType<PermissionPolicyRole>())
                {
                    System.Diagnostics.Debug.WriteLine($"User {currentUser.UserName} Role: {role.Name}");
                }
            }

            // kiểm tra role
            if (currentUser.Roles.Any(r => r.Name == "QuanTri"))
            {
                baiViet.TrangThai = WorkflowState.Approved;
                ObjectSpace.CommitChanges();
            }
            else
            {
                throw new UserFriendlyException("Bạn không có quyền duyệt bài.");
            }
        }
    }
}
