using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Ulohy
{
    class Uloha9 : IUloha
    {
        public void Execute()
        {
            //data def
            var data = "1; 2; 3; 4; 15; 7; 20; 6";

            if (data.Length == 0)
            {
                Console.WriteLine("No data");
                return;
            }

            var split = data.Split(';');

            //convert
            var converted = new List<float>();
            var avg = 0f;
            foreach (var item in split)
            {
                float val;
                if (!float.TryParse(item, out val))
                {
                    continue;
                }

                avg += val;
                converted.Add(val);
            }

            if (converted.Count() == 0)
            {
                Console.WriteLine("No data");
                return;
            }

            avg /= converted.Count();

            var sum = 0f;
            //do stuff
            foreach (var item in converted)
            {
                if (item > avg)
                {
                    sum += item;
                }
            }

            Console.WriteLine($"Suma je {sum}");
        }
    }
}
