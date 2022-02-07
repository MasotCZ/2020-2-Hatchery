using System;

namespace IO.Ulohy
{
    class QuadraticEquationSolver
    {
        /// <summary>
        /// a*x^2 + b*x + c = 0 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public Roots Calculate(int a, int b, int c)
        {
            var roots = new Roots();

            double D = (b * b) - (4 * a * c);

            if (D < 0 || D is double.NaN || 2 * a == 0)
            {
                //new vyjimka
                throw new EquationHasNoSolution(
                    String.Format("{0} < 0 || {0} is double.NaN || 2 * {1} == 0", D, a),
                    new int[] { a, b, c });
            }

            var sqrt = Math.Sqrt(D);

            return new Roots() {
                (-b + sqrt) / (2 * a),
                (-b - sqrt) / (2 * a)
            };
        }
    }
}
