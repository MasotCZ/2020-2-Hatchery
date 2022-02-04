using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Ulohy
{
    class Uloha8 : IUloha
    {
        public void Execute()
        {
            Console.WriteLine($"napis prvni retezec");
            var first = Console.ReadLine();
            Console.WriteLine($"napis prvni retezec");
            var second = Console.ReadLine();

            var n = 0;
            //rychlejsi
            for (int i = 0; i < first.Length - second.Length; i++)
            {
                bool ok = true;
                for (int j = 0; j < second.Length; j++)
                {
                    if (first[i + j] != second[j])
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                {
                    n++;
                }
            }

            //pomalejsi, compaktnejsi
            /*
            for (int i = 0; i < first.Length - second.Length; i++)
            {
                if (first.Substring(i, second.Length).Contains(second))
                {
                    n++;
                }
            }
            */

            Console.WriteLine($"retez \"{second}\" je obsazen {n} krat");
        }
    }
}
