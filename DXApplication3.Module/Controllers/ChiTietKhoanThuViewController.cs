using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DXApplication3.Module.BusinessObjects;
using DXApplication3.Module.BusinessObjects.DonHangs;
using DXApplication3.Module.Common;

namespace DXApplication3.Module.Controllers
{
    public class ChiTietKhoanThuViewController: ViewController
    {
        public ChiTietKhoanThuViewController()
        {
            TargetObjectType = typeof(ChiTietKhoanThu);
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            // Theo dõi khi thuộc tính DonHang thay đổi
            if (View?.CurrentObject is ChiTietKhoanThu chiTiet)
            {
                var donHangId = UserSessionStore.GetValue<Guid?>("SelectedDonHangId");
                if (donHangId != null)
                {
                    //var donHang = ObjectSpace.GetObjectByKey<DonHang>(donHangId.Value);
                    //if (donHang != null)
                    //{
                    //    var chiTiet = View.CurrentObject as ChiTietDonHang;
                    //    if (chiTiet != null)
                    //        chiTiet.DonHang = donHang;
                    //}

                    // Tìm đơn hàng trong ObjectSpace
                    var donHang = ObjectSpace.FindObject<DonHang>(
                        CriteriaOperator.Parse("ID = ?", donHangId)
                    );

                    // Gán vào thuộc tính DonHang nếu chưa có
                    if (chiTiet.DonHang == null && donHang != null)
                    {
                        chiTiet.DonHang = donHang;
                        ObjectSpace.SetModified(chiTiet); // Đánh dấu để XAF biết là có thay đổi
                    }
                }
                
            }


        }
    }
}
