using DXApplication3.Module.NonPersistent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using DXApplication3.Module.BusinessObjects.DonHangs;
using DXApplication3.Module.BusinessObjects.DanhMuc;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraRichEdit.Import.Html;
using DXApplication3.Module.BusinessObjects;

namespace DXApplication3.Module.Controllers.ThongKes
{
   
    public class ThongKeDoanhThuViewController :  DevExpress.ExpressApp.ViewController
    {
        public ThongKeDoanhThuViewController()
        {
            TargetObjectType = typeof(ThongKeKinhDoanh);
        }

     
        protected override void OnActivated()
        {
            base.OnActivated();

            IObjectSpace os = Application.CreateObjectSpace(typeof(DonHang));

            // ✅ Bước 1: Lấy dữ liệu gọn, chỉ cần các trường cần thiết
            var donhangs = os.GetObjectsQuery<DonHang>()
                .Select(x => new { x.ID, x.MaDonHang, x.TenDonHang })
                .ToList();

            var donhang_khoanthu = os.GetObjectsQuery<DonHangDoanhThu>()
                .Where(x => !x.KhoanThus.isBackCom)
                .Select(x => new { x.DonHangID, x.TenKhoanThu, x.SoTien })
                .ToList();

            // ✅ Bước 2: Tính tổng 1 lần duy nhất
            decimal tongTatCa = donhang_khoanthu.Sum(x => x.SoTien);

            // ✅ Bước 3: Dùng group join (hiệu quả hơn)
            var query = from dh in donhangs
                        join dhkt in donhang_khoanthu on dh.ID equals dhkt.DonHangID into g
                        from item in g.DefaultIfEmpty()
                        select new ThongKeKinhDoanh
                        {
                            DonHangId = dh.ID,
                            MaDon = dh.MaDonHang,
                            TenKhachHang = dh.TenDonHang,
                            TenKhoanThu = item?.TenKhoanThu ?? "",
                            SoTien = item?.SoTien ?? 0,
                            TongSoTien = tongTatCa
                        };

            // ✅ Bước 4: Cập nhật ListView nhanh, không tạo ObjectSpace mới
            if (View is ListView listView && listView.CollectionSource is CollectionSourceBase cs)
            {
                cs.List.Clear();
                foreach (var item in query)
                    cs.List.Add(item);
            }
        }

        //protected override void OnActivated()
        //{
        //    base.OnActivated();

        //    // Lấy ObjectSpace của các entity thật
        //    //var os = Application.CreateObjectSpace(typeof(DonHang));
        //    IObjectSpace os = Application.CreateObjectSpace(typeof(DonHang));

        //    var donhangs = os.GetObjectsQuery<DonHang>();
        //    var donhang_khoanthu = os.GetObjectsQuery<DonHangDoanhThu>();

        //    var tongTatCa = donhang_khoanthu.Where(x=>x.KhoanThus.isBackCom == false).Sum(x => x.SoTien);

        //    // Giả sử DonHang có các thuộc tính: Id, MaDon, KhachHang (navigation)
        //    // và KhachHang có TenKhachHang
        //    // Còn DonHang có DanhMucKhoanThuId => liên kết tới DanhMucKhoanThu

        //    var query = from dh in donhangs
        //                join dhkt in donhang_khoanthu on dh.ID equals dhkt.DonHangID
        //                where dhkt.KhoanThus.isBackCom == false
        //                select new ThongKeKinhDoanh
        //                {
        //                    DonHangId = dh.ID,
        //                    MaDon = dh.MaDonHang,
        //                    TenKhachHang = dh.TenDonHang,
        //                    TenKhoanThu = dhkt.TenKhoanThu,
        //                    SoTien = dhkt.SoTien ,// giá trị mặc định hoặc tính toán
        //                    TongSoTien = tongTatCa // ✅ tổng theo đơn
        //                };

        //    // Kết quả gán vào ListView
        //    var list = query.ToList();
        //    var listView = View as ListView;
        //    listView.CollectionSource.List.Clear();
        //    foreach (var item in list)
        //        listView.CollectionSource.List.Add(item);

        //    //Bước 3.Thêm View này vào Navigation

        //    //Mở Model Editor → Navigation Items → thêm mục mới:

        //    //            Caption: Thống kê kinh doanh

        //    //Object Type: chọn ThongKeKinhDoanh

        //    //Khi chạy ứng dụng, bạn sẽ thấy menu mới hiển thị danh sách thống kê được tính từ query LINQ.
        //}
    }
}
