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
            _fullName = $"{name} {surname}";
            Birthday = birthday;
            Position = position;
            this.salary = salary;
        }

        public Employee(string ss)
        {
            //todo string to employee
        }

        public string Name { get; }
        public string Surname { get; }

        [NonSerialized]
        private string _fullName;
        public string Fullname => _fullName;
        public DateTime Birthday { get; }
        public string Position { get; }
        public decimal salary { get; }

        public void OnDeserialization(object sender)
        {
            _fullName = $"{Name} {Surname}";
        }

        public override string ToString()
        {
            return $"{Fullname}, {Birthday}, {Position}, {salary}";
        }
    }
}
