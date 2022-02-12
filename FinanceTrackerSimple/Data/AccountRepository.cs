using Microsoft.EntityFrameworkCore;
using SimpleFinanceTracker.Web.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SimpleFinanceTracker.Web.Data {
    public class AccountRepository : IAccountRepository {
        private readonly ApplicationDbContext _dbContext;

        public AccountRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<bool> InsertAccount(Account account) {
            account.Active = true;
            await _dbContext.Accounts.AddAsync(account);
            int affectedRows = await _dbContext.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<List<Account>> GetActiveAccountsForUser(string userId) {
            
            return await _dbContext.Accounts.Include(a => a.Values)
                                            .Where(a => a.Active && a.UserId == userId)
                                            .ToListAsync();
        }

        public async Task<Account> GetAccount(int id) {
            return await _dbContext.Accounts.Include(a => a.Values).FirstAsync(a => a.Id == id);
        }

        public async Task<Account> UpdateAccount(Account account) {
            Account accountFromDB = await _dbContext.Accounts.FindAsync(account.Id);

            if(accountFromDB != null) {
                var updatedAccount = _dbContext.Accounts.Update(account);
                await _dbContext.SaveChangesAsync();
                return updatedAccount.Entity;
            } else {
                return null;
            }
        }

        public bool DeleteAccount(Account account) {
            Account dbAccount = _dbContext.Accounts.Find(account.Id);

            if(dbAccount != null) {
                dbAccount.DeactivateDate = DateTime.UtcNow;
                dbAccount.Active = false;

                _dbContext.Accounts.Update(dbAccount);
                int rowsAffected = _dbContext.SaveChanges();
                return rowsAffected > 0;
            }
            return false;
        }

        public bool HideAccount(Account account)
        {
            Account dbAccount = _dbContext.Accounts.Find(account.Id);
            if(dbAccount != null)
            {
                dbAccount.Hidden = true;
                _dbContext.Accounts.Update(dbAccount);
                int rowsAffected = _dbContext.SaveChanges();
                return rowsAffected > 0;
            }
            return false;
        }
    }

    public class AccountValueRepository : IAccountValueRepository {
        private ApplicationDbContext _dbContext;

        public AccountValueRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<bool> InsertAccountValue(AccountValue accountValue) {
            accountValue.Active = true;
            await _dbContext.AccountValues.AddAsync(accountValue);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<AccountValue>> GetAccountValues(int accountId) {
            return await _dbContext.AccountValues.Where(a => a.AccountId == accountId).ToListAsync();
        }

        public bool DeleteAccountValue(AccountValue accountValue) {
            AccountValue dbAccountValue = _dbContext.AccountValues.Find(accountValue.Id);

            if(dbAccountValue != null) {
                dbAccountValue.DeactivateDate = DateTime.UtcNow;
                dbAccountValue.Active = false;

                _dbContext.AccountValues.Update(dbAccountValue);
                int rowsAffected = _dbContext.SaveChanges();
                return rowsAffected > 0;
            }
            return false;
        }

        public Dictionary<DateTime, decimal> GetHistoricalAccountValueSummaryForUser(int days, List<Account> activeAccounts) {
            DateTime startDate = DateTime.UtcNow.AddDays(-days).Date;
            Dictionary<DateTime, decimal> dailyAccountSummary = new Dictionary<DateTime, decimal>();

            for(DateTime runningDate = DateTime.UtcNow.Date; runningDate >= startDate; runningDate = runningDate.AddDays(-1).Date) {
                decimal dailyRunningAccountTotal = 0;
                foreach(Account account in activeAccounts) {
                    decimal currentDayAccountValue = 0;
                    if(account.Values.Any(v => v.Active && v.CreateDate.Date <= runningDate)) {
                        currentDayAccountValue = account.Values.Where(av => av.Active && av.CreateDate == account.Values.Where(v => v.Active && v.CreateDate.Date <= runningDate).Max(v => v.CreateDate)).First().Value;
                    }
                    dailyRunningAccountTotal += currentDayAccountValue;
                }
                dailyAccountSummary.Add(runningDate, dailyRunningAccountTotal);
            }
            return dailyAccountSummary;
        }
    }
}
