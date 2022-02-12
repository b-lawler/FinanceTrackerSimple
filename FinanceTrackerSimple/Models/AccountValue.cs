using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFinanceTracker.Web.Data {
    public class AccountValue : Base {
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
        public string FormattedValue {
            get {
                return string.Format("{0:C0}", Value);
            }
        }

        public AccountValue() {
            Active = true;
            CreateDate = DateTime.UtcNow;
        }
    }
}
