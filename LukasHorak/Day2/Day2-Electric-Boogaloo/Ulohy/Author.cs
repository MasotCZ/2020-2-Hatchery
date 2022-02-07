using System;

namespace Day2_Electric_Boogaloo.Ulohy
{
    class Author : IComparable<Author>
    {
        public Author(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public string Name { get; }
        public string Surname { get; }

        public int CompareTo(Author other)
        {
            var sameName = Name.CompareTo(other.Name);
            return sameName == 0 ? Surname.CompareTo(other.Surname) : sameName;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Author)
            {
                return false;
            }

            var otherAuthor = (obj as Author);

            return otherAuthor.Name.Equals(Name) && otherAuthor.Surname.Equals(Surname);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Surname);
        }

        public override string ToString()
        {
            return $"{Name}, {Surname}";
        }

    }
}
