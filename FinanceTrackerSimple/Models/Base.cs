using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFinanceTracker.Web.Data {
    public class Base {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Active { get; set; }
        public DateTime DeactivateDate { get; set; }
    }
}
