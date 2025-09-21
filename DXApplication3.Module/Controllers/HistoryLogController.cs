using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp;
using DXApplication3.Module.BusinessObjects;

namespace DXApplication3.Module.Controllers
{

    public class HistoryLogController : ViewController
    {
        public HistoryLogController()
        {
            TargetObjectType = typeof(DXApplication3.Module.BusinessObjects.Task); // Ghi log cho Task
            TargetObjectType = typeof(Employee); // Ghi log cho Employee
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Lắng nghe sự kiện tạo, sửa, xóa
            var newObjectController = Frame.GetController<NewObjectViewController>();
            if (newObjectController != null)
            {
                newObjectController.ObjectCreated += NewObjectController_ObjectCreated;
            }

            var modifyController = Frame.GetController<ModificationsController>();
            if (modifyController != null)
            {
                modifyController.SaveAction.Execute += SaveAction_Execute;
            }

            var deleteController = Frame.GetController<DeleteObjectsViewController>();
            if (deleteController != null)
            {
                deleteController.DeleteAction.Execute += DeleteAction_Execute;
            }
        }

        private void NewObjectController_ObjectCreated(object sender, ObjectCreatedEventArgs e)
        {
            var obj = e.CreatedObject;
            if (obj is DXApplication3.Module.BusinessObjects.Task task)
            {
                CreateLog("Tạo", task, $"Tạo công việc: {task.Subject}");
            }
            else if (obj is Employee employee)
            {
                CreateLog("Tạo", employee, $"Tạo nhân viên: {employee.FirstName} {employee.LastName}");
            }
        }

        private void SaveAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            foreach (var obj in e.SelectedObjects)
            {
                if (obj is DXApplication3.Module.BusinessObjects.Task task)
                {
                    CreateLog("Sửa", task, $"Sửa công việc: {task.Subject}");
                }
                else if (obj is Employee employee)
                {
                    CreateLog("Sửa", employee, $"Sửa nhân viên: {employee.FirstName} {employee.LastName}");
                }
            }
        }

        private void DeleteAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            foreach (var obj in e.SelectedObjects)
            {
                if (obj is DXApplication3.Module.BusinessObjects.Task task)
                {
                    CreateLog("Xóa", task, $"Xóa công việc: {task.Subject}");
                }
                else if (obj is Employee employee)
                {
                    CreateLog("Xóa", employee, $"Xóa nhân viên: {employee.FirstName} {employee.LastName}");
                }
            }
        }

        private void CreateLog(string action, object obj, string description)
        {
            var log = ObjectSpace.CreateObject<HistoryLog>();
            log.Action = action;
            log.Timestamp = DateTime.Now;
            log.UserName = SecuritySystem.CurrentUserName ?? "Unknown";
            log.ObjectType = obj.GetType().Name;
            log.ObjectId = obj is DXApplication3.Module.BusinessObjects.Task task ? task.ID : (obj is Employee employee ? employee.ID : Guid.Empty);
            log.Description = description;
            ObjectSpace.CommitChanges();
        }

        protected override void OnDeactivated()
        {
            var newObjectController = Frame.GetController<NewObjectViewController>();
            if (newObjectController != null)
            {
                newObjectController.ObjectCreated -= NewObjectController_ObjectCreated;
            }

            var modifyController = Frame.GetController<ModificationsController>();
            if (modifyController != null)
            {
                modifyController.SaveAction.Execute -= SaveAction_Execute;
            }

            var deleteController = Frame.GetController<DeleteObjectsViewController>();
            if (deleteController != null)
            {
                deleteController.DeleteAction.Execute -= DeleteAction_Execute;
            }

            base.OnDeactivated();
        }
    }
}
