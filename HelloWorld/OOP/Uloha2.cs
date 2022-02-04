using HelloWorld.Ulohy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.OOP
{
    class Uloha2 : IUloha
    {
        //2
        /*
            2. Toto cvičení přímo navazuje na cvičení 1. Implementujte následující operace s použitím kolekcí List a Dictionary z předchozího cvičení:
            Vypište jméno zaměstnance s Id = 2
            Vypište jméno zaměstnance s nejvyšším platem
            Vypište jméno nejstaršího zaměstnance
            Vypište průměrný plat zaměstnanců
         */


        public Dictionary<int, Employee> input;
        public void Execute()
        {
            var employee2 = input[2];
            Console.WriteLine($"{employee2.FirstName} {employee2.LastName}");

            var maxSalary = input.Max(d => d.Value.salary);
            Console.WriteLine($"nejvyssi plat: {input.First(d => d.Value.salary == maxSalary)}");

            var oldest = input.Min(d => d.Value.Birthdate);
            Console.WriteLine($"nejstarsi clovek: {input.First(d => d.Value.Birthdate == oldest)}");

            Console.WriteLine($"prumer platu: {input.Sum(d => d.Value.salary) / input.Count}");
        }
    }
}
