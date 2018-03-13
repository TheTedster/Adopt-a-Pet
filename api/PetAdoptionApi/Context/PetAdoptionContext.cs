using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PetAdoptionApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoptionApi.Context
{
    public class PetAdoptionContext : DbContext
    {
        //private IConfigurationRoot _config;

        public PetAdoptionContext(DbContextOptions options) : base(options)
        {            
            //IConfigurationRoot config
            //_config = config;
        }

        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Cat> Cats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Breed>().ToTable("Breed");
            modelBuilder.Entity<Dog>().ToTable("Dogs");
            modelBuilder.Entity<Cat>().ToTable("Cats");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //var sqlConnection = ConfigurationManager.ConnectionStrings["PetAdoptionContextConnection"].ConnectionString;

            
            //optionsBuilder.UseSqlServer(sqlConnection);// _config["ConnectionStrings:PetAdoptionContextConnection"]);
            
        }
    }
}
