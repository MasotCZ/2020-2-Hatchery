using HelloWorld.Ulohy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.OOP
{
    class Uloha1 : IUloha
    {
        public Dictionary<int, Employee> result = null;

        public void Execute()
        {
            //1
            /*
             1. Vytvořte třídu Employee s následujícími vlastnostmi: Id (unikátní identifikátor reprezentovaný celým číslem),
                FirstName, LastName, Birthdate a Salary. Konstruktor bude přijímat tři parametry – FirstName, LastName a Salary. 
                Id je generováno automaticky – první zaměstnanec má Id = 1, druhý zaměstnanec má Id = 2, atd.
                Použijte vhodnou statickou metodu/field pro implementaci tohoto generování.
                Poté vytvořte několik instancí třídy Employee (přímo ve zdrojovém kódu) a přidejte je do kolekce typu List<Employee> a Dictionary<int,Employee> (Id je klíčem).
             */
            var employees = new List<Employee>()
            {
                new Employee("A", "B", 8),
                new Employee("C", "D", 9),
                new Employee("F", "E", 10),
                new Employee("H", "I", 11),
                new Employee("J", "K", 12),
            };

            result = employees.ToDictionary(d => d.Id);
        }
    }
}
