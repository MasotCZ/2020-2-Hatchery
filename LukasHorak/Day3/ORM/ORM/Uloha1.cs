using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UlohyUtility;

namespace ORM.ORM
{
    class MyDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

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

        public string id { get; set; }
        public string? spz { get; set; }

        public override string ToString()
        {
            return $"{id}, {spz}";
        }
    }

    class Car : Vehicle
    {

        public Car(string id, string spz, string color, decimal power) : base(id, spz)
        {
            Color = color;
            Power = power;
        }

        public string Color { get; set; }
        public decimal Power { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, {Color}, {Power}";
        }
    }

    class Uloha1 : IUloha
    {
        public void Execute()
        {
            using (var db = new MyDbContext())
            {
                var cars = db.Cars.Where(d => d.Power < 50);
                foreach (var car in cars)
                {
                    car.Color = "silver";
                    Console.WriteLine(car);
                }

                db.Cars.Add(new Car(Guid.NewGuid().ToString(), "aaaa-ddd", "red", 220));

                db.SaveChanges();
            }
        }
    }
}
