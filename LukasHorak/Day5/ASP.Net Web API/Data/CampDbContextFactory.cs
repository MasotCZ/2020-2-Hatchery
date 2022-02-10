using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ASP.Net_Web_API.Data
{
    public class CampDbContextFactory : IDbContextFactory<CampContext>
    {
        public CampContext CreateDbContext()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return new CampContext(new DbContextOptionsBuilder<CampContext>().Options, config);
        }
    }
}
