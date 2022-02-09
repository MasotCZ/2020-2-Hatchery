using BankingApp;
using NUnit.Framework;
using System;

namespace TestBankingApp_NUnit_NSubstitute
{
    // Example for Exercise 2
    [TestFixture]
    public class AdvancedBankingAccountTest
    {
        private BankAccount bankAccount;

        [SetUp]
        public void Initialize()
        {
            bankAccount = new BankAccount();
        }

        [Test]
        public void TestDeposit()
        {
            bankAccount.Deposit(1000);
            Assert.That(bankAccount.AccountBalance, Is.EqualTo(1000));
        }

        [Test]
        public void TestWithdrawal()
        {
            bankAccount.Deposit(1000);
            decimal balanceAfterWithdrawal = bankAccount.Withdraw(400);
            Assert.That(balanceAfterWithdrawal, Is.EqualTo(600));
            Assert.That(bankAccount.AccountBalance, Is.EqualTo(600));
        }

        
        [Test]
        public void TestWithdrawalException()
        {
            Assert.That(() => { return bankAccount.Withdraw(1500); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }
        
    }
}
