using System;

namespace BankingApp
{

    public class BankAccount
    {
        public decimal AccountBalance { get; private set; }

        public decimal Deposit(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException("Dont be negative");
            AccountBalance += amount;
            return AccountBalance;
        }

        public decimal Withdraw(decimal amount)
        {
            if (amount > AccountBalance)
            {
                throw new ArgumentOutOfRangeException("Not enough funds on the bank account");
            }
            AccountBalance -= amount;
            return AccountBalance;
        }
    }
}