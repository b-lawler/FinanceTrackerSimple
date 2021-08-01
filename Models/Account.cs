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
                if(Values.Any(v => v.Active)) {
                    return Values.Where(v => v.Active).Last();
                } else {
                    return new AccountValue {
                        Value = 0
                    };
                }
            }
        }
        public List<AccountValue> Values { get; set; }

        public Account() {
            Values = new List<AccountValue>();
            CreateDate = DateTime.UtcNow;
        }

    }
}
