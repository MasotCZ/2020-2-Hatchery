using System;

namespace HelloWorld.OOP
{
    /// <summary>
    /// Id (unikátní identifikátor reprezentovaný celým číslem), FirstName, LastName, Birthdate a Salary.
    /// Konstruktor bude přijímat tři parametry – FirstName, LastName a Salary.
    /// Id je generováno automaticky – první zaměstnanec má Id = 1, druhý zaměstnanec má Id = 2, atd
    /// . Použijte vhodnou statickou metodu/field pro implementaci tohoto generování.
    //Poté vytvořte několik instancí třídy Employee(přímo ve zdrojovém kódu) a přidejte je do kolekce typu List<Employee> a Dictionary<int, Employee>(Id je klíčem).
    /// </summary>
    internal class Employee
    {
        private static int ID = 0;

        public readonly int Id;
        public readonly string FirstName;
        public readonly string LastName;
        public readonly DateTime Birthdate;
        public float salary;

        public Employee(string firstName, string lastName, float salary)
        {
            //check for validation?

            Id = ++ID;
            FirstName = firstName;
            LastName = lastName;
            this.salary = salary;
        }

        public override string ToString()
        {
            return $"[id|{Id}] {FirstName} {LastName}, birth: {Birthdate.Day}/{Birthdate.Month}/{Birthdate.Year}, salary: {salary}";
        }
    }
}