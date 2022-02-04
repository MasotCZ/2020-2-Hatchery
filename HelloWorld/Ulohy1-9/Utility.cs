using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Ulohy
{
    static class Utility
    {
        public static int GetIntFromConsole()
        {
            Console.WriteLine("Napis cislo");
            int x = 0;
            while (!Int32.TryParse(Console.ReadLine(), out x))
            {
                Console.WriteLine("it has to be a number");
            }

            return x;
        }

        public static float GetFloatFromConsole()
        {
            Console.WriteLine("Napis cislo");
            float x = 0;
            while (!float.TryParse(Console.ReadLine(), out x))
            {
                Console.WriteLine("it has to be a number");
            }

            return x;
        }

        public static float GetFloatFromConsoleButEndOnWhiteSpace(out bool whiteSpace)
        {
            whiteSpace = false;
            return stuff(ref whiteSpace, float.TryParse);
        }

        public delegate bool MyDel(string s, out float x);
        private static float stuff(ref bool whiteSpace, MyDel parse)
        {
            Console.Write("number:");
            float x = 0;
            while (true)
            {
                var s = Console.ReadLine();

                if (s == null || s == "" || s == "\n")
                {
                    whiteSpace = true;
                    return float.NaN;
                }

                if (parse(s, out x))
                {
                    return x;
                }
            }
        }

        public static float GetNumberFromConsoleButEndOnWhiteSpace(out bool whiteSpace)
        {
            whiteSpace = false;
            return stuff(ref whiteSpace, float.TryParse);
        }
    }
}
