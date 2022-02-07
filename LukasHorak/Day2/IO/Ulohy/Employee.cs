using System;

namespace IO.Ulohy
{
    [Serializable]
    class Employee : System.Runtime.Serialization.IDeserializationCallback
    {
        public Employee(string name, string surname, DateTime birthday, string position, decimal salary)
        {
            Name = name;
            Surname = surname;
            Fullname = $"{name} {surname}";
            Birthday = birthday;
            Position = position;
            this.salary = salary;
        }

        public string Name { get; }
        public string Surname { get; }
        public string Fullname { get; private set; }
        public DateTime Birthday { get; }
        public string Position { get; }
        public decimal salary { get; }

        public void OnDeserialization(object sender)
        {
            Fullname = $"{Name} {Surname}";
        }

        public override string ToString()
        {
            return $"{Fullname}, {Birthday}, {Position}, {salary}";
        }
    }
}
