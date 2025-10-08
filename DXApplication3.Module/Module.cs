using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.ReportsV2;
using DXApplication3.Module.BusinessObjects;
using DevExpress.ExpressApp.StateMachine;
using DXApplication3.Module.Workflows;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;

namespace DXApplication3.Module;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
public sealed class DXApplication3Module : ModuleBase {
    public DXApplication3Module() {
		// 
		// DXApplication3Module
		// 
		AdditionalExportedTypes.Add(typeof(DXApplication3.Module.BusinessObjects.ApplicationUser));
		AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRole));
		AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.ModelDifference));
		AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.ModelDifferenceAspect));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.SystemModule.SystemModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Security.SecurityModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Dashboards.DashboardsModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ReportsV2.ReportsModuleV2));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Validation.ValidationModule));
		DevExpress.ExpressApp.Security.SecurityModule.UsedExportedTypes = DevExpress.Persistent.Base.UsedExportedTypes.Custom;
		AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.FileData));
		AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.FileAttachment));
        //Nếu là non-persistent object bạn cần đăng ký trong Module.cs:
        AdditionalExportedTypes.Add(typeof(CustomPageViewModel));
        //kich hoat StateMachine 
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.StateMachine.StateMachineModule));


    }
    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
        ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
        return new ModuleUpdater[] { updater };
    }
    public override void Setup(XafApplication application) {
        base.Setup(application);

        ////Thiet lap quy trinh bai viet
        //// Khi ObjectSpace đã tạo xong mới gọi
        //application.ObjectSpaceCreated += (s, e) => {
        //    if (e.ObjectSpace is not NonPersistentObjectSpace)
        //    {
        //        // Tạo quy trình trong DB nếu chưa có
        //        DuyetBaiStateMachineHelper.Create(e.ObjectSpace);
        //    }
        //};

        //Kích hoạt StateMachine Module
        //application.Modules.Add(new StateMachineModule()); // Kích hoạt StateMachine Module

        //application.SetupComplete += (s, e) =>
        //{
        //    var objectSpace = application.CreateObjectSpace();
        //    new DuyetBaiStateMachine().Register(objectSpace);
        //};

        // Manage various aspects of the application UI and behavior at the module level.

        // đăng ký event tại đây cho view tuy chinh de tu sinh detailview
        //application.ObjectSpaceCreated += (s, e) => {
        //    if (e.ObjectSpace is NonPersistentObjectSpace npos)
        //    {
        //        npos.ObjectsGetting += (sender, args) => {
        //            if (args.ObjectType == typeof(MyCustomData))
        //            {
        //                var os = Application.CreateObjectSpace(typeof(OtherTable));
        //                var otherData = os.GetObjects<OtherTable>().Where(o => o.IsActive)
        //                    .Select(x => new MyCustomData
        //                    {
        //                        Name = x.Name,
        //                        Address = x.Address
        //                    }).ToList();
        //                args.Objects = otherData;
        //            }
        //        };
        //    }
        //};
    }
    public override void Setup(ApplicationModulesManager moduleManager) {
        base.Setup(moduleManager);

	}
}
