using Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Dal
{

    public class BigdataContext : DbContext
    {
        public BigdataContext(DbContextOptions<BigdataContext> dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Properties> Properties { get; set; }
        public DbSet<Feature> Feature { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //       optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = BigDataWeather; Trusted_Connection = True; ");
        // }

    }

}