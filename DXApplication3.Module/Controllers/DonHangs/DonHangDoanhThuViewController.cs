

using DevExpress.ExpressApp;
using DXApplication3.Module.BusinessObjects.DanhMuc;
using DXApplication3.Module.BusinessObjects.DonHangs;

namespace DXApplication3.Module.Controllers.DonHangs
{
    public class DonHangDoanhThuViewController: DevExpress.ExpressApp.ViewController
    {
        public DonHangDoanhThuViewController()
        {
            TargetObjectType = typeof(DonHangDoanhThu);
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Theo dõi khi thuộc tính DonHang thay đổi trên form donhang-doanhthu
            View.ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;

        }

        private void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            //Kiểm tra đối tuọng danh mục doanh thu thay đổi
            if (e.PropertyName == "KhoanThus" )
            {
                //e.Object: chính là object đang bị thay đổi
                var donHangDoanhThu = e.Object as DonHangDoanhThu;
                if (donHangDoanhThu != null)
                {
                    var dmKhoanThu = donHangDoanhThu.KhoanThus;
                    if (dmKhoanThu != null)
                    {
                        // Ví dụ: tự động điền tên khoản thu
                        donHangDoanhThu.TenKhoanThu = dmKhoanThu.TenKhoanThu;
                        donHangDoanhThu.SoTien = dmKhoanThu.SoTien;
                        // Hiển thị thông báo ngắn trên UI
                        Application.ShowViewStrategy.ShowMessage($"Đã chọn : {dmKhoanThu.TenKhoanThu}");
                    }
                }
                   
            }
        }
    }
}
