using ASP.Net_Web_API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.Net_Web_API.Data
{

    public class CampContext : DbContext
    {
        private readonly IConfiguration _config;

        public CampContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        public DbSet<Camp> Camps { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Talk> Talks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(
            _config.GetConnectionString("CodeCamp")
                );

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Camp>().HasData(
                new
                {
                    CampId = 1,
                    Moniker = "Hatchery2022-2",
                    Name = ".NET Hatchery 2022",
                    EventDate = new DateTime(2022, 2, 18),
                    locationId = 1,
                    Length = 1
                });

            modelBuilder.Entity<Location>().HasData(
                new
                {
                    LocationId = 1,
                    Address = "Adresa 123",
                    City = "Praha",
                    Country = "Czech republic",
                    PostalCode = "111 50",
                });

            modelBuilder.Entity<Talk>().HasData(
                new
                {
                    TalkId = 1,
                    Title = "ASP.NET Core WebAPI",
                    Abstract = "bla bla bla",
                    Camp = 1,
                    Speaker = 1
                },
                new
                {
                    TalkId = 2,
                    Title = "C# fundamental",
                    Abstract = "bla bla bla",
                    Camp = 1,
                    Speaker = 2
                }
                );

            modelBuilder.Entity<Speaker>().HasData(
                new
                {
                    SpeakerId = 1,
                    FirstName = "Radek",
                    LastName = "Garzina",
                    Company = "Unicorn",
                    Email = "radek.garzina@email.com"
                },
                new
                {
                    SpeakerId = 2,
                    FirstName = "Bill",
                    LastName = "Gates",
                    Company = "Microsfot",
                    Email = "bill.gates@microsoft.com"
                }
                );
        }
    }
}
