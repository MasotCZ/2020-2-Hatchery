using BankingApp;
using Shouldly;
using System;
using Xunit;

namespace TestBankingApp_xUnit_moq_shouldly
{
    // Example for Exercise 2
    public class AdvancedBankingAccountTest
    {
        private readonly BankAccount bankAccount;

        public AdvancedBankingAccountTest()
        {
            bankAccount = new BankAccount();
        }

        [Fact]
        public void TestDeposit()
        {
            // Act
            bankAccount.Deposit(1000);

            // Assert
            bankAccount.AccountBalance.ShouldBe(1000);
        }

        [Fact]
        public void TestWithdrawal()
        {
            // Arrange
            bankAccount.Deposit(1000);

            // Act
            decimal balanceAfterWithdrawal = bankAccount.Withdraw(400);

            // Assert
            balanceAfterWithdrawal.ShouldBe(600);
            bankAccount.AccountBalance.ShouldBe(600);
        }

        [Fact]
        public void TestWithdrawalWithNotEnoughFunds()
        {
            // Arrange
            var account = new BankAccount();
            account.Deposit(10000);

            // Act & Assert
            Should.Throw<ArgumentOutOfRangeException>(() => account.Withdraw(13000));
            account.AccountBalance.ShouldBe(10000);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(300)]
        public void TestMethodWithData(int amount)
        {
            // Arrange
            bankAccount.Deposit(1000);
            decimal expected = bankAccount.AccountBalance - amount;

            // Act
            bankAccount.Withdraw(amount);

            // Assert
            bankAccount.AccountBalance.ShouldBe(expected);
        }
    }
}
