using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceTrackerSimple.Data {
    public class Account : Base {
        [Required]
        public string UserId { get; }
        [Required]
        public string Name { get; set; }
        public string Link { get; set; }
        public string UserName { get; set; }
        public bool Hidden { get; set; }
        public List<AccountValue> Values { get; set; }
        public Account(string userId) {
            Values = new List<AccountValue>();
            CreateDate = DateTime.UtcNow;
            UserId = UserId;
        }

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
        public DateTime LastUpdated {
            get {
                if(CurrentValue != null) {
                    return CurrentValue.CreateDate;
                }
                return DateTime.MinValue;
            }
        }

        // If account has not been updated after X number of days, return true
        public bool IsYellowStale {
            get {
                int daysSpan = 5;
                TimeSpan spanDifference = DateTime.UtcNow.Subtract(LastUpdated);
                return spanDifference.TotalDays >= daysSpan;
            }
        }

        // If account has not been updated after X number of days, return true
        public bool IsRedStale {
            get {
                int daysSpan = 10;
                TimeSpan spanDifference = DateTime.UtcNow.Subtract(LastUpdated);
                return spanDifference.TotalDays >= daysSpan;
            }
        }
    }
}
