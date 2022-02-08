using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UlohyUtility;

namespace ORM.ORM
{
    class MyDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Person> Persons { get; set; }

        public MyDbContext()
        {
        }

        //give him connection info and select the server u wanna use sql/oracle...
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(Config.ConnectionString);
    }

    class Vehicle
    {
        public Vehicle(string id, string spz)
        {
            this.id = id;
            this.spz = spz;
        }

        [Key]
        public string id { get; set; }
        public string? spz { get; set; }

        public override string ToString()
        {
            return $"{id}, {spz}";
        }
    }

    class Car : Vehicle
    {

        public Car(string id, string spz, string color, decimal power, decimal price) : base(id, spz)
        {
            Color = color;
            Power = power;
            Price = price;
        }

        public string Color { get; set; }
        public decimal Power { get; set; }
        [ForeignKey("owner_id")]
        public Person Owner { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, {Color}, {Power}";
        }
    }

    class Person
    {
        public Person(string id, string name, DateTime birthday, string email)
        {
            Id = id;
            Name = name;
            Birthday = birthday;
            this.email = email;
        }

        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string? email { get; set; }
    }

    class Uloha1 : IUloha
    {
        static string[] colors = new string[3] { "red", "blue", "green" };

        public void Execute()
        {
            using (var db = new MyDbContext())
            {
                CreateCars(db, 50);
                CreatePersons(db, 10);

                SellCarsToMorePeople(db, colors[0], 50, 90);
                SellCarsToOne(db, 300);
                SellCheapCar(db, 100000);

                db.SaveChanges();
            }
        }

        private static void CreatePersons(MyDbContext db, int persons)
        {
            var rnd = new Random();

            for (int i = 0; i < persons; i++)
            {
                db.Persons.Add(
                    new Person(Guid.NewGuid().ToString(), $"lowas{rnd.Next()}", DateTime.Now, $"borec{rnd.Next()}@email.com")
                    );
            }

            Console.WriteLine(db.Persons.Count());
        }

        private static void SellCheapCar(MyDbContext db, decimal maxPrice)
        {
            var carsToSell = db.Cars.Local.Where(d => d.Price < maxPrice && d.Owner == null);
            var rnd = new Random();

            var car = db.Cars.Local.FirstOrDefault();

            if (car is null || car is default(Car))
            {
                return;
            }

            var possibleOwner = db.Persons.Local.FirstOrDefault(p => db.Cars.FirstOrDefault(c => p.Id == c.id) == default(Car));

            if (possibleOwner is default(Person))
            {
                return;
            }

            car.spz = $"ccc-{rnd.Next(0, 999)}";
        }

        private static void SellCarsToMorePeople(MyDbContext db, string color, int powerLower, int powerUpper)
        {
            var carsToSell = db.Cars.Local.Where(d => d.Color == color && d.Power > powerLower && d.Power < powerUpper && d.Owner == null);
            var rnd = new Random();

            foreach (var car in carsToSell)
            {
                var possibleOwner = db.Persons.Local.FirstOrDefault(p => db.Cars.Local.FirstOrDefault(c => p.Id == c.id) == default(Car));

                if (possibleOwner is default(Person))
                {
                    return;
                }

                car.Owner = possibleOwner;
                car.spz = $"mmm-{rnd.Next(0, 999)}";
            }
        }

        private static void SellCarsToOne(MyDbContext db, int powerLower)
        {
            var carsToSell = db.Cars.Local.Where(d => d.Power > powerLower && d.Owner == null);
            var rnd = new Random();
            var person = db.Persons.Local.First();

            if (person is null)
            {
                return;
            }

            foreach (var car in carsToSell)
            {
                car.Owner = person;
                car.spz = $"ooo-{rnd.Next(0, 999)}";
            }
        }

        private static void CreateCars(MyDbContext db, int number)
        {
            var rnd = new Random();

            for (int i = 0; i < number; i++)
            {
                db.Cars.Local.Add(
                        new Car(
                            Guid.NewGuid().ToString(),
                            "",
                            colors[rnd.Next(0, 3)],
                            rnd.Next(10, 400),
                            rnd.Next(5000, 5000000))
                    );
            }
        }
    }
}
