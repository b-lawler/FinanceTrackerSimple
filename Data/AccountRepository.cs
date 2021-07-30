using Microsoft.EntityFrameworkCore;
using FinanceTrackerSimple.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceTrackerSimple.Data {
    public class AccountRepository : IAccountRepository {
        private readonly ApplicationDbContext _db;

        public AccountRepository(ApplicationDbContext appDBContext) {
            _db = appDBContext;
        }

        public async Task<bool> InsertAccount(Account account) {
            await _db.Accounts.AddAsync(account);
            int affectedRows = await _db.SaveChangesAsync();
            return affectedRows > 0;
        }

        public List<Account> GetActiveAccounts() {
            // add flag for active in DB
            return _db.Accounts.Include(a => a.Values).ToList();
        }

        public async Task<Account> GetAccount(int id) {
            return await _db.Accounts.Include(a => a.Values).FirstAsync(a => a.Id == id);
        }

        public async Task<Account> UpdateAccount(Account account) {

            Account accountFromDB = await _db.Accounts.FindAsync(account.Id);

            if(accountFromDB != null) {
                var updatedAccount = _db.Accounts.Update(account);
                await _db.SaveChangesAsync();
                return updatedAccount.Entity;
            } else {
                return null;
            }
        }
    }

    public class AccountValueRepository : IAccountValueRepository {
        private readonly ApplicationDbContext _appDBContext;

        public AccountValueRepository(ApplicationDbContext appDBContext) {
            _appDBContext = appDBContext;
        }

        public async Task<bool> InsertAccountValueAsync(AccountValue accountValue) {
            await _appDBContext.AccountValues.AddAsync(accountValue);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<AccountValue>> GetAccountValues(int accountId) {
            return await _appDBContext.AccountValues.Where(a => a.AccountId == accountId).ToListAsync();
        }
    }
}
