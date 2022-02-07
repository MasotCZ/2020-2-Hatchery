using System;

namespace Day2_Electric_Boogaloo.Ulohy
{
    internal class BookBase
    {
        public BookBase(string name, DateTime dateOfPublication)
        {
            Name = name;
            DateOfPublication = dateOfPublication;
        }

        public string Name { get; }
        public DateTime DateOfPublication { get; }

        public override bool Equals(object obj)
        {
            if (obj is not Book || obj is null)
            {
                return false;
            }

            var otherBook = (obj as Book);

            return
                otherBook.Name.Equals(Name) &&
                otherBook.DateOfPublication.Equals(DateOfPublication);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, DateOfPublication);
        }

        public override string ToString()
        {
            return $"{Name}, {DateOfPublication}";
        }
    }
}