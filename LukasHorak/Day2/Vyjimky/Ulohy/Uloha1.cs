using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UlohyUtility;

namespace IO.Ulohy
{
    class Uloha1 : IUloha
    {
        public void Execute()
        {
            try
            {
                var solver = new QuadraticEquationSolver();

                Console.WriteLine(solver.Calculate(2, 4, 1));
                Console.WriteLine(solver.Calculate(1, 2, 3));

            }
            catch (EquationHasNoSolution e)
            {
                Console.WriteLine($"rovnice {e.EquationString()}, nema reseni {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERR {e.Message}");
            }
            finally
            {
                Console.WriteLine("Finally");
            }
        }
    }
}
