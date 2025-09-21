using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DXApplication3.Module.BusinessObjects
{
    // Class Employee (tương thích với Security System)
    [DefaultClassOptions]
    [ImageName("BO_User")]
    [NavigationItem("Users2")]
    public class Employee : BaseObject
        //PermissionPolicyUser // Kế thừa từ PermissionPolicyUser
    {
        //[Key]  // Bắt buộc nếu tên không phải 'Id'
        //public virtual Guid Id { get; set; }  // Hoặc dùng 'Id' để EF tự nhận

        [StringLength(100)]
        public virtual string FirstName { get; set; }

        [StringLength(100)]
        public virtual string LastName { get; set; }

        // Navigation property cho many-to-many với Task
       // public virtual ICollection<Task> AssignedTasks { get; set; } = new List<Task>();

    }
}
