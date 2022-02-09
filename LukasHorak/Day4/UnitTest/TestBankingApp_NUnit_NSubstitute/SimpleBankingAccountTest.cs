using BankingApp;
using NUnit.Framework;

namespace TestBankingApp_NUnit_NSubstitute
{
    // Example for Exercise 1
    [TestFixture]
    public class SimpleBankingAccountTest
    {
        [Test]
        public void TestWithdrawal()
        {
            BankAccount account = new BankAccount();
            account.Deposit(1000);
            Assert.That(account.AccountBalance, Is.EqualTo(1000));

            account.Deposit(5000);
            Assert.That(account.AccountBalance, Is.EqualTo(6000));
        }
    }
}
