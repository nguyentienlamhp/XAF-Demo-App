using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication3.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_TaskAssignment")]
    public class TaskAssignment : IXafEntityObject
    {
        //[Key]
        //public Guid Id { get; set; }

        public Guid TaskId { get; set; }
        [ForeignKey("TaskId")]
        public virtual Task Task { get; set; }

        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        [StringLength(50)]
        public string Role { get; set; } // e.g., "Owner", "Reviewer"

        public void OnCreated() { }
        public void OnSaving() { }
        public void OnLoaded() { }
    }
}
