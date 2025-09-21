using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication3.Module.Model
{
    [DomainComponent]
    [DefaultClassOptions]
    public class DateTimeRange
    {
        [ImmediatePostData]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [ImmediatePostData]
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);
    }
}
