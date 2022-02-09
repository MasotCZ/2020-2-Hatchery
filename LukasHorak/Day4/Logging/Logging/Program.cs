using Logging.Banking;
using Logging.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    class Program
    {
        static void Main(string[] args)
        {
            CarShop carShop = new CarShop();
            carShop.SellCar();
            Console.ReadLine();

            Bank bank = new Bank();
            BankAccount account = new BankAccount();
            Console.ReadLine();

            bank.SomeLogic();
            Console.ReadLine();
        }
    }
}