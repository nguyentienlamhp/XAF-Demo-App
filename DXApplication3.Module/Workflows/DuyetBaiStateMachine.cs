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

namespace DXApplication3.Module.Workflows
{
    public class DuyetBaiStateMachine : IStateMachineProvider
    {
        public IList<IStateMachine> GetStateMachines()
        {
            throw new NotImplementedException();
        }


        public void Register(IObjectSpace objectSpace)
        {
            var stateMachine = objectSpace.CreateObject<StateMachine>();
            stateMachine.Name = "DuyetBaiStateMachine";
            stateMachine.StatePropertyName = new StringObject(nameof(BaiViet.TrangThai));
            stateMachine.TargetObjectType = typeof(BaiViet);

            var draftState = CreateState(objectSpace, stateMachine, "Draft", "Bản nháp");
            var pendingState = CreateState(objectSpace, stateMachine, "Pending", "Chờ duyệt");
            var approvedState = CreateState(objectSpace, stateMachine, "Approved", "Đã duyệt");
            var rejectedState = CreateState(objectSpace, stateMachine, "Rejected", "Bị từ chối");
            var publishedState = CreateState(objectSpace, stateMachine, "Published", "Đã xuất bản");

            CreateTransition(objectSpace, draftState, pendingState, "Gửi duyệt", "Tác giả");
            CreateTransition(objectSpace, pendingState, approvedState, "Duyệt sơ bộ", "Biên tập viên");
            CreateTransition(objectSpace, pendingState, rejectedState, "Từ chối", "Biên tập viên");
            CreateTransition(objectSpace, approvedState, publishedState, "Xuất bản", "Trưởng ban");
            CreateTransition(objectSpace, approvedState, rejectedState, "Từ chối", "Trưởng ban");

            //objectSpace.CommitChanges();
        }


        private StateMachineState CreateState(
     IObjectSpace objectSpace,
     StateMachine stateMachine,
     string stateValue,
     string caption)
        {
            var state = objectSpace.CreateObject<StateMachineState>();
            state.StateMachine = stateMachine;
            //state.StatePropertyValue = stateValue;  // giá trị enum hoặc string thực lưu trong BaiViet.TrangThai
            state.Caption = caption;                // nhãn hiển thị
            return state;
        }

        private void CreateTransition(IObjectSpace objectSpace,
    StateMachineState sourceState,
    StateMachineState targetState,
    string caption,
    string roleName)
        {
            var transition = objectSpace.CreateObject<StateMachineTransition>();
            transition.SourceState = sourceState;
            transition.TargetState = targetState;
            transition.Caption = caption;

            //var securityRole = objectSpace.FindObject<PermissionPolicyRole>(
            //    CriteriaOperator.Parse("Name = ?", roleName));
            //if (securityRole != null)
            //    transition.Roles.Add(securityRole);
        }
    }
}
