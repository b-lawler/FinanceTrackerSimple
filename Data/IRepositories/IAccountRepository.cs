﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceTrackerSimple.Data.IRepositories {
    public interface IAccountRepository {
        public Task<Account> GetAccount(int id);
        public Task<List<Account>> GetActiveAccounts();
        public Task<bool> InsertAccount(Account account);
        public Task<Account> UpdateAccount(Account account);
        public bool DeleteAccount(Account account);
    }

    public interface IAccountValueRepository {
        public Task<bool> InsertAccountValue(AccountValue accountValue);
        public Task<List<AccountValue>> GetAccountValues(int accountId);
        public bool DeleteAccountValue(AccountValue accountValue);
    }
}
