using SimpleFinanceTracker.Core.Models;
using System;
using Xunit;

namespace SimpleFinanceTracker.Tests {
    public class AccountTests {
        [Fact]
        public void AddValueToAccount_CurrentValueMatches() {
            // Arrange
            Account account = new Account("test");
            AccountValue newValue = new AccountValue { Value = 20.00m };

            // Act
            account.Values.Add(newValue);


            // Assert
            Assert.Equal(20.00m, account.CurrentValue.Value);
        }

        [Fact]
        public void AddValueToAccount_LastUpdatedIsAccurate() {
            // Arrange
            Account account = new Account("test");
            AccountValue newValue = new AccountValue();
            DateTime createdDateTime = newValue.CreateDate;

            // Act
            account.Values.Add(newValue);

            // Assert
            Assert.Equal(createdDateTime, account.LastUpdated);
        }

        [Fact]
        public void AddStaleValueToAccount_ShouldBeYellowStale() {
            // Arrange
            Account account = new Account("test");
            AccountValue newValue = new AccountValue();
            newValue.CreateDate = DateTime.UtcNow.AddDays(-6);

            // Act
            account.Values.Add(newValue);

            // Assert
            Assert.True(account.IsYellowStale);
        }

        [Fact]
        public void AddStaleValueToAccount_ShouldBeRedStale() {
            // Arrange
            Account account = new Account("test");
            AccountValue newValue = new AccountValue();
            newValue.CreateDate = DateTime.UtcNow.AddDays(-11);

            // Act
            account.Values.Add(newValue);

            // Assert
            Assert.True(account.IsRedStale);
        }

        [Fact]
        public void AddNewValueToAccount_ShouldNotBeRedOrYellowStale() {
            // Arrange
            Account account = new Account("test");
            AccountValue newValue = new AccountValue();

            // Act
            account.Values.Add(newValue);

            // Assert
            Assert.True(!account.IsYellowStale && !account.IsRedStale);
        }
    }
}