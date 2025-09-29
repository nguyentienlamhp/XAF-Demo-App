Folder Description

The "BusinessObjects" project folder is intended for storing code of your data model.
In XAF, a business object can be implemented as an ORM-based persistent class
or a non-persistent POCO.


Relevant Documentation

Business Model Design
https://docs.devexpress.com/eXpressAppFramework/113461

Use the Entity Framework Core Data Model
https://docs.devexpress.com/eXpressAppFramework/402972

Non-Persistent Objects
https://docs.devexpress.com/eXpressAppFramework/116516

Data Types Supported by built-in Editors
https://docs.devexpress.com/eXpressAppFramework/113014

Implement Custom Business Classes and Reference Properties (EF Core)
https://docs.devexpress.com/eXpressAppFramework/402978

Ways to Implement Business Logic
https://docs.devexpress.com/eXpressAppFramework/113710

Debugging, Unit and Functional Testing
https://docs.devexpress.com/eXpressAppFramework/112572



Install-Package DevExpress.ExpressApp.StateMachine -Version 22.2.3
Install-Package DevExpress.ExpressApp.EF -Version 22.2.3
Install-Package DevExpress.Persistent.BaseImpl.EF -Version 22.2.3




OK 👍 mình sẽ hướng dẫn bạn **từng bước** để có State Machine (workflow phê duyệt) cho `BaiViet` trong **XAF EF** — có nút duyệt trạng thái và tự check quyền dựa trên Role.

---

## 🟩 1. Chuẩn bị dự án XAF EF

Trong **Module** của bạn:

```csharp
RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.StateMachine.StateMachineModule));
```

Trong **DbContext** (ApplicationDbContext):

```csharp
using DevExpress.Persistent.BaseImpl.EF.StateMachine;

public DbSet<StateMachine> StateMachines { get; set; }
public DbSet<StateMachineState> StateMachineStates { get; set; }
public DbSet<StateMachineTransition> StateMachineTransitions { get; set; }
```

> 🔹 Nếu chưa có → add Migration để EF tạo bảng mới.

---

## 🟩 2. Khai báo trường trạng thái trong `BaiViet`

```csharp
public class BaiViet : IXafEntityObject {
    // các field khác

    public virtual string TrangThai { get; set; } // dùng string cho đơn giản
}
```

📌 Sau này XAF sẽ tự thay đổi giá trị `TrangThai` dựa trên State Machine.

---

## 🟩 3. Ẩn không cho sửa trực tiếp `TrangThai`

Trong **Model Editor**:

* Chọn `BaiViet` → `BOModel` → `BaiViet` → `OwnMembers` → `TrangThai`
* Set `AllowEdit = False`

Hoặc attribute `[ModelDefault("AllowEdit","False")]`:

```csharp
[ModelDefault("AllowEdit", "False")]
public virtual string TrangThai { get; set; }
```

---

## 🟩 4. Chạy ứng dụng → tạo State Machine trong UI

Khi bạn chạy XAF EF app, ở menu trái sẽ có **“State Machines”** (tự sinh ra nhờ `StateMachineModule`).

* Click **“State Machines” → New StateMachine**

  * Name: `DuyetBaiStateMachine`
  * TargetObjectType: `BaiViet`
  * StatePropertyName: `TrangThai`

* Add các **States**:

  * Draft (Bản nháp)
  * Pending (Chờ duyệt)
  * Approved (Đã duyệt)
  * Rejected (Bị từ chối)
  * Published (Đã xuất bản)

* Add các **Transitions**:

  * Draft → Pending (Caption: Gửi duyệt)
  * Pending → Approved (Caption: Duyệt sơ bộ)
  * Pending → Rejected (Caption: Từ chối)
  * Approved → Published (Caption: Xuất bản)
  * Approved → Rejected (Caption: Từ chối)

---

## 🟩 5. Gán Role cho Transition

Trong giao diện **State Machine → Transitions**:

* Mỗi Transition có tab **Security / Roles**
* Gán Role nào được quyền thực hiện Transition đó (VD “Tác giả”, “Biên tập viên”, “Trưởng ban”).

📌 Lúc này XAF tự check quyền dựa trên Role của `SecuritySystem.CurrentUser`.

---

## 🟩 6. Kết quả trên DetailView `BaiViet`

* Trường `TrangThai` bị khóa (không cho edit trực tiếp).
* Khi mở `BaiViet` → XAF sẽ hiển thị **nút chuyển trạng thái** (do StateMachineModule tạo).
* Nếu user không có Role → nút không hiển thị.
* Nếu user có Role → nút hiện ra và đổi trạng thái khi click.

---

## 🟩 7. Nếu muốn xử lý sau khi chuyển trạng thái

Bạn có thể handle `ObjectSpace.ObjectChanged` hoặc override `OnSaving()` của `BaiViet` để xử lý logic khi trạng thái đổi.

---

## 🟩 8. Tóm tắt luồng

1. **Cài StateMachineModule + khai báo DbSet**.
2. **Tạo StateMachine trong UI** gắn vào `BaiViet`.
3. **Đặt AllowEdit=False** cho `TrangThai`.
4. **Gán Role cho Transitions** trong UI.
5. **XAF tự hiển thị nút chuyển trạng thái** + kiểm tra quyền.

📌 Không cần tự viết Controller hay check Role bằng tay nữa.

---

Bạn có muốn mình chụp **ảnh màn hình hoặc mockup** cho từng bước tạo State Machine trong UI để bạn dễ làm theo không? (mình có thể vẽ sơ đồ).



add-migration FixDB-44 -StartupProject "DXApplication3.Module" -Project "DXApplication3.Module"

update-database -StartupProject "DXApplication3.Module" -Project "DXApplication3.Module"

Update-Database -StartupProject "DXApplication3.Module" -Project "DXApplication3.Module"

Drop-Database -StartupProject "DXApplication3.Module" -Project "DXApplication3.Module"