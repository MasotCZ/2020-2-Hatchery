using BankingApp;
using Xunit;
using Shouldly;
using System;

namespace TestBankingApp_xUnit_moq_shouldly
{
    public class TransferFundBetweenAccountsTest
    {
        private readonly BankAccount a;
        private readonly BankAccount b;

        public TransferFundBetweenAccountsTest()
        {
            a = new BankAccount();
            b = new BankAccount();
        }

        [Fact]
        public void TestTransferBetweenAccounts()
        {
            //arrange
            a.Deposit(3000);

            //action
            TransferController.TransferFromTo(a, b, 1000);

            //assert
            a.AccountBalance.ShouldBe(2000, "Sender has wrong account balance");
            b.AccountBalance.ShouldBe(1000, "Recipient has wrong account balance");
        }

        [Fact]
        public void TestTransferWithSenderNotEnoughFunds()
        {
            //arrange

            //action
            Should.Throw<ArgumentOutOfRangeException>(() => TransferController.TransferFromTo(a, b, 1000));

            //assert
            a.AccountBalance.ShouldBe(0, "Sender has wrong account balance");
            b.AccountBalance.ShouldBe(0, "Recipient has wrong account balance");
        }

        [Fact]
        public void TestTransferWithZeroAmount()
        {
            //arrange
            a.Deposit(0);

            //action
            Should.Throw<ArgumentException>(() => TransferController.TransferFromTo(a, b, 0));

            //assert
            a.AccountBalance.ShouldBe(0, "Sender has wrong account balance");
            b.AccountBalance.ShouldBe(0, "Recipient has wrong account balance");
        }

        [Fact]
        public void TestTransferWithNegativeAmount()
        {
            //arrange
            a.Deposit(3000);

            //action
            Should.Throw<ArgumentOutOfRangeException>(() => TransferController.TransferFromTo(a, b, -1000));

            //assert
            a.AccountBalance.ShouldBe(3000, "Sender has wrong account balance");
            b.AccountBalance.ShouldBe(0, "Recipient has wrong account balance");
        }
    }
}
