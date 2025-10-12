

using DevExpress.ExpressApp;
using DXApplication3.Module.BusinessObjects.DanhMuc;
using DXApplication3.Module.BusinessObjects.DonHangs;

namespace DXApplication3.Module.Controllers.DonHangs
{
    public class DonHangChiPhiViewController: DevExpress.ExpressApp.ViewController
    {
        public DonHangChiPhiViewController()
        {
            TargetObjectType = typeof(DonHangChiPhi);
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
            if (e.PropertyName == "KhoanChis" )
            {
                //e.Object: chính là object đang bị thay đổi
                var donHangChiPhi = e.Object as DonHangChiPhi;
                if (donHangChiPhi != null)
                {
                    var dmKhoanThu = donHangChiPhi.KhoanChis;
                    if (dmKhoanThu != null)
                    {
                        // Ví dụ: tự động điền tên khoản thu
                        donHangChiPhi.TenKhoanChi = dmKhoanThu.TenKhoanChi;
                        donHangChiPhi.SoTien = dmKhoanThu.SoTien;
                        // Hiển thị thông báo ngắn trên UI
                        Application.ShowViewStrategy.ShowMessage($"Đã chọn : {dmKhoanThu.TenKhoanChi}");
                    }
                }
                   
            }
        }
    }
}
