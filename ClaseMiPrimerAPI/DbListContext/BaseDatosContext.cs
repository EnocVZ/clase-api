using ClaseMiPrimerAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ClaseMiPrimerAPI.DbListContext
{
    public class BaseDatosContext : DbContext

    {
        public BaseDatosContext(DbContextOptions<BaseDatosContext> options) : base(options) { }
        //PERSONAS
        public DbSet<Persona> Persona { get; set; }
        //VEHICULOS
        public DbSet<Vehiculo> Vehiculo { get; set; }
        //RELACION 
        public DbSet<PersonaVehiculo> PersonaVehiculo { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Persona>().HasIndex(c => c.Id).IsUnique();
            modelBuilder.Entity<Vehiculo>().HasIndex(c => c.Id).IsUnique();
            modelBuilder.Entity<PersonaVehiculo>().HasIndex(c => c.Id).IsUnique();
        }
    }
}
