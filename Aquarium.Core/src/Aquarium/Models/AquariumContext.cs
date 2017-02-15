using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aquarium.Models
{
    public class AquariumContext : DbContext 
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Aquarium;Trusted_Connection=True;");
        }
        public DbSet<Fish> Fishes { get; set; }

        public AquariumContext()
        {

        }
    }
}
