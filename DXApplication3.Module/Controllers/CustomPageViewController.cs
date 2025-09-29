using DevExpress.ExpressApp;
using DXApplication3.Module.BusinessObjects;
using DXApplication3.Module.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DXApplication3.Module.Controllers
{
    public class CustomPageViewController : ViewController
    {
        public CustomPageViewController()
        {
            TargetObjectType = typeof(CustomPageViewModel);
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            try
            {
                var navManager = (NavigationManager)Application.ServiceProvider.GetService(typeof(NavigationManager));
                if (navManager != null)
                {
                    // Điều hướng sang Razor page
                    navManager.NavigateTo("/custompage", forceLoad: true, replace: true);
                }
                ////Vi du lay du lieu tu bang khac
                //// Lấy ObjectSpace mới để query bảng khác
                //IObjectSpace os = Application.CreateObjectSpace(typeof(OtherTable));

                //// Query dữ liệu từ bảng khác
                //var otherData = os.GetObjects<OtherTable>()
                //                  .Where(o => o.IsActive)
                //                  .ToList();

                //// Làm gì đó với dữ liệu: map sang View hiện tại hoặc bind vào non-persistent object
                //var listView = View as ListView;
                //// Đổ dữ liệu vào listview
                //listView.CollectionSource.List.Clear();
                //foreach (var item in otherData)
                //{
                //    listView.CollectionSource.List.Add(item);
                //}

                //Muon sinh ra detail view tuy chinh thi them dong sau vao Module.cs
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
            catch (Exception ex)
            {
                Application.ShowViewStrategy.ShowMessage(
                     ex.Message,
                     InformationType.Error,   // Success, Warning, Error, Info
                     4000,                      // thời gian hiển thị (ms)
                     InformationPosition.Top    // vị trí hiển thị (Top/Bottom)
                 );

            }

        }
    }
}
