using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UlohyUtility;

namespace IO.Ulohy
{
    class Uloha2 : IUloha
    {
        public string file = "./data";

        public void Execute()
        {
            //data
            var data = new List<Employee>()
            {
                new Employee( "Name1", "Surname1", DateTime.Now, "Borec1", 2 ),
                new Employee( "Name2", "Surname2", DateTime.Now, "Borec2", 4 ),
                new Employee( "Name3", "Surname3", DateTime.Now, "Borec3", 8 ),
                new Employee( "Name4", "Surname4", DateTime.Now, "Borec4", 16 ),
                new Employee( "Name5", "Surname5", DateTime.Now, "Borec5", 32 ),
                new Employee( "Name6", "Surname6", DateTime.Now, "Borec6", 64 ),
            };
            var empDB = new EmployeeDatabase(data);

            //save
            using (FileStream fs = File.OpenWrite(file))
            {
                empDB.Save(fs);
            }

            //load
            empDB.Clear();
            using (FileStream fs = File.OpenRead(file))
            {
                empDB.Load(fs);
            }

            Console.WriteLine(empDB);
        }
    }
}
