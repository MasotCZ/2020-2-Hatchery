using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2_Electric_Boogaloo.Ulohy
{
    class Magazine : BookBase
    {
        public ICollection<Author> Authors { get; }

        public Magazine(string name, DateTime dateOfPublication, ICollection<Author> authors) : base(name, dateOfPublication)
        {
            Authors = authors;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Magazine)
            {
                return false;
            }

            return base.Equals(obj) && (obj as Magazine).Authors.Equals(Authors);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Authors.GetHashCode());
        }

        public override string ToString()
        {
            return $"{base.ToString()} \nAuthors:{Authors.ToString()}";
        }
    }
}
