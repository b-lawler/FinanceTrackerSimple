using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleFinanceTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFinanceTracker.Core.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountValue> AccountValues { get; set; }
    }
}
