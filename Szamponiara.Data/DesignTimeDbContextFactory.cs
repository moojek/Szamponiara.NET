using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szamponiara.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            //string path = @"C:\Users\grzegorz.suwaj\source\repos\moojek\Szamponiara.ASP\Szamponiara.App\appsettings.json";

            //IConfigurationBuilder builder = new ConfigurationBuilder().Add(new JsonConfigurationSource { Path = path });
            //IConfigurationRoot config = builder.Build();

            //string connectionString = config.GetConnectionString("DefaultConnection");

            DbContextOptionsBuilder<ApplicationDbContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Szamponiara;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new ApplicationDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
