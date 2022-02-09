using System;

namespace BankingApp
{
    public interface ICalculator
    {
        int Add(decimal a, decimal b);
        void Clear();
        string Mode { get; set; }
        event EventHandler PoweringUp;
    }
}