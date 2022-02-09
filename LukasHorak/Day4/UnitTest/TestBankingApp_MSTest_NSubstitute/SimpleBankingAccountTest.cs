// Example for Exercise 1
using BankingApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestBankingApp_MSTest_NSubstitute
{

    [TestClass]
    public class SimpleBankingAccountTest
    {
        [TestMethod]
        public void TestDeposit()
        {
            BankAccount account = new BankAccount();
            account.Deposit(1000);
            Assert.AreEqual(1000, account.AccountBalance);

            account.Deposit(5000);
            Assert.AreEqual(6000, account.AccountBalance);
        }

        [TestMethod]
        public void TestDepositNegativeAmountThrowsException()
        {
            BankAccount account = new BankAccount();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => account.Deposit(-1000));
            Assert.AreEqual(0, account.AccountBalance);
        }
    }
}