using DevExpress.ExpressApp;
using DXApplication3.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.StateMachine;
using DevExpress.ExpressApp.Security;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EF.StateMachine;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.StateMachine.NonPersistent;

namespace DXApplication3.Module.Workflows
{
    public static class DuyetBaiStateMachineHelper
    {

        private static void CreateTransition(IObjectSpace os, StateMachineState source, StateMachineState target, string caption)
        {
            var t = os.CreateObject<StateMachineTransition>();
            t.SourceState = source;
            t.TargetState = target;
            t.Caption = caption;
        }
         

        public static void Create1(IObjectSpace objectSpace)
        {
            // Tránh tạo lại nhiều lần
            if (objectSpace.GetObjects<StateMachine>()
                           .Any(sm => sm.Name == "Quy trình bài viết"))
                return;

            var sm = objectSpace.CreateObject<StateMachine>();
            sm.Name = "Quy trình bài viết";
            sm.TargetObjectType = typeof(BaiViet);
            sm.StatePropertyName = new DevExpress.ExpressApp.Utils.StringObject(nameof(BaiViet.TrangThai)); //new StringObject("TrangThai"); // property enum

            // 🟩 Trạng thái – Caption phải trùng tên enum
            var draft = objectSpace.CreateObject<StateMachineState>();
            draft.Caption = WorkflowState.Draft.ToString();
            draft.StateMachine = sm;

            var pending = objectSpace.CreateObject<StateMachineState>();
            pending.Caption = WorkflowState.Pending.ToString();
            pending.StateMachine = sm;

            var approved = objectSpace.CreateObject<StateMachineState>();
            approved.Caption = WorkflowState.Approved.ToString();
            approved.StateMachine = sm;

            var rejected = objectSpace.CreateObject<StateMachineState>();
            rejected.Caption = WorkflowState.Rejected.ToString();
            rejected.StateMachine = sm;

            var published = objectSpace.CreateObject<StateMachineState>();
            published.Caption = WorkflowState.Published.ToString();
            published.StateMachine = sm;

            // Trạng thái bắt đầu
            sm.StartState = draft;

            // 🟩 Các chuyển trạng thái
            // Draft -> Pending
            var t1 = objectSpace.CreateObject<StateMachineTransition>();
            t1.SourceState = draft;
            t1.TargetState = pending;
            t1.Caption = "Gửi duyệt";

            // Pending -> Approved
            var t2 = objectSpace.CreateObject<StateMachineTransition>();
            t2.SourceState = pending;
            t2.TargetState = approved;
            t2.Caption = "Duyệt bài";

            // Pending -> Rejected
            var t3 = objectSpace.CreateObject<StateMachineTransition>();
            t3.SourceState = pending;
            t3.TargetState = rejected;
            t3.Caption = "Từ chối";

            // Approved -> Published
            var t4 = objectSpace.CreateObject<StateMachineTransition>();
            t4.SourceState = approved;
            t4.TargetState = published;
            t4.Caption = "Xuất bản";

            objectSpace.CommitChanges();
        }
        public static void CreateWithString(IObjectSpace objectSpace)
        {
            if (objectSpace.GetObjects<StateMachine>().Count > 0)
                return; // tránh tạo lại nhiều lần
            // Tạo StateMachine
            var sm = objectSpace.CreateObject<StateMachine>();
            sm.Name = "Quy trình bài viết";
            sm.TargetObjectType = typeof(BaiViet);
            sm.StatePropertyName = new StringObject("TrangThai");

            // Trạng thái
            var stateChoDuyet = objectSpace.CreateObject<StateMachineState>();
            stateChoDuyet.Caption = "Chờ duyệt";
            stateChoDuyet.StateMachine = sm;

            var stateDaDuyet = objectSpace.CreateObject<StateMachineState>();
            stateDaDuyet.Caption = "Đã duyệt";
            stateDaDuyet.StateMachine = sm;

            var stateXuatBan = objectSpace.CreateObject<StateMachineState>();
            stateXuatBan.Caption = "Xuất bản";
            stateXuatBan.StateMachine = sm;

            sm.StartState = stateChoDuyet;

            // Transition Chờ duyệt → Đã duyệt
            var trans1 = objectSpace.CreateObject<StateMachineTransition>();
            trans1.SourceState = stateChoDuyet;
            trans1.TargetState = stateDaDuyet;
            trans1.Caption = "Duyệt bài";

            // Transition Đã duyệt → Xuất bản
            var trans2 = objectSpace.CreateObject<StateMachineTransition>();
            trans2.SourceState = stateDaDuyet;
            trans2.TargetState = stateXuatBan;
            trans2.Caption = "Xuất bản";

            objectSpace.CommitChanges();
        }

        internal static void Create(IObjectSpace objectSpace)
        {
            if (objectSpace.GetObjects<StateMachine>().Any(sm => sm.Name == "Quy trình bài viết"))
                return;

            var sm = objectSpace.CreateObject<StateMachine>();
            sm.Name = "Quy trình bài viết";
            sm.TargetObjectType = typeof(BaiViet);
            sm.StatePropertyName = new StringObject(nameof(BaiViet.TrangThai));

            // Trạng thái
            var draft = objectSpace.CreateObject<StateMachineState>();
            draft.Caption = WorkflowState.Draft.ToString();
            draft.StateMachine = sm;

            var pending = objectSpace.CreateObject<StateMachineState>();
            pending.Caption = WorkflowState.Pending.ToString();
            pending.StateMachine = sm;

            var approved = objectSpace.CreateObject<StateMachineState>();
            approved.Caption = WorkflowState.Approved.ToString();
            approved.StateMachine = sm;

            var rejected = objectSpace.CreateObject<StateMachineState>();
            rejected.Caption = WorkflowState.Rejected.ToString();
            rejected.StateMachine = sm;

            var published = objectSpace.CreateObject<StateMachineState>();
            published.Caption = WorkflowState.Published.ToString();
            published.StateMachine = sm;

            sm.StartState = draft;

            // Transition
            CreateTransition(objectSpace, draft, pending, "Gửi duyệt");
            CreateTransition(objectSpace, pending, approved, "Duyệt bài");
            CreateTransition(objectSpace, pending, rejected, "Từ chối");
            CreateTransition(objectSpace, approved, published, "Xuất bản");

            objectSpace.CommitChanges();
        }
        
    }
}
