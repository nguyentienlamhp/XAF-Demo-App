using DevExpress.ExpressApp;
using DXApplication3.Module.BusinessObjects;
using DXApplication3.Module.BusinessObjects.DanhMuc;
using DXApplication3.Module.BusinessObjects.DonHangs;
using DXApplication3.Module.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication3.Module.Controllers.DonHangs
{
    public class DonHangViewController: ViewController
    {
        public DonHangViewController()
        {
            TargetObjectType = typeof(DonHang);
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            // Theo dõi khi them don hang
            //View.ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
            //Sự kiện này được gọi trước khi lưu object. Bạn có thể thêm các bản ghi liên quan, XAF sẽ tự động lưu chúng cùng với đơn hàng.
            View.ObjectSpace.ObjectSaving += ObjectSpace_ObjectSaving;
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
                    // ObjectSpace sẽ tự lưu khi commit DonHang
                }

            }
        }

        protected override void OnDeactivated()
        {
            ObjectSpace.ObjectSaving -= ObjectSpace_ObjectSaving;
            base.OnDeactivated();
        }
    }
}
