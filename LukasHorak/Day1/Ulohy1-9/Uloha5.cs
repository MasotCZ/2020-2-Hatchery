using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Ulohy
{
    class Uloha5 : IUloha
    {
        public void Execute()
        {
            var numbers = new List<float>();

            bool empty = false;
            while (!empty)
            {
                numbers.Add(Utility.GetFloatFromConsoleButEndOnWhiteSpace(out empty));
            }

            Console.WriteLine($"max value is {numbers.Max()}");
        }
    }
}
