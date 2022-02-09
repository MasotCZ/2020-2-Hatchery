using BankingApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace TestBankingApp_MSTest_NSubstitute
{

    [TestClass]
    public class BankAccountWithNSubstitute
    {
        [TestMethod]
        public void TestDeposit()
        {
            var calc = Substitute.For<BankingApp.ICalculator>();
            BankAccount bk = new BankAccount(calc);
            calc.Add(Arg.Any<decimal>(), Arg.Any<decimal>()).Returns(555);

            bk.Deposit(1000);
            Assert.AreEqual(555, bk.AccountBalance);

            bk.Deposit(5000);
            Assert.AreEqual(555, bk.AccountBalance);
        }
    }
}
