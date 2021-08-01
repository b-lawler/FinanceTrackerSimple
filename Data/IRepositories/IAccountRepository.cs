using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceTrackerSimple.Data.IRepositories {
    public interface IAccountRepository {
        // Get account
        public Task<Account> GetAccount(int id);

        // Get accounts
        public Task<List<Account>> GetActiveAccounts();

        // Create account
        public Task<bool> InsertAccount(Account account);

        // Update account
        public Task<Account> UpdateAccount(Account account);

        // Delete account
        public bool DeleteAccount(Account account);
    }

    public interface IAccountValueRepository {
        // Insert account value
        public Task<bool> InsertAccountValueAsync(AccountValue accountValue);

        // Get account values
        public Task<List<AccountValue>> GetAccountValues(int accountId);
    }
}
