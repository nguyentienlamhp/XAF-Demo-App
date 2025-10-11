using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DXApplication3.Module.BusinessObjects;
using DXApplication3.Module.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication3.Module.Controllers
{
    public class ChiPhiDonHangViewController:ViewController
    {
        public ChiPhiDonHangViewController()
        {
            TargetObjectType = typeof(ChiPhiDonHang);
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            // Theo dõi khi thuộc tính DonHang thay đổi
            View.ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;


        }

        private void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ChiPhiDonHang.DonHang))
            {
                var chiPhi = View.CurrentObject as ChiPhiDonHang;
                if (chiPhi?.DonHang != null)
                {
                    // Lưu donHangId vào Frame để view khác có thể đọc
                
                    Guid donHangId = chiPhi.DonHang.ID;
                    // ✅ Cách lưu ID đơn hàng vào Frame (đúng cú pháp)
                    UserSessionStore.SetValue("SelectedDonHangId", donHangId);

                    // Hiển thị thông báo ngắn trên UI
                    Application.ShowViewStrategy.ShowMessage($"Đã chọn đơn hàng ID: {donHangId}");
                  
                }
            }
        }
    }
}
