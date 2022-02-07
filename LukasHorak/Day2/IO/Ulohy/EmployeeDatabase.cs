﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace IO.Ulohy
{
    [Serializable]
    class EmployeeDatabase : List<Employee>
    {
        public EmployeeDatabase(IEnumerable<Employee> collection) : base(collection)
        {
        }

        public EmployeeDatabase()
        {
        }

        public void Save(FileStream fs)
        {
            try
            {
                IFormatter formater = new BinaryFormatter();
                formater.Serialize(fs, this);
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERR {e.Message}");
                throw e;
            }
        }

        public void SaveString(FileStream fs)
        {

        }

        public void Load(FileStream fs)
        {
            //probly should clear before loading but lets give the user some work
            //Clear();

            try
            {
                IFormatter formater = new BinaryFormatter();
                AddRange(formater.Deserialize(fs) as IEnumerable<Employee>);
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERR {e.Message}");
                throw e;
            }
        }

        public void LoadString(FileStream fs)
        {

        }

        public override string ToString()
        {
            var ret = "";

            foreach (var item in this)
            {
                ret += $"{item} \n";
            }

            return ret;
        }
    }
}
