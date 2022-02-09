using System;

namespace BankingApp
{
    public static class TransferController
    {
        public static void TransferFromTo(BankAccount from, BankAccount to, decimal amount)
        {
            //if theres an expensive call to change funds we should ignore zeroes
            //probly?
            if (amount == 0)
            {
                throw new ArgumentException("Cant transfer 0 funds");
            }

            try
            {
                from.Withdraw(amount);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }

            try
            {
                to.Deposit(amount);
            }
            catch (ArgumentOutOfRangeException)
            {
                from.Deposit(amount);
                throw;
            }
        }
    }

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