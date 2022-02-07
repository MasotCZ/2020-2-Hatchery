using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Ulohy
{
    class Uloha7 : IUloha
    {
        public void Execute()
        {
            for (int n = 1; n < 11; n++)
            {
                var s = "";
                for (int i = 1; i < 11; i++)
                {
                    s += $" {i * n}";
                }

                Console.WriteLine(s);
            }
        }
    }
}
