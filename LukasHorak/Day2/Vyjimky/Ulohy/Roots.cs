using System.Collections.Generic;

namespace IO.Ulohy
{
    class Roots : List<double>
    {
        public override string ToString()
        {
            var ret = "";

            foreach (var item in this)
            {
                ret += $"{item} ";
            }

            return ret;
        }
    }
}
