using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Persistent.BaseImpl.EF;
using System.Collections.ObjectModel;

namespace DXApplication3.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Task")]
    [NavigationItem("Tasks")]
    public class Task : BaseObject
    {
        //[Key]
        //public virtual Guid Id { get; set; }
        public virtual TaskStatus Status { get; set; }

        [StringLength(255)]
        public virtual string Subject { get; set; }
        public virtual DateTime DueDate { get; set; }

        //public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        //Tao 1 nut hoanh thanh
        [Action(Caption = "Hoàn thành task", AutoCommit = true, ImageName = "State_Task_Completed")]
        public void MarkCompleted()
        {
            Status = TaskStatus.Completed;
        }

        public virtual IList<Employee> Employees { get; set; } = new ObservableCollection<Employee>();

    }

    public enum TaskStatus
    {
        NotStarted = 0,
        InProgress = 1,
        Completed = 2,
        Deferred = 3
    }
}
