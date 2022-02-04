
using System;

namespace HelloWorld.Ulohy.Uloha1
{
    class Uloha1 : IUloha
    {
        public void Execute()
        {
            var x = Utility.GetFloatFromConsole();
            var y = Utility.GetFloatFromConsole();

            Console.WriteLine($"{x} + {y} = {x + y}");
            Console.WriteLine($"{x} - {y} = {x - y}");
            Console.WriteLine($"{x} * {y} = {x * y}");
            
            if (y == 0)
            {
                Console.WriteLine($"{x} / {y} = {float.NaN}");
            }
            else
            {
                Console.WriteLine($"{x} / {y} = {(float)x / y}");
            }
        }
    }
}
