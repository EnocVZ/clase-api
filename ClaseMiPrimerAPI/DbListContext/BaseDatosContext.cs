using ClaseMiPrimerAPI.Model;
using ConcesionariaBarrios.Modelos;
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
        public DbSet<Mecanico> Mecanico { get; set; }
        public DbSet<Concesionario> Concesionario { get; set; }
        public DbSet<Servicio> Servicio { get; set; }
        public DbSet<Vendedor> Vendedor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Persona>().HasIndex(c => c.Id).IsUnique();
            modelBuilder.Entity<Vehiculo>().HasIndex(c => c.Id).IsUnique();
            modelBuilder.Entity<PersonaVehiculo>().HasIndex(c => c.Id).IsUnique();
            modelBuilder.Entity<Mecanico>().HasIndex(c => c.Id).IsUnique();
            modelBuilder.Entity<Concesionario>().HasIndex(c => c.Id).IsUnique();
            modelBuilder.Entity<Servicio>().HasIndex(c => c.Id).IsUnique();
            modelBuilder.Entity<Vendedor>().HasIndex(c=> c.Id).IsUnique();
        }
    }
}
