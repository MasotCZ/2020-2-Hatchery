using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UlohyUtility;

namespace Generic.Generic
{
    interface INotificable
    {
        void Notify(string text);
    }

    class Evidence<_T> : List<_T> where _T : INotificable
    {
        public void NotifyAll(string text)
        {
            foreach (var item in this)
            {
                item.Notify(text);
            }
        }
    }

    class Employee : INotificable
    {
        public string Name { get; }

        public Employee(string name)
        {
            Name = name;
        }

        public void Notify(string text)
        {
            Console.WriteLine($"{Name}: {text}");
        }
    }

    class Uloha1 : IUloha
    {
        public void Execute()
        {
            var ev = new Evidence<Employee>()
            {
                new Employee("Bob"),
                new Employee("Jill"),
                new Employee("Jake"),
                new Employee("John"),
                new Employee("Bill"),
                new Employee("Gill"),
            };

            ev.NotifyAll("vstavame");
        }
    }
}
