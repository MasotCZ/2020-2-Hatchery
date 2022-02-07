using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2_Electric_Boogaloo.Ulohy
{
    class Book : BookBase, IComparable<Book>
    {
        public Book(string name, DateTime dateOfPublication, Author author) : base(name, dateOfPublication)
        {
            Author = author;
        }

        /// <summary>
        /// v realu je vic authoru na book taky normal ale vis co
        /// </summary>
        public Author Author { get; }

        public override bool Equals(object obj)
        {
            if (obj is not Book || obj is null)
            {
                return false;
            }

            return base.Equals(obj) && (obj as Book).Author.Equals(Author);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Author);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, {Author}";
        }

        public int CompareTo(Book other)
        {
            if (other is null)
            {
                return 1;
            }

            var dayCompare = DateOfPublication.CompareTo(other.DateOfPublication);
            return dayCompare == 0 ? Author.CompareTo(other.Author) : dayCompare;
        }
    }
}
