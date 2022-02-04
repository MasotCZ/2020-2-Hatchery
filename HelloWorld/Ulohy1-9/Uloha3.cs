using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Ulohy
{
    class Uloha3 : IUloha
    {
        public void Execute()
        {
            var x = Utility.GetIntFromConsole();
            var ss = x % 2 == 0 ? "sude" : "liche";
            Console.WriteLine($"cislo {x} je {ss}");
        }
    }
}
