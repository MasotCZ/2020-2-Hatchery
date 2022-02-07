using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Ulohy
{
    class Uloha6 : IUloha
    {
        public void Execute()
        {
            var data = new float[20]{
                20,20,20,20,30,30,30,30,40,50,60,5,06,61,61,64,65,65,54,997
            };

            var dict = new Dictionary<float, int>();

            foreach (var item in data)
            {
                if (!dict.ContainsKey(item))
                {
                    dict.Add(item, 0);
                }
                dict[item]++;
            }

            if (dict.Count == 0)
            {
                Console.WriteLine("no data");
            }

            var max = dict.Max(d => d.Value);
            var maxT = dict.First(d => d.Value == max);
            Console.WriteLine($"value:{maxT.Key}, is repeated {maxT.Value} times");
        }
    }
}
