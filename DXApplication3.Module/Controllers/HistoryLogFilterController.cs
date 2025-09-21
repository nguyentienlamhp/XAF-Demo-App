using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DXApplication3.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DXApplication3.Module.Model;

namespace DXApplication3.Module.Controllers
{
    public class HistoryLogFilterController : ViewController
    {
        public HistoryLogFilterController()
        {
            TargetObjectType = typeof(HistoryLog);
            TargetViewType = ViewType.ListView;

            var optionAction = new SingleChoiceAction(this, "OptiionAction", PredefinedCategory.Edit)
            {
                Caption="Chọn Option",
                ItemType=SingleChoiceActionItemType.ItemIsOperation,
                SelectionDependencyType=SelectionDependencyType.RequireSingleObject
            };
            optionAction.Items.Add(new ChoiceActionItem ("Option 1", "Option 1"));
            optionAction.Items.Add(new ChoiceActionItem("Option 2", "Option 2"));
            optionAction.Execute += History_OptionAction_Execute;

            var filterAction = new PopupWindowShowAction(this, "FilterByDateRange", PredefinedCategory.View)
            {
                Caption = "Lọc theo khoảng thời gian",
                ToolTip = "Chọn khoảng thời gian để lọc lịch sử",
                ImageName = "Action_Filter"
            };
            filterAction.CustomizePopupWindowParams += FilterAction_CustomizePopupWindowParams;
            filterAction.Execute += FilterAction_Execute;
            ;     }

        private void History_OptionAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            string selectedOption = e.SelectedChoiceActionItem.Data as String;
            Application.ShowViewStrategy.ShowMessage($"You selected: {selectedOption}");
        }

        private void FilterAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var objectSpace = Application.CreateObjectSpace(typeof(DateTimeRange));
            var dateRange = objectSpace.CreateObject<DateTimeRange>();
            e.View = Application.CreateDetailView(objectSpace, dateRange);
            e.View.Caption = "Chọn khoảng thời gian";
        }

        private void FilterAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var dateRange = (DateTimeRange)e.PopupWindowViewCurrentObject;
            var listView = (ListView)View;
            var criteria = CriteriaOperator.Parse("Timestamp >= ? AND Timestamp <= ?",
                dateRange.StartDate, dateRange.EndDate);
            listView.CollectionSource.Criteria["DateRangeFilter"] = criteria;
        }
    }
}
