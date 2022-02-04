using HelloWorld.Ulohy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.OOP
{
    class Uloha3 : IUloha
    {
        public void Execute()
        {
            var mat1 = new Matrix("[1, 2, 5, 3; 8, 6, 7, 12; 5, 6, 6, 1]");
            var mat2 = new Matrix("[8, 6, 7, 12; 5, 6, 6, 1; 1, 2, 5, 3]");
            Console.WriteLine("MAT1 ----------------------------------");
            Console.WriteLine(mat1);
            Console.WriteLine("MAT2 ----------------------------------");
            Console.WriteLine(mat2);
            Console.WriteLine("TRANS MAT1 ----------------------------------");
            Console.WriteLine(mat1.Transpose());
            Console.WriteLine("MAT1 + MAT2 ----------------------------------");
            Console.WriteLine(mat1 + mat2);
            Console.WriteLine("MAT1 - MAT2 ----------------------------------");
            Console.WriteLine(mat1 - mat2);
            Console.WriteLine("MAT1 * MAT2 TRANS----------------------------------");
            Console.WriteLine(mat1 * mat1.Transpose());
            Console.WriteLine("----------------------------------");
        }
    }
}
