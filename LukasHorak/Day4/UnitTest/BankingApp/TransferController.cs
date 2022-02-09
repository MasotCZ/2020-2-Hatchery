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
}