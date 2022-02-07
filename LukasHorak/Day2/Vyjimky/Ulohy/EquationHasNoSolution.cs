using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IO.Ulohy
{
    class EquationHasNoSolution : Exception
    {
        public ICollection<int> arguments { get; }

        public EquationHasNoSolution(string message, ICollection<int> arguments) : base(message)
        {
            this.arguments = arguments;
        }

        public string EquationString()
        {
            var ret = "";
            var pow = arguments.Count - 1;

            foreach (var item in arguments.SkipLast(1))
            {
                ret += $"{item}*x^{pow--} + ";
            }

            ret += arguments.Last();

            return ret;
        }
    }
}
