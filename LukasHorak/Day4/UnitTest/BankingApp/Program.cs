using System;

namespace BankingApp
{

    static class Program
    {
        static void Main(string[] args)
        {
            var account = new BankAccount();
            account.WriteBalance();

            account.Deposit(1000);
            account.WriteBalance();
        }

        private static void WriteBalance(this BankAccount account) =>
            Console.WriteLine($"Account balance: {account.AccountBalance}");
    }
}