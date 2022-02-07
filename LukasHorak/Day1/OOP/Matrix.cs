using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.OOP
{
    /*
        3. Vytvořte třídu Matrix reprezentující matici (Wikipedia – Matrix) s rozměry m, n. Třída bude obsahovat konstruktor přijímající jeden řetězec jako parametr v následujícím formátu:
        "[1, 2, 5, 3; 8, 6, 7, 12; 5, 6, 6, 1]"
        Středníky oddělují řádky matice a čárky oddělují hodnoty v jednotlivých řádcích. Výše uvedený řetězec tedy reprezentuje následující matici:
        1   2   5   3
        8   6   7  12
        5   6   6   1
        Ve třídě Matrix implementujte následující metody
        Matrix Transpose() – vrací transponovanou matici
        Matrix Add(Matrix matrix) – vrací výsledek součtu dvou matic, přičemž druhá matice je předána jako parametr
        Matrix Subtract(Matrix matrix) – vrací výsledek rozdílu dvou matic, přičemž druhá matice je předána jako parametr
        Matrix Multiply(Matrix matix) – vrací výsledek násobku dvou matic, přičemž druhá matice je předána jako parametr
    */

    class AutoFillDictionary<_T> : Dictionary<(int, int), _T>
    {
        public int rows = 0;
        public int cols = 0;

        public new _T this[(int x, int y) index]
        {
            get
            {
                if (!this.ContainsKey(index))
                {
                    //questionable, but error prone, unless u use the data somewhere else
                    return default(_T);
                    //throw new IndexOutOfRangeException($"{nameof(index)}");
                }

                //dont forget BASE
                return base[index];
            }
            set
            {
                //Console.WriteLine(index);
                if (!this.ContainsKey(index))
                {
                    this.Add(index, value);
                    UpdateSize(index);
                    return;
                }

                //dont forget BASE
                base[index] = value;
                UpdateSize(index);
            }
        }

        private void UpdateSize((int y, int x) index)
        {
            if (index.y >= rows)
            {
                rows = index.y + 1;
            }

            if (index.x >= cols)
            {
                cols = index.x + 1;
            }
        }

        public override string ToString()
        {
            var ret = "";
            for (int y = 0; y < cols; y++)
            {
                for (int x = 0; x < rows; x++)
                {
                    ret += $"{this[(x, y)]} ";
                }

                if (y < cols - 1)
                    ret += '\n';
            }

            return ret;
        }
    }

    class Matrix
    {
        public AutoFillDictionary<float> data = new AutoFillDictionary<float>();
        public int m => data.rows;
        public int n => data.cols;

        public Matrix(string input)
        {
            //input
            var index = input.IndexOf('[');
            input = input.Remove(index, 1);
            index = input.IndexOf(']');
            input = input.Remove(index, 1);
            var lines = input.Split(';');

            for (int y = 0; y < lines.Length; y++)
            {
                var values = lines[y].Split(',');
                for (int x = 0; x < values.Length; x++)
                {
                    float converted;
                    if (!float.TryParse(values[x], out converted))
                    {
                        throw new ArgumentException($"{nameof(values)} at index {x}");
                    }

                    data[(x, y)] = converted;
                }
            }
        }

        private Matrix(AutoFillDictionary<float> data)
        {
            this.data = data;
        }

        public Matrix Transpose()
        {
            var newData = new AutoFillDictionary<float>();
            foreach (var item in data)
            {
                newData[(item.Key.Item2, item.Key.Item1)] = item.Value;
            }
            return new Matrix(newData);
        }

        private static Matrix MapSimpleMatrixOperation(Matrix a, Matrix b, Func<float, float, float> operation)
        {
            if (a.m != b.m || a.n != b.n)
            {
                throw new ArgumentException("different sizes of matrices");
            }

            var newData = new AutoFillDictionary<float>();

            for (int y = 0; y < a.n; y++)
            {
                for (int x = 0; x < a.m; x++)
                {
                    var index = (x, y);
                    newData[index] = operation(a.data[index], b.data[index]);
                }
            }

            return new Matrix(newData);
        }

        private static Matrix MapMultiplyMatrixOperation(Matrix a, Matrix b, Func<IEnumerable<float>, IEnumerable<float>, float> operation)
        {
            if (a.m != b.n || a.n != b.m)
            {
                throw new ArgumentException("different sizes of matrices");
            }

            var newData = new AutoFillDictionary<float>();
            /*
            var cache = new AutoFillDictionary<IEnumerable<float>>();

            for (int i = 0; i < a.m; i++)
            {
                //theres stuff here
                var list = a.data.Where(d => d.Key.Item2 == i).Select(d => d.Value);
                list.Count();
                cache.Add((-1, i), list);
            }

            //but not here
            for (int i = 0; i < b.n; i++)
            {
                var list = b.data.Where(d => d.Key.Item1 == i).Select(d => d.Value);
                list.Count();
                cache.Add((i, -1), list);
            }
            */
            for (int y = 0; y < a.m; y++)
            {
                for (int x = 0; x < a.n; x++)
                {
                    //very inefective
                    //newData[(x, y)] = operation(cache[(-1, y)], cache[(x, -1)]);
                    newData[(x, y)] = operation(
                        a.data.Where(d => d.Key.Item2 == y).Select(d => d.Value),
                        b.data.Where(d => d.Key.Item1 == x).Select(d => d.Value)
                        );
                }
            }

            return new Matrix(newData);
        }

        public static Matrix operator +(Matrix a, Matrix b) =>
            MapSimpleMatrixOperation(a, b, (x, y) =>
            {
                return x + y;
            });

        public static Matrix operator -(Matrix a, Matrix b) =>
            MapSimpleMatrixOperation(a, b, (x, y) =>
            {
                return x - y;
            });

        public static Matrix operator *(Matrix a, Matrix b) =>
            MapMultiplyMatrixOperation(a, b, (IEnumerable<float> row, IEnumerable<float> col) =>
            {
                var ret = 0f;
                var yipperino = row.Zip(col);
                foreach (var item in row.Zip(col))
                {
                    ret += item.First * item.Second;
                }
                return ret;
            });

        public override string ToString() => data.ToString();
    }
}
