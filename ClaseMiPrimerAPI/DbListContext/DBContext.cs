using ClaseMiPrimerAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ClaseMiPrimerAPI.DbListContext
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<Persona> Persona { get; set; }
        public DbSet<Vehiculo> Vehiculo { get; set; }

        public DbSet<PersonaVehiculo> PersonasVehiculo { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Persona>().HasIndex(p => p.Id).IsUnique();
            modelBuilder.Entity<Vehiculo>().HasIndex(p => p.Id).IsUnique();
            modelBuilder.Entity<PersonaVehiculo>().HasIndex(p => p.Id).IsUnique();
        }
    }
}
