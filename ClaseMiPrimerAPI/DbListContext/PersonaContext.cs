using ClaseMiPrimerAPI.Model;
using ClaseMiPrimerAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace API_3.DbListContext
{
    public class PersonaContext: DbContext
    {
        public PersonaContext(DbContextOptions<PersonaContext> options): base(options) 
        {
        }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Vehiculo> Vehiculo { get; set; }
        public DbSet<PersonaVehiculo> PersonaVehiculo { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Persona>().HasIndex(c => c.Id).IsUnique();
            modelBuilder.Entity<Vehiculo>().HasIndex(c => c.Id).IsUnique();
        }
    }
}
