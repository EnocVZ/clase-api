using ClaseMiPrimerAPI.Model;
using Microsoft.EntityFrameworkCore;
using ClaseMiPrimerAPI;
using System.Data.Common;

namespace ClaseMiPrimerAPI.DbListContext
{
    public class BDContext : DbContext
    {
        public BDContext(DbContextOptions<BDContext> options) : base(options)
        {
        }

        public DbSet<Persona> Persona { get; set; }

        public DbSet<Vehiculo> Vehiculo { get; set; }

        public DbSet<PersonaVehiculo> PersonaVehiculo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Persona>().HasIndex(c => c.Id).IsUnique();
            modelBuilder.Entity<Vehiculo>().HasIndex(c => c.id).IsUnique();
            modelBuilder.Entity<PersonaVehiculo>().HasIndex(c => c.Id).IsUnique();
        }
    }
}
