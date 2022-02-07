using System;

namespace HelloWorld.Ulohy.Uloha2
{
    class Uloha2 : IUloha
    {
        public void Execute()
        {
            var date = DateTime.Now;

            Console.WriteLine($"rok: {date.Year}");
            Console.WriteLine($"mesic: {date.Month}");
            Console.WriteLine($"den: {date.Day}");
        }
    }
}
