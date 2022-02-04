using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Ulohy
{
    class Uloha4 : IUloha
    {
        public void Execute()
        {
            Console.WriteLine($"napis prvni retezec");
            var first = Console.ReadLine();
            Console.WriteLine($"napis prvni retezec");
            var second = Console.ReadLine();

            var ss = first.Contains(second) ? "je obsazen" : "neni obsazen";
            Console.WriteLine($"retez 2 {ss}");
        }
    }
}
