using System;

namespace BankingApp
{
    public class BankAccount
    {
        private readonly ICalculator calc;
        public decimal AccountBalance { get; private set; }

        public BankAccount() { }

        public BankAccount(ICalculator calc)
        {
            this.calc = calc;
        }

        public decimal Deposit(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException("Dont be negative");

            var newBalance = calc.Add(amount, AccountBalance);

            AccountBalance = newBalance;
            return AccountBalance;
        }

        public decimal Withdraw(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException("Dont be negative");

            if (amount > AccountBalance)
            {
                throw new ArgumentOutOfRangeException("Not enough funds on the bank account");
            }
            AccountBalance -= amount;
            return AccountBalance;
        }
    }
}