using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceTrackerSimple.Data {
    public class Account : Base {
        public string Name { get; set; }
        public string Link { get; set; }
        public AccountValue CurrentValue {
            get {
                return Values.Last();
            }
        }
        public List<AccountValue> Values { get; set; }

        public Account() {
            Values = new List<AccountValue>();
            CreateDate = DateTime.UtcNow;
        }

    }
}
