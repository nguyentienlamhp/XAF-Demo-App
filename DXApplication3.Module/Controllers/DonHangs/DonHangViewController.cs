using DevExpress.Data;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Blazor.Editors.Grid;
using DevExpress.ExpressApp.Blazor.Editors.Models;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DXApplication3.Module.BusinessObjects;
using DXApplication3.Module.BusinessObjects.DanhMuc;
using DXApplication3.Module.BusinessObjects.DonHangs;
using DXApplication3.Module.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;




namespace DXApplication3.Module.Controllers.DonHangs
{
    public class DonHangViewController : ViewController
    {
        public DonHangViewController()
        {
            TargetObjectType = typeof(DonHang);
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            //// WinForms
            //try
            //{
            //    if (View.Control is GridControl gridControl)
            //    {
            //        GridView gridView = gridControl.MainView as GridView;
            //        if (gridView != null && gridView.Columns["TongChiPhi"] != null)
            //        {
            //            var amountColumn = gridView.Columns["TongChiPhi"];
            //            amountColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //            amountColumn.DisplayFormat.FormatString = "c";
            //            amountColumn.Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TongChiPhi", "Tổng chi phí: {0:c}");
            //            gridView.OptionsView.ShowFooter = true;
            //            gridView.BestFitColumns();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //        Debug.WriteLine($"Error setting up summary: {ex.Message}");

            //}
            // Blazor
            //else if (View.Editor is GridListEditor gridListEditor)
            //{
            //    var gridAdapter = gridListEditor.GetControlAdapter() as DxGridAdapter;
            //    if (gridAdapter != null)
            //    {
            //        var grid = gridAdapter.ComponentModel as DxGrid;
            //        if (grid != null)
            //        {
            //            grid.TotalSummaryItems.Clear();
            //            grid.TotalSummaryItems.Add(new DxGridSummaryItem
            //            {
            //                FieldName = "Amount",
            //                SummaryType = DxGridSummaryType.Sum,
            //                DisplayFormat = "Tổng: {0:c}"
            //            });
            //            grid.DataColumns.FirstOrDefault(c => c.FieldName == "Amount")?.DisplayFormat = "{0:c}";
            //        }
            //    }
            //}

            // Lấy GridView từ ListView
            //if (View.Control is GridControl gridControl)
            //{
            //    GridView gridView = gridControl.MainView as GridView;
            //    if (gridView != null)
            //    {
            //        try
            //        {
            //            var amountColumn = gridView.Columns["TongChiPhi"];
            //            // Hiển thị footer
            //            gridView.OptionsView.ShowFooter = true;

            //            // Thêm summary cho cột cụ thể (ví dụ: cột "Amount" với Sum)
            //            GridColumnSummaryItem gridColumnSummaryItem = amountColumn.Summary.Add(SummaryItemType.Sum, "TongChiPhi", "Tổng: {0}");
            //            amountColumn.SummaryItem.DisplayFormat = "{0:N2}";

            //            // Tùy chỉnh chiều rộng cột (nếu cần)
            //            //amountColumn.Width = 100;

            //            // Refresh giao diện
            //            gridView.BestFitColumns();
            //        }
            //        catch (Exception ex)
            //        {
            //            // Handle exception (e.g., log it)
            //            System.Diagnostics.Debug.WriteLine($"Error setting up summary: {ex.Message}");
            //        }
            //    }
            //}
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            // Theo dõi khi them don hang
            //View.ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
            //Sự kiện này được gọi trước khi lưu object. Bạn có thể thêm các bản ghi liên quan, XAF sẽ tự động lưu chúng cùng với đơn hàng.
            ObjectSpace.ObjectSaving += ObjectSpace_ObjectSaving; // 🟢 Bắt sự kiện lưu
        }



        private void ObjectSpace_ObjectSaving(object sender, ObjectManipulatingEventArgs e)
        {
            if (e.Object is DonHang donHang)
            {
                //Tự động khởi tạo ra đơn hang _ doanh thu
                //Kiem tra don hang chua cau hinh doanh thu
                var danhMucDoanhThu = ObjectSpace.GetObjects<DanhMucKhoanThu>();
                foreach (var dt in danhMucDoanhThu)
                {
                    var dhdt = ObjectSpace.CreateObject<DonHangDoanhThu>();
                    dhdt.KhoanThus = dt;     // liên kết với đơn hàng
                    dhdt.DonHang = donHang;         // liên kết với danh mục
                    dhdt.SoTien = dt.SoTien;   // ví dụ gán giá trị mặc định
                    dhdt.TenKhoanThu = dt.TenKhoanThu; // ví dụ gán giá trị mặc định

                    // donHang.DonHangDoanhThus.Add(dhdt); // 👈 EF sẽ tự cascade save
                    // ObjectSpace sẽ tự lưu khi commit DonHang
                }
                //Tu dong khoi tao don hang chi phi
                var danhMucChiPhi = ObjectSpace.GetObjects<DanhMucKhoanChi>();
                foreach (var cp in danhMucChiPhi)
                {
                    var dhcp = ObjectSpace.CreateObject<DonHangChiPhi>();
                    dhcp.KhoanChis = cp;     // liên kết với đơn hàng
                    dhcp.DonHang = donHang;         // liên kết với danh mục
                    dhcp.SoTien = cp.SoTien;   // ví dụ gán giá trị mặc định
                    dhcp.TenKhoanChi = cp.TenKhoanChi; // ví dụ gán giá trị mặc định
                    //donHang.DonHangChiPhis.Add(dhcp); // 👈 EF sẽ tự cascade save
                    // ObjectSpace sẽ tự lưu khi commit DonHang
                }
                //ObjectSpace.CommitChanges();
            }
        }

        protected override void OnDeactivated()
        {
            ObjectSpace.ObjectSaving -= ObjectSpace_ObjectSaving;
            base.OnDeactivated();
        }
    }
}
