using BankingApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestBankingApp_MSTest_NSubstitute
{
    // Example for Exercise 2
    [TestClass]
    public class AdvancedBankingAccountTest
    {
        private BankAccount bankAccount;

        [TestInitialize]
        public void Initialize()
        {
            bankAccount = new BankAccount();
        }

        [TestMethod]
        public void TestDeposit()
        {
            bankAccount.Deposit(1000);
            Assert.AreEqual(1000, bankAccount.AccountBalance);
        }

        [TestMethod]
        public void TestWithdrawal()
        {
            bankAccount.Deposit(1000);
            decimal balanceAfterWithdrawal = bankAccount.Withdraw(400);
            Assert.AreEqual(600, balanceAfterWithdrawal);
            Assert.AreEqual(600, bankAccount.AccountBalance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestWithdrawalException()
        {
            bankAccount.Withdraw(1500);
        }

        
        [DataTestMethod]
        [DataRow(100)]
        [DataRow(200)]
        [DataRow(300)]
        public void TestMethodWithData(int amount)
        {
            bankAccount.Deposit(1000);
            decimal expected = bankAccount.AccountBalance - amount;
            bankAccount.Withdraw(amount);
            Assert.AreEqual(expected, bankAccount.AccountBalance);
        }
    }
}
