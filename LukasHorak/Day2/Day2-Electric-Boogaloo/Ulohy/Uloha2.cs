using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UlohyUtility;

namespace Day2_Electric_Boogaloo.Ulohy
{
    class Uloha2 : IUloha
    {
        public void Execute()
        {
            var author1 = new Author("Polian", "polabsky");
            var author2 = new Author("Anton", "Antonovic");

            var books = new List<Book>{
                new Book( "book3", DateTime.Today, author1),
                new Book( "book2", DateTime.Today, author2),
                new Book( "book1", DateTime.Today.AddDays(-1), author1),
                new Book( "book0", DateTime.Today.AddDays(-1), author2),
            };

            foreach (var item in books)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("-----------------------------------------");

            books.Sort();

            foreach (var item in books)
            {
                Console.WriteLine(item);
            }

        }
    }
}
